using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Logging;
using Ych.Api;
using Ych.Api.X3;
using Ych.Api.Statistics;
using RestSharp;

namespace YchApiFunctions.X3
{
    public class GetLotQuantitiesByVariety : ApiFunction
    {
        private IX3Service X3Service;
        private IValidationService validation;

        public GetLotQuantitiesByVariety(IX3Service x3Service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.X3Service = x3Service;
            this.validation = validation;
        }

        [Function(nameof(GetLotQuantitiesByVariety))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{varietyCode}/lots/{cropYear}")] HttpRequest req, string varietyCode, string cropYear)
        {
            return await ProcessRequest(req, async () =>
            {
                string lot = req.Query["lot"].ToString();

                if (string.IsNullOrEmpty(lot))
                {
                    return SuccessResponse(await X3Service.GetLotQuantitiesByVariety(varietyCode, cropYear));
                }
                else
                {
                    return SuccessResponse(await X3Service.GetLotQuantitiesByVariety(varietyCode, cropYear, lot));
                }
            });
        }
    }
}