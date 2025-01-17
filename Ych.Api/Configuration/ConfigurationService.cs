using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
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
    public interface IConfigurationService
    {
        Task<VersionedAppSettings[]> GetAppSettings(string system, DeploymentEnvironments environment, int? currentVersion = null, int? targetVersion = null);
        Task<VersionedAppSettings[]> SaveAppSettings(string system, DeploymentEnvironments environment, int version, bool isEncrypted, SettingsFormats format, string settingsFile);
        Task DeleteAppSettings(string system, DeploymentEnvironments environment, int version);
        Task<VersionedAppSettings[]> ListAppSettings(string system = null, params DeploymentEnvironments[] environments);
        Task<ClientVersionResult[]> VerifyMobileClientVersion(string systemName, string clientVersion);
    }

    public interface IVersionedSettingsProvider
    {
        Task<VersionedAppSettings> GetAppSettings(string system, DeploymentEnvironments environment, int? currentVersion = null, int? targetVersion = null);
        Task<VersionedAppSettings> SaveAppSettings(string system, DeploymentEnvironments environment, int version, bool isEncrypted, SettingsFormats format, string settingsFile);
        Task DeleteAppSettings(string system, DeploymentEnvironments environment, int version);
        Task<VersionedAppSettings[]> ListAppSettings(string system = null, params DeploymentEnvironments[] environments);
    }

    public class ConfigurationService : IConfigurationService
    {
        IVersionedSettingsProvider versionedSettingsProvider;
        private ISettingsProvider settings;
        private ILogWriter log;

        public ConfigurationService(IVersionedSettingsProvider versionedSettingsProvider, ISettingsProvider settings, ILogWriter log)
        {
            this.versionedSettingsProvider = versionedSettingsProvider;
            this.settings = settings;
            this.log = log;
        }

        public async Task<VersionedAppSettings[]> GetAppSettings(string system, DeploymentEnvironments environment, int? currentVersion = null, int? targetVersion = null)
        {
            VersionedAppSettings settings = await versionedSettingsProvider.GetAppSettings(system, environment, currentVersion, targetVersion).ConfigureAwait(false);

            return settings == null ? new VersionedAppSettings[0] : new VersionedAppSettings[] { settings };
        }

        public async Task<VersionedAppSettings[]> SaveAppSettings(string system, DeploymentEnvironments environment, int version, bool isEncrypted, SettingsFormats format, string settingsFile)
        {
            VersionedAppSettings settings = await versionedSettingsProvider.SaveAppSettings(system, environment, version, isEncrypted, format, settingsFile).ConfigureAwait(false);

            return settings == null ? new VersionedAppSettings[0] : new VersionedAppSettings[] { settings };
        }

        public Task DeleteAppSettings(string system, DeploymentEnvironments environment, int version)
        {
            return versionedSettingsProvider.DeleteAppSettings(system, environment, version);
        }

        public Task<VersionedAppSettings[]> ListAppSettings(string system = null, params DeploymentEnvironments[] environments)
        {
            return versionedSettingsProvider.ListAppSettings(system, environments);
        }

        public async Task<ClientVersionResult[]> VerifyMobileClientVersion(string systemName, string clientVersion)
        {
            IMobileClientSystem mobileClientSystem = ApiSettings.GetSystem<IMobileClientSystem>(systemName);

            if (mobileClientSystem == null)
            {
                throw new ApiException($"Invalid MobileClientSystem: {systemName}.");
            }

            string currentVersion = settings.GetValue<string>(mobileClientSystem.CurrentMobileVersion());

            if (string.IsNullOrEmpty(currentVersion))
            {
                log.Error(GetType().Name, $"System {systemName} is not configured for client version enforcement.");

                currentVersion = clientVersion;
            }

            VersionedAppSettings versionedAppSettings = null;

            if (settings.GetValue(Config.Settings.Api().VersionedSettings().Enabled(), true))
            {
                try
                {
                    versionedAppSettings = await versionedSettingsProvider.GetAppSettings(systemName, Config.Environment.Type).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    log.Error(GetType().Name, ex, $"Unexpected exception getting VersionedAppSettings for {systemName}");
                }
            }

            ClientVersionResult result = new ClientVersionResult
            {
                SystemName = systemName,
                ClientVersion = clientVersion,
                CurrentVersion = currentVersion,
                AppSettings = versionedAppSettings
            };

            if (!result.IsClientCurrent)
            {
                log.Warning(GetType().Name, $"Mobile client is not running current version.", additionalProps: new (string, object)[]
                {
                    ("SystemName", systemName),
                    ("ClientVersion", clientVersion),
                    ("CurrentVersion", currentVersion)
                });
            }

            return new ClientVersionResult[] { result };
        }
    }
}
