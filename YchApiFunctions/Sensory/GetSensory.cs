using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Sensory;
using System.Collections.Generic;

namespace YchApiFunctions.Lims
{
    public class GetSensory : ApiFunction
    {
        private ISensoryService service;
        private IValidationService validation;


        public GetSensory(ISensoryService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetSensory))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/sensory")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                string[] lotNumbers = req.Query["lots"].ToString().Replace(" ", "").Split(",");
                validation.ValidateLotNumbers(LotNumberTypes.Any, lotNumbers);

                Dictionary<string, object> results = new Dictionary<string, object>();

                foreach (string lotNumber in lotNumbers)
                {
                    // Return a SuccessResponse containing the result of your service method here
                    results[lotNumber] = await service.GetLotSensory(lotNumber);
                }

                return SuccessResponse(results);
            });
        }
    }
}
