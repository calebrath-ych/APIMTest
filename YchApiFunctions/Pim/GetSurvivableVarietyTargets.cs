using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ych.Api;
using Ych.Api.Pim;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Pim
{
    public class GetSurvivableVarietyTargets : ApiFunction
    {
        private IPimService pimService;
        private IValidationService validation;

        public GetSurvivableVarietyTargets(IPimService pimService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.pimService = pimService;
            this.validation = validation;
        }

        [Function(nameof(GetSurvivableVarietyTargets))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "survivable-targets/")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await pimService.GetSurvivableVarietyTargets(), serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}