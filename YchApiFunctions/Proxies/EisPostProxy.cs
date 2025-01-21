using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Configuration;
using Ych.Logging;

namespace YchApiFunctions.Proxies
{
    public class EisPostProxy : PostProxy
    {
        public EisPostProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        protected override string GetSystemName(HttpRequest req)
        {
            return Config.Settings.Api().Eis().Name;
        }

        [Function(nameof(EisPostProxy))]
        public override Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, new[] { "post" }, Route = "proxy/eis/post")] HttpRequest req)
        {
            return base.Run(req);
        }
    }
}