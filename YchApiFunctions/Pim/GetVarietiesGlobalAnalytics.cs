using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych;
using Ych.Api;
using Ych.Api.Pim;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Pim
{
    public class GetVarietiesGlobalAnalytics : ApiFunction
    {
        private IPimService PimService;
        private IValidationService validation;

        public GetVarietiesGlobalAnalytics(IPimService PimService, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.PimService = PimService;
            this.validation = validation;
        }

        [Function(nameof(GetVarietiesGlobalAnalytics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "varieties/analytics/global")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] varieties = req.Query["varieties"].ToString().Replace(" ", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                string productLine = req.Query["productLine"].ToString();

                this.validation.ValidateVarietyCodes(varieties);

                if (varieties.Length < 1)
                {
                    throw new ApiValidationException("varieties", varieties, "At least one variety is required");
                }

                if (productLine.IsNullOrEmpty())
                {
                    // Default product line
                    productLine = "CON02";
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await PimService.GetVarietiesGlobalAnalytics(varieties, productLine));
            });
        }
    }
}