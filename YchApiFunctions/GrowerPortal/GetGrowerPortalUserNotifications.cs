using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrowerPortalUserNotifications : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerPortalUserNotifications(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerPortalUserNotifications))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/user/{userId}/notifications")] HttpRequest req, int userId)
        {
            return await ProcessRequest(req, async () =>
            {
                int page = 0;
                int limit = 20;
                if (!req.Query["page"].ToString().IsNullOrEmpty())
                {
                     page = Int32.Parse(req.Query["page"].ToString());
                }
                if (!req.Query["limit"].ToString().IsNullOrEmpty())
                {
                     limit = Int32.Parse(req.Query["limit"].ToString());
                }
                
                return SuccessResponse(await growerPortalService.GetGrowerPortalUserNotifications(userId, page, limit));

            });
        }
    }
}