using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Configuration;
using Ych.Logging;

namespace YchApiFunctions.Proxies
{
    public class LimsGetProxy : GetProxy
    {
        public LimsGetProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        protected override string GetSystemName(HttpRequest req)
        {
            return Config.Settings.Api().Lims().Name;
        }

        [Function(nameof(LimsGetProxy))]
        public override Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, ["get"], Route = "proxy/lims/get")] HttpRequest req)
        {
            return base.Run(req);
        }
    }
}
