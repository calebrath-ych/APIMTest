using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetRoguingSchedules : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetRoguingSchedules(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetRoguingSchedules))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "roguing/schedules/")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year - 1);

                int? teamId = null;
                int? userId = null;
                DateTime? windowStart = null;
                DateTime? windowEnd = null;
                bool onlyCurrent = true;
                bool onlyFinished = false;

                if (!req.Query["teamId"].ToString().IsNullOrEmpty())
                {
                    teamId = Convert.ToInt32(req.Query["teamId"]);
                }

                if (!req.Query["userId"].ToString().IsNullOrEmpty())
                {
                    userId = Convert.ToInt32(req.Query["userId"]);
                }

                if (!req.Query["windowStart"].ToString().IsNullOrEmpty())
                {
                    windowStart = Convert.ToDateTime(req.Query["windowStart"]);
                }

                if (!req.Query["windowEnd"].ToString().IsNullOrEmpty())
                {
                    windowEnd = Convert.ToDateTime(req.Query["windowEnd"]);
                }

                if (!req.Query["onlyCurrent"].ToString().IsNullOrEmpty())
                {
                    onlyCurrent = this.validation.ValidateBool(req.Query["onlyCurrent"], onlyCurrent);
                }

                if (!req.Query["onlyFinished"].ToString().IsNullOrEmpty())
                {
                    onlyFinished = this.validation.ValidateBool(req.Query["onlyFinished"], onlyFinished);
                }

                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetRoguingSchedules(year, teamId, userId, windowStart, windowEnd, onlyCurrent, onlyFinished));
               
            });
        }
    }
}