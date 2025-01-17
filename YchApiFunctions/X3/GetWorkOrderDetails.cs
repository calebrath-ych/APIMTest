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

namespace YchApiFunctions.X3
{
    public class GetWorkOrderDetails : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetWorkOrderDetails(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetWorkOrderDetails))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "work-order/{workOrderNumber}")] HttpRequest req, string workOrderNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetWorkOrderDetails(workOrderNumber));
            });
        }
    }
}
