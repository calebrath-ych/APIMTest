using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Api.X3;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Pim;
using Ych.Api.Selection;
using Ych.Api.GrowerPortal;
using Ych.Api.Magento;
using Ych.Api.Client.Data.X3;
using System.Linq;
using System;
using System.Collections;
using Ych.Configuration;
using Ych.Api.Sensory;

namespace YchApiFunctions.X3
{
    public class GetLotLookup : ApiFunction
    {
        private IX3Service x3Service;
        private IPimService pimService;
        private IGrowerPortalService gpService;
        private ISelectionService selectionService;
        private ILimsService limsService;
        private ISensoryService sensoryService;
        private IValidationService validation;
        private ISettingsProvider settings;
        private ILogWriter log;

        public GetLotLookup(IX3Service x3Service, IPimService pimService, IGrowerPortalService gpService, ISelectionService selectionService, ILimsService limsService,
            ISensoryService sensoryService, IValidationService validation, ISettingsProvider settings, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            // Inject any additional dependencies here
            this.x3Service = x3Service;
            this.limsService = limsService;
            this.pimService = pimService;
            this.gpService = gpService;
            this.selectionService = selectionService;
            this.limsService = limsService;
            this.sensoryService = sensoryService;
            this.log = log;
            this.settings = settings;
            this.validation = validation;
        }

        [Function(nameof(GetLotLookup))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "lots/lookup")]
            HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                const string PimTask = "pim";
                const string GpTask = "gp";
                const string HarvestSampleTask = "sample";
                const string SelectionTask = "selection";
                const string SensoryTask = "sensory";
                const string LimsTask = "lims";

                string[] lotList = req.Query["lots"].ToString().Replace(" ", "")
                        .Split(",", System.StringSplitOptions.RemoveEmptyEntries);

                validation.ValidateLotNumbers(LotNumberTypes.Any, lotList);

                if (lotList.Length < 1)
                {
                    throw new ApiValidationException("lotList", lotList,
                        "At least one valid lot is required");
                }

                var lots = await x3Service.GetLotLookup(lotList).ConfigureAwait(false);

                foreach (var lot in lots)
                {

                    Dictionary<string, Task<IEnumerable>> tasks = new Dictionary<string, Task<IEnumerable>>();

                    // Get brewing value ranges from PIM
                    tasks.Add(
                        PimTask,
                        LogErrors(async () => { return await pimService.GetBrewingValueRangesByVarietyAndProduct(lot["variety_code"].ToString(), lot["product_code"].ToString()); })
                    );

                    // Get farm data for all lots including component lots from Grower Portal
                    string componentLotString = lot["component_lots"].ToString() + "," + lot["lot_number"];
                    string[] componentLots = componentLotString.Split(",");
                    lot.Remove("component_lots");

                    tasks.Add(
                        GpTask,
                        LogErrors(async () => { return await gpService.GetLotFarmData(componentLots); })
                    );
                    
                    // Get harvest sample data from Selection
                    tasks.Add(
                        HarvestSampleTask,
                        LogErrors(async () =>
                            {
                                return await selectionService.GetLotHarvestSample(lot["lot_number"].ToString());
                            }
                        ));

                    // Get sensory from Selection
                    tasks.Add(
                        SelectionTask,
                        LogErrors(async () => { return await (lot["show_sensory"].ToString() == "1" ? selectionService.GetLotSensory(lot["lot_number"].ToString()) : Task.FromResult((IEnumerable)null)); })
                    );

                    // Get sensory from Sensory
                    tasks.Add(
                        SensoryTask,
                        LogErrors(async () => { return await (lot["show_sensory"].ToString() == "1" ? sensoryService.GetLotSensory(lot["lot_number"].ToString()) : Task.FromResult((IEnumerable)null)); })
                    );

                    lot.Remove("show_sensory");

                    // Get survivable data from LIMS
                    tasks.Add(
                        LimsTask,
                        LogErrors(async () => { return await limsService.GetLotSurvivableData(lot["lot_number"].ToString()) as IEnumerable; })
                    );

                    // Add metadata
                    var notes = new List<string>();

                    if (lot["mrl_code"] != null)
                    {
                        string mrl = lot["mrl_code"].ToString();

                        if (mrl.EndsWith('E'))
                        {
                            notes.Add("Etoxazole was applied");
                        }
                        else
                        {
                            notes.Add("Etoxazole was not applied");
                            if (mrl.EndsWith("B"))
                            {
                                notes.Add("Bifenazate was applied");
                            }
                            else
                            {
                                notes.Add("Bifenazate was not applied");
                            }
                        }
                    }
                    lot["notes"] = notes;
                    lot.Remove("mrl_code");

                    lot["cone_image"] = settings[Config.Settings.Api().Cdn().ConeImageBaseUrl()] + "/" + lot["variety_code"] + ".png";

                    await Task.WhenAll(tasks.Select(t => t.Value)).ConfigureAwait(false);

                    lot["brewing_value_ranges"] = tasks[PimTask].Result;
                    lot["farm_data"] = tasks[GpTask].Result;
                    lot["harvest_sample_data"] = tasks[HarvestSampleTask].Result;
                    lot["sensory"] = tasks[SensoryTask].Result ?? tasks[SelectionTask].Result;
                    lot["survivable_compounds"] = tasks[LimsTask].Result;
                }

                return SuccessResponse(lots);
            });
        }
    }
}