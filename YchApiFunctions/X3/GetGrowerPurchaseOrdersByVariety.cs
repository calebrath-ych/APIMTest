using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetGrowerPurchaseOrdersByVariety : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetGrowerPurchaseOrdersByVariety(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerPurchaseOrdersByVariety))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/purchase-orders/varieties/")] HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                validation.ValidateGrowerIds(growerId);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetGrowerPurchaseOrdersByVariety(growerId, year));
            });
        }
    }
}
