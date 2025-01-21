using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Newtonsoft.Json.Linq;
using Ych.Api.Data;
using Ych.Api.Data.Pim;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Data.Templating;
using Ych.Logging;

namespace Ych.Api.Pim
{
    /// <summary>
    /// Contract for Pim Service. 
    /// </summary>
    public interface IPimService : IHealthyService
    {
        Task<IEnumerable> GetVarietyCodeAndNameMap();
        Task<IEnumerable> GetProductLineCodeAndNameMap();
        Task<IEnumerable> GetHarvestVarietyCodeAndNameMapByCountry(string countryCode);

        /// <summary>
        /// Implementation of GetVarietyAnalytics containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetVarietiesGlobalAnalytics(string[] varietyCodeList, string productLine);

        Task<IEnumerable> GetVarieties();
        Task<IEnumerable> GetSurvivableVarieties(int? productLineId = null);
        Task<IEnumerable> GetSurvivableVarietyTargets(int? productLineId = null);
        Task<IEnumerable> GetSolubleBeerCompoundData(int? productLineId = null);
        Task<IEnumerable> GetSurvivableVarietyValues(string survivableVariety, int? productLineId = null);

        /// <summary>
        /// Implementation of GetVarietyStandardBrewingValuesAndAromas containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetVarietyStandardBrewingValuesAndAromas(string varietyCode, string productLineCode);

        Task<IEnumerable> GetBrewingValueRangesByVarietyAndProduct(string varietyCode, string productLineCode);
    }

    /// <summary>
    /// Implementation of IPimService contract. 
    /// </summary>
    public class PimService : ApiDataService, IPimService
    {
        public const string PimSystemName = "Pim";
        public override string SystemName => PimSystemName;

        protected override ApiDataSource Db => db;

        private ISettingsProvider settings;
        private PimDataSource db;
        private ILogWriter log;
        private readonly int defaultAromaProductId = 11;

        public PimService(ISettingsProvider settings, PimDataSource db, ILogWriter log)
        {
            this.settings = settings;
            this.db = db;
            this.log = log;
        }

        public async Task<IEnumerable> GetVarietyCodeAndNameMap()
        {
            string sql = @"SELECT variety_code, 
                                COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) as name 
                            FROM varieties 
                            LEFT OUTER JOIN brands on varieties.brand_id = brands.id 
                            LEFT OUTER JOIN cultivars on varieties.cultivar_id = cultivars.id
                            ORDER by COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) asc;";

            IEnumerable results = null;

            await db.ExecuteSqlQuery(sql, null, (reader) => { results = reader.ToDictionary("variety_code", "name"); })
                .ConfigureAwait(false);

            return new object[] { results };
        }

        public async Task<IEnumerable> GetHarvestVarietyCodeAndNameMapByCountry(string countryCode)
        {
            object[] parameters = null;
            string sql = @"SELECT variety_code,
                           COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) as name
                    FROM varieties
                             LEFT OUTER JOIN brands on varieties.brand_id = brands.id
                             LEFT OUTER JOIN cultivars on varieties.cultivar_id = cultivars.id
                             LEFT OUTER JOIN country_codes on varieties.country_id = country_codes.id
                    where blend is false ";

            if (!countryCode.IsNullOrEmpty())
            {
                parameters = new object[] { countryCode };
                sql += " and country_codes.country_code = ? ";
            }

            sql += " ORDER by COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) asc;";

            IEnumerable results = null;


            await db.ExecuteSqlQuery(sql, parameters,
                    (reader) => { results = reader.ToDictionary("variety_code", "name"); })
                .ConfigureAwait(false);

            return new object[] { results };
        }

        public async Task<IEnumerable> GetProductLineCodeAndNameMap()
        {
            string sql = @"SELECT product_line_code as code, product_line_name as name FROM product_lines;";

            IEnumerable results = null;

            await db.ExecuteSqlQuery(sql, null, (reader) => { results = reader.ToDictionary("code", "name"); })
                .ConfigureAwait(false);

            return new object[] { results };
        }

        public async Task<IEnumerable> GetVarietiesGlobalAnalytics(string[] varietyCodeList, string productLine)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetVarietiesGlobalAnalytics"));

            string varietyCodes = ($"'{String.Join("','", varietyCodeList)}'");

            // Raw SQL statement
            string sql =
                $@"select varieties.variety_code                                                                     as variety_id,
                           ifnull(varieties.display_name, cultivars.cultivar)                                                 as variety_name,
                           country_codes.country_code                                                                         as country,
                           ''                                                                                                 as type,
                           'Pacific Northwest'                                                                                as region,
                           group_concat(DISTINCT aromas.aroma ORDER BY aromas.aroma ASC SEPARATOR ', ')                       as aroma_short,
                           varieties.marketing_description                                                                    as description,
                           format(max(alpha_ave), 2)                                                                          as alpha_avg,
                           format(max(alpha_low), 2)                                                                          as alpha_low,
                           format(max(alpha_high), 2)                                                                         as alpha_high,
                           format(max(beta_ave), 2)                                                                           AS beta_avg,
                           format(max(beta_low), 2)                                                                           as beta_low,
                           format(max(beta_high), 2)                                                                          as beta_high,
                           format(max(alpha_ave) / max(beta_ave), 2)                                                          as alpha_beta,
                           CONCAT(round(max(co_h_low), 0), '-', round(max(co_h_high), 0), '%')                                AS co_h,
                           format(max(oil_ave), 2)                                                                            AS total_oil_avg,
                           format(max(oil_low), 2)                                                                            AS total_oil_low,
                           format(max(oil_high), 2)                                                                           AS total_oil_high,
                           CONCAT(round(max(b_pinene_low), 1), '-', round(max(b_pinene_high), 1), '% of total oil')           AS b_pinene,
                           CONCAT(round(max(myrcene_low), 0), '-', round(max(myrcene_high), 0), '% of total oil')             AS myrcene,
                           CONCAT(round(max(linalool_low), 1), '-', round(max(linalool_high), 1), '% of total oil')           AS linalool,
                           CONCAT(round(max(caryophyllene_low), 1), '-', round(max(caryophyllene_high), 1), '% of total oil') AS caryophyllene,
                           CONCAT(round(max(farnesene_low), 0), '-', round(max(farnesene_high), 0), '% of total oil')         AS farnesene,
                           CONCAT(round(max(humulene_low), 0), '-', round(max(humulene_high), 0), '% of total oil')           AS humulene,
                           CONCAT(round(max(geraniol_low), 1), '-', round(max(geraniol_high), 1), '% of total oil')           AS geraniol
                    from brewing_values
                             join varieties on brewing_values.variety_id = varieties.id
                             join product_lines on brewing_values.product_line_id = product_lines.id
                             join country_codes on varieties.country_id = country_codes.id
                             join aromas_varieties on aromas_varieties.variety_id = varieties.id
                             join cultivars on varieties.cultivar_id = cultivars.id
                             join aromas on aromas_varieties.aroma_id = aromas.id                            
                         where product_lines.product_line_code = ?
                    group by varieties.id
            having variety_id in ({varietyCodes})";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, productLine).ConfigureAwait(false);

            // Return our mapped results
            return results;
        }

        public async Task<IEnumerable> GetSurvivableVarietyValues(string survivableVariety, int? productLineId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetSurvivableVarietyValues"));

            // Raw SQL statement
            string sql =
                $@"select variety_survivability_values.id,
                       variety_survivability_values.variety_code,
                       ifnull(ifnull(varieties.display_name, brands.brand_name), cultivars.cultivar) as variety_name,
                       variety_survivability_values.alpha,
                       variety_survivability_values.oil,
                       variety_survivability_values.hsi,
                       JSON_OBJECTAGG(
                               ifnull(aromas.aroma, ''),
                               major_aromas_varieties.aroma_intensity
                           )                                                                         as aromas,
                       variety_survivability_values.myrcene_ppm,
                       variety_survivability_values.caryophyllene_ppm,
                       variety_survivability_values.humulene_ppm,
                       variety_survivability_values.geraniol_ppm,
                       variety_survivability_values.linalool_ppm,
                       variety_survivability_values.methyl_geranate_ppm,
                       variety_survivability_values.`2_methylbutyl_isobutyrate_area_counts`,
                       variety_survivability_values.butanoic_acid_3_methyl_butyl_ester_area_counts,
                       variety_survivability_values.`2_methylpropyl_isobutyrate_area_counts`,
                       variety_survivability_values.`3_mercapto_hexanol_area_counts`,
                       variety_survivability_values.p_mentha_8_thiol_3_one_area_counts,
                       variety_survivability_values.s_methylthiosulfonate_area_counts,
                       variety_survivability_values.methyl_heptanethioate_area_counts,
                       variety_survivability_values.methyl_thio_hexanoate_area_counts,
                       variety_survivability_values.s_methyl_pentanethioate_area_counts,
                       variety_survivability_values.`8_methylnonanoic_acid_area_counts`,
                       variety_survivability_values.methyl_6_methyloctanoate_area_counts,
                       variety_survivability_values.prenyl_butyrate_area_counts,
                       variety_survivability_values.methyl_heptanoate_area_counts,
                       variety_survivability_values.methyl_hexanoate_area_counts,
                       variety_survivability_values.methyl_octanoate_area_counts,
                       variety_survivability_values.hexyl_isobutanoate_area_counts,
                       variety_survivability_values.heptyl_isobutyrate_area_counts,
                       variety_survivability_values.methyl_decanoate_area_counts,
                       variety_survivability_values.amyl_acetate_area_counts,
                       variety_survivability_values.methyl_6_methyl_heptanoate_area_counts,
                       variety_survivability_values.nonoaic_methyl_ester_area_counts,
                       variety_survivability_values.`4_decenoic_methyl_ester_area_counts`,
                       varieties.magento_url_key,
                       varieties.marketing_description,
                       varieties.magento_image_path
                from variety_survivability_values
                         join varieties on varieties.variety_code like concat(variety_survivability_values.variety_code, '%')
                         left join brands on varieties.brand_id = brands.id
                         left join cultivars on varieties.cultivar_id = cultivars.id
                         left join major_aromas_varieties on varieties.id = major_aromas_varieties.variety_id and aroma_intensity 
                                                                 and major_aromas_varieties.product_line_id = ?
                         left join aromas on major_aromas_varieties.aroma_id = aromas.id
                    where variety_survivability_values.deleted_at is null
                  and variety_survivability_values.variety_code = ?
                group by variety_survivability_values.id;";

            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, (productLineId ?? defaultAromaProductId), survivableVariety)
                    .ConfigureAwait(false);

            foreach (var result in results)
            {
                result["ecommerce_link"] = result["magento_url_key"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().ECommerceBaseUrl()] + result["magento_url_key"] + ".html"
                    : null;

                result["cone_image"] = result["magento_image_path"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().AssetBaseUrl()] + result["magento_image_path"]
                    : null;

                if (result["aromas"].ToString() != "")
                {
                    JObject aromas = JObject.Parse(result["aromas"].ToString());
                    aromas.Remove("");
                    result["aromas"] = aromas;
                }
            }

            // Return our mapped results
            return results;
        }

        public async Task<IEnumerable> GetSurvivableVarieties(int? productLineId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetSurvivableVarieties"));

            // Raw SQL statement
            string sql = $@"select variety_survivability_values.id,
                                   variety_survivability_values.variety_code,
                                   ifnull(ifnull(varieties.display_name, brands.brand_name), cultivars.cultivar) as variety_name,
                                   variety_survivability_values.alpha,
                                   variety_survivability_values.oil,
                                   variety_survivability_values.hsi,
                                   JSON_OBJECTAGG(
                                           ifnull(aromas.aroma, ''),
                                           major_aromas_varieties.aroma_intensity
                                       )                                                                         as aromas,
                                   varieties.marketing_description,
                                   varieties.magento_url_key,
                                   varieties.magento_image_path
                            from variety_survivability_values
                                     join varieties on varieties.variety_code like concat(variety_survivability_values.variety_code, '%')
                                     left join brands on varieties.brand_id = brands.id
                                     left join cultivars on varieties.cultivar_id = cultivars.id
                                     left join major_aromas_varieties on varieties.id = major_aromas_varieties.variety_id and aroma_intensity
                                                                             and major_aromas_varieties.product_line_id = ?
                                     left join aromas on major_aromas_varieties.aroma_id = aromas.id
                                where variety_survivability_values.deleted_at is null
                                    and varieties.survivable_blend_option = true
                            group by variety_survivability_values.id;";

            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, (productLineId ?? defaultAromaProductId)).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["ecommerce_link"] = result["magento_url_key"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().ECommerceBaseUrl()] + result["magento_url_key"] + ".html"
                    : null;

                result["cone_image"] = result["magento_image_path"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().AssetBaseUrl()] + result["magento_image_path"]
                    : null;

                if (result["aromas"].ToString() != "")
                {
                    JObject aromas = JObject.Parse(result["aromas"].ToString());
                    aromas.Remove("");
                    result["aromas"] = aromas;
                }
            }

            // Return our mapped results
            return results;
        }

        public async Task<IEnumerable> GetSurvivableVarietyTargets(int? productLineId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetSurvivableVarietyTargets"));

            // Raw SQL statement
            string sql = $@"select variety_survivability_values.id,
                                   variety_survivability_values.variety_code,
                                   ifnull(ifnull(varieties.display_name, brands.brand_name), cultivars.cultivar) as variety_name,
                                   variety_survivability_values.alpha,
                                   variety_survivability_values.oil,
                                   variety_survivability_values.hsi,
                                   JSON_OBJECTAGG(
                                           ifnull(aromas.aroma, ''),
                                           major_aromas_varieties.aroma_intensity
                                       )                                                                         as aromas,
                                   varieties.marketing_description,
                                   varieties.magento_url_key,
                                   varieties.magento_image_path
                            from variety_survivability_values
                                     join varieties on varieties.variety_code like concat(variety_survivability_values.variety_code, '%')
                                     left join brands on varieties.brand_id = brands.id
                                     left join cultivars on varieties.cultivar_id = cultivars.id
                                     left join major_aromas_varieties on varieties.id = major_aromas_varieties.variety_id and aroma_intensity
                                                                             and major_aromas_varieties.product_line_id = ?
                                     left join aromas on major_aromas_varieties.aroma_id = aromas.id
                                where variety_survivability_values.deleted_at is null
                                     and varieties.survivable_target = true
                            group by variety_survivability_values.id;";

            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, (productLineId ?? defaultAromaProductId)).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["ecommerce_link"] = result["magento_url_key"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().ECommerceBaseUrl()] + result["magento_url_key"] + ".html"
                    : null;

                result["cone_image"] = result["magento_image_path"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().AssetBaseUrl()] + result["magento_image_path"]
                    : null;

                if (result["aromas"].ToString() != "")
                {
                    JObject aromas = JObject.Parse(result["aromas"].ToString());
                    aromas.Remove("");
                    result["aromas"] = aromas;
                }
            }

            // Return our mapped results
            return results;
        }

        public async Task<IEnumerable> GetSolubleBeerCompoundData(int? productLineId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetSolubleBeerCompoundData"));

            // Raw SQL statement
            string sql =
                $@"select
                    variety_survivability_values.variety_code,
                    ifnull(ifnull(varieties.display_name, brands.brand_name), cultivars.cultivar) as variety_name,
                   JSON_OBJECTAGG(
                           ifnull(aromas.aroma, ''),
                           major_aromas_varieties.aroma_intensity
                       )                                                                         as aromas,
                    variety_survivability_values.geraniol_ppm,
                    variety_survivability_values.linalool_ppm,
                    variety_survivability_values.methyl_geranate_ppm,
                    variety_survivability_values.2_methylbutyl_isobutyrate_area_counts,
                    variety_survivability_values.butanoic_acid_3_methyl_butyl_ester_area_counts,
                    variety_survivability_values.2_methylpropyl_isobutyrate_area_counts,
                    variety_survivability_values.3_mercapto_hexanol_area_counts,
                    variety_survivability_values.p_mentha_8_thiol_3_one_area_counts,
                    varieties.marketing_description,
                    varieties.magento_url_key,
                    varieties.magento_image_path
                from variety_survivability_values
                         join varieties on varieties.variety_code like concat(variety_survivability_values.variety_code, '%')
                         left join brands on varieties.brand_id = brands.id
                         left join cultivars on varieties.cultivar_id = cultivars.id
                         left join major_aromas_varieties on varieties.id = major_aromas_varieties.variety_id and aroma_intensity 
                                                                 and major_aromas_varieties.product_line_id = ?
                         left join aromas on major_aromas_varieties.aroma_id = aromas.id
                    where variety_survivability_values.deleted_at is null
                group by variety_survivability_values.id;";

            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, (productLineId ?? defaultAromaProductId)).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["ecommerce_link"] = result["magento_url_key"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().ECommerceBaseUrl()] + result["magento_url_key"] + ".html"
                    : null;

                result["cone_image"] = result["magento_image_path"].ToString() != ""
                    ? settings[Config.Settings.Api().Magento().AssetBaseUrl()] + result["magento_image_path"]
                    : null;

                if (result["aromas"].ToString() != "")
                {
                    JObject aromas = JObject.Parse(result["aromas"].ToString());
                    aromas.Remove("");
                    result["aromas"] = aromas;
                }
            }

            // Return our mapped results
            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetAllVarieties()
        {
            string sql = @"SELECT varieties.id                                                            as id,
            COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) as display_name,
            (IF(COALESCE(varieties.display_name, brands.brand_name, cultivars.cultivar) =
                cultivars.cultivar, '', cultivars.cultivar))                        as cultivar,
            varieties.variety_code                                                  as variety_code,
            country_codes.country_name                                              as country_name,
            country_codes.country_code                                              as country_code,
            varieties.marketing_description                                         as description,
            varieties.featured                                                      as featured,
            varieties.blend                                                         as blend,
            varieties.experimental                                                  as experimental,
            varieties.organic                                                       as organic,
            varieties.updated_at                                                    as updated_at,
            varieties.created_at                                                    as created_at,
            varieties.deleted_at                                                    as deleted_at
            FROM varieties
            left outer join brands on varieties.brand_id = brands.id
            left outer join country_codes on varieties.country_id = country_codes.id
            join cultivars on varieties.cultivar_id = cultivars.id";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql).ConfigureAwait(false);

            if (!results.Any())
            {
                throw new ApiResourceNotFoundException("No Variety information found.");
            }

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetAromas(params string[] varietyIds)
        {
            string sql = $@"SELECT aromas_varieties.variety_id, group_concat(aromas.aroma) as aromas
                            FROM aromas_varieties
                                     JOIN aromas on aromas_varieties.aroma_id = aromas.id
                            WHERE aromas_varieties.variety_id in ({string.Join(", ", varietyIds.Select(s => "?"))})
                            group by aromas_varieties.variety_id";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, varietyIds).ConfigureAwait(false);

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetVarietyAromaValues(int? productLineId = null,
            params string[] varietyIds)
        {
            string sql = $@"select aromas.aroma, major_aromas_varieties.aroma_intensity
                            from major_aromas_varieties
                            join aromas on major_aromas_varieties.aroma_id = aromas.id
                            where major_aromas_varieties.variety_id in ({string.Join(", ", varietyIds.Select(s => "?"))})
                                and major_aromas_varieties.product_line_id = {productLineId}
                            order by major_aromas_varieties.aroma_intensity desc";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, varietyIds).ConfigureAwait(false);

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetVarietyBeerTypes(params string[] varietyIds)
        {
            string sql = $@"SELECT beer_types_varieties.variety_id, 
                        group_concat(beer_types.beer_type) as beer_types
                    FROM beer_types_varieties 
                        JOIN beer_types on beer_types_varieties.beer_type_id = beer_types.id 
                    WHERE beer_types_varieties.variety_id in ({string.Join(", ", varietyIds.Select(s => "?"))})
                    group by beer_types_varieties.variety_id";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, varietyIds).ConfigureAwait(false);

            return results;
        }

        private async Task<Dictionary<string, object>> GetVarietyBrewingValues(string varietyId)
        {
            string sql = @"SELECT
                        product_lines.product_line_name as name,
                        product_lines.product_line_code as code,
                        brewing_values.alpha_low,brewing_values.alpha_ave,brewing_values.alpha_high,brewing_values.beta_low,
                        brewing_values.beta_ave,brewing_values.beta_high,brewing_values.oil_low,brewing_values.oil_ave,
                        brewing_values.oil_high,brewing_values.co_h_low,brewing_values.co_h_ave,brewing_values.co_h_high,
                        brewing_values.b_pinene_low,brewing_values.b_pinene_ave,brewing_values.b_pinene_high,
                        brewing_values.myrcene_low,brewing_values.myrcene_ave,brewing_values.myrcene_high,
                        brewing_values.linalool_low,brewing_values.linalool_ave,brewing_values.linalool_high,
                        brewing_values.caryophyllene_low,brewing_values.caryophyllene_ave,brewing_values.caryophyllene_high,
                        brewing_values.farnesene_low,brewing_values.farnesene_ave,brewing_values.farnesene_high,
                        brewing_values.humulene_low,brewing_values.humulene_ave,brewing_values.humulene_high,brewing_values.geraniol_low,
                        brewing_values.geraniol_ave,brewing_values.geraniol_high,brewing_values.silinene_low,brewing_values.silinene_ave,
                        brewing_values.silinene_high,brewing_values.other_low,brewing_values.other_ave,other_high
                    FROM brewing_values
                        JOIN product_lines ON brewing_values.product_line_id = product_lines.id
                    WHERE variety_id = ?";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, varietyId).ConfigureAwait(false);

            Dictionary<string, object> transmutedResults = new Dictionary<string, object>();
            foreach (var result in results)
            {
                Dictionary<string, object> transmutedResult = new Dictionary<string, object>();

                foreach (KeyValuePair<string, object> entry in result)
                {
                    if (entry.Key.Equals("name"))
                    {
                        transmutedResult["name"] = entry.Value;
                    }
                    else if (entry.Key.Equals("code"))
                    {
                        transmutedResult["code"] = entry.Value;
                    }
                    else if (entry.Key.Contains('_'))
                    {
                        string[] value = entry.Key.Split('_');
                        string valueType = value.FirstOrDefault();
                        string valueAttribute = value.LastOrDefault();

                        if (!transmutedResult.ContainsKey(valueType))
                        {
                            transmutedResult[valueType] = new Dictionary<string, object>();
                        }

                        if (valueType != null && valueAttribute != null)
                        {
                            ((Dictionary<string, object>)transmutedResult[valueType])[valueAttribute] = entry.Value;
                        }
                    }
                }

                transmutedResults[transmutedResult["code"].ToString()] = transmutedResult;
            }

            return transmutedResults;
        }

        public async Task<IEnumerable> GetVarieties()
        {
            Dictionary<string, object> keyedVarieties = new Dictionary<string, object>();

            List<Dictionary<string, object>> allVarieties = await this.GetAllVarieties().ConfigureAwait(false);

            string[] varietyIds = new string[allVarieties.Count];
            for (int i = 0; i < allVarieties.Count; i++)
            {
                varietyIds[i] = allVarieties[i]["id"].ToString();
            }

            List<Dictionary<string, object>> aromas = await this.GetAromas(varietyIds).ConfigureAwait(false);
            List<Dictionary<string, object>> beerTypes =
                await this.GetVarietyBeerTypes(varietyIds).ConfigureAwait(false);

            foreach (var variety in allVarieties)
            {
                int varietyId = int.Parse(variety["id"].ToString());

                Dictionary<string, object> varietyBeerType = beerTypes
                    .Where(beerType => beerType["variety_id"].ToString().Equals(variety["id"].ToString()))
                    .FirstOrDefault();

                Dictionary<string, object> varietyAroma = aromas
                    .Where(aroma => int.Parse(aroma["variety_id"].ToString()) == varietyId)
                    .FirstOrDefault();


                variety["aromas"] = varietyAroma != null
                    ? varietyAroma["aromas"].ToString().Split(',').Where(aroma => !string.IsNullOrWhiteSpace(aroma))
                        .ToArray()
                    : null;
                variety["beer_types"] = varietyBeerType != null
                    ? varietyBeerType["beer_types"].ToString().Split(',')
                        .Where(beerType => !string.IsNullOrWhiteSpace(beerType)).ToArray()
                    : null;
                variety["brewing_values"] =
                    await this.GetVarietyBrewingValues(variety["id"].ToString()).ConfigureAwait(false);


                keyedVarieties[variety["variety_code"].ToString()] = variety;
            }

            return keyedVarieties;


            // throw new NotImplementedException();
        }

        public async Task<IEnumerable> GetVarietyStandardBrewingValuesAndAromas(string varietyCode,
            string productLineCode)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetVarietyStandardBrewingValuesAndAromas: {varietyCode}, {productLineCode}"));


            // Raw SQL statement
            string sql = @"select id,
                                  display_name,
                                  variety_code
                        from varieties
                        where variety_code = ?
                        limit 1";


            //use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                varieties = await db.SqlQueryToList(sql, varietyCode).ConfigureAwait(false);

            var variety = varieties.FirstOrDefault();

            if (variety != null)
            {
                variety["brewing_values"] =
                    await GetVarietyBrewingValuesByProductLine(variety["id"].ToString(), productLineCode);

                List<Dictionary<string, object>> varietyAroma =
                    await GetAromas(new string[] { variety["id"].ToString() });

                variety["aromas"] = varietyAroma.FirstOrDefault() != null
                    ? varietyAroma.FirstOrDefault()["aromas"].ToString().Split(',')
                        .Where(aroma => !string.IsNullOrWhiteSpace(aroma))
                        .ToArray()
                    : null;

                var productLine = await GetProductLineFromCode(productLineCode);

                int productLineId = productLine["id"].ToString() != ""
                    ? Int32.Parse(productLine["id"].ToString())
                    : defaultAromaProductId;

                var aromaValues = await GetVarietyAromaValues(productLineId, new string[] { variety["id"].ToString() });

                if (aromaValues.IsNullOrEmpty())
                {
                    aromaValues = await GetVarietyAromaValues(defaultAromaProductId, new string[] { variety["id"].ToString() });
                }
                variety["aroma_values"] = aromaValues;
            }

            return variety;
        }

        private async Task<Dictionary<string, object>> GetProductLineFromCode(string productLineCode)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(
                new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetProductLineFromCode: {productLineCode}"));

            // Raw SQL statement
            string sql = @"select id,
                                  product_line_code,
                                  product_line_name,
                                  created_at                                
                        from product_lines
                        where product_line_code = ?
                        limit 1";

            //use SqlQueryToList to execute query and return results
            Dictionary<string, object> productLine =
                (await db.SqlQueryToList(sql, productLineCode).ConfigureAwait(false)).FirstOrDefault();
            return productLine;
        }

        //TODO: Refactor brewing value functions to reduce duplicated code
        private async Task<Dictionary<string, object>> GetVarietyBrewingValuesByProductLine(string varietyId,
            string productLineCode)
        {
            string sql = @"select
                            product_lines.product_line_name as product_line_name,
                            product_lines.product_line_code as product_line_code,
                            brewing_values.alpha_ave,
                            brewing_values.beta_ave,
                            brewing_values.oil_ave,
                            brewing_values.co_h_ave,
                            brewing_values.b_pinene_ave,
                            brewing_values.myrcene_ave,
                            brewing_values.linalool_ave,
                            brewing_values.caryophyllene_ave,
                            brewing_values.farnesene_ave,
                            brewing_values.humulene_ave,
                            brewing_values.geraniol_ave,
                            brewing_values.silinene_ave,
                            brewing_values.other_ave
                          from brewing_values
                          join product_lines ON brewing_values.product_line_id = product_lines.id
                          where variety_id = ? and product_line_code = ?";

            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, varietyId, productLineCode).ConfigureAwait(false);

            return results.FirstOrDefault();
        }

        //TODO: Refactor brewing value functions to reduce duplicated code
        public async Task<IEnumerable> GetBrewingValueRangesByVarietyAndProduct(string varietyCode,
            string productLineCode)
        {
            string sql = @"select brewing_values.alpha_low,
                                  brewing_values.alpha_ave,
                                  brewing_values.alpha_high,
                                  brewing_values.beta_low,
                                  brewing_values.beta_ave,
                                  brewing_values.beta_high,
                                  brewing_values.oil_low,
                                  brewing_values.oil_ave,
                                  brewing_values.oil_high,
                                  brewing_values.co_h_low as cohumulone_low,
                                  brewing_values.co_h_ave as cohumulone_ave,
                                  brewing_values.co_h_high as cohumulone_high,
                                  brewing_values.b_pinene_low as beta_pinene_low,
                                  brewing_values.b_pinene_ave as beta_pinene_ave,
                                  brewing_values.b_pinene_high as beta_pinene_high,
                                  brewing_values.myrcene_low,
                                  brewing_values.myrcene_ave,
                                  brewing_values.myrcene_high,
                                  brewing_values.linalool_low,
                                  brewing_values.linalool_ave,
                                  brewing_values.linalool_high,
                                  brewing_values.caryophyllene_low,
                                  brewing_values.caryophyllene_ave,
                                  brewing_values.caryophyllene_high,
                                  brewing_values.farnesene_low,
                                  brewing_values.farnesene_ave,
                                  brewing_values.farnesene_high,
                                  brewing_values.humulene_low,
                                  brewing_values.humulene_ave,
                                  brewing_values.humulene_high,
                                  brewing_values.geraniol_low,
                                  brewing_values.geraniol_ave,
                                  brewing_values.geraniol_high,
                                  brewing_values.silinene_low,
                                  brewing_values.silinene_ave,
                                  brewing_values.silinene_high,
                                  brewing_values.other_low,
                                  brewing_values.other_ave,
                                  brewing_values.other_high
                           from brewing_values
                           join product_lines on brewing_values.product_line_id = product_lines.id
                           join varieties on brewing_values.variety_id = varieties.id
                           where variety_code = ? AND product_line_code = ?";

            Dictionary<string, object> results =
                (await db.SqlQueryToList(sql, varietyCode, productLineCode).ConfigureAwait(false)).FirstOrDefault();

            Dictionary<string, object> transmutedResults = null;

            if (results != null && results.Any())
            {
                transmutedResults = new Dictionary<string, object>();

                foreach (KeyValuePair<string, object> entry in results)
                {
                    if (entry.Key.Contains("_low") || entry.Key.Contains("_ave") || entry.Key.Contains("_high"))
                    {
                        string key = entry.Key;
                        int index = key.LastIndexOf('_');
                        string valueType = key[..index];
                        string valueAttribute = key[(index + 1)..];

                        if (!transmutedResults.ContainsKey(valueType))
                        {
                            transmutedResults[valueType] = new Dictionary<string, object>();
                        }

                        if (valueType != null && valueAttribute != null)
                        {
                            ((Dictionary<string, object>)transmutedResults[valueType])[valueAttribute] = entry.Value;
                        }
                    }
                }
            }

            return transmutedResults;
        }
    }
}