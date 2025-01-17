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
    public class GetProductLineCodeAndNameMap : ApiFunction
    {
        private IPimService pimService;
        private IValidationService validation;

        public GetProductLineCodeAndNameMap(IPimService pimService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.pimService = pimService;
            this.validation = validation;
        }

        [Function(nameof(GetProductLineCodeAndNameMap))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "product-lines/list")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await pimService.GetProductLineCodeAndNameMap(), serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}