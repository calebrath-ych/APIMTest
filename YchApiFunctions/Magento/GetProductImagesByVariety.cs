using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.Magento;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Magento
{
    public class GetProductImagesByVariety : ApiFunction
    {
        private IMagentoService magentoService;
        private IValidationService validation;

        public GetProductImagesByVariety(IMagentoService magentoService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.magentoService = magentoService;
            this.validation = validation;
        }

        [Function(nameof(GetProductImagesByVariety))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "varieties/{varietyCode}/product-images")] HttpRequest req, string varietyCode)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                this.validation.IsVarietyCodeValid(varietyCode);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await magentoService.GetMagentoProductImagesByVariety(varietyCode));
            });
        }
    }
}