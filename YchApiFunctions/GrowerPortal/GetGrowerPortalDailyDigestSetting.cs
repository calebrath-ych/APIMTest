using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrowerPortalDailyDigestSetting : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerPortalDailyDigestSetting(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerPortalDailyDigestSetting))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/user/{userId}/notifications/daily-digest")] HttpRequest req, string growerId, int userId)
        {
            return await ProcessRequest(req, async () =>
            {
                // Validate Grower ID
                this.validation.ValidateGrowerIds(growerId);

                return SuccessResponse(await growerPortalService.GetGrowerPortalDailyDigestSetting(userId, growerId));

            });
        }
    }
}