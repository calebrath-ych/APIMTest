using Microsoft.AspNetCore.Http;
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
    public class GetUnprocessedSampleMetadata : ApiFunction
    {
        private ISelectionService SelectionService;
        private IValidationService validation;

        public GetUnprocessedSampleMetadata(ISelectionService SelectionService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.SelectionService = SelectionService;
            this.validation = validation;
        }

        [Function(nameof(GetUnprocessedSampleMetadata))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sample-meta/unprocessed")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await SelectionService.GetUnprocessedSampleMetadata());
            });
        }
    }
}