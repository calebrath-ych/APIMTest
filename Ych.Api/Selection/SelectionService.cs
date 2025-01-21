using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ych.Logging;
using Ych.Api.Data.Selection;
using Ych.Api.Logging;
using Ych.Api.Data;
using System.Linq;
using Ych.Configuration;

namespace Ych.Api.Selection
{
    /// <summary>
    /// Contract for Selection Service. 
    /// </summary>
    public interface ISelectionService : IHealthyService
    {
        /// <summary>
        /// Implementation of GetBrewerFeedback containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetBrewerFeedback(DateTime startDate);

        /// <summary>
        /// Implementation of GetOfficialFeedback containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetOfficialFeedback(DateTime startDate);

        /// <summary>
        /// Implementation of GetLotSensory containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetLotSensory(string lotNumber);

        /// <summary>
        /// Implementation of GetLotHarvestSample containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetLotHarvestSample(string lotNumber);

        /// <summary>
        /// Implementation of GetUnprocessedSampleMetadata containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetUnprocessedSampleMetadata();
    }

    /// <summary>
    /// Implementation of ISelectionService contract. 
    /// </summary>
    public class SelectionService : ApiDataService, ISelectionService
    {
        public const string SelectionSystemName = "Selection";
        public override string SystemName => SelectionSystemName;

        protected override ApiDataSource Db => db;

        private SelectionDataSource db;
        private ILogWriter log;
        private ISettingsProvider settings;
        private string logSource => GetType().Name;

        public SelectionService(SelectionDataSource db, ILogWriter log, ISettingsProvider settings)
        {
            this.db = db;
            this.log = log;
            this.settings = settings;
        }

        public async Task<IEnumerable> GetBrewerFeedback(DateTime startDate)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetBrewerFeedback: starting at {startDate}"));

            // Raw SQL statement
            string sql = @"SELECT users.first_name,
                           users.last_name,
                           users.customer_display,
                           users.customer_id as customer_x3_id,
                           sensory.lot_number,
                           sensory.notes    AS sensory_notes,
                           selections.notes AS selection_notes,
                           sensory.ranking,
                           sensory.rating,
                           selections.selected,
                           sensory.berry,
                           sensory.stone_fruit,
                           sensory.pomme,
                           sensory.melon,
                           sensory.tropical,
                           sensory.citrus,
                           sensory.floral,
                           sensory.herbal,
                           sensory.vegetal,
                           sensory.grassy,
                           sensory.earthy,
                           sensory.woody,
                           sensory.spicy,
                           sensory.sweet_aromatic,
                           sensory.og,
                           sensory.burnt_rubber,
                           sensory.cardboard,
                           sensory.catty,
                           sensory.cheesy,
                           sensory.diesel,
                           sensory.plastic,
                           sensory.smoky,
                           sensory.sulfur,
                           sensory.sweaty,
                           sensory.created_at,
                           sensory.deleted_at,
                           sensory.updated_at
                    FROM sensory
                             LEFT JOIN users ON sensory.user_id = users.id
                             LEFT JOIN selections ON sensory.lot_number = selections.lot_number AND sensory.customer_id = selections.customer_id
                    WHERE (sensory.customer_id IS NOT NULL AND sensory.customer_id <> 'OPE001' AND sensory.customer_id <> 'QUA100')  
                        AND (sensory.updated_at > ? OR sensory.created_at > ?)";

            return await db.SqlQueryToList(sql, startDate, startDate).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetOfficialFeedback(DateTime startDate)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetOfficialFeedback: starting at {startDate}"));

            // Raw SQL statement
            string sql = @"SELECT lot_number,
                                shatter,
                                greenness, 
                                rating,
                                aroma_profile,
                                greenness_shatter_image_path,
                                greenness_shatter_image_captured_at,
                                created_at,
                                updated_at
                            FROM samples
                            WHERE shatter <> 0 
                                AND greenness <> 0 
                                AND aroma_profile IS NOT NULL 
                                AND (updated_at > ? OR created_at > ?)";

            return await db.SqlQueryToList(sql, startDate, startDate).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetUnprocessedSampleMetadata()
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetUnprocessedSampleMetadata"));

            // Raw SQL statement
            string sql = @"select samples.id,
                                   samples.lot_number,
                                   queued_sample_metadata.greenness as queued_greenness,
                                   queued_sample_metadata.shatter as queued_shatter,
                                   concat(""/sample-meta/"", samples.id, ""/greenness-shatter/"", samples.greenness_shatter_image_path) as image_url
                            from samples
                                     left join queued_sample_metadata on queued_sample_metadata.lot_number = samples.lot_number
                            where queued_sample_metadata.greenness is null
                              and samples.greenness_shatter_image_path is not null
                            order by greenness_shatter_image_captured_at desc
                            ";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql).ConfigureAwait(false);
            foreach (Dictionary<string, object> result in results)
            {
                result["image_url"] = settings[Config.Settings.Api().Selection().BaseUrl()] + result["image_url"];
            }

            return results;
        }

        public async Task<IEnumerable> GetLotSensory(string lotNumber)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetLotSensory: for lot {lotNumber}"));

            string minPanelistsForLotLookup = settings.GetValue(Config.Settings.Api().Sensory().MinPanelistsForLotLookup(), "6");

            // Grabs validated sensory only from CATA, Harvest and Production averaging them if it exists in multiple.
            string sql = @"SELECT (SUM(dried_fruit) / SUM(panelists)) * 100.00 AS dried_fruit,
                                  (SUM(berry) / SUM(panelists)) * 100.00 AS berry,
                                  (SUM(citrus) / SUM(panelists)) * 100.00 AS citrus,
                                  (SUM(floral) / SUM(panelists)) * 100.00 AS floral,
                                  (SUM(herbal) / SUM(panelists)) * 100.00 AS herbal,
                                  (SUM(melon) / SUM(panelists)) * 100.00 AS melon,
                                  (SUM(spicy) / SUM(panelists)) * 100.00 AS spicy,
                                  (SUM(stone_fruit) / SUM(panelists)) * 100.00 AS stone_fruit,
                                  (SUM(sweet_aromatic) / SUM(panelists)) * 100.00 AS sweet_aromatic,
                                  (SUM(tropical) / SUM(panelists)) * 100.00 AS tropical,
                                  (SUM(pomme) / SUM(panelists)) * 100.00 AS pomme,
                                  (SUM(vegetal) / SUM(panelists)) * 100.00 AS vegetal,
                                  (SUM(grassy) / SUM(panelists)) * 100.00 AS grassy,
                                  (SUM(earthy) / SUM(panelists)) * 100.00 AS earthy,
                                  (SUM(woody) / SUM(panelists)) * 100.00 AS woody
                           FROM (
                               SELECT SUM(IF(cata_flavors.name = 'Dried Fruit', 1, 0)) AS dried_fruit,
                                      SUM(IF(cata_flavors.name = 'Berry', 1, 0)) AS berry,
                                      SUM(IF(cata_flavors.name = 'Citrus', 1, 0)) AS citrus,
                                      SUM(IF(cata_flavors.name = 'Floral', 1, 0)) AS floral,
                                      SUM(IF(cata_flavors.name = 'Herbal', 1, 0)) AS herbal,
                                      SUM(IF(cata_flavors.name = 'Melon', 1, 0)) AS melon,
                                      SUM(IF(cata_flavors.name = 'Spicy', 1, 0)) AS spicy,
                                      SUM(IF(cata_flavors.name = 'Stone Fruit', 1, 0)) AS stone_fruit,
                                      SUM(IF(cata_flavors.name = 'Sweet Aromatic', 1, 0)) AS sweet_aromatic,
                                      SUM(IF(cata_flavors.name = 'Tropical', 1, 0)) AS tropical,
                                      SUM(IF(cata_flavors.name = 'Pomme', 1, 0)) AS pomme,
                                      SUM(IF(cata_flavors.name = 'Vegetal', 1, 0)) AS vegetal,
                                      SUM(IF(cata_flavors.name = 'Grassy', 1, 0)) AS grassy,
                                      SUM(IF(cata_flavors.name = 'Earthy', 1, 0)) AS earthy,
                                      SUM(IF(cata_flavors.name = 'Woody', 1, 0)) AS woody,
                                      COUNT(DISTINCT sensory_id) AS panelists
                               FROM cata_sensory
                               LEFT JOIN cata_samples on cata_sensory.cata_sample_id = cata_samples.id
                               LEFT JOIN cata_flavors_sensory ON cata_sensory.id = cata_flavors_sensory.sensory_id
                               LEFT JOIN cata_flavors ON cata_flavors_sensory.flavor_id = cata_flavors.id
                               WHERE validated = 1
                                 AND cata_samples.title = ?
                               HAVING COUNT(DISTINCT sensory_id) > 0

                               UNION ALL

                               SELECT SUM(IF(dried_fruit IS NOT NULL AND dried_fruit > 0, 1, 0)) AS dried_fruit,
                                      SUM(IF(berry IS NOT NULL AND berry > 0, 1, 0)) AS berry,
                                      SUM(IF(citrus IS NOT NULL AND citrus > 0, 1, 0)) AS citrus,
                                      SUM(IF(floral IS NOT NULL AND floral > 0, 1, 0)) AS floral,
                                      SUM(IF(herbal IS NOT NULL AND herbal > 0, 1, 0)) AS herbal,
                                      SUM(IF(melon IS NOT NULL AND melon > 0, 1, 0)) AS melon,
                                      SUM(IF(spicy IS NOT NULL AND spicy > 0, 1, 0)) AS spicy,
                                      SUM(IF(stone_fruit IS NOT NULL AND stone_fruit > 0, 1, 0)) AS stone_fruit,
                                      SUM(IF(sweet_aromatic IS NOT NULL AND sweet_aromatic > 0, 1, 0)) AS sweet_aromatic,
                                      SUM(IF(tropical IS NOT NULL AND tropical > 0, 1, 0)) AS tropical,
                                      SUM(IF(pomme IS NOT NULL AND pomme > 0, 1, 0)) AS pomme,
                                      SUM(IF(vegetal IS NOT NULL AND vegetal > 0, 1, 0)) AS vegetal,
                                      SUM(IF(grassy IS NOT NULL AND grassy > 0, 1, 0)) AS grassy,
                                      SUM(IF(earthy IS NOT NULL AND earthy > 0, 1, 0)) AS earthy,
                                      SUM(IF(woody IS NOT NULL AND woody > 0, 1, 0)) AS woody,
                                      COUNT(*) AS panelists
                               FROM sensory
                               LEFT JOIN samples on sensory.sample_id = samples.id
                               WHERE customer_id is null
                                 AND validated = 1
                                 AND sensory.lot_number = ?
                               HAVING COUNT(*) > 0

                               UNION ALL

                               SELECT SUM(IF(dried_fruit IS NOT NULL AND dried_fruit > 0, 1, 0)) AS dried_fruit ,
                                      SUM(IF(berry IS NOT NULL AND berry > 0, 1, 0)) AS berry ,
                                      SUM(IF(citrus IS NOT NULL AND citrus > 0, 1, 0)) AS citrus ,
                                      SUM(IF(floral IS NOT NULL AND floral > 0, 1, 0)) AS floral ,
                                      SUM(IF(herbal IS NOT NULL AND herbal > 0, 1, 0)) AS herbal ,
                                      SUM(IF(melon IS NOT NULL AND melon > 0, 1, 0)) AS melon ,
                                      SUM(IF(spicy IS NOT NULL AND spicy > 0, 1, 0)) AS spicy ,
                                      SUM(IF(stone_fruit IS NOT NULL AND stone_fruit > 0, 1, 0)) AS stone_fruit ,
                                      SUM(IF(sweet_aromatic IS NOT NULL AND sweet_aromatic > 0, 1, 0)) AS sweet_aromatic ,
                                      SUM(IF(tropical IS NOT NULL AND tropical > 0, 1, 0)) AS tropical ,
                                      SUM(IF(pomme IS NOT NULL AND pomme > 0, 1, 0)) AS pomme ,
                                      SUM(IF(vegetal IS NOT NULL AND vegetal > 0, 1, 0)) AS vegetal ,
                                      SUM(IF(grassy IS NOT NULL AND grassy > 0, 1, 0)) AS grassy ,
                                      SUM(IF(earthy IS NOT NULL AND earthy > 0, 1, 0)) AS earthy ,
                                      SUM(IF(woody IS NOT NULL AND woody > 0, 1, 0)) AS woody,
                                      COUNT(*) AS panelists
                               FROM production_sensory
                               LEFT JOIN production_samples on production_sensory.production_sample_id = production_samples.id
                               WHERE production_sensory.validated = 1
                                 AND production_samples.lot_number = ?
                               HAVING COUNT(*) > 0
                           ) AS unioned_sensory
                           HAVING SUM(panelists) >= ?";

            var results = await db.SqlQueryToList(sql, lotNumber, lotNumber, lotNumber, minPanelistsForLotLookup).ConfigureAwait(false);

            return results.FirstOrDefault();
        }

        public async Task<IEnumerable> GetLotHarvestSample(string lotNumber)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetLotHarvestSample: for lot {lotNumber}"));

            string sql = @"SELECT id, lot_number, bin, deleted_at
                            FROM (
                                     SELECT
                                         samples.id AS id,
                                         samples.lot_number AS lot_number,
                                         bins.name AS bin,
                                         samples.created_at,
                                         samples.deleted_at
                                     FROM samples
                                              LEFT JOIN bins ON samples.bin_id = bins.id
                                     WHERE samples.lot_number = ? AND samples.deleted_at IS NULL
                                     ORDER BY samples.created_at DESC
                                     LIMIT 1
                                 ) AS NonDeletedSample

                            UNION ALL

                            SELECT id, lot_number, bin, deleted_at
                            FROM (
                                     SELECT
                                         samples.id AS id,
                                         samples.lot_number AS lot_number,
                                         bins.name AS bin,
                                         samples.created_at,
                                         samples.deleted_at
                                     FROM samples
                                              LEFT JOIN bins ON samples.bin_id = bins.id
                                     WHERE samples.lot_number = ?
                                     ORDER BY samples.created_at DESC
                                     LIMIT 1
                                 ) AS DeletedSample
                            WHERE NOT EXISTS (
                                SELECT 1
                                FROM samples
                                WHERE samples.lot_number = ? AND samples.deleted_at IS NULL
                            );
                             ";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, lotNumber, lotNumber, lotNumber).ConfigureAwait(false);

            return results.FirstOrDefault();
        }
    }
}