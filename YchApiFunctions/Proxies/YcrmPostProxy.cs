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
    public class YcrmPostProxy : PostProxy
    {
        public YcrmPostProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        protected override string GetSystemName(HttpRequest req)
        {
            return Config.Settings.Api().Ycrm().Name;
        }

        [Function(nameof(YcrmPostProxy))]
        public override Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, new[] { "post" }, Route = "proxy/ycrm/post")] HttpRequest req)
        {
            return base.Run(req);
        }
    }
}