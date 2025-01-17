using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ych.Logging;
using Ych.Api.Logging;
using Ych.Data.Templating;
using Ych.Configuration;
using Ych.Api.Data.Sensory;
using System.Linq;

namespace Ych.Api.Sensory
{
    /// <summary>
    /// Contract for Selection Service. 
    /// </summary>
    public interface ISensoryService
    {
        /// <summary>
        /// Implementation of GetLotSensory containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetLotSensory(string lotNumber);
    }

    /// <summary>
    /// Implementation of ISensoryService contract. 
    /// </summary>
    public class SensoryService : ISensoryService
    {
        private SensoryDataSource db;
        private ISettingsProvider settings;
        private ILogWriter log;
        private string logSource => GetType().Name;

        public SensoryService(SensoryDataSource db, ISettingsProvider settings, ILogWriter log)
        {
            this.db = db;
            this.settings = settings;
            this.log = log;
        }

        public async Task<IEnumerable> GetLotSensory(string lotNumber)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetLotSensory: for lot {lotNumber}"));

            string organizationId = settings[Config.Settings.Api().Sensory().OrganizationId()];
            string minPanelistsForLotLookup = settings.GetValue(Config.Settings.Api().Sensory().MinPanelistsForLotLookup(), "6");

            string sql = $@"SELECT (SUM(dried_fruit) / SUM(panelists)) * 100.00 AS dried_fruit,
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
                                     SELECT SUM(IF(lexicon.name = 'Dried Fruit', cata_sensory_lexicon.detected, 0)) AS dried_fruit,
                                            SUM(IF(lexicon.name = 'Berry', cata_sensory_lexicon.detected, 0)) AS berry,
                                            SUM(IF(lexicon.name = 'Citrus', cata_sensory_lexicon.detected, 0)) AS citrus,
                                            SUM(IF(lexicon.name = 'Floral', cata_sensory_lexicon.detected, 0)) AS floral,
                                            SUM(IF(lexicon.name = 'Herbal', cata_sensory_lexicon.detected, 0)) AS herbal,
                                            SUM(IF(lexicon.name = 'Melon', cata_sensory_lexicon.detected, 0)) AS melon,
                                            SUM(IF(lexicon.name = 'Spicy', cata_sensory_lexicon.detected, 0)) AS spicy,
                                            SUM(IF(lexicon.name = 'Stone Fruit', cata_sensory_lexicon.detected, 0)) AS stone_fruit,
                                            SUM(IF(lexicon.name = 'Sweet Aromatic', cata_sensory_lexicon.detected, 0)) AS sweet_aromatic,
                                            SUM(IF(lexicon.name = 'Tropical', cata_sensory_lexicon.detected, 0)) AS tropical,
                                            SUM(IF(lexicon.name = 'Pomme', cata_sensory_lexicon.detected, 0)) AS pomme,
                                            SUM(IF(lexicon.name = 'Vegetal', cata_sensory_lexicon.detected, 0)) AS vegetal,
                                            SUM(IF(lexicon.name = 'Grassy', cata_sensory_lexicon.detected, 0)) AS grassy,
                                            SUM(IF(lexicon.name = 'Earthy', cata_sensory_lexicon.detected, 0)) AS earthy,
                                            SUM(IF(lexicon.name = 'Woody', cata_sensory_lexicon.detected, 0)) AS woody,
                                            COUNT(DISTINCT user_id) AS panelists
                                     FROM cata_sensory
                                              LEFT JOIN cata_sensory_lexicon ON cata_sensory.id = cata_sensory_lexicon.cata_sensory_id
                                              LEFT JOIN lexicon ON cata_sensory_lexicon.lexicon_id = lexicon.id
                                              LEFT JOIN cata_tests ON cata_sensory.cata_test_id = cata_tests.id
                                              LEFT JOIN cata_test_sample ON cata_tests.id = cata_test_sample.cata_test_id
                                              LEFT JOIN hop_samples ON cata_test_sample.sample_id = hop_samples.id AND cata_test_sample.sample_type = 'App\\Models\\Samples\\HopSample'
                                              LEFT JOIN panels ON cata_tests.panel_id = panels.id
                                     WHERE panels.organization_uuid = ?
                                       AND hop_samples.lot_number = ?
                                       AND cata_tests.publish = 1
                                     HAVING COUNT(DISTINCT user_id) > 0

                                     UNION ALL

                                     SELECT SUM(IF(lexicon.name = 'Dried Fruit' AND qda_sensory_lexicon.value > 0, 1, 0)) AS dried_fruit,
                                            SUM(IF(lexicon.name = 'Berry' AND qda_sensory_lexicon.value > 0, 1, 0)) AS berry,
                                            SUM(IF(lexicon.name = 'Citrus' AND qda_sensory_lexicon.value > 0, 1, 0)) AS citrus,
                                            SUM(IF(lexicon.name = 'Floral' AND qda_sensory_lexicon.value > 0, 1, 0)) AS floral,
                                            SUM(IF(lexicon.name = 'Herbal' AND qda_sensory_lexicon.value > 0, 1, 0)) AS herbal,
                                            SUM(IF(lexicon.name = 'Melon' AND qda_sensory_lexicon.value > 0, 1, 0)) AS melon,
                                            SUM(IF(lexicon.name = 'Spicy' AND qda_sensory_lexicon.value > 0, 1, 0)) AS spicy,
                                            SUM(IF(lexicon.name = 'Stone Fruit' AND qda_sensory_lexicon.value > 0, 1, 0)) AS stone_fruit,
                                            SUM(IF(lexicon.name = 'Sweet Aromatic' AND qda_sensory_lexicon.value > 0, 1, 0)) AS sweet_aromatic,
                                            SUM(IF(lexicon.name = 'Tropical' AND qda_sensory_lexicon.value > 0, 1, 0)) AS tropical,
                                            SUM(IF(lexicon.name = 'Pomme' AND qda_sensory_lexicon.value > 0, 1, 0)) AS pomme,
                                            SUM(IF(lexicon.name = 'Vegetal' AND qda_sensory_lexicon.value > 0, 1, 0)) AS vegetal,
                                            SUM(IF(lexicon.name = 'Grassy' AND qda_sensory_lexicon.value > 0, 1, 0)) AS grassy,
                                            SUM(IF(lexicon.name = 'Earthy' AND qda_sensory_lexicon.value > 0, 1, 0)) AS earthy,
                                            SUM(IF(lexicon.name = 'Woody' AND qda_sensory_lexicon.value > 0, 1, 0)) AS woody,
                                            COUNT(DISTINCT user_id) as panelists
                                     FROM qda_sensory
                                              LEFT JOIN qda_sensory_lexicon ON qda_sensory.id = qda_sensory_lexicon.qda_sensory_id
                                              LEFT JOIN lexicon ON qda_sensory_lexicon.lexicon_id = lexicon.id
                                              LEFT JOIN qda_tests ON qda_sensory.qda_test_id = qda_tests.id
                                              LEFT JOIN qda_test_sample ON qda_tests.id = qda_test_sample.qda_test_id
                                              LEFT JOIN hop_samples ON qda_test_sample.sample_id = hop_samples.id AND qda_test_sample.sample_type = 'App\\Models\\Samples\\HopSample'
                                              LEFT JOIN panels ON qda_tests.panel_id = panels.id
                                     WHERE panels.organization_uuid = ?
                                       AND hop_samples.lot_number = ?
                                       AND qda_tests.publish = 1
                                     HAVING COUNT(DISTINCT user_id) > 0
                                 ) AS CATA_QDA_COMBINED
                                 HAVING SUM(panelists) >= ?";

            var results = await db.SqlQueryToList(sql, organizationId, lotNumber, organizationId, lotNumber, minPanelistsForLotLookup).ConfigureAwait(false);

            return results.FirstOrDefault();
        }
    }
}