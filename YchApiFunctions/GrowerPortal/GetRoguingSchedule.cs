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
using Castle.Core.Internal;

namespace YchApiFunctions.GrowerPortal
{
    public class GetRoguingSchedule : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetRoguingSchedule(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetRoguingSchedule))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "roguing/schedule/{scheduleId}")] HttpRequest req, int scheduleId)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year - 1);

                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetRoguingSchedule(scheduleId, year));
               
            });
        }
    }
}