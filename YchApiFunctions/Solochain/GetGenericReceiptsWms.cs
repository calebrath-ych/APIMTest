using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.Logging;
using Ych.Api.Solochain;
using Ych.Logging;

namespace YchApiFunctions.Solochain
{
    public class GetGenericReceiptsWms : ApiFunction
    {
        private ISolochainService service;
        private IValidationService validation;

        public GetGenericReceiptsWms(ISolochainService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetGenericReceiptsWms))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "generic-receipts/wms")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetGenericReceiptsWms(year));
            });
        }
    }
}
