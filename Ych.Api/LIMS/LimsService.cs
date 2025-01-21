using System;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api.Data.Lims;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Data.GrowerPortal;
using Ych.Api.Data.Lims.Models;
using Ych.Configuration;
using Ych.Api.GrowerPortal;
using Ych.Api.Data.Ycrm.Models;
using Ych.Api.Data;

namespace Ych.Api.Lims
{
    /// <summary>
    /// Contract for LIMS Service. 
    /// </summary>
    public interface ILimsService : IHealthyService
    {
        /// <summary>
        /// Implementation of GetDryMatterGetTotalOilResultsForSamples containing data access using raw SQL.
        /// </summary>
        Task<List<Dictionary<string, object>>> GetDryMatterResultsForSample(string[] sampleIds);

        /// <summary>
        /// Implementation of GetVarietyAnalytics containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetVarietyAnalytics(string varietyCode, string region, int year);

        /// <summary>
        /// Implementation of GetMultiLotAnalytics containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetLotQcData(string[] lotNumbers);

        /// <summary>
        /// Implementation of GetLotSurvivableData containing data access using raw SQL.
        /// </summary>
        Task<Dictionary<string, object>> GetLotSurvivableData(string lotNumbers);

        /// <summary>
        /// Implementation of GetPreHarvestByGrower containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetPreHarvestRequests(int year, string growerId = null, int? statusId = null);

        /// <summary>
        /// Implementation of GetPreHarvestByGrower containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetPreHarvestRequestsByRegion(int year, int regionId, int? statusId = null);
    }

    /// <summary>
    /// Implementation of ILimsService contract. 
    /// </summary>
    public class LimsService : ApiDataService, ILimsService
    {
        public const string LimsSystemName = "Lims";
        public override string SystemName => LimsSystemName;

        protected override ApiDataSource Db => db;

        private LimsDataSource db;
        private IGrowerPortalService growerPortalService;
        private ILogWriter log;
        private IValidationService validation;

        private string logSource => GetType().Name;

        public LimsService(LimsDataSource db,
            IValidationService validation,
            IGrowerPortalService growerPortalService,
            ILogWriter log)
        {
            this.db = db;
            this.growerPortalService = growerPortalService;
            this.log = log;
            this.validation = validation;
        }

        public async Task<List<Dictionary<string, object>>> GetDryMatterResultsForSample(string[] sampleCodes)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetDryMatterResultsForSample: {($"'{String.Join("','", sampleCodes)}'")}"));

            // Raw SQL statement
            string sql = $@"select 
                            id,
                            sample_code,
                            flask_identifier,
                            sample_weight,
                            extracted_moisture_volume,
                            notes,
                            analysis_date,
                            completed,
                            created_at,
                            updated_at,
                            round((100 - (extracted_moisture_volume / sample_weight) * 100), 1) as dry_matter_percentage,
                            round((extracted_moisture_volume / sample_weight * 100), 1) as moisture_percentage
                            from `results_dry_matter`
                            where `results_dry_matter`.`sample_code` in ({string.Join(", ", sampleCodes.Select(s => "?"))})
                                and `results_dry_matter`.`deleted_at` is null
                            order by `results_dry_matter`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleCodes).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetPreHarvestRequestsByRegion(int year, int regionId, int? statusId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetPreHarvestRequestsByRegion: Year: {year} regionId:  {regionId} statusId:  {statusId}"));

            List<Dictionary<string, object>> requests =
                (List<Dictionary<string, object>>)await GetPreHarvestRequests(year, null, statusId);

            List<Dictionary<string, object>> stateGrowers =
                (List<Dictionary<string, object>>)await growerPortalService.GetGrowersByRegion(regionId);

            if (stateGrowers.Any() && requests.Any())
            {
                var stateGrowerIds = stateGrowers.Select(grower => grower["grower_id"]);
                return requests.Where(request =>
                    stateGrowerIds.Contains(request["grower_id"]));
            }

            return null;
        }

        public async Task<IEnumerable> GetPreHarvestRequests(int year, string growerId = null, int? statusId = null)
        {
            // Raw SQL statement
            string sql = @"select analysis_requests.id                              as analysis_request_id,
                               analysis_requests.requester,
                               YEAR(meta_pre_harvests.created_at)                   as year, 
                               analysis_request_statuses.id                         as status_id,
                               ifnull(analysis_request_statuses.name, 'Processing') as status,
                               users.name                                           as owner,
                               analysis_requests.notes,
                               ifnull(analysis_requests.created_at, meta_pre_harvests.created_at) as created_at,
                               analysis_requests.deleted_at                         as completed_at,
                               samples.id                                           as sample_id,
                               samples.sample_code,
                               samples.completed,
                               meta_pre_harvests.grower_id,
                               meta_pre_harvests.field_id,
                               meta_pre_harvests.variety_code
                        from meta_pre_harvests
                                 join samples on samples.id = meta_pre_harvests.sample_id and samples.deleted_at is null
                                 left join analysis_requests on samples.id = analysis_requests.sample_id and analysis_requests.deleted_at is null
                                 left join analysis_request_statuses on analysis_requests.status_id = analysis_request_statuses.id
                                 left join users on analysis_requests.owner_id = users.id
                                 where YEAR(meta_pre_harvests.created_at) = ?
                                 ";
            if (growerId != null)
            {
                sql += " and meta_pre_harvests.grower_id = '" + growerId + "' ";
            }

            if (statusId != null)
            {
                sql += " and analysis_request_statuses.id = " + statusId + " ";
            }

            sql += @" order by analysis_requests.created_at desc, meta_pre_harvests.created_at desc;";

            List<Dictionary<string, object>> requests = await db.SqlQueryToList(sql, year).ConfigureAwait(false);
            return await PopulatePreHarvestRequests(requests);
        }

        private async Task<IEnumerable> PopulatePreHarvestRequests(List<Dictionary<string, object>> preHarvestRequests)
        {
            if (preHarvestRequests.Any())
            {
                string[] fieldIds = preHarvestRequests.Select(x => x["field_id"].ToString()).ToArray();

                var growers =
                    (List<Dictionary<string, object>>)await growerPortalService.GetGrowers().ConfigureAwait(false);
                var fields = await growerPortalService.GetFieldNamesFromFieldIds(fieldIds).ConfigureAwait(false);

                string[] sampleCodes = preHarvestRequests.Select(x => x["sample_code"].ToString()).ToArray();

                var resultsUv = await GetUVResultsForSamples(sampleCodes).ConfigureAwait(false);
                var resultsDryMatter = await GetDryMatterResultsForSample(sampleCodes).ConfigureAwait(false);
                var resultsHplc = await GetHPLCResultsForSamples(sampleCodes).ConfigureAwait(false);
                var resultsLcv = await GetLCVResultsForSamples(sampleCodes).ConfigureAwait(false);
                var resultsOvenMoisture = await GetOvenMoistureResultsForSamples(sampleCodes).ConfigureAwait(false);
                var resultsTotalOil = await GetTotalOilResultsForSamples(sampleCodes).ConfigureAwait(false);
                var resultsOilComponents = await GetOilComponentsResultsForSamples(sampleCodes).ConfigureAwait(false);


                foreach (var request in preHarvestRequests)
                {
                    request["field_name"] =
                        fields.Where(x => x["id"].ToString() == request["field_id"].ToString())
                            .Select(x => x["field_name"]).FirstOrDefault();
                    request["field"] =
                        fields.FirstOrDefault(field => field["id"].ToString() == request["field_id"].ToString());

                    request["grower"] =
                        growers.FirstOrDefault(grower =>
                            grower["grower_id"].ToString() == request["grower_id"].ToString());

                    request["results_uv"] =
                        resultsUv.Where(x => x["sample_code"].ToString() == request["sample_code"].ToString()).Take(1);
                    request["results_dry_matter"] = resultsDryMatter.Where(x =>
                        x["sample_code"].ToString() == request["sample_code"].ToString()).Take(1);
                    request["results_hplc"] =
                        resultsHplc.Where(x => x["sample_code"].ToString() == request["sample_code"].ToString())
                            .Take(1);
                    request["results_lcv"] =
                        resultsLcv.Where(x => x["sample_code"].ToString() == request["sample_code"].ToString()).Take(1);
                    request["results_oil_components"] =
                        resultsOilComponents
                            .Where(x => x["sample_code"].ToString() == request["sample_code"].ToString()).Take(1);
                    request["results_oven_moisture"] =
                        resultsOvenMoisture.Where(x => x["sample_code"].ToString() == request["sample_code"].ToString())
                            .Take(1);
                    request["results_total_oil"] =
                        resultsTotalOil.Where(x => x["sample_code"].ToString() == request["sample_code"].ToString())
                            .Take(1);
                }
            }

            return preHarvestRequests;
        }

        public async Task<List<Dictionary<string, object>>> GetTotalOilResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetTotalOilResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select 
                            id,
                            sample_code,
                            sample_weight,
                            oil_volume,
                            round((oil_volume/sample_weight)*100,1) as total_oil_percent,
                            location,
                            notes,
                            analysis_date,
                            completed,
                            created_at,
                            updated_at
                            from `results_total_oil`
                            where `results_total_oil`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_total_oil`.`deleted_at` is null
                            order by `results_total_oil`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }


        public async Task<List<Dictionary<string, object>>> GetOvenMoistureResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetOvenMoistureResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select 
                            id,
                            sample_code,
                            pan_weight_empty,
                            pan_weight_wet,
                            pan_weight_dry,
                            round((pan_weight_wet-pan_weight_dry)/(pan_weight_wet-pan_weight_empty)*100,1) as moisture,
                            round((1 - (pan_weight_wet-pan_weight_dry)/(pan_weight_wet-pan_weight_empty))*100,1) as dry_matter,
                            notes,
                            analysis_date,
                            completed,
                            created_at,
                            updated_at
                            from `results_oven_moisture`
                            where `results_oven_moisture`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_oven_moisture`.`deleted_at` is null
                            order by `results_oven_moisture`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }


        public async Task<List<Dictionary<string, object>>> GetLCVResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetLCVResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select *
                            from `results_lcv`
                            where `results_lcv`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_lcv`.`deleted_at` is null
                            order by `results_lcv`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }


        public async Task<List<Dictionary<string, object>>> GetOilComponentsResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetOilComponentsResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select *
                            from `results_oil_components`
                            where `results_oil_components`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_oil_components`.`deleted_at` is null
                            order by `results_oil_components`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }


        public async Task<List<Dictionary<string, object>>> GetHPLCResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetHPLCResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select *
                            from `results_hplc`
                            where `results_hplc`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_hplc`.`deleted_at` is null
                            order by `results_hplc`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }


        public async Task<List<Dictionary<string, object>>> GetUVResultsForSamples(string[] sampleIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetUVResultsForSamples: {($"'{String.Join("','", sampleIds)}'")}"));

            // Raw SQL statement
            string sql = $@"select *
                            from `results_uv`
                            where `results_uv`.`sample_code` in ({string.Join(", ", sampleIds.Select(s => "?"))})
                                and `results_uv`.`deleted_at` is null
                            order by `results_uv`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, sampleIds).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetVarietyAnalytics(string varietyCode, string region, int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetVarietyAnalytics: {varietyCode},  {region},  {year}"));

            int hopProductType = 1;

            DateTime startDate = new DateTime(year, 1, 1); // beginning of year
            DateTime endDate = new DateTime(year + 1, 1, 1); // end of year

            string sampleCodePattern = $"{startDate.ToString("yy")}-{region}%";

            // Raw SQL statement
            string sql =
                @"select completed_samples.sample_code as lot_number, `results_uv`.`alpha`, `results_uv`.`beta`, `results_uv`.`hsi`, `results_uv`.`analysis_date`, round((results_total_oil.oil_volume / results_total_oil.sample_weight) * 100, 2) as total_oil, `meta_harvests`.`variety_code`
                            from `samples`
                                     inner join `samples` as `completed_samples` on `samples`.`parent_id` = `completed_samples`.`id`
                                     inner join `results_uv` on `completed_samples`.`sample_code` = `results_uv`.`sample_code`
                                     inner join `meta_harvests` on `samples`.`id` = `meta_harvests`.`sample_id`
                                     inner join `results_total_oil` on `completed_samples`.`sample_code` = `results_total_oil`.`sample_code`
                            where `samples`.`deleted_at` is null
                              and `meta_harvests`.`deleted_at` is null
                              and `results_uv`.`deleted_at` is null
                              and `completed_samples`.`deleted_at` is null
                              and `completed_samples`.`product_type_id` = ?
                              and `completed_samples`.`is_cumulative` = true
                              and `results_uv`.`sample_code` like ?
                              and `results_uv`.`analysis_date` > ?
                              and `results_uv`.`analysis_date` < ?
                              and `meta_harvests`.`variety_code` = ?
                            order by `results_uv`.`analysis_date` desc";

            return await db.SqlQueryToList(sql, hopProductType, sampleCodePattern, startDate, endDate, varietyCode)
                .ConfigureAwait(false);
        }


        public async Task<IEnumerable> GetLotQcData(string[] lotNumbers)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetMultiLotAnalytics: {string.Join(", ", lotNumbers)}"));

            // Raw SQL statement
            string sql = $@"select max(completed_samples.created_at) as date_received,
                                  completed_samples.sample_code as lot,
                                  sum(meta_harvests.bale_count) as qty_bales,
                                  round(weightedAverage(json_arrayagg(meta_harvests.bale_count), json_arrayagg(meta_harvests.probe_temp_min), sum(meta_harvests.bale_count)), 2)     as temp_min,
                                  round(weightedAverage(json_arrayagg(meta_harvests.bale_count), json_arrayagg(meta_harvests.probe_temp_max), sum(meta_harvests.bale_count)), 2)     as temp_max,
                                  round(weightedAverage(json_arrayagg(meta_harvests.bale_count), json_arrayagg(meta_harvests.probe_moisture_min), sum(meta_harvests.bale_count)), 2) as moist_min,
                                  round(weightedAverage(json_arrayagg(meta_harvests.bale_count), json_arrayagg(meta_harvests.probe_moisture_max), sum(meta_harvests.bale_count)), 2) as moist_max,
                                  round((avg(results_oven_moisture.pan_weight_wet) - avg(results_oven_moisture.pan_weight_dry)) / (avg(results_oven_moisture.pan_weight_wet) - avg(results_oven_moisture.pan_weight_empty)),2) as moisture_oven,
                                  round((avg(results_total_oil.oil_volume) / avg(results_total_oil.sample_weight)) * 100, 1) as total_oil,
                                  '' as moisture_meter,
                                  avg(results_uv.alpha) as uv_alpha,
                                  avg(results_uv.beta) as uv_beta,
                                  avg(results_uv.hsi) as hsi,
                                  avg(results_oil_components.a_pinene) as oil_a_pinene,
                                  avg(results_oil_components.b_pinene) as oil_b_pinene,
                                  avg(results_oil_components.myrcene) as oil_myrcene,
                                  avg(results_oil_components.two_methyl_butyl_isobutyrate) as oil_2_methyl_butyl,
                                  avg(results_oil_components.limonene) as oil_limonene,
                                  avg(results_oil_components.methyl_heptanoate) as oil_methyl_heptonate,
                                  avg(results_oil_components.methyl_octonoate) as oil_methyl_octonoate,
                                  avg(results_oil_components.linalool) as oil_linalool,
                                  avg(results_oil_components.caryophyllene) as oil_caryophyllene,
                                  avg(results_oil_components.farnesene) as oil_farnesene,
                                  avg(results_oil_components.humulene) as oil_humulene,
                                  avg(results_oil_components.geraniol) as oil_geraniol,
                                  avg(results_oil_components.caryophyllene_oxide) as oil_caryoxide,
                                  max(meta_harvests.variety_code) as variety_code
                           FROM samples
                           left join samples completed_samples on samples.parent_id = completed_samples.id and completed_samples.deleted_at is null
                           left join results_uv on samples.sample_code = results_uv.sample_code  and results_uv.deleted_at is null
                           left join results_oven_moisture on samples.sample_code = results_oven_moisture.sample_code  and results_oven_moisture.deleted_at is null
                           left join results_oil_components on samples.sample_code = results_oil_components.sample_code  and results_oil_components.deleted_at is null
                           left join results_total_oil on samples.sample_code = results_total_oil.sample_code  and results_total_oil.deleted_at is null
                           join meta_harvests on samples.id = meta_harvests.sample_id
                           where samples.deleted_at is null
                             and meta_harvests.deleted_at is null
                             and completed_samples.deleted_at is null
                             and completed_samples.product_type_id = 1
                             and completed_samples.sample_code in ({string.Join(", ", lotNumbers.Select(s => "?"))})
                             and completed_samples.is_cumulative = 1
                           group by completed_samples.sample_code";

            IEnumerable results = await db.SqlQueryToList(sql, lotNumbers).ConfigureAwait(false);

            if (!results.Any())
            {
                throw new ApiResourceNotFoundException($"No Analytics found for lots {string.Join(", ", lotNumbers)}.");
            }

            return results;
        }

        public async Task<Dictionary<string, object>> GetLotSurvivableData(string lotNumber)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetLotSurvivableData: {lotNumber}"));

            // Raw SQL statement
            string sql = $@"select isobutyl_isobutyrate,
                                    methyl_hexanoate,
                                    alpha_pinene,
                                    myrcene,
                                    beta_pinene,
                                    isoamyl_isobutyrate,
                                    two_methylbutyl_isobutyrate,
                                    methyl_heptanoate,
                                    limonene,
                                    two_nonanone,
                                    linalool,
                                    methyl_octanoate,
                                    methyl_nonanoate,
                                    geraniol,
                                    methyl_decanoate,
                                    methyl_geranate,
                                    geranyl_acetate,
                                    trans_caryophyllene,
                                    humulene,
                                    caryophyllene_oxide,
                                    trans_beta_farnesene,
                                    three_mercaptohexanol,
                                    notes,
                                    analysis_date
                            from results_gcms
                            where deleted_at is null
                              and completed is not null
                              and approved is not null
                              and sample_code = ?";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);

            return results.FirstOrDefault();
        }
    }
}