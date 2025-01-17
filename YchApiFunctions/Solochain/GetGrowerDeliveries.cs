using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.Solochain;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Solochain
{
    public class GetGrowerDeliveries : ApiFunction
    {
        private ISolochainService service;
        private IValidationService validation;

        public GetGrowerDeliveries(ISolochainService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerDeliveries))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/deliveries/")] HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                string status = string.IsNullOrWhiteSpace(req.Query["status"].ToString()) ? null : req.Query["status"].ToString();

                // Handle any input validation here using the injected ValidationService
                this.validation.ValidateGrowerIds(growerId);
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetGrowerDeliveries(growerId, year, status));
            });
        }
    }
}
