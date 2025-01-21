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
    public class GetGrowersDetails : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowersDetails(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowersDetails))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "grower-details/")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                var defaultYear = DateTime.UtcNow.ToPst().Year;

                if (DateTime.UtcNow.ToPst().Month <= 8)
                {
                    defaultYear -= 1;
                }

                int year = this.validation.ValidateYear(req.Query["year"].ToString(), defaultYear);

                return SuccessResponse(await growerPortalService.GetGrowersDetails(year));
            });
        }
    }
}