using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Ycrm;

namespace YchApiFunctions.Ycrm
{
    public class GetYcrmUserNotificationSettings : ApiFunction
    {
        private IYcrmService ycrmService;
        private IValidationService validation;

        public GetYcrmUserNotificationSettings(IYcrmService ycrmService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.ycrmService = ycrmService;
            this.validation = validation;
        }

        [Function(nameof(GetYcrmUserNotificationSettings))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userId}/ycrm/notifications/settings")] HttpRequest req, int userId)
        {
            return await ProcessRequest(req, async () =>
            {
                return SuccessResponse(await ycrmService.GetYcrmUserNotificationSettings(userId));

            });
        }
    }
}