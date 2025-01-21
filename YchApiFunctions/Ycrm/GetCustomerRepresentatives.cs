using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Ycrm;
using System;

namespace YchApiFunctions.Ycrm
{
    public class GetCustomerRepresentatives : ApiFunction
    {
        private IYcrmService service;
        private IValidationService validation;

        public GetCustomerRepresentatives(IYcrmService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetCustomerRepresentatives))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{customerId}/representatives")] HttpRequest req, string customerId)
        {
            return await ProcessRequest(req, async () =>
            {
                return SuccessResponse(await service.GetCustomerRepresentatives(customerId));
            });
        }
    }
}
