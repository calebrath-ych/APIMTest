using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Ycrm;
using System;

namespace YchApiFunctions.Ycrm
{
    public class GetYcrmUserNotifications : ApiFunction
    {
        private IYcrmService service;
        private IValidationService validation;

        public GetYcrmUserNotifications(IYcrmService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetYcrmUserNotifications))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userId}/ycrm/notifications")] HttpRequest req, int userId)
        {
            return await ProcessRequest(req, async () =>
            {
                int page = 0;
                int limit = 20;
                if (!string.IsNullOrEmpty(req.Query["page"].ToString()))
                {
                    page = Int32.Parse(req.Query["page"].ToString());
                }
                if (!string.IsNullOrEmpty(req.Query["limit"].ToString()))
                {
                    limit = Int32.Parse(req.Query["limit"].ToString());
                }


                return SuccessResponse(await service.GetYcrmUserNotifications(userId, page, limit));
            });
        }
    }
}
