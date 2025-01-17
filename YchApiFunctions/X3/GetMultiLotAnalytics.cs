using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Api.Selection;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetMultiLotAnalytics : ApiFunction
    {
        private IX3Service x3Service;
        private ILimsService limsService;
        private ISelectionService selectionService;
        private IValidationService validation;

        public GetMultiLotAnalytics(IX3Service x3Service, ILimsService limsService, ISelectionService selectionService,
            IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.x3Service = x3Service;
            this.limsService = limsService;
            this.selectionService = selectionService;
            this.validation = validation;
        }

        [Function(nameof(GetMultiLotAnalytics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/analytics")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] lotList = req.Query["lots"].ToString().Replace(" ", "")
                    .Split(",", System.StringSplitOptions.RemoveEmptyEntries);

                validation.ValidateLotNumbers(LotNumberTypes.Any, lotList);

                if (lotList.Length < 1)
                {
                    throw new ApiValidationException("lotList", lotList,
                        "At least one valid lot is required");
                }

                List<Dictionary<string, object>> lots = await x3Service.GetMultiLotAnalytics(lotList);
                foreach (var lot in lots)
                {
                    lot["survivable_compounds"] = await limsService.GetLotSurvivableData(lot["lot_number"].ToString());
                    lot["harvest_sample_data"] =
                        await selectionService.GetLotHarvestSample(lot["lot_number"].ToString());
                }
                
                return SuccessResponse(lots);
            });
        }
    }
}