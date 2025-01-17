using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.X3;
using Ych.Logging;

namespace YchApiFunctions.Fusion.Customer
{
    public class GetCustomerContacts : FusionFunction<ICustomerContactsService>
    {
        public GetCustomerContacts(IApiSystemServiceProvider serviceProvider, IValidationService validation, 
            ILogWriter log, IApiStatisticsService statistics) : base(serviceProvider, validation, log, statistics)
        {
        }

        [Function(nameof(GetCustomerContacts))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{customerCode}/contacts")] HttpRequest req, string customerCode)
        {
            return await ProcessFusionRequest(req, 
                service => service.GetCustomerContacts(customerCode),
                validation => validation.ValidateCustomerCodes(customerCode));
        }
    }
}
