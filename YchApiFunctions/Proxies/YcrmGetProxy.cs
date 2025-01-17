using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Configuration;
using Ych.Logging;

namespace YchApiFunctions.Proxies
{
    public class YcrmGetProxy : GetProxy
    {
        public YcrmGetProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        protected override string GetSystemName(HttpRequest req)
        {
            return Config.Settings.Api().Ycrm().Name;
        }

        [Function(nameof(YcrmGetProxy))]
        public override Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, new[] { "get" }, Route = "proxy/ycrm/get")] HttpRequest req)
        {
            return base.Run(req);
        }
    }
}
