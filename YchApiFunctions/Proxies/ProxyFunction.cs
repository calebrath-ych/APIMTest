using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Configuration;
using Ych.Logging;
using Ych;

namespace YchApiFunctions.Proxies
{
    public abstract class ProxyFunction : ApiFunction
    {
        protected const string SystemParameter = "_system";
        protected const string EndpointParameter = "_endpoint";
        // While we transition away from a generic proxy system, these are the systems that have
        // a dedicated proxy and may not be accessed using the generic proxies
        private static readonly string[] ProhibitedSystems = new string[]
        {
            Config.Settings.Api().Eis().Name,
        };

        protected IProxyRoutingService ProxyService { get; private set; }

        protected ProxyFunction(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            ProxyService = service;
        }

        protected virtual string GetSystemName(HttpRequest req)
        {
            // This functionality is meant for the generic proxy functions only. It allows the client to specify the target proxy system
            // which could lead to unintended access to other systems. We will transition away from this and use a dedicated proxy function
            // for each system. Until we have transitioned all clients, this functionality needs to still exist.

            StringValues result;

            if (req.Query.TryGetValue(SystemParameter, out result) || (req.HasFormContentType && req.Form.TryGetValue(SystemParameter, out result)))
            {
                if (ProhibitedSystems.Any(s => s.Equals(result, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new ApiException($"The requested system is not available through this proxy function", ApiErrorCode.ResourceNotFound_0x7106, ApiResponseCodes.ResourceNotFound);
                }

                return result;
            }
            else
            {
                throw new ApiException($"Unable to determine target system, a {SystemParameter} parameter was not found in the request.", ApiErrorCode.ResourceNotFound_0x7106, ApiResponseCodes.ResourceNotFound);
            }
        }
    }
}
