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
    public class GetSamples : ApiFunction
    {
        private IScadaService scadaService;
        private IValidationService validation;

        public GetSamples(IScadaService scadaService, IValidationService validation, ILogWriter log,
            IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.scadaService = scadaService;
            this.validation = validation;
        }

        [Function(nameof(GetSamples))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "samples/scada/{sampleId}/{sampleType?}")]
            HttpRequest req, string sampleId, string? sampleType = null)
        {
            return await ProcessRequest(req, async () =>
            {
                string[] sampleComponents = sampleId.Split(".");

                IEnumerable response;
                if (sampleComponents.Length == 2)
                {
                    sampleId = sampleComponents.First();
                    int.TryParse(sampleComponents.Last(), out int identifier);
                    response = await scadaService.GetSamples(sampleId, identifier, sampleType);
                }
                else
                {
                    response = await scadaService.GetSamples(sampleId, sampleType);
                }

                // Return a SuccessResponse containing the result of your service method here
                return SuccessResponse(response, serializerSettings: new JsonSerializerSettings());
            });
        }
    }
}