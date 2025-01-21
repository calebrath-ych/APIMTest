using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Lims
{
    public class GetPreHarvestRequests : ApiFunction
    {
        private ILimsService limsService;
        private IValidationService validation;

        public GetPreHarvestRequests(ILimsService limsService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.limsService = limsService;
            this.validation = validation;
        }

        [Function(nameof(GetPreHarvestRequests))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "fields/pre-harvest-requests")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService

                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                string growerId = null;
                int? statusId = null;

                if (!req.Query["grower_id"].ToString().IsNullOrEmpty())
                {
                    this.validation.IsGrowerIdValid(req.Query["grower_id"].ToString());
                    growerId = req.Query["grower_id"].ToString();
                }

                if (!req.Query["status_id"].ToString().IsNullOrEmpty())
                {
                    statusId = this.validation.ValidateInteger(req.Query["status_id"].ToString());
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await limsService.GetPreHarvestRequests(year, growerId, statusId));
            });
        }
    }
}