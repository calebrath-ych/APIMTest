using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Examples.GrowerPortal
{
    public class GetHarvestInformationByLot_Example : ApiFunction
    {
        private IExampleGrowerPortalService gpService;
        private IValidationService validation;

        public GetHarvestInformationByLot_Example(IExampleGrowerPortalService gpService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.gpService = gpService;
            this.validation = validation;
        }

        // Using nameof(ApiFunctionClass) will ensure that if the class name ever changes, so will the function name.
        [Function(nameof(GetHarvestInformationByLot_Example))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "examples/lots/{lotNumber}/harvest-data")] HttpRequest req, string lotNumber)
        {
            // Wrapping function implementation in ProcessRequest will guarantee error and response handing is standardized.
            return await ProcessRequest(req, async () =>
            {
                // Validate request parameters. Validation functions should generally throw an ApiValidationException when they do not pass,
                // unless function specific logic needs to be applied to the result.
                validation.ValidateLotNumbers(LotNumberTypes.Harvest, lotNumber);

                // Generally the last step is to invoke the corresponding service, and return the result as success.
                // If any exception is thrown at this point, it should be allowed to bubble up to the ProcessRequest error handling.
                return SuccessResponse(await gpService.GetHarvestInformationByLot(lotNumber));
            });
        }
    }
}
