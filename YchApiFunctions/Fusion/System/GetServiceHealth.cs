using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.X3;
using Ych.Logging;

namespace YchApiFunctions.Fusion.System
{
    public class GetServiceHealth : FusionFunction<IHealthyService>
    {
        public GetServiceHealth(IApiSystemServiceProvider serviceProvider, IValidationService validation, 
            ILogWriter log, IApiStatisticsService statistics) : base(serviceProvider, validation, log, statistics)
        {
        }

        [Function(nameof(GetServiceHealth))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "service/health")] HttpRequest req)
        {
            return await ProcessFusionRequest(req, 
                service => service.ServiceHealth());
        }
    }
}
