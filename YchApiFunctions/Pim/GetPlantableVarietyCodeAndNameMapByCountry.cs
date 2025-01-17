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
    public class GetHarvestVarietyCodeAndNameMapByCountry : ApiFunction
    {
        private IPimService pimService;
        private IValidationService validation;

        public GetHarvestVarietyCodeAndNameMapByCountry(IPimService pimService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.pimService = pimService;
            this.validation = validation;
        }

        [Function(nameof(GetHarvestVarietyCodeAndNameMapByCountry))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "varieties/harvest")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string countryCode = req.Query["countryCode"].ToString();
                
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await pimService.GetHarvestVarietyCodeAndNameMapByCountry(countryCode), serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}