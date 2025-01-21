using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Client.Data.Configuration;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Logging;

namespace Ych.Api.Configuration
{
    public class TableStorageVersionedSettingsProvider : IVersionedSettingsProvider
    {
        private ISettingsProvider settings;
        private ILogWriter log;
        private TableClient tableClient;

        public TableStorageVersionedSettingsProvider(ISettingsProvider settings, ILogWriter log)
        {
            this.settings = settings;
            this.log = log;
        }

        public Task<VersionedAppSettings> GetAppSettings(string system, DeploymentEnvironments environment, int? currentVersion = null, int? targetVersion = null)
        {
            tableClient = tableClient ?? InitializeTableClient();

            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(ent => ent.PartitionKey == GetPartitionKey(system, environment));
            TableEntity row = null;

            if (targetVersion != null)
            {
                // If targetVersion is specified, only return results if that version exists regardless of whether it is newer than currentVersion
                row = entities.Where(s => s.RowKey == targetVersion.Value.ToString()).FirstOrDefault();
            }
            else
            {
                // Otherwise grab the most recent version
                row = entities.OrderByDescending(s => int.Parse(s.RowKey)).FirstOrDefault();
            }

            if (row != null)
            {
                SettingsFormats format;

                if (!Enum.TryParse(row.GetString("Format"), out format))
                {
                    // This should never fail, assume Json if it does?...
                    format = SettingsFormats.Json;
                }

                VersionedAppSettings versionedSettings = FromTableEntity(row);

                if (currentVersion != null && versionedSettings.Version <= currentVersion.Value)
                {
                    // If current version is provided, only return results if they are newer than current version
                    return Task.FromResult<VersionedAppSettings>(null);
                }

                return Task.FromResult(versionedSettings);
            }
            else
            {
                return Task.FromResult<VersionedAppSettings>(null);
            }
        }

        public async Task<VersionedAppSettings> SaveAppSettings(string system, DeploymentEnvironments environment, int version, bool isEncrypted, SettingsFormats format, string settingsFile)
        {
            tableClient = tableClient ?? InitializeTableClient();

            VersionedAppSettings versionedSettings = new VersionedAppSettings
            {
                System = system,
                Environment = environment,
                Version = version,
                IsEncrypted = isEncrypted,
                Format = format,
                Settings = settingsFile
            };

            TableEntity row = ToTableEntity(versionedSettings);

            Response response = await tableClient.UpsertEntityAsync(row);

            HandleErrorResponse(response);

            return versionedSettings;
        }

        public async Task DeleteAppSettings(string system, DeploymentEnvironments environment, int version)
        {
            tableClient = tableClient ?? InitializeTableClient();

            Response response = await tableClient.DeleteEntityAsync(GetPartitionKey(system, environment), version.ToString());

            HandleErrorResponse(response);
        }

        public Task<VersionedAppSettings[]> ListAppSettings(string system = null, params DeploymentEnvironments[] environments)
        {
            tableClient = tableClient ?? InitializeTableClient();

            StringBuilder filter = new StringBuilder();

            if (!string.IsNullOrEmpty(system))
            {
                filter.Append($"System eq '{system}'");
            }

            if (environments != null && environments.Any())
            {
                filter.Append(" and (");
                int i = 0;

                foreach (DeploymentEnvironments env in environments)
                {
                    if (i > 0)
                    {
                        filter.Append(" or");
                    }

                    filter.Append($" Environment eq '{env}'");

                    i++;
                }

                filter.Append(")");
            }

            Pageable<TableEntity> entities = tableClient.Query<TableEntity>(filter: filter.ToString());

            return Task.FromResult(entities.Select(s => FromTableEntity(s)).ToArray());
        }

        private void HandleErrorResponse(Response response)
        {
            if (response.Status < 200 || response.Status >= 300)
            {
                // Only accept a 2xx response as a success
                throw new ApiException($"{nameof(SaveAppSettings)} failed with status {response.Status} and reason {response.ReasonPhrase}", ApiErrorCode.InternalError_0x7105, ApiResponseCodes.InternalError);
            }
        }

        private string GetPartitionKey(string system, DeploymentEnvironments environment) => $"{system}_{environment}";

        private TableClient InitializeTableClient()
        {
            return new TableClient(settings[Config.Settings.Api().VersionedSettings().ConnectionString()],
                settings.GetValue(Config.Settings.Api().VersionedSettings().TableName(), "AppSettings"));
        }

        private VersionedAppSettings FromTableEntity(TableEntity row)
        {
            return new VersionedAppSettings
            {
                System = row.GetString("System"),
                Environment = (DeploymentEnvironments)Enum.Parse(typeof(DeploymentEnvironments), row.GetString("Environment"), true),
                Version = row.GetInt32("Version").Value,
                IsEncrypted = row.GetBoolean("IsEncrypted").Value,
                Format = (SettingsFormats)Enum.Parse(typeof(SettingsFormats), row.GetString("Format"), true),
                Settings = row.GetString("Settings"),
            };
        }

        private TableEntity ToTableEntity(VersionedAppSettings settings)
        {
            return new TableEntity(GetPartitionKey(settings.System, settings.Environment), settings.Version.ToString())
            {
                { "System", settings.System },
                { "Environment", settings.Environment.ToString() },
                { "Version", settings.Version },
                { "IsEncrypted", settings.IsEncrypted },
                { "Format", settings.Format.ToString() },
                { "Settings", settings.Settings }
            };
        }
    }
}
