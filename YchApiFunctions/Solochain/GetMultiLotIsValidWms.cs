using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.Solochain;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Solochain
{
    public class GetMultiLotIsValidWms : ApiFunction
    {
        private ISolochainService service;
        private IValidationService validation;

        public GetMultiLotIsValidWms(ISolochainService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetMultiLotIsValidWms))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/valid/wms/")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] lots = req.Query["lots"].ToString().Replace(" ", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                validation.ValidateLotNumbers(LotNumberTypes.Harvest, lots);

                if (lots.Length < 1)
                {
                    throw new ApiValidationException("lots", lots, "At least one lot is required");
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetMultiLotIsValidWms(lots));

            });
        }
    }
}
