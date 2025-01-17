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
    public class PostGrowerPortalNotification : ApiFunction
    {
        private INotificationService notificationService;
        private IValidationService validationService;
        private IGrowerPortalService growerPortalService;

        public PostGrowerPortalNotification(INotificationService notificationService, IValidationService validationService, IGrowerPortalService growerPortalService, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.notificationService = notificationService;
            this.validationService = validationService;
            this.growerPortalService = growerPortalService;
        }

        [Function(nameof(PostGrowerPortalNotification))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grower-portal/notification/")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                validationService.ValidateNotificationForm(req.Form.Keys);

                string growerId;

                switch (req.Form["identifierType"])
                {
                    case "GrowerId":
                        growerId = req.Form["identifier"];
                        break;
                    case "LotNumber":
                        growerId = await growerPortalService.LotNumberToGrower(req.Form["identifier"]);
                        break;
                    case "RecipientList":
                        growerId = "ALL001";
                        break;
                    default:
                        throw new ApiValidationException("Indentifier Type", req.Form["identifierType"], $"Identifier Type { req.Form["identifierType"] } not valid.");
                }

                string response = await notificationService.PostGrowerPortalNotification(
                    req.Form["notificationTypeId"],
                    growerId,
                    req.Form["recipientEmail"],
                    req.Form["sentDate"],
                    req.Form["data"]
                );

                return SuccessResponse(response);
            });
        }
    }
}
