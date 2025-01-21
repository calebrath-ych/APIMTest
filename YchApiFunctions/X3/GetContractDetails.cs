using System;
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

namespace YchApiFunctions.Ycrm
{
    public class GetContractDetails : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetContractDetails(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetContractDetails))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contracts/customers/{customerCode}/varieties/{varietyCode}")] HttpRequest req, string customerCode, string varietyCode)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                // Handle any input validation here using the injected ValidationService
                validation.ValidateCustomerCodes(customerCode);
                this.validation.ValidateVarietyCodes(varietyCode);


                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetContractDetails(customerCode, varietyCode, year));
            });
        }
    }
}