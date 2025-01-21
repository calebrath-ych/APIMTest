using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Client.Data.Configuration;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Logging;

namespace Ych.Api.Configuration
{
    // TODO: Decide if this provider is necessary since we have table storage working.
    // If so, test it and implement an API system interface for it. If not, get rid of it?
    public class AppSettingsVersionedSettingsProvider : IVersionedSettingsProvider
    {
        private ISettingsProvider settings;
        private ILogWriter log;

        public AppSettingsVersionedSettingsProvider(ISettingsProvider settings, ILogWriter log)
        {
            this.settings = settings;
            this.log = log;
        }

        public Task<VersionedAppSettings> GetAppSettings(string system, DeploymentEnvironments environment, int? currentVersion = null, int? targetVersion = null)
        {
            string settingsKey = $"Api.{system}.AppSettings";

            return Task.FromResult(JsonConvert.DeserializeObject<VersionedAppSettings>(settings[settingsKey]));
        }

        public Task<VersionedAppSettings> SaveAppSettings(string system, DeploymentEnvironments environment, int version, bool isEncrypted, SettingsFormats format, string settingsFile)
        {
            throw new NotImplementedException("AppSettingsVersionedSettingsProvider can not write to Azure environment settings. You must update the setting manually in the Azure Portal.");
        }

        public Task DeleteAppSettings(string system, DeploymentEnvironments environment, int version)
        {
            throw new NotImplementedException("AppSettingsVersionedSettingsProvider can not delete Azure environment settings. You must delete the setting manually in the Azure Portal.");
        }

        public Task<VersionedAppSettings[]> ListAppSettings(string system = null, params DeploymentEnvironments[] environments)
        {
            throw new NotImplementedException();
        }
    }
}
