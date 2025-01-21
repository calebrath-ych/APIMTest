using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.Solochain;
using Ych.Logging;

namespace YchApiFunctions.Solochain
{
    public class GetSingleLotLocation : ApiFunction
    {
        private ISolochainService service;
        private IValidationService validation;

        public GetSingleLotLocation(ISolochainService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetSingleLotLocation))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/{lotNumber}/location")] HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                validation.IsHarvestLotNumberValid(lotNumber);

                return SuccessResponse(await service.GetSingleLotLocation(lotNumber));
            });
        }
    }
}