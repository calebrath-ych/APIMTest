using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetFields : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetFields(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetFields))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/fields")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string growerId = null;
                string searchTerm = null;
                string selectedVariety = null;
                string selectedState = null;

                if (!req.Query["growerId"].ToString().IsNullOrEmpty())
                {
                    this.validation.ValidateGrowerIds(req.Query["growerId"].ToString().Split(","));
                    growerId = req.Query["growerId"].ToString();
                }

                if (!req.Query["q"].ToString().IsNullOrEmpty())
                {
                    searchTerm = req.Query["q"].ToString();
                }

                if (!req.Query["variety"].ToString().IsNullOrEmpty())
                {
                    selectedVariety = req.Query["variety"].ToString();
                }

                if (!req.Query["state"].ToString().IsNullOrEmpty())
                {
                    selectedState = req.Query["state"].ToString();
                }
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);


                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetFields(searchTerm, growerId, selectedVariety, selectedState, year));
            });
        }
    }
}