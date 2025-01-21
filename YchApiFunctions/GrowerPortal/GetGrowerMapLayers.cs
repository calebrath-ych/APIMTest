using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetGrowerMapLayers : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetGrowerMapLayers(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetGrowerMapLayers))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/map")]
            HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                string coordinates = null;
                double? radius = null;

                // Validate Grower ID
                this.validation.ValidateGrowerIds(growerId);

                // Check if center filter exists
                if (!string.IsNullOrEmpty(req.Query["center"].ToString()))
                {
                    string[] latLng = req.Query["center"].ToString().Split(" ");

                    var latitude = this.validation.ValidateDouble(latLng.First());
                    var longitude = this.validation.ValidateDouble(latLng.Last());

                    if (latitude < -90 || latitude > 90)
                    {
                        (longitude, latitude) = (latitude, longitude);
                    }

                    coordinates = $"{latitude} {longitude}";
                }

                if (!string.IsNullOrEmpty(req.Query["radius"].ToString()))
                {
                    radius = this.validation.ValidateDouble(req.Query["radius"].ToString());
                }

                // If we get a response of ALL001, empty grower id and return all grower data
                growerId = (growerId.ToUpper() != "ALL001" ? growerId : null);

                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(await growerPortalService.GetGrowerMapLayers(growerId, year, coordinates, radius));
            });
        }
    }
}