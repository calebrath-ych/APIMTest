using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrowerFieldKilnSampleReport : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerFieldKilnSampleReport(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerFieldKilnSampleReport))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "fields/{fieldId}/kiln-sample-reports")]
            HttpRequest req, int fieldId)
        {
            return await ProcessRequest(req, async () =>
            {
                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetGrowerFieldKilnSampleReports(fieldId));
            });
        }
    }
}