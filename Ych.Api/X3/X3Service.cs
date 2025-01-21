using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Data.Lims.Models;
using Ych.Api.Data.X3;
using Ych.Api.Data.X3.Views;
using Ych.Api.Data.Ycrm.Models;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Logging;

namespace Ych.Api.X3
{
    /// <summary>
    /// Contract for a DI compatible component/service. DI requires an interface and one or more implementations.
    /// For single implementation contracts, the interface and class can remain in the same file for simplicity.
    /// For interfaces with multiple implementations, the interface and all implementations should exist in separate files.
    /// </summary>
    public interface IX3Service : IApiSystemService, ICustomerContactsService, IHealthyService
    {
        Task<BaleInventory[]> GetAvailableBaleInventory(string lotNumber);

        Task<IEnumerable> GetAvailableLotInventory(string lotNumber);

        Task<IEnumerable> GetAvailableLotInventoryMulti(IEnumerable<string> lotNumberList);

        /// <summary>
        /// Implementation of GetGrowerAnalyticsByVariety containing data access using raw SQL to a stored procedure.
        /// </summary>
        Task<IEnumerable> GetGrowerAnalyticsByVariety(string growerId, int year);

        /// <summary>
        /// Implementation of GetGrowerAnalyticsByLot containing data access using raw SQL to a stored procedure.
        /// </summary>
        Task<IEnumerable> GetGrowerAnalyticsByLot(string growerId, int year);

        /// <summary>
        /// Implementation of GetCustomerInformation containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetCustomerInformation(string customerCode);

        /// <summary>
        /// Implementation of CustomerExists containing data access using a stored procedure.
        /// </summary>
        Task<bool> GetCustomerExists(string customerCode);

        /// <summary>
        /// Implementation of GetGrowerVarietyLotCount containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetGrowerVarietyLotCount(string growerId, int year);

        /// <summary>
        /// Implementation of GetCustomerSelectableVarieties containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetCustomerSelectableVarieties(string customerCode);

        /// <summary>
        /// Implementation of GetLotBaleWeight containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetLotBaleWeight(string lotNumber);

        /// <summary>
        /// Implementation of GetGrowerPurchaseOrdersByVariety containing data access using raw SQL to a stored procedure.
        /// </summary>
        Task<POVarietySummary[]> GetGrowerPurchaseOrdersByVariety(string growerId, int year);

        Task<IEnumerable> GetGrowerPurchaseReceiptsByLot(string growerId, int year);

        /// <summary>
        /// Implementation of GetMultiLotIsValidErp containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetMultiLotIsValidErp(string[] lotNumberList);

        /// <summary>
        /// Implementation of GetLotAnalytics containing data access using a stored procedure.
        /// </summary>
        Task<Dictionary<string, object>> GetLotAnalytics(string lotNumber);

        /// <summary>
        /// Implementation of GetLotLookup containing data access using a stored procedure.
        /// </summary>
        Task<List<Dictionary<string, object>>> GetLotLookup(string[] lotNumbers);

        /// <summary>
        /// Implementation of GetGrowerPurchaseReceiptsByVariety containing data access using raw SQL to a stored procedure.
        /// </summary>
        Task<IEnumerable> GetGrowerPurchaseReceiptsByVariety(string growerId, int year);
        Task<IEnumerable> GetContractDetails(string customerCode, string varietyCode, int year);
        Task<List<Dictionary<string, object>>> GetMultiLotAnalytics(string[] varietyCodeList);

        /// <summary>
        /// Implementation of GetGenericReceiptsErp containing data access using raw SQL to a stored procedure.
        /// </summary>
        Task<GrowerAllErpDelivery[]> GetGenericReceiptsErp(int year);

        /// <summary>
        /// Implementation of GetCustomerDeliveryDetails containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetCustomerDeliveryDetails(string deliveryCode);

        /// <summary>
        /// Implementation of GetTransferDeliveryDetails containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetTransferDeliveryDetails(string deliveryCode);

        /// <summary>
        /// Implementation of GetSalesOrderLineUnitConversions containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetSalesOrderLineUnitConversions(string salesOrderCode);

        /// <summary>
        /// Implementation of GetProductUnitConversions containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetProductUnitConversions(string productcode);

        /// <summary>
        /// Implementation of GetWorkOrderDetails containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetWorkOrderDetails(string workOrderNumber);

        /// <summary>
        /// Implementation of GetCustomerTypes containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetCustomerTypes();

        /// <summary>
        /// Implementation of GetPackSizesForContractLines containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetPackSizesForContractLines(string customerCode, string cropYear, string productLineCode, string varietyCode);

        /// <summary>
        /// Implementation of GetLotQuantitiesByVariety containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetLotQuantitiesByVariety(string varietyCode, string cropYear, string lot = null);

        /// <summary>
        /// Implementation of GetPodSelectionContracts containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetPodSelectionContracts(int cropYear, string varietyCode, string productLineCode);

        /// <summary>
        /// Implementation of GetWorkOrderQueue containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> GetWorkOrderQueue();
        
        /// <summary>
        /// Implementation of DeleteQueuedWorkOrder containing data access using a stored procedure.
        /// </summary>
        Task<IEnumerable> DeleteQueuedWorkOrder(string workOrderCode);
    }

    /// <summary>
    /// Implementation of IX3Service contract. Business logic should exist in services and components like these
    /// rather than directly in the Azure functions. These services can then be injected into other services so long as they don't
    /// create circular dependencies.
    /// </summary>
    public class X3Service : ApiDataService, IX3Service
    {
        public const string X3SystemName = "X3";
        public override string SystemName => X3SystemName;

        protected override ApiDataSource Db => db;

        private X3DataSource db;
        private ILogWriter log;

        public X3Service(X3DataSource db, ILogWriter log)
        {
            this.db = db;
            this.log = log;
        }

        /// <summary>
        /// EXAMPLE
        /// </summary>
        public async Task<BaleInventory[]> GetAvailableBaleInventory(string lotNumber)
        {
            return await db.BaleInventories.FromSqlRaw("exec API_LIVE_AvailableBaleInventory {0}", lotNumber)
                .ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetAvailableLotInventory(string lotNumber)
        {
            return await db.SprocQueryToList("API_LIVE_AvailableBaleInventory", new QueryParameter[] { new QueryParameter { Name = "lot", Value = lotNumber } }).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetAvailableLotInventoryMulti(IEnumerable<string> lotNumberList)
        {
            string lotString = String.Join(",", lotNumberList);
            return await db.SprocQueryToList("API_LIVE_AvailableBaleInventoryMultiLot", new QueryParameter[] { new QueryParameter { Name = "lots", Value = lotString } }).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerAnalyticsByVariety(string growerId, int year)
        {
            string sql = "";
            string grower = "";
            QueryParameter[] parameters;


            // TEMPORARY until stored procedure is updated
            switch (growerId)
            {
                case "SEL001":
                    sql = "API_LIVE_BaleQC_SBG";
                    grower = "Yakima Chief Ranches";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
                case "VIR001":
                    sql = "API_LIVE_BaleQC_VGF";
                    grower = "Virgil Gamache Farms, Inc";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
                default:
                    sql = "API_LIVE_BaleQC";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "growerID", Value = growerId},
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
            }

            ArrayList results = new ArrayList();

            await db.ExecuteSprocQuery(sql, parameters, (reader) =>
            {
                while (reader.Read())
                {
                    results.Add(new VarietyAnalytic
                    {
                        Grower = grower != "" ? grower : reader["grower"].ToString(),
                        Variety = reader["variety"].ToString(),
                        VarietyId = reader["variety_id"].ToString(),
                        AvgUvAlpha = reader["avg_uv_alpha"].ToString(),
                        AvgUvBeta = reader["avg_uv_beta"].ToString(),
                        AvgHsi = reader["avg_hsi"].ToString(),
                        AvgOil = reader["avg_oil"].ToString(),
                        AvgTemp = reader["avg_temp"].ToString(),
                        AvgMois = reader["avg_mois"].ToString(),
                        LotCount = reader["lot_count"].ToString(),
                        YchAvgUvAlpha = reader["ych_avg_uv_alpha"].ToString(),
                        YchAvgUvBeta = reader["ych_avg_uv_beta"].ToString(),
                        YchAvgHsi = reader["ych_avg_hsi"].ToString(),
                        YchAvgOil = reader["ych_avg_oil"].ToString(),
                        YchAvgTemp = reader["ych_avg_temp"].ToString(),
                        YchAvgMois = reader["ych_avg_mois"].ToString()
                    });
                }
            }).ConfigureAwait(false);

            // In the case of no results throw resource not found exception
            if (results.Count == 0)
            {
                throw new ApiResourceNotFoundException($"No grower analytics found for grower {growerId}.");
            }

            return results;
        }

        public async Task<IEnumerable> GetGrowerAnalyticsByLot(string growerId, int year)
        {
            string sql;
            QueryParameter[] parameters;

            // TEMPORARY until stored procedure is updated
            switch (growerId)
            {
                case "SEL001":
                    sql = "API_LIVE_BaleQC_SBG";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
                case "VIR001":
                    sql = "API_LIVE_BaleQC_VGF";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
                default:
                    sql = "API_LIVE_BaleQC";
                    parameters = new QueryParameter[]
                    {
                        new QueryParameter {Name = "growerID", Value = growerId},
                        new QueryParameter {Name = "cropYear", Value = year}
                    };
                    break;
            }

            IEnumerable results = null;

            await db.ExecuteSprocQuery(sql, parameters, (reader) =>
            {
                // Grabs next table of results
                reader.NextResult();

                results = reader.ToList();

            }).ConfigureAwait(false);

            // In the case of no results throw resource not found exception
            if (!results.Any())
            {
                throw new ApiResourceNotFoundException($"No grower analytics found for grower {growerId}.");
            }

            return results;
        }

        public async Task<IEnumerable> GetCustomerContacts(string customerCode)
        {
            try
            {
                return SetSourceSystem(await db.SprocQueryToList("API_LIVE_CrmCustomerContacts", new QueryParameter("bpnum", customerCode)).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                log.Error(GetType().Name, ex, additionalProps: new (string, object)[]
                {
                    (nameof(customerCode), customerCode)
                });

                return Array.Empty<object>();
            }
        }

        public async Task<List<Dictionary<string, object>>> GetMultiLotAnalytics(string[] lotList)
        {
            string lots = ($"{String.Join(",", lotList)}");

            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "lot", Value = lots } };
            return await db.SprocQueryToList("API_LIVE_LotDetails_Multi", parameters).ConfigureAwait(false);
        }

        private async Task<List<Dictionary<string, object>>> GetCustomerContractDetailsByYear(string customerCode,
            int year)
        {
            QueryParameter[] parameters = {
                new() { Name = "customer_id", Value = customerCode },
                new() { Name = "crop_year", Value = year }
            };
            return await db.SprocQueryToList("API_CustomerContractDetails", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetCustomerInformation(string customerCode)
        {
            Dictionary<string, object> customer = null;

            await db.ExecuteSprocQuery("API_LIVE_CrmCustomerInformation",
                new QueryParameter[] { new QueryParameter { Name = "bpnum", Value = customerCode } },  (reader) =>
                {
                    customer = reader.ToList().FirstOrDefault();

                    if (customer != null)
                    {
                        // Load contracts result set
                        reader.NextResult();
                        // Add result set to customer.contracts
                        customer["contracts"] = reader.ToList();

                        // Load recent orders result set
                        reader.NextResult();
                        // Add result set to customer.recent_orders
                        customer["recent_orders"] = reader.ToList();

                        // Load product and varieties result set
                        reader.NextResult();
                        // Add result set to customer.varieties_and_products
                        customer["varieties_and_products"] = reader.ToList().Select(s => s.Values.First().ToString());
                    }
                    else
                    {
                        throw new ApiResourceNotFoundException("No customer information found.");
                    }
                }).ConfigureAwait(false);

            
            foreach (var contract in (List<Dictionary<string, object>>)customer["contracts"])
            {
                int.TryParse(contract["year"].ToString(), out int year);
                var details =  await this.GetCustomerContractDetailsByYear(customerCode, year).ConfigureAwait(false);
                contract["orderData"] = details;
            }
            
            return new Object[] { customer };
        }

        public async Task<bool> GetCustomerExists(string customerCode)
        {
            Dictionary<string, object> customer = null;

            await db.ExecuteSprocQuery("API_LIVE_CrmCustomerInformation",
                new QueryParameter[] { new QueryParameter { Name = "bpnum", Value = customerCode } }, (reader) =>
                {
                    customer = reader.ToList().FirstOrDefault();
                }).ConfigureAwait(false);

            return customer != null;
        }


        public async Task<IEnumerable> GetGrowerVarietyLotCount(string growerId, int year)
        {
            string sql;
            QueryParameter[] parameters;

            // TEMPORARY until stored procedure is updated
            switch (growerId)
            {
                case "SEL001":
                    sql = "API_LIVE_BaleQC_SBG_Summary";
                    parameters = new QueryParameter[] { new QueryParameter { Name = "cropYear", Value = year } };
                    break;
                case "VIR001":
                    sql = "API_LIVE_BaleQC_VGF_Summary";
                    parameters = new QueryParameter[] { new QueryParameter { Name = "cropYear", Value = year } };
                    break;
                default:
                    sql = "API_LIVE_BaleQC_Summary";
                    parameters = new QueryParameter[] { new QueryParameter { Name = "growerID", Value = growerId }, new QueryParameter { Name = "cropYear", Value = year } };
                    break;
            }

            IEnumerable results = null;

            await db.ExecuteSprocQuery(sql, parameters, (reader) =>
            {
                results = reader.ToList();

            }).ConfigureAwait(false);

            return results;
        }

        public async Task<IEnumerable> GetCustomerSelectableVarieties(string customerCode)
        {
            List<Dictionary<string, object>> results = await db.SprocQueryToList("API_LIVE_SelectionVarieties",
                    new QueryParameter[] { new QueryParameter { Name = "customerID", Value = customerCode } })
                .ConfigureAwait(false);

            // Flattens results into an array of variety codes
            return results.Select(s => s.Values.First().ToString());
        }

        public async Task<IEnumerable> GetLotBaleWeight(string lotNumber)
        {
            List<Dictionary<string, object>> results = await db.SprocQueryToList("API_LIVE_LotBaleWeight",
                new QueryParameter[] { new QueryParameter { Name = "lotId", Value = lotNumber } }).ConfigureAwait(false);

            var test = results[0].Values.First();

            if (!results.Any() || results.First().Values.First() == DBNull.Value)
            {
                throw new ApiResourceNotFoundException($"No average bale weight found for lot {lotNumber}.");
            }

            return results;
        }

        public async Task<Dictionary<string, object>> GetLotAnalytics(string lotNumber)
        {
            List<Dictionary<string, object>> results = await db.SprocQueryToList("API_LIVE_LotDetails",
                new QueryParameter[] { new QueryParameter { Name = "lot", Value = lotNumber } }).ConfigureAwait(false);
            
            if (!results.Any())
            {
                throw new ApiResourceNotFoundException($"No Lot information found for lot {lotNumber}.");
            }

            Dictionary<string, object> lotAnalytics = results.FirstOrDefault();

            var notes = new List<string>();
            if (lotAnalytics["mrl_code"] != null)
            {
                string mrl = lotAnalytics["mrl_code"].ToString();

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

            lotAnalytics["notes"] = notes;


            return lotAnalytics;
        }

        public async Task<List<Dictionary<string, object>>> GetLotLookup(string[] lotNumbers)
        {

            string lots = ($"{String.Join(",", lotNumbers)}");

            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "lots", Value = lots } };
            List<Dictionary<string, object>> results = await db.SprocQueryToList("API_LotLookup", parameters).ConfigureAwait(false);

            if (!results.Any())
            {
                throw new ApiResourceNotFoundException($"No Lot information found for lots {lots}.");
            }

            return results;
        }

        public async Task<IEnumerable> GetGrowerPurchaseReceiptsByVariety(string growerId, int year)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "growerID", Value = growerId }, new QueryParameter { Name = "cropYear", Value = year } };
            return await db.SprocQueryToList("API_LIVE_POSummary_Receipts", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerPurchaseReceiptsByLot(string growerId, int year)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "growerid", Value = growerId }, new QueryParameter { Name = "crpyr", Value = year } };
            return await db.SprocQueryToList("API_LIVE_DeliveryByLot", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetMultiLotIsValidErp(string[] lotNumberList)
        {
            string stringifiedLots = string.Join(",", lotNumberList);
            return await db.SprocQueryToList("API_LIVE_LotValidation_Multi", new QueryParameter[] { new QueryParameter { Name = "lots", Value = stringifiedLots } }).ConfigureAwait(false);
        }

        public async Task<POVarietySummary[]> GetGrowerPurchaseOrdersByVariety(string growerId, int year)
        {
            POVarietySummary[] results = await db.POVarietySummaries.FromSqlRaw("exec API_LIVE_POSummary {0}, {1}", growerId, year).ToArrayAsync().ConfigureAwait(false);

            return results;
        }

        public async Task<IEnumerable> GetContractDetails(string customerCode, string varietyCode, int year)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@customer_id", Value = customerCode }, new QueryParameter { Name = "@crop_year", Value = year }, new QueryParameter { Name = "@variety_id", Value = varietyCode } };
            return await db.SprocQueryToList("API_LIVE_SelectionContractDetails", parameters).ConfigureAwait(false);
        }

        public async Task<GrowerAllErpDelivery[]> GetGenericReceiptsErp(int year)
        {
            return await db.GrowerAllErpDeliveries.FromSqlRaw("exec API_LIVE_DeliveryByLot_AllGrowers {0}", year).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetCustomerDeliveryDetails(string deliveryCode)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@sdhnum", Value = deliveryCode } };
            return await db.SprocQueryToList("Eis-GetDeliveryDetails", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetTransferDeliveryDetails(string deliveryCode)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@sdhnum", Value = deliveryCode } };
            return await db.SprocQueryToList("Eis-GetTransferDetails", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetWorkOrderDetails(string workOrderNumber)
        {
            Dictionary<string, object> workOrder = null;

            await db.ExecuteSprocQuery("API_LIVE_WorkOrderDetails",
                new QueryParameter[] { new QueryParameter { Name = "mfgnum", Value = workOrderNumber } }, (reader) =>
                {
                    workOrder = reader.ToList().FirstOrDefault();

                    if (workOrder != null)
                    {
                        reader.NextResult();
                        workOrder["work_order_details"] = reader.ToList();
                    }
                    else
                    {
                        throw new ApiResourceNotFoundException("No work order information found.");
                    }
                }).ConfigureAwait(false);

            return new Object[] { workOrder };
        }

        public async Task<IEnumerable> GetSalesOrderLineUnitConversions(string salesOrderCode)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@sohnum", Value = salesOrderCode } };
            return await db.SprocQueryToList("Eis-GetSalesOrderLineUnitConversions", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetProductUnitConversions(string productCode)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@itmref", Value = productCode } };
            return await db.SprocQueryToList("Eis-GetProductUnitConversions", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetCustomerTypes()
        {
            List<Dictionary<string, object>> results = await db.SprocQueryToList("API_LIVE_CustomerTypes", null).ConfigureAwait(false);
            return results.Select(s => s.Values.First().ToString());
        }

        public async Task<IEnumerable> GetPackSizesForContractLines(string customerCode, string cropYear, string productLineCode, string varietyCode)
        {
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@cropYear", Value = cropYear }, new QueryParameter { Name = "@customerCode", Value = customerCode }, new QueryParameter { Name = "@productLineCode", Value = productLineCode }, new QueryParameter { Name = "@varietyCode", Value = varietyCode } };
            List<Dictionary<string, object>> results = await db.SprocQueryToList("[API_LIVE_GetPackSizesForContractLines]", parameters).ConfigureAwait(false);

            return results.Select(s => s.Values.FirstOrDefault().ToString());
        }

        public async Task<IEnumerable> GetLotQuantitiesByVariety(string varietyCode, string cropYear, string lot = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
             $"GetLotQuantitiesByVariety: {varietyCode} {cropYear} {lot}"));

            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@cropYear", Value = cropYear }, new QueryParameter { Name = "@varietyCode", Value = varietyCode }, 
                                                                new QueryParameter { Name = "@lot", Value = lot } };

            List<LotSelectorData> results = new List<LotSelectorData>();
            Dictionary<string, object> brewingValues = new Dictionary<string, object>();
            Dictionary<string, object> calculatedValues = new Dictionary<string, object>();
            Dictionary<string, object> harvestSheetValues = new Dictionary<string, object>();
            List<Dictionary<string, object>> productTypes = new List<Dictionary<string, object>>();

            string lotNumber = null;
            string previousLotNumber = null;
            string growerName = null;

            await db.ExecuteSprocQuery("[API_LotSelector]", parameters, (reader) =>
            {
                //loop through lots, assuming sproc results are sorted by lot number
                while (reader.Read())
                {
                    lotNumber = reader["lot_number"].ToString();

                    if (previousLotNumber != lotNumber)
                    {
                        //if this lot is not the first, add the previous lot to results and reset values
                        if (previousLotNumber != null)
                        {
                            results.Add(new LotSelectorData
                            {
                                LotNumber = previousLotNumber,
                                GrowerName = growerName,
                                ProductTypes = productTypes,
                                BrewingValues = brewingValues,
                                CalculatedValues = calculatedValues,
                                HarvestSheetValues = harvestSheetValues
                            }); ;

                            productTypes = new List<Dictionary<string, object>>();
                            brewingValues = new Dictionary<string, object>();
                            calculatedValues = new Dictionary<string, object>();
                            harvestSheetValues = new Dictionary<string, object>();
                        }

                        //collect brewing values for current lot
                        brewingValues.Add("alpha", reader["alpha"]);
                        brewingValues.Add("beta", reader["beta"]);
                        brewingValues.Add("total_oil_percentage", reader["total_oil_percentage"]);
                        brewingValues.Add("hsi", reader["hsi"]);
                        brewingValues.Add("a_pinene", reader["a_pinene"]);
                        brewingValues.Add("b_pinene", reader["b_pinene"]);
                        brewingValues.Add("myrcene", reader["myrcene"]);
                        brewingValues.Add("two_methyl_butyl_isobutyrate", reader["two_methyl_butyl_isobutyrate"]);
                        brewingValues.Add("limonene", reader["limonene"]);
                        brewingValues.Add("methyl_heptanoate", reader["methyl_heptanoate"]);
                        brewingValues.Add("methyl_octonoate", reader["methyl_octonoate"]);
                        brewingValues.Add("linalool", reader["linalool"]);
                        brewingValues.Add("caryophyllene", reader["caryophyllene"]);
                        brewingValues.Add("farnesene", reader["farnesene"]);
                        brewingValues.Add("humulene", reader["humulene"]);
                        brewingValues.Add("geraniol", reader["geraniol"]);
                        brewingValues.Add("caryophyllene_oxide", reader["caryophyllene_oxide"]);
                        brewingValues.Add("moisture", reader["moisture"]);

                        calculatedValues.Add("qi", reader["qi"]);
                        calculatedValues.Add("mrl", reader["mrl"]);

                        harvestSheetValues.Add("harvest_start_at", reader["harvest_start_at"]);
                        harvestSheetValues.Add("harvest_end_at", reader["harvest_end_at"]);
                        harvestSheetValues.Add("drying_temp_f", reader["drying_temp_f"]);
                        harvestSheetValues.Add("shatter", reader["shatter"]);
                        harvestSheetValues.Add("greenness", reader["greenness"]);
                        harvestSheetValues.Add("seed", reader["seed"]);
                        harvestSheetValues.Add("stem", reader["stem"]);
                        harvestSheetValues.Add("average_bale_weight", reader["average_bale_weight"]);
                    }

                    Dictionary<string, object> productType = new Dictionary<string, object>();

                    productType.Add("code", reader["product_line_code"]);
                    productType.Add("name", reader["product_line_name"]);
                    productType.Add("pack_size", reader["pack_size"]);
                    productType.Add("status", reader["status"]);
                    productType.Add("qty", reader["qty"]);
                    productType.Add("finished_good_lot", reader["finished_good_lot"]);

                    productTypes.Add(productType);
                    growerName = reader["grower_name"].ToString();
                    previousLotNumber = lotNumber;
                }
            }).ConfigureAwait(false);

            //add last lot in sproc to results
            if (previousLotNumber != null)
            {
                results.Add(new LotSelectorData
                {
                    LotNumber = previousLotNumber,
                    GrowerName = growerName,
                    ProductTypes = productTypes,
                    BrewingValues = brewingValues,
                    CalculatedValues = calculatedValues,
                    HarvestSheetValues = harvestSheetValues
                });
            }

            // In the case of no results throw resource not found exception
            if (results.Count == 0)
            {
                throw new ApiResourceNotFoundException($"No lot information found for variety code {varietyCode}, crop year {cropYear} and lot {lot}. Please ensure that the variety and crop year selected match the provided lot");
            }

            return results;
        }   

        public async Task<IEnumerable> GetPodSelectionContracts(int cropYear, string varietyCode, string productLineCode)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetPodSelectionContracts: {varietyCode} {cropYear} {productLineCode}"));

            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@crop_year", Value = cropYear }, new QueryParameter { Name = "@variety_code", Value = varietyCode }, new QueryParameter { Name = "@product_line", Value = productLineCode } };
            return await db.SprocQueryToList("API_LIVE_PodSelectionContracts", parameters).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetWorkOrderQueue()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetWorkOrderQueue"));
            
            return await db.SprocQueryToList("API_LIVE_WorkOrderQ", null).ConfigureAwait(false);
        }
        
        public async Task<IEnumerable> DeleteQueuedWorkOrder(string workOrderCode)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"DeleteQueuedWorkOrder: {workOrderCode}"));
            
            QueryParameter[] parameters = new QueryParameter[] { new QueryParameter { Name = "@workOrderData", Value = workOrderCode }};
            return await db.SprocQueryToList("API_LIVE_WorkOrderQDelete", parameters).ConfigureAwait(false);
        }
    }
}
