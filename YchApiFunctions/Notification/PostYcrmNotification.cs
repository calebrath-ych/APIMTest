using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using System.Collections.Generic;
using System.IO;
using Ych.Communication;
using Ych.Api.Notification;
using Ych.Api.GrowerPortal;

namespace YchApiFunctions.Notification
{
    public class PostYcrmNotification : ApiFunction
    {
        private INotificationService notificationService;
        private IValidationService validationService;
        private IGrowerPortalService growerPortalService;

        public PostYcrmNotification(INotificationService notificationService, IValidationService validationService,
            IGrowerPortalService growerPortalService, ILogWriter log, IApiStatisticsService statistics) :
            base(log, statistics)
        {
            this.notificationService = notificationService;
            this.validationService = validationService;
            this.growerPortalService = growerPortalService;
        }

        [Function(nameof(PostYcrmNotification))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ycrm/notification/")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                var recipientEmail = req.Form["recipientEmail"].ToString();
                var customerId = !string.IsNullOrEmpty(req.Form["customerId"].ToString())
                    ? Convert.ToInt32(req.Form["customerId"])
                    : (int?) null;
                if (customerId == null && string.IsNullOrEmpty(recipientEmail))
                {
                    List<(string, object, string)> failures = new List<(string, object, string)>();

                    failures.Add(("customerId", customerId, "Required without recipientEmail"));
                    failures.Add(("recipientEmail", recipientEmail, "Required without customerId"));
                    throw new ApiValidationException(failures.ToArray());
                }

                string response = await notificationService.PostYcrmNotification(
                    req.Form["notificationTypeId"],
                    customerId,
                    recipientEmail,
                    req.Form["sentDate"],
                    req.Form["triggeredBy"],
                    req.Form["data"]
                );

                return SuccessResponse(response);
            });
        }
    }
}