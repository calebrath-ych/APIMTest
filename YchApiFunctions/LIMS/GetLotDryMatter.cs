using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Lims
{
    public class GetLotDryMatter : ApiFunction
    {
        private ILimsService limsService;
        private IValidationService validation;

        public GetLotDryMatter(ILimsService limsService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.limsService = limsService;
            this.validation = validation;
        }

        [Function(nameof(GetLotDryMatter))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/{lotNumber}/analytics/dry-matters")] HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                this.validation.IsHarvestLotNumberValid(lotNumber);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await limsService.GetDryMatterResultsForSample(new string[] {lotNumber}));
            });
        }
    }
}
