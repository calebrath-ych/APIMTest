using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Configuration;
using Ych.Configuration;

namespace YchApiFunctions.Configuration
{
    public class SaveAppSettings : ApiFunction
    {
        private IConfigurationService service;
        private IValidationService validation;

        public SaveAppSettings(IConfigurationService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(SaveAppSettings))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "configuration/app-settings")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                return SuccessResponse(await service.SaveAppSettings(
                    GetRequiredString(req, "system"),
                    GetRequiredEnum<DeploymentEnvironments>(req, "environment"),
                    GetRequiredInt(req, "version"),
                    GetRequiredBool(req, "is_encrypted"),
                    GetRequiredEnum<SettingsFormats>(req, "format"),
                    GetRequiredString(req, "settings")));
            });
        }
    }
}
