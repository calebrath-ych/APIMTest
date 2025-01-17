using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Lims
{
    public class GetVarietyAnaltyics : ApiFunction
    {
        private ILimsService limsService;
        private IValidationService validation;

        public GetVarietyAnaltyics(ILimsService limsService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.limsService = limsService;
            this.validation = validation;
        }

        [Function(nameof(GetVarietyAnaltyics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "varieties/{varietyCode}/analytics")] HttpRequest req, string varietyCode)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                this.validation.ValidateVarietyCodes(varietyCode);

                string region = req.Query["region"].ToString();

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await limsService.GetVarietyAnalytics(varietyCode, region, year));
            });
        }
    }
}