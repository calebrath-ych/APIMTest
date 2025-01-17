
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrowerBrewerFeedback : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerBrewerFeedback(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerBrewerFeedback))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/brewer-feedback")]
            HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {

                // Validate Grower ID
                this.validation.ValidateGrowerIds(growerId);
                
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetGrowerBrewerFeedback(year,growerId));
            });
        }
    }
}