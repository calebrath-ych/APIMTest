using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class GetFeatureByCoordinates : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public GetFeatureByCoordinates(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        [Function(nameof(GetFeatureByCoordinates))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "growers/{growerId}/feature")]
            HttpRequest req, string growerId)
        {
            return await ProcessRequest(req, async () =>
            {
                double zoom = this.validation.ValidateDouble(req.Query["zoom"].ToString());
                double latitude = this.validation.ValidateDouble(req.Query["latitude"].ToString());
                double longitude = this.validation.ValidateDouble(req.Query["longitude"].ToString());

                if (latitude < -90 || latitude > 90 )
                {
                    (longitude, latitude) = (latitude, longitude);
                }
                
                int year = this.validation.ValidateYear(req.Query["year"].ToString(), DateTime.UtcNow.ToPst().Year);
                List<string> exclude = new List<string>(req.Query["exclude"].ToString().Split(","));
                this.validation.ValidateGrowerIds(growerId);
                growerId = (growerId.ToUpper() != "ALL001" ? growerId : null);
                

                // Return a SuccessResponse containing the result of your service method her
                return SuccessResponse(
                    await growerPortalService.GetFeatureByCoordinates(growerId, latitude, longitude, zoom, year, exclude));
            });
        }
    }
}