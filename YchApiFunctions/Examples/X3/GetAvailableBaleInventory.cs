using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Examples.X3
{
    public class GetAvailableBaleInventory_Example : ApiFunction
    {
        private IX3Service x3Service;
        private IValidationService validation;

        public GetAvailableBaleInventory_Example(IX3Service x3Service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.x3Service = x3Service;
            this.validation = validation;
        }

        // Using nameof(ApiFunctionClass) will ensure that if the class name ever changes, so will the function name.
        [Function(nameof(GetAvailableBaleInventory_Example))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "examples/lots/{lotNumber}/available-bale-inventory")] HttpRequest req, string lotNumber)
        {
            // Wrapping function implementation in ProcessRequest will guarantee error and response handing is standardized.
            return await ProcessRequest(req, async () =>
            {
                // Validate request parameters. Validation functions should generally throw an ApiValidationException when they do not pass,
                // unless function specific logic needs to be applied to the result.
                validation.ValidateLotNumbers(LotNumberTypes.Harvest, lotNumber);

                // Generally the last step is to invoke the corresponding service, and return the result as success.
                // If any exception is thrown at this point, it should be allowed to bubble up to the ProcessRequest error handling.
                return SuccessResponse(await x3Service.GetAvailableBaleInventory(lotNumber));
            });
        }
    }
}
