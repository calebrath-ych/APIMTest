using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.GrowerPortal
{
    public class GetLotHarvestData : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetLotHarvestData(IGrowerPortalService growerPortalService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetLotHarvestData))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/{lotNumber}/harvest-data")] HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                // Validate Lot Number
                // this.validation.ValidateLotNumbers(LotNumberTypes.Harvest, lotNumber);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await growerPortalService.GetLotHarvestData(lotNumber));
            });
        }
    }
}