using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using System.Linq;
using Ych.Api.Lims;
using Ych.Api.Selection;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetLotAnalytics : ApiFunction
    {
        private IX3Service x3Service;
        private IGrowerPortalService gpService;
        private ILimsService limsService;
        private ISelectionService selectionService;
        private IValidationService validation;

        public GetLotAnalytics(IX3Service x3Service, 
            ISelectionService selectionService, 
            ILimsService limsService, 
            IGrowerPortalService gpService,
            IValidationService validation,
            ILogWriter log, 
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.x3Service = x3Service;
            this.gpService = gpService;
            this.limsService = limsService;
            this.selectionService = selectionService;
            this.validation = validation;
        }

        [Function(nameof(GetLotAnalytics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/{lotNumber}/analytics/")]
            HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                validation.ValidateLotNumbers(LotNumberTypes.Any, lotNumber);

                Dictionary<string, object> lotAnalytics = await x3Service.GetLotAnalytics(lotNumber);
                
                // Combine component lots with the given lot (lotNumber)
                string lotNumberString = lotAnalytics["component_lots"].ToString() + "," + lotNumber;
                lotAnalytics.Remove("component_lots");

                // Split the component_lots into an array
                string[] lotNumbers = lotNumberString.Split(",");

                // Get farm data from Grower Portal
                lotAnalytics["farmData"] = await gpService.GetLotFarmData(lotNumbers);
                
                // Get sample data from Selection
                lotAnalytics["sampleData"] = await selectionService.GetLotHarvestSample(lotNumber);
                
                // Get farm data from LIMS
                // lotAnalytics["survivable_compounds"] = await limsService.GetLotSurvivableData(lotNumber);

                // YchApi contract states it should always return a list/array result set for client predictability, even if it only returns one object
                // This API still returns a single object because lot lookup depends on that, but it is being updated to match other API's.
                // While we transition clients, we need to maintain support for a single objetc result, but add support for multi-object for updated clients
                if (req.Query.TryGetValue("multi", out _))
                {
                    return SuccessResponse(new Dictionary<string, object>[] { lotAnalytics });
                }
                else
                {
                    return SuccessResponse(lotAnalytics);
                }
            });
        }
    }
}