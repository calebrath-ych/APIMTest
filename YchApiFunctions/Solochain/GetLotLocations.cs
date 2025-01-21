using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.Logging;
using Ych.Api.Solochain;
using Ych.Api.X3;
using Ych.Logging;

namespace YchApiFunctions.Solochain
{
    public class GetLotLocations : ApiFunction
    {
        private ISolochainService service;
        private IValidationService validation;

        public GetLotLocations(ISolochainService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetLotLocations))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/locations/{year}")] HttpRequest req, string year)
        {
            return await ProcessRequest(req, async () =>
            {
                int yearAsInteger = validation.ValidateYear(year);

                return SuccessResponse(await service.GetLotLocations(yearAsInteger));
            });
        }
    }
}