using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Lims
{
    public class GetPreHarvestRequestsByRegion : ApiFunction
    {
        private ILimsService limsService;
        private IValidationService validation;

        public GetPreHarvestRequestsByRegion(ILimsService limsService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.limsService = limsService;
            this.validation = validation;
        }

        [Function(nameof(GetPreHarvestRequestsByRegion))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "grower-regions/{region}/pre-harvest-requests")]
            HttpRequest req, string region)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService

                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                int? statusId = null;
                
                
                int regionId = this.validation.ValidateInteger(region);

                if (!string.IsNullOrEmpty(req.Query["status_id"].ToString()))
                {
                    statusId = this.validation.ValidateInteger(req.Query["status_id"].ToString());
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await limsService.GetPreHarvestRequestsByRegion(year, regionId, statusId));
            });
        }
    }
}