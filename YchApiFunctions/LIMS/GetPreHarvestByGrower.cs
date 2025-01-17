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
    public class GetPreHarvestByGrower : ApiFunction
    {
        private ILimsService limsService;
        private IValidationService validation;

        public GetPreHarvestByGrower(ILimsService limsService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.limsService = limsService;
            this.validation = validation;
        }

        [Function(nameof(GetPreHarvestByGrower))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/pre-harvest-requests")]
            HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                this.validation.ValidateGrowerIds(growerId);
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await limsService.GetPreHarvestRequests(year, growerId));
            });
        }
    }
}