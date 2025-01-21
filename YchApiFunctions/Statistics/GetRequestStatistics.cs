using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;
using System;

namespace YchApiFunctions.Statistics
{
    public class GetRequestStatistics : ApiFunction
    {
        private IApiStatisticsService statistics;

        public GetRequestStatistics(ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.statistics = statistics;
        }

        [Function(nameof(GetRequestStatistics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "request-statistics")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string api = req.Query["api"].ToString();
                string operation = req.Query["operation"].ToString();

                return SuccessResponse(await statistics.GetRequestStatistics(api, operation));
            });
        }
    }
}
