using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrower : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrower(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrower))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId?}")] HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                if (!string.IsNullOrWhiteSpace(growerId))
                {
                    // Validate Grower ID
                    this.validation.ValidateGrowerIds(growerId);

                    // Return a SuccessResponse containing the result of your service method here
                    return SuccessResponse(await growerPortalService.GetGrower(growerId));
                }
                else
                {
                    // Return a SuccessResponse containing the result of your service method here
                    return SuccessResponse(await growerPortalService.GetGrowers());
                }
            });
        }
    }
}