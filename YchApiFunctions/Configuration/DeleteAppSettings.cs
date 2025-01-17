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
    public class DeleteAppSettings : ApiFunction
    {
        private IConfigurationService service;
        private IValidationService validation;

        public DeleteAppSettings(IConfigurationService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(DeleteAppSettings))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "configuration/app-settings/delete")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                await service.DeleteAppSettings(
                    GetRequiredString(req, "system"),
                    GetRequiredEnum<DeploymentEnvironments>(req, "environment"),
                    GetRequiredInt(req, "version"));

                return SuccessResponse(true);
            });
        }
    }
}
