using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Newtonsoft.Json;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.Pim;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Pim
{
    public class GetVarietyStandardBrewingValuesAndAromas : ApiFunction
    {
        private IPimService pimService;
        private IValidationService validation;

        public GetVarietyStandardBrewingValuesAndAromas(IPimService pimService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.pimService = pimService;
            this.validation = validation;
        }

        [Function(nameof(GetVarietyStandardBrewingValuesAndAromas))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "varieties/{varietyCode}/standard-values")] HttpRequest req, string varietyCode)
        {
            return await ProcessRequest(req, async () =>
            {
                // Defaults to Type 90 Hop Pellets "PEL02" when product line code is not provided
                string productLineCode = string.IsNullOrWhiteSpace(req.Query["productLineCode"].ToString()) ? "PEL02" : req.Query["productLineCode"].ToString();

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await pimService.GetVarietyStandardBrewingValuesAndAromas(varietyCode, productLineCode), serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}