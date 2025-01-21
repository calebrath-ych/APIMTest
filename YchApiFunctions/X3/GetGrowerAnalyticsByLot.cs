using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.X3
{
    public class GetGrowerAnalyticsByLot : ApiFunction
    {
        private IX3Service service;
        private IValidationService validation;

        public GetGrowerAnalyticsByLot(IX3Service service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerAnalyticsByLot))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/analytics/lots/")] HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);

                validation.ValidateGrowerIds(growerId);

                return SuccessResponse(await service.GetGrowerAnalyticsByLot(growerId, year));
            });
        }
    }
}
