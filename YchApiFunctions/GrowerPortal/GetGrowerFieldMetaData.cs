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
    public class GetGrowerFieldMetaData : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerFieldMetaData(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerFieldMetaData))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "fields/{fieldId}/meta")]
            HttpRequest req, int fieldId)
        {
            return await ProcessRequest(req, async () =>
            {
                if (!string.IsNullOrEmpty(req.Query["year"].ToString()))
                {
                    int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                    // Return a SuccessResponse containing the result of your service method her
                    return SuccessResponse(await growerPortalService.GetGrowerFieldMetaData(fieldId, year));
                }
                else
                {
                    // Return a SuccessResponse containing the result of your service method her
                    return SuccessResponse(await growerPortalService.GetGrowerFieldMetaData(fieldId));
                }
            });
        }
    }
}