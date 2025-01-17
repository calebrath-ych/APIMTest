using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Configuration;

namespace YchApiFunctions.Configuration
{
    public class VerifyMobileClientVersion : ApiFunction
    {
        private IConfigurationService service;
        private IValidationService validation;

        public VerifyMobileClientVersion(IConfigurationService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(VerifyMobileClientVersion))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "configuration/{systemName}/version")] HttpRequest req, string systemName)
        {
            return await ProcessRequest(req, async () =>
            {
                string clientVersion = GetRequiredString(req, "version");

                return SuccessResponse(await service.VerifyMobileClientVersion(systemName, clientVersion));
            });
        }
    }
}
