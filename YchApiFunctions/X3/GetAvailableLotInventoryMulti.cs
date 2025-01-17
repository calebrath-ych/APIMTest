using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetAvailableLotInventoryMulti : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetAvailableLotInventoryMulti(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetAvailableLotInventoryMulti))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "lots/inventory")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] lots = req.Form["lots"].ToString().Split(",");
                
                validation.ValidateLotNumbers(LotNumberTypes.Harvest, lots);
                
                return SuccessResponse(await service.GetAvailableLotInventoryMulti(lots));
            });
        }
    }
}
