using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Newtonsoft.Json;
using Ych.Api;
using Ych.Api.Scada;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Scada
{
    public class GetSampleMeta : ApiFunction
    {
        private IScadaService scadaService;
        private IValidationService validation;

        public GetSampleMeta(IScadaService scadaService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.scadaService = scadaService;
            this.validation = validation;
        }

        [Function(nameof(GetSampleMeta))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sample/meta/{sampleId}/scada")]
            HttpRequest req, string sampleId)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] sampleComponents = sampleId.Split(".");

                IEnumerable response;
                if (sampleComponents.Length == 2)
                {
                    sampleId = sampleComponents.First();
                    int.TryParse(sampleComponents.Last(), out int identifier);
                    response = await scadaService.GetSampleMeta(sampleId, identifier);
                }
                else
                {
                    response = await scadaService.GetSampleMeta(sampleId);
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(response, serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}