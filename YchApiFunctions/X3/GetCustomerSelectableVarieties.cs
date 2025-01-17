using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetCustomerSelectableVarieties : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetCustomerSelectableVarieties(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetCustomerSelectableVarieties))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "contracts/customers/{customerCode}/selectable-varieties/")] HttpRequest req, string customerCode)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                validation.ValidateGrowerIds(customerCode);

                return SuccessResponse(await service.GetCustomerSelectableVarieties(customerCode));
            });
        }
    }
}
