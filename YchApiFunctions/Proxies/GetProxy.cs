using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Logging;
using Ych;

namespace YchApiFunctions.Proxies
{
    public class GetProxy : ProxyFunction
    {
        public GetProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        [Function(nameof(GetProxy))]
        public virtual async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "proxy/get")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string system = GetSystemName(req);
                string destinationEndpoint = null;

                List<(string key, object value)> forwardedParameters = new List<(string, object)>();

                foreach (var parameter in req.Query)
                {
                    if (parameter.Key.Equals(SystemParameter, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!system.Equals(parameter.Value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Log.Error(GetType().Name, $"A {SystemParameter} parameter was provided that does not match the target system for this proxy and it will be ignored.");
                        }
                    }
                    else if (parameter.Key.Equals(EndpointParameter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        destinationEndpoint = parameter.Value;
                    }
                    else
                    {
                        forwardedParameters.Add((parameter.Key, parameter.Value.ToString()));
                    }
                }

                string response = await ProxyService.RouteGetRequest(system, destinationEndpoint, forwardedParameters.ToArray());

                return RawResponse(response);
            });
        }
    }
}


