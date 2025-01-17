using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Lims
{
    public class GetLotQcData : ApiFunction
    {
        private ILimsService service;
        private IValidationService validation;


        public GetLotQcData(ILimsService service, IValidationService validation, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.service = service;
            this.validation = validation;
        }

        [Function(nameof(GetLotQcData))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/qc")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                // Handle any input validation here using the injected ValidationService
                string[] lotNumbers = req.Query["lots"].ToString().Replace(" ", "").Split(",");
                validation.ValidateLotNumbers(LotNumberTypes.Any, lotNumbers);

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(await service.GetLotQcData(lotNumbers));
            });
        }
    }
}
