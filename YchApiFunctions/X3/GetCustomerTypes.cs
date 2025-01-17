using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;
using System;

namespace YchApiFunctions.X3
{
    public class GetCustomerTypes : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetCustomerTypes(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
        }

        [Function(nameof(GetCustomerTypes))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer-types")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetCustomerTypes());
            });
        }
    }
}
