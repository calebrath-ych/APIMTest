using Microsoft.AspNetCore.Http;
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
    public class GetPackSizesForContractLines : ApiFunction
    {
        private IX3Service X3Service;
        private IValidationService validation;

        public GetPackSizesForContractLines(IX3Service x3Service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.X3Service = x3Service;
            this.validation = validation;
        }

        [Function(nameof(GetPackSizesForContractLines))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customer/{customerCode}/contracts/pack-sizes")] HttpRequest req, string customerCode)
        {
            return await ProcessRequest(req, async () =>
            {
                validation.ValidateCustomerCodes(customerCode);
                string cropYear = req.Query["cropYear"].ToString();
                string productLineCode = req.Query["productLineCode"].ToString();
                string varietyCode = req.Query["varietyCode"].ToString();
                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await X3Service.GetPackSizesForContractLines(customerCode, cropYear, productLineCode, varietyCode));
            });
        }
    }
}
