﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Logging;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetAvailableLotInventory : ApiFunction
    {
        private IX3Service X3Service;
        private IValidationService validation;

        public GetAvailableLotInventory(IX3Service x3Service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.X3Service = x3Service;
            this.validation = validation;
        }

        [Function(nameof(GetAvailableLotInventory))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/{lotNumber}/inventory")] HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                validation.ValidateLotNumbers(LotNumberTypes.Harvest, lotNumber);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await X3Service.GetAvailableLotInventory(lotNumber));
            });
        }
    }
}
