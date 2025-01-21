using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Logging;
using Ych.Api;
using Ych.Api.X3;
using Ych.Api.Statistics;
using System;

namespace YchApiFunctions.X3
{
    public class GetPodSelectionContracts : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetPodSelectionContracts(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetPodSelectionContracts))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contracts/pod/varieties/{varietyCode}/productLine/{productLineCode}")] HttpRequest req, string varietyCode, string productLineCode)
        {
            return await ProcessRequest(req, async () =>
             {
                 // Handle any input validation here using the injected ValidationService
                 int cropYear = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                 validation.ValidateVarietyCodes(varietyCode);
                
                 // Return a SuccessResponse containing the result of your service method here
                 return SuccessResponse(await service.GetPodSelectionContracts(cropYear, varietyCode, productLineCode));
             });
        }
    }
}