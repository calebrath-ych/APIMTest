using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.GrowerPortal;
using Ych.Api.Logging;
using Ych.Logging;

namespace YchApiFunctions.GrowerPortal
{
    public class UpdateGrowerPortalLotAnalytics : ApiFunction
    {
        private IGrowerPortalService growerPortalService;
        private IValidationService validation;

        public UpdateGrowerPortalLotAnalytics(IGrowerPortalService growerPortalService, IValidationService validation,
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.growerPortalService = growerPortalService;
            this.validation = validation;
        }

        private decimal? convertStringToDecimal(string stringValue)
        {
            bool isDecimal = decimal.TryParse(stringValue, out decimal decimalValue);
            return isDecimal ? decimalValue : (decimal?) null;
        }

        [Function(nameof(UpdateGrowerPortalLotAnalytics))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "grower-portal/lots/{lotNumber}/analytics")]
            HttpRequest req, string lotNumber)
        {
            return await ProcessRequest(req, async () =>
            {
                this.validation.ValidateLotNumbers(LotNumberTypes.Harvest, lotNumber);

                decimal? quantityBales = convertStringToDecimal(req.Form["qty_bales"].ToString());
                decimal? tempMin = convertStringToDecimal(req.Form["temp_min"].ToString());
                decimal? tempMax = convertStringToDecimal(req.Form["temp_max"].ToString());
                decimal? moistMin = convertStringToDecimal(req.Form["moist_min"].ToString());
                decimal? moistMax = convertStringToDecimal(req.Form["moist_max"].ToString());
                decimal? uvAlpha = convertStringToDecimal(req.Form["uv_alpha"].ToString());
                decimal? uvBeta = convertStringToDecimal(req.Form["uv_beta"].ToString());
                decimal? hsi = convertStringToDecimal(req.Form["hsi"].ToString());

                List<(string, object, string)> failures = new List<(string, object, string)>();

                if (quantityBales == null)
                {
                    failures.Add(("quantityBales", quantityBales, "Missing required parameter"));
                }
                if (tempMin == null)
                {
                    failures.Add(("tempMin", tempMin, "Missing required parameter"));
                }
                if (tempMax == null)
                {
                    failures.Add(("tempMax", tempMax, "Missing required parameter"));
                }
                if (moistMin == null)
                {
                    failures.Add(("moistMin", moistMin, "Missing required parameter"));
                }
                if (moistMax == null)
                {
                    failures.Add(("moistMax", moistMax, "Missing required parameter"));
                }
                if (uvAlpha == null)
                {
                    failures.Add(("uvAlpha", uvAlpha, "Missing required parameter"));
                }
                if (uvBeta == null)
                {
                    failures.Add(("uvBeta", uvBeta, "Missing required parameter"));
                }
                if (hsi == null)
                {
                    failures.Add(("hsi", hsi, "Missing required parameter"));
                }

                if (failures.Count > 0)
                {
                    throw new ApiValidationException(failures.ToArray());
                }

                decimal? oilByDist = convertStringToDecimal(req.Form["total_oil"].ToString());
                decimal? moistureOven = convertStringToDecimal(req.Form["moisture_oven"].ToString());
                decimal? oilAPinene = convertStringToDecimal(req.Form["oil_a_pinene"].ToString());
                decimal? oilBPinene = convertStringToDecimal(req.Form["oil_b_pinene"].ToString());
                decimal? oilMyrcene = convertStringToDecimal(req.Form["oil_myrcene"].ToString());
                decimal? oil2MethylButyl = convertStringToDecimal(req.Form["oil_2_methyl_butyl"].ToString());
                decimal? oilLimonene = convertStringToDecimal(req.Form["oil_limonene"].ToString());
                decimal? oilMethylHeptonate = convertStringToDecimal(req.Form["oil_methyl_heptanoate"].ToString());
                decimal? oilMethylOctonoate = convertStringToDecimal(req.Form["oil_methyl_octonoate"].ToString());
                decimal? oilLinalool = convertStringToDecimal(req.Form["oil_linalool"].ToString());
                decimal? oilCaryophyllene = convertStringToDecimal(req.Form["oil_caryophyllene"].ToString());
                decimal? oilFarnesene = convertStringToDecimal(req.Form["oil_farnesene"].ToString());
                decimal? oilHumulene = convertStringToDecimal(req.Form["oil_humulene"].ToString());
                decimal? oilGeraniol = convertStringToDecimal(req.Form["oil_geraniol"].ToString());
                decimal? oilCaryoxide = convertStringToDecimal(req.Form["oil_caryophyllene_oxide"].ToString());

                return SuccessResponse(await growerPortalService.UpdateGrowerPortalLotAnalytics(lotNumber,
                    quantityBales, tempMin, tempMax, moistMin, moistMax, uvAlpha, uvBeta, hsi, oilByDist, moistureOven,
                    oilAPinene, oilBPinene, oilMyrcene, oil2MethylButyl, oilLimonene, oilMethylHeptonate,
                    oilMethylOctonoate, oilLinalool, oilCaryophyllene, oilFarnesene, oilHumulene, oilGeraniol,
                    oilCaryoxide));
            });
        }
    }
}