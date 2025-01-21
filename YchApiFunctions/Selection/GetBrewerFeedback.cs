﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Selection;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Selection
{
    public class GetBrewerFeedback : ApiFunction
    {
        private ISelectionService SelectionService;
        private IValidationService validation;

        public GetBrewerFeedback(ISelectionService SelectionService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.SelectionService = SelectionService;
            this.validation = validation;
        }

        [Function(nameof(GetBrewerFeedback))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "feedback/brewer")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                int hourOffset = this.validation.ValidateInteger(req.Query["hours"].ToString(), 24);
                DateTime startDate = DateTime.UtcNow.ToPst().AddHours(hourOffset * -1);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await SelectionService.GetBrewerFeedback(startDate));
            });
        }
    }
}