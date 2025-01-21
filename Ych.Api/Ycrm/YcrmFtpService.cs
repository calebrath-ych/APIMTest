using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data.Ftp;
using Ych.Logging;

namespace Ych.Api.Ycrm
{
    //TODO: This class will not be needed if we move file upload endpoints to custom apps.
    public class YcrmFtpService : SftpService
    {
        protected override string HostConfigKey => "YcrmFtpHost";
        protected override string PortConfigKey => "YcrmFtpPort";
        protected override string UsernameConfigKey => "YcrmFtpUsername";
        protected override string PasswordConfigKey => "YcrmFtpPassword";

        private string basePath;

        public YcrmFtpService(ISettingsProvider settings, ILogWriter log) : base(settings, log) // TODO: log
        {
            basePath = settings["YcrmFtpBasePath"];
        }

        public async Task VerifyInteractionPath(ulong customerId, ulong interactionId)
        {
            string interactionPath = $"{basePath}/customer_{customerId}/interactions";

            if (!await PathExists(interactionPath))
            {
                await CreatePath(interactionPath);
            }
        }
    }
}
