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
    public class GetGrowerPortalUserNotificationSettings : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerPortalUserNotificationSettings(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerPortalUserNotificationSettings))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/user/{userId}/notifications/settings")] HttpRequest req, string growerId, int userId)
        {
            return await ProcessRequest(req, async () =>
            {

                
                return SuccessResponse(await growerPortalService.GetGrowerPortalUserNotificationSettings(userId, growerId));

            });
        }
    }
}