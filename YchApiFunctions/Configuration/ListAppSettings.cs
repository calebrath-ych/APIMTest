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
    public class ListAppSettings : ApiFunction
    {
        private IConfigurationService service;
        private IValidationService validation;

        public ListAppSettings(IConfigurationService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(ListAppSettings))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "configuration/app-settings/list")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                return SuccessResponse(await service.ListAppSettings(
                    GetString(req, "system"),
                    GetEnumArray<DeploymentEnvironments>(req, "environment")));
            });
        }
    }
}
