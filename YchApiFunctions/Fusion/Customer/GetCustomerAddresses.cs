using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Statistics;
using Ych.Api;
using Ych.Logging;

namespace YchApiFunctions.Fusion.Customer
{
    public class GetCustomerAddresses : FusionFunction<ICustomerAddressesService>
    {
        protected override bool DefaultNormalizeDataSets => false; // Only 1 data source currently

        public GetCustomerAddresses(IApiSystemServiceProvider serviceProvider, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(serviceProvider, validation, log, statistics)
        {
        }

        [Function(nameof(GetCustomerAddresses))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{customerCode}/addresses")] HttpRequest req, string customerCode)
        {
            return await ProcessFusionRequest(req, 
                service => service.GetCustomerAddresses(customerCode), 
                validation => validation.ValidateCustomerCodes(customerCode));
        }
    }
}
