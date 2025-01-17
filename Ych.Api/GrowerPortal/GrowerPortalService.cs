using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Newtonsoft.Json.Linq;
using Ych.Api.Data.GrowerPortal;
using Ych.Api.Data.GrowerPortal.Models;
using Ych.Api.Logging;
using Ych.Logging;
using Ych.Api.Pim;
using Ych.Configuration;
using Stubble.Core.Builders;
using Ych.Api.Data.Solochain.Views;
using Ych.Api.Solochain;
using Ych.Api.X3;
using Ych.Api.Ycrm;
using System.Text.RegularExpressions;
using Ych.Api.Data;
using Ych.Api.Data.Lims;

namespace Ych.Api.GrowerPortal
{
    /// <summary>
    /// Contract for GrowerPortal Service. 
    /// </summary>
    public interface IGrowerPortalService : IHealthyService
    {
        /// <summary>
        /// Get a single grower based on grower id.
        /// </summary>
        Task<List<Dictionary<string, object>>> GetGrower(string growerId);

        /// <summary>
        /// Get an list of all growers
        /// </summary>
        Task<IEnumerable> GetGrowers();

        /// <summary>
        /// Get an list of all grower regions
        /// </summary>
        Task<IEnumerable> GetGrowerRegions();

        /// <summary>
        /// Get an list of all growers in a given regions
        /// </summary>
        Task<IEnumerable> GetGrowersByRegion(int regionId);

        /// <summary>
        /// Get an list of all growers
        /// </summary>
        Task<IEnumerable> GetGrowersDetails(int year);

        /// <summary>
        /// Get harvest data for a given lot
        /// </summary>
        Task<IEnumerable> GetLotHarvestData(string lotId);

        /// <summary>
        /// Get harvest data for a given grower and year
        /// </summary>
        Task<IEnumerable> GetGrowerHarvestData(string growerId, int year);

        /// <summary>
        /// Get spray records for a given grower and year
        /// </summary>
        Task<IEnumerable> GetGrowerSprayRecords(string growerId, int year);

        /// <summary>
        /// Get delivery data for a given grower and year
        /// </summary>
        Task<IEnumerable> GetGrowerDeliveryDetails(string growerId, int year);

        /// <summary>
        /// Get fields for a given grower
        /// </summary>
        Task<IEnumerable> GetGrowerFields(string growerId);

        /// <summary>
        /// Get fields for a given grower
        /// </summary>
        Task<IEnumerable> GetFields(string searchTerm, string growerId, string selectedVariety, string selectedState,
            int year);

        /// <summary>
        /// Get notifications for a given user
        /// </summary>
        Task<IEnumerable> GetGrowerPortalUserNotifications(int userId, int page, int limit);

        /// <summary>
        /// Get notification settings for a given user
        /// </summary>
        Task<IEnumerable> GetGrowerPortalUserNotificationSettings(int userId, string growerId);

        /// <summary>
        /// Get daily digest setting for a given user
        /// </summary>
        Task<IEnumerable> GetGrowerPortalDailyDigestSetting(int userId, string growerId);

        /// <summary>
        /// Get map layers for a given grower
        /// </summary>
        Task<IEnumerable> GetGrowerMapLayers(string growerId, int year, string center = null, double? radius = null);

        /// <summary>
        /// Get year/field specific meta data
        /// </summary>
        Task<List<Dictionary<string, object>>> GetGrowerFieldMetaData(int fieldId, int year);

        /// <summary>
        /// Get field specific meta data for all years
        /// </summary>
        Task<List<Dictionary<string, object>>> GetGrowerFieldMetaData(int fieldId);

        /// <summary>
        /// Get year/field specific meta data
        /// </summary>
        Task<IEnumerable> GetGrowerFieldKilnSampleReports(int fieldId);

        /// <summary>
        /// Get yearly kiln sample reports
        /// </summary>
        Task<IEnumerable> GetKilnSampleReports(int year);

        /// <summary>
        /// Get year/region agronomy hub updates
        /// </summary>
        Task<IEnumerable> GetAgronomyHubUpdates(int year, int? regionId);

        /// <summary>
        /// Get brewer feedback
        /// </summary>
        Task<IEnumerable> GetGrowerBrewerFeedback(int year, string growerId = null);

        /// <summary>
        /// Get roguing schedules for a specific user
        /// </summary>
        Task<IEnumerable> GetUserRoguingSchedules(int userId, int year);

        /// <summary>
        /// Get roguing schedules based on parameters
        /// </summary>
        Task<IEnumerable> GetRoguingSchedules(int year, int? teamId, int? userId, DateTime? windowStart,
            DateTime? windowEnd, bool onlyCurrent, bool onlyFinished);

        /// <summary>
        /// Get roguing schedules based on parameters
        /// </summary>
        Task<IEnumerable> GetRoguingSchedule(int scheduleId, int year);

        /// <summary>
        /// Get roguing teams
        /// </summary>
        Task<IEnumerable> GetRoguingTeams();

        /// <summary>
        /// Get sub area data
        /// </summary>
        Task<Dictionary<string, object>> GetGrowerSubArea(int subAreaId);

        /// <summary>
        /// Get mobile configuration for a given grower
        /// </summary>
        Task<IEnumerable> GetGrowerMobileConfig(string growerId);

        /// <summary>
        /// Get feature data by location
        /// </summary>
        Task<IEnumerable> GetFeatureByCoordinates(string growerId, double latitude, double longitude, double zoom,
            int year,
            List<string> exclude);

        /// <summary>
        /// Get feature data by location
        /// </summary>
        Task<Dictionary<string, object>> GetFeaturePropertiesByTypeAndId(string type, int id, int year);

        /// <summary>
        /// Get global mobile configuration 
        /// </summary>
        Task<IEnumerable> GetMobileConfig();

        /// <summary>
        /// Get field shape data for a given grower
        /// </summary>
        Task<Dictionary<string, object>> GetGrowerFieldsGeojson(string growerId, string varietyId = null,
            string geometryType = null, string center = null, double? radius = null);

        /// <summary>
        /// Get field shape data for a given grower
        /// </summary>
        Task<IEnumerable> GetGrowerPointsGeojson(string growerId, int pointTypeId, int year, string center = null,
            double? radius = null);

        /// <summary>
        /// Convert Lot Number to Grower Id
        /// </summary>
        Task<string> LotNumberToGrower(string lotNumber);

        /// <summary>
        /// Get field shape data for a given grower
        /// </summary>
        Task<IEnumerable> GetGrowerPoints(string growerId, int pointTypeId, int year, string center = null,
            double? radius = null);

        /// <summary>
        /// Get fields for a given grower
        /// </summary>
        Task<bool> UpdateGrowerPortalLotAnalytics(string lotNumber, decimal? quantityBales, decimal? tempMin,
            decimal? tempMax, decimal? moistMin, decimal? moistMax, decimal? uvAlpha, decimal? uvBeta, decimal? hsi,
            decimal? oilByDist, decimal? moistureOven, decimal? oilAPinene, decimal? oilBPinene, decimal? oilMyrcene,
            decimal? oil2MethylButyl, decimal? oilLimonene, decimal? oilMethylHeptonate, decimal? oilMethylOctonoate,
            decimal? oilLinalool, decimal? oilCaryophyllene, decimal? oilFarnesene, decimal? oilHumulene,
            decimal? oilGeraniol, decimal? oilCaryoxide);

        /// <summary>
        /// Get fields for a given grower
        /// </summary>
        Task<IEnumerable> GetLotFarmData(params string[] lotNumbers);

        /// <summary>
        /// Get field names for a list of field ids
        /// </summary>
        Task<List<Dictionary<string, object>>> GetFieldNamesFromFieldIds(string[] fieldIds);

        /// <summary>
        /// Get harvest windows for a given state and crop year
        /// </summary>
        Task<IEnumerable> GetHarvestWindows(string state, int year);

        /// <summary>
        /// Get harvest exception requests for a given grower and crop year
        /// </summary>
        Task<IEnumerable> GetGrowerHarvestExceptionRequests(string growerId, int cropYear);

        /// Get field warnings for a specific grower
        /// </summary>
        Task<IEnumerable> GetGrowerFieldWarnings(string growerIdInt, int? year);

        /// Get facility assessment drafts for a user
        /// </summary>
        Task<IEnumerable> GetUserDraftedFacilityAssessments(int userId);

        /// Get completed facility assessments for a grower
        /// </summary>
        Task<IEnumerable> GetGrowerCompletedFacilityAssessments(int growerId);
    }

    /// <summary>
    /// Implementation of IGrowerPortalService contract. 
    /// </summary>
    public class GrowerPortalService : ApiDataService, IGrowerPortalService
    {
        public const string GrowerPortalSystemName = "GrowerPortal";
        public override string SystemName => GrowerPortalSystemName;

        protected override ApiDataSource Db => db;

        private GrowerPortalDataSource db;
        private ILogWriter log;
        private ISettingsProvider settings;
        private IPimService pimService;
        private IX3Service x3Service;
        private ISolochainService solochianService;
        private IYcrmService ycrmService;
        private string logSource => GetType().Name;
        private double FilterFeatureRadius => 2;


        public GrowerPortalService(GrowerPortalDataSource db, ILogWriter log, ISettingsProvider settings,
            IPimService pimService, IX3Service x3Service, ISolochainService solochianService, IYcrmService ycrmService)
        {
            this.db = db;
            this.log = log;
            this.settings = settings;
            this.pimService = pimService;

            this.x3Service = x3Service;
            this.ycrmService = ycrmService;
            this.solochianService = solochianService;
        }

        public async Task<IEnumerable> GetGrowerDeliveryDetails(string growerId, int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerDeliveryDetails: {growerId} {year.ToString()}"));
            var response = new List<Dictionary<string, object>>();

            List<GrowerOpenDelivery> openLots =
                ((GrowerOpenDelivery[])await this.solochianService.GetGrowerDeliveries(growerId, year, "OPEN"))
                .ToList();
            var closedLots =
                (List<Dictionary<string, object>>)await this.x3Service.GetGrowerPurchaseReceiptsByLot(growerId, year);
            var growerPortalLots = (List<Dictionary<string, object>>)await this.GetGrowerLots(growerId, year);

            var allLotNumbers = new HashSet<string>();

            var openLotNumbers = openLots.Select(x => x.Lot.Trim().ToUpper()).ToList();
            var closedLotNumbers = closedLots.Select(x => ((string)x["lot"]).Trim().ToUpper()).ToList();
            var growerLotNumbers = growerPortalLots.Select(x => (string)x["lot_number"]);

            foreach (var openLotNumber in openLotNumbers)
            {
                allLotNumbers.Add(openLotNumber);
            }

            foreach (var closedLotNumber in closedLotNumbers)
            {
                allLotNumbers.Add(closedLotNumber);
            }

            foreach (var growerLotNumber in growerLotNumbers)
            {
                allLotNumbers.Add(growerLotNumber);
            }


            foreach (var lotNumber in allLotNumbers)
            {
                var lotDetails = new Dictionary<string, object>();

                var openLotDetails = openLots.Where(x => x.Lot.Trim().ToUpper().Equals(lotNumber));
                var closedLotDetails = closedLots.Where(x => ((string)x["lot"]).Trim().ToUpper().Equals(lotNumber));
                var growerPortalLotDetails =
                    growerPortalLots.Where(x => ((string)x["lot_number"]).Trim().ToUpper().Equals(lotNumber));

                string variety = null;
                string dateReceived = null;
                int? totalBales = null;
                decimal? totalPounds = null;
                decimal? averageBaleWeight = null;
                decimal? leafStem = null;
                decimal? seed = null;
                DateTime? hdsCreationDate = null;
                string status = "open";

                var enumerable = closedLotDetails as Dictionary<string, object>[] ?? closedLotDetails.ToArray();
                if (enumerable.Any())
                {
                    if (decimal.TryParse(((string)enumerable.First()["leaf_stem"])?.Trim(), out decimal leafStemResult))
                    {
                        leafStem = leafStemResult;
                    }
                    else
                    {
                        log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                            $"GetGrowerDeliveryDetails: failure attempting to parse {enumerable.First()["leaf_stem"]} from {lotNumber}"));
                    }

                    if (decimal.TryParse(((string)enumerable.First()["seed"])?.Trim(), out decimal seedResult))
                    {
                        seed = seedResult;
                    }
                    else
                    {
                        log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                            $"GetGrowerDeliveryDetails: failure attempting to parse {enumerable.First()["seed"]} from {lotNumber}"));
                    }

                    variety = (string)enumerable.First()["variety"];
                    dateReceived = (string)enumerable.First()["date_received"];
                    totalBales = (int)enumerable.First()["qty_bales_dlv"];
                    totalPounds = (decimal)enumerable.First()["qty_lbs_dlv"];
                    averageBaleWeight = (decimal)enumerable.First()["bale_wt"];
                    status = "closed";
                }

                if (variety.IsNullOrEmpty() && openLotDetails.Any())
                {
                    variety = openLotDetails.First().Variety;
                }

                if (growerPortalLotDetails.Any())
                {
                    hdsCreationDate = (DateTime?)growerPortalLotDetails.First()["harvest_start_at"];

                    if (variety.IsNullOrEmpty())
                    {
                        variety = (string)growerPortalLotDetails.First()["variety"];
                    }
                }

                lotDetails["lot_number"] = lotNumber;
                lotDetails["variety"] = variety;
                lotDetails["date_received"] = dateReceived;
                lotDetails["total_bales"] = totalBales;
                lotDetails["total_lbs"] = totalPounds;
                lotDetails["average_bale_weight"] = averageBaleWeight;
                lotDetails["leaf_stem"] = leafStem;
                lotDetails["seed"] = seed;
                lotDetails["status"] = status;
                lotDetails["harvested_at"] = hdsCreationDate;
                lotDetails["quantities_received"] = openLotDetails;

                response.Add(lotDetails);
            }

            return response;
        }

        public async Task<List<Dictionary<string, object>>> GetGrower(string growerId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrower: {growerId}"));

            // Raw SQL statement
            string sql = @"SELECT id, grower_id, name FROM growers where grower_id = ?";

            //use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> growers = await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
            return growers;
        }

        public async Task<IEnumerable> GetGrowerFromPrimaryKey(int growerId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerFromPrimaryKey: {growerId}"));

            // Raw SQL statement
            string sql = @"select id,
                                  uuid,
                                  grower_id,
                                  agrian_grower_id,
                                  agrian_grower_uuid,
                                  name,
                                  address,
                                  city,
                                  state,
                                  zip,
                                  created_at,
                                  updated_at,
                                  deleted_at
                           from growers
                           where id = ?";

            //use SqlQueryToList to execute query and return results
            var grower = await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
            return grower.FirstOrDefault();
        }

        private async Task<IEnumerable> GetGrowerLots(string growerId, int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGroweLots: {growerId} {year}"));

            // Raw SQL statement
            string sql = @"SELECT growerlots.lot_number,
                                   growerlots.crop_year,
                                   group_concat(distinct growers.name)                        as grower_name,
                                   group_concat(growerfields.field_name)                      as fields,
                                   ifnull(growerlots.variety_id, growerlots.variety_other)    as variety,
                                   growerlots.notes,
                                   concat(users.first_name, ' ', users.last_name)             as created_by,
                                   max( harvest_datasheets.harvest_start_at) as harvest_start_at,
                                   max( harvest_datasheets.harvest_end_at)   as harvest_end_at,
                                   growerlots.mrl,
                                   growerlots.mrl_reason_text,
                                   growerlots.mrl_generated_at
                            FROM growerlots
                                     left join growers on growerlots.grower_id = growers.grower_id
                                     left join users on users.id = growerlots.created_by_id
                                     left join field_lot on field_lot.lot_id = growerlots.id
                                     left join growerfields on field_lot.field_id = growerfields.id
                                     left join harvest_datasheets on growerlots.lot_number like harvest_datasheets.lot_number
                            where growerlots.grower_id = ?
                              and growerlots.crop_year = ?
                              and growerlots.deleted_at is null
                            group by growerlots.id";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, growerId, year).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetGrower(int growerId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrower (int): {growerId}"));

            // Raw SQL statement
            string sql = @"SELECT id, grower_id, name FROM growers where id = ?";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerPortalUserNotifications(int userId, int page, int limit)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPortalUserNotifications: {userId} {page} {limit}"));

            // Raw SQL statement
            string sql = @"select notifications.id,
                                   notifications.grower_id,
                                   notifications.context,
                                   notification_types.name      as notification_type_name,
                                   notification_types.icon         notification_type_icon,
                                   notification_types.notification_subject,
                                   notification_types.message_template as message,
                                   notification_types.help_text as notification_type_help_text,
                                   user_notification.push_sent,
                                   user_notification.email_sent,
                                   user_notification.push_read,
                                   user_notification.emails_processed,
                                   user_notification.created_at,
                                   user_notification.deleted_at
                            from notifications
                                     join user_notification on notifications.id = user_notification.notification_id and user_notification.deleted_at is null
                                     join notification_types on notifications.notification_type_id = notification_types.id
                            where user_notification.user_id = ?
                            order by push_sent desc
                            limit ?
                            offset ?";

            //use SqlQueryToList to execute query and return results
            var notifications = await db.SqlQueryToList(sql, userId, limit, limit * page).ConfigureAwait(false);

            var stubble = new StubbleBuilder().Configure(settings => { settings.SetIgnoreCaseOnKeyLookup(true); })
                .Build();

            foreach (var notification in notifications)
            {
                var context = !notification["context"].ToString().IsNullOrEmpty() &&
                              notification["context"].ToString() != "[]"
                    ? JObject.Parse(notification["context"].ToString())
                    : new JObject();

                notification["context"] = context;
                notification["message"] = stubble.Render(notification["message"].ToString(), context);
            }

            return notifications;
        }

        public async Task<IEnumerable> GetGrowerPortalUserNotificationSettings(int userId, string growerId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPortalUserNotificationSettings: {userId} {growerId}"));

            // Raw SQL statement
            string sql = @"select user_notification_settings.notification_type_id,
                               notification_types.name,
                               notification_types.icon,
                               notification_types.notification_subject,
                               notification_types.help_text,
                               user_notification_settings.email,
                               user_notification_settings.push
                        from notification_types
                                 right outer join user_notification_settings
                                                  on user_notification_settings.notification_type_id = notification_types.id
                        where user_id = ?
                          and user_notification_settings.grower_id = ?;";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, userId, growerId).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerPortalDailyDigestSetting(int userId, string growerId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPortalDailyDigestSetting: {userId} {growerId}"));

            // Raw SQL statement
            string sql = @"select daily_digest
                        from grower_user
                        where user_id = ?
                          and grower_id = (select id from growers where grower_id = ?);";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, userId, growerId).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowers()
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowers"));

            // Raw SQL statement
            string sql = @"SELECT id, grower_id, name, region_id FROM growers                                    
                                    where growers.deleted_at is null
                                      ORDER BY grower_id ASC";


            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerRegions()
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerRegions"));

            // Raw SQL statement
            string sql = @"SELECT regions.id, regions.name, `regions`.`order`, count(growers.id) as grower_count
                            FROM regions
                            left join growers on regions.id = growers.region_id
                            where regions.deleted_at is null
                            group by regions.id";

            var regions = await db.SqlQueryToList(sql).ConfigureAwait(false);
            return regions;
        }

        public async Task<IEnumerable> GetGrowersByRegion(int regionId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowersByRegion: {regionId}"));

            // Raw SQL statement

            string sql =
                $@"select id, grower_id, name,  json_extract(st_asgeojson(center),'$.coordinates') as center, address, city, state, zip from growers 
                                                                                       where growers.deleted_at is null
                                                                                       and region_id = {regionId}
                                                                                       order by name;";


            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowersDetails(int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowersDetails"));

            // Raw SQL statement
            string sql =
                @"select id, grower_id, name,  st_asgeojson(center) as center, address, city, state, zip from growers where growers.deleted_at is null order by name;";

            //use SqlQueryToList to execute query and return results
            var growers = await db.SqlQueryToList(sql).ConfigureAwait(false);
            List<Dictionary<string, object>> growerDetails = new List<Dictionary<string, object>>();

            var users = (List<Dictionary<string, object>>)await GetGrowerUsers().ConfigureAwait(false);
            var selected = (List<Dictionary<string, object>>)await GetGrowerBrewerFeedback(year).ConfigureAwait(false);
            var fields = (List<Dictionary<string, object>>)await GetGrowerFieldsAndLots(year).ConfigureAwait(false);

            foreach (var grower in growers)
            {
                var growerId = Convert.ToInt32(grower["id"].ToString());

                List<JToken> centerCoords = null;
                if (grower["center"] != null && !String.IsNullOrEmpty(grower["center"].ToString()))
                {
                    var geojsonCenter = JObject.Parse(grower["center"].ToString());
                    if (geojsonCenter["coordinates"] != null)
                    {
                        centerCoords = geojsonCenter["coordinates"].ToList();
                    }

                    grower["center"] = centerCoords;
                }

                grower["users"] = users.Where(x => Convert.ToInt32(x["grower_id"].ToString()) == growerId);
                grower["selected_lots"] = selected.Where(x => Convert.ToInt32(x["grower_id"].ToString()) == growerId)
                    .Where(x => !x["customer"].ToString()!.Equals("Official Sensory"))
                    .OrderByDescending(x => Convert.ToInt32(x["ranking"].ToString()));
                grower["field_data"] = fields.Where(x => Convert.ToInt32(x["grower_id"].ToString()) == growerId);
                grower["facilities"] = await GetGrowerFacilities(grower["grower_id"].ToString());
                grower["interactions"] = await ycrmService.GetAccountInteractions(grower["grower_id"].ToString());
                growerDetails.Add(grower);
            }

            return growerDetails;
        }

        private async Task<IEnumerable> GetGrowerUsers(int? growerId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerUsers"));

            // Raw SQL statement
            string sql =
                @"select grower_user.grower_id, users.first_name, users.last_name, users.position, users.email, users.phone
                            from users join grower_user on users.id = grower_user.user_id
                            " + (growerId != null ? "where grower_user.grower_id = " + growerId : "");


            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerBrewerFeedback(int year, string growerId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(
                new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerBrewerFeedback {year} {growerId}"));

            // Raw SQL statement
            string sql = @"select brewer_feedback.*,
                                  growers.id as grower_id,
                                  growerlots.variety_id,
                                  ifnull(global_qc.variety_name, growerlots.variety_other) as variety_name
                            from brewer_feedback
                                     join growerlots on growerlots.lot_number = brewer_feedback.lot_number
                                     join growers on growers.grower_id = growerlots.grower_id
                                     join global_qc on growerlots.variety_id = global_qc.variety_id
                                where growerlots.crop_year = ?
                                and growerlots.deleted_at is null
                                and brewer_feedback.customer not in ('Official Sensory', 'Test Brewery', '*YCH (4Group)')
                                and growers.deleted_at is null  " +
                         (growerId != null ? " and growers.grower_id = '" + growerId + "'" : "")
                         + "  order by brewer_feedback.created_at desc";

            return await db.SqlQueryToList(sql, year).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetGrowerFieldsAndLots(int year, int? growerId = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerFieldsAndLots"));

            // Raw SQL statement
            string sql = @"select  growerfields.id,
                                   growerfields.field_name,
                                   growerfields.acres,
                                   isnull(growerfields.shape) as has_shape,
                                   case
                                       when isnull(growerfields.shape) is false then
                                            ST_AsGeoJSON(ST_Centroid(st_geomfromtext(st_astext(growerfields.shape))))
                                       else null
                                   end as center,
                                   growerfields.notes,
                                   growers.id  as grower_id,
                                   trim(group_concat(' ',growerlots.lot_number)) as lot_numbers,
                                   ifnull(
                                        (select variety
                                        from field_meta_data
                                        where field_meta_data.field_id = growerfields.id
                                        order by year desc
                                        limit 1),
                                        (select group_concat(distinct ifnull(variety_id, variety_other))
                                        from growerlots
                                        left join field_lot on field_lot.lot_id = growerlots.id
                                        where field_lot.field_id = growerfields.id
                                        group by field_lot.field_id, crop_year
                                        order by crop_year desc
                                        limit 1)
                                        )  as  predicted_variety
                            from growerfields
                                     join growers on growers.grower_id = growerfields.grower_id
                                     left join field_lot on field_lot.field_id = growerfields.id
                                     left join growerlots on field_lot.lot_id = growerlots.id and crop_year = ? and growerlots.deleted_at is null
                             where growerfields.deleted_at is null "
                         + (growerId != null ? " and growers.id = " + growerId : "") +
                         " group by growerfields.id";

            var fields = await db.SqlQueryToList(sql, year).ConfigureAwait(false);

            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();
            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

            var fieldWarnings = await GetActiveFieldWarnings().ConfigureAwait(false);

            foreach (var field in fields)
            {
                field["active_field_warnings"] =
                    fieldWarnings?.Where(s => s["field_id"].ToString() == field["id"].ToString());

                field["center"] =
                    JObject.Parse(!String.IsNullOrEmpty(field["center"].ToString())
                        ? field["center"].ToString()
                        : "{}")?["coordinates"];

                var variety = varietyList
                    .FirstOrDefault(x => x.Key.ToString() == field["predicted_variety"].ToString());
                field["predicted_variety"] = variety.Value;
            }

            return fields;
        }

        public async Task<IEnumerable> GetLotHarvestData(string lotNumber)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetLotHarvestData: {lotNumber}"));

            // Raw SQL statement
            string sql = @"SELECT harvest_datasheets.grower_id,
                               harvest_datasheets.grown_by,
                               harvest_datasheets.bale_moisture_low,
                               harvest_datasheets.lot_number,
                               harvest_datasheets.bale_moisture_high,
                               storage_conditions.type AS storage_conditions,
                               harvest_datasheets.cooling_hours_before_baler,
                               harvest_datasheets.cooling_hours_in_kiln,
                               harvest_datasheets.drying_hours,
                               harvest_datasheets.drying_temp_f,
                               harvest_datasheets.humidified,
                               harvest_datasheets.kiln_depth_in,
                               facilities.name AS facility_name,
                               harvest_datasheets.facility_other,
                               kiln_fuels.type AS kiln_fuel_type,
                               picker_types.type AS picker_type,
                               harvest_datasheets.harvest_start_at,                            
                               ifnull(harvest_datasheets.variety_id,
                               harvest_datasheets.variety_other) as variety_code,                            
                               harvest_datasheets.harvest_end_at
                        FROM harvest_datasheets
                                 LEFT JOIN storage_conditions ON harvest_datasheets.bale_storage_conditions = storage_conditions.id
                                 LEFT JOIN facilities ON harvest_datasheets.facility_id = facilities.id
                                 LEFT JOIN kiln_fuels  ON harvest_datasheets.kiln_fuel_id = kiln_fuels.id
                                 LEFT JOIN picker_types ON harvest_datasheets.picker_type_id = picker_types.id
                        WHERE harvest_datasheets.lot_number = ?";

            //use SqlQueryToList to execute query and return results
            var datasheets = await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);
            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

            foreach (var datasheet in datasheets)
            {
                Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();
                var variety = varietyList.Where(x => x.Key.ToString() == datasheet["variety_code"].ToString());

                datasheet["variety"] = variety.Any() ? variety.First().Value : datasheet["variety_code"].ToString();

                datasheet["fields"] = await GetHarvestDataSheetFields(datasheet["lot_number"].ToString());
                var grower = await GetGrower(datasheet["grower_id"].ToString());
                datasheet["grower"] = grower.First();
            }

            return datasheets;


            return await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerHarvestData(string growerId, int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerHarvestData: {growerId} {year.ToString()}"));

            // Raw SQL statement
            string sql = @"SELECT
                               harvest_datasheets.id, 
                               harvest_datasheets.grower_id,
                               harvest_datasheets.lot_number,
                               harvest_datasheets.grown_by,
                               harvest_datasheets.crop_year,
                               harvest_datasheets.bale_moisture_low,
                               harvest_datasheets.bale_moisture_high,
                               harvest_datasheets.bale_storage_conditions,
                               storage_conditions.type as storage_conditions_name,
                               harvest_datasheets.cooling_hours_before_baler,
                               harvest_datasheets.cooling_hours_in_kiln,
                               harvest_datasheets.drying_hours,
                               harvest_datasheets.drying_temp_f,
                               harvest_datasheets.humidified,
                               harvest_datasheets.kiln_depth_in,
                               harvest_datasheets.facility_id,
                               facilities.name AS facility_name,
                               harvest_datasheets.facility_other,
                               harvest_datasheets.kiln_fuel_id,
                               kiln_fuels.type AS kiln_fuel_type,
                               harvest_datasheets.picker_type_id,
                               picker_types.type AS picker_type,
                               harvest_datasheets.total_bales,
                               harvest_datasheets.variety_id,
                               harvest_datasheets.variety_other,
                               harvest_datasheets.variety_agrian,
                               harvest_datasheets.comments,
                               harvest_datasheets.harvest_start_at,
                               harvest_datasheets.harvest_end_at,
                               harvest_datasheets.created_at,
                               harvest_datasheets.updated_at
                        FROM harvest_datasheets
                                 LEFT JOIN storage_conditions ON harvest_datasheets.bale_storage_conditions = storage_conditions.id
                                 LEFT JOIN facilities ON harvest_datasheets.facility_id = facilities.id
                                 LEFT JOIN kiln_fuels  ON harvest_datasheets.kiln_fuel_id = kiln_fuels.id
                                 LEFT JOIN picker_types ON harvest_datasheets.picker_type_id = picker_types.id
                        WHERE harvest_datasheets.grower_id = ? and harvest_datasheets.crop_year = ?
                        order by harvest_datasheets.harvest_start_at desc";


            //use SqlQueryToList to execute query and return results
            var datasheets = await db.SqlQueryToList(sql, growerId, year).ConfigureAwait(false);
            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

            foreach (var datasheet in datasheets)
            {
                Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();
                var variety = varietyList.Where(x => x.Key.ToString() == datasheet["variety_id"].ToString());

                datasheet["variety"] =
                    variety.Any() ? variety.First().Value : datasheet["variety_id"].ToString();

                datasheet["fields"] = await GetHarvestDataSheetFields(datasheet["lot_number"].ToString());
            }

            return datasheets;
        }

        public async Task<IEnumerable> GetHarvestDataSheetFields(string lotNumber)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetHarvestDataSheetFields: {lotNumber}"));

            // Raw SQL statement
            string sql = @"select * from (
                                          select growerfields.id,
                                                 growerfields.field_name,
                                                 growerfields.standardized_name,
                                                 growers.id as grower_id,
                                                 growers.name as grower_name,
                                                 growerfields.agrian_site_uuid,
                                                 ifnull(
                                                     (select variety
                                                      from field_meta_data
                                                      where field_meta_data.field_id = growerfields.id
                                                      order by year desc
                                                      limit 1),
                                                     (select group_concat(distinct ifnull(variety_id, variety_other))
                                                      from growerlots
                                                      left join field_lot on field_lot.lot_id = growerlots.id
                                                      where field_lot.field_id = growerfields.id
                                                      group by field_lot.field_id, crop_year
                                                      order by crop_year desc
                                                      limit 1)
                                                     )  as  predicted_variety,
                                                 st_asText(growerfields.shape) as shape,
                                                 growerfields.acres,
                                                 growerfields.notes,
                                                 growerfields.created_by_id
                                          from growerfields
                                          left join growers on growerfields.grower_id = growers.grower_id
                                          left join field_lot on field_lot.field_id = growerfields.id
                                                   left join field_meta_data on field_meta_data.field_id = growerfields.id
                                          where (growerfields.shape is null or ST_IsValid(ST_GeomFromText(ST_AsText(growerfields.shape))))
                                            and growerfields.deleted_at is null
                                            and field_lot.lot_id = (select id from growerlots where growerlots.lot_number = ? limit 1)
                                          order by growerfields.field_name
                            ) as fields
                        group by id
                        order by field_name;";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerSprayRecords(string growerId, int year)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerSprayRecords: {growerId} {year.ToString()}"));

            // Raw SQL statement
            string sql = @"select sprays.id,
                                   growers.name,

                                   ifnull(chemicals.epa_reg_num, other_epa_reg_num)                                       as chemical_epa_reg_num,
                                   ifnull(chemicals.trade_name, other_trade_name)                                         as chemical_trade_name,
                                   ifnull(chemicals.common_name, other_common_name)                                       as chemical_common_name,
                                   ifnull(ifnull(chemicals.chemical_type, other_chemical_type), other_chemical_type_name) as chemical_type,
                                   spray_licensees.firm_name                                                              as spray_licensee_firm_name,
                                   ifnull(measurement_units.type, measurement_unit_other)                                 as measurement_unit,
                                   crop_year,
                                   application_date,
                                   rate_per_acre,
                                   concentration_applied,
                                   comments,
                                   spray_application_methods.name                                                         as spray_application_method,
                                   concat(creators.first_name, ' ', creators.last_name)                                   as created_by,
                                   concat(approvers.first_name, ' ', approvers.last_name)                                 as approved_by,
                                   custom_chem_approved,
                                   sprays.created_at,
                                   application_time_start,
                                   application_time_end,
                                   temperature_range_low,
                                   temperature_range_high,
                                   wind_vector,
                                   (select count(spray_id) from field_spray where spray_id = sprays.id)                   as field_count
                            from sprays
                                     left join growers on sprays.grower_id = growers.grower_id
                                     left join chemicals on sprays.chemical_id = chemicals.id
                                     left join spray_licensees on growers.id = spray_licensees.grower_id
                                     left join measurement_units on sprays.measurement_unit_id = measurement_units.id
                                     left join spray_application_methods on sprays.spray_application_method_id = spray_application_methods.id
                                     left join users as creators on sprays.created_by_id = creators.id
                                     left join users as approvers on sprays.custom_chem_approved_by_id = approvers.id
                            where sprays.deleted_at is null
                              and sprays.grower_id = ?
                              and crop_year = ?
                            order by application_date desc, application_time_start desc";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, growerId, year).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerMapLayers(string growerId, int year, string center = null,
            double? radius = null)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerMapLayers: {growerId} {year.ToString()} {center} {radius}"));

            List<Dictionary<string, object>> layers = new List<Dictionary<string, object>>();

            // Base
            string baseSql =
                @"select name,  url, icon,  `default` as show_by_default,  color
                                from map_base_layers
                                where map_base_layers.deleted_at is null
                                order by `order`";
            var baseLayers = await db.SqlQueryToList(baseSql).ConfigureAwait(false);
            foreach (var baseLayer in baseLayers)
            {
                baseLayer["group"] = "Base";
                baseLayer["icon_shape"] = "square";
                baseLayer["format"] = "mapbox-tile";
                baseLayer["type"] = "tile";
                baseLayer["show_in_menu"] = true;
                baseLayer["slug"] = baseLayer["name"].ToString().Replace(" ", "_").ToLower(); // Slugify name
                layers.Add(baseLayer);
            }

            string growerFieldVarietyFilterSql = " ";
            string featureFieldVarietyFilterSql = " ";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerFieldVarietyFilterSql = $" and vwSimplifiedGrowerfields.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featureFieldVarietyFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({center})', 0)) < {radius ?? FilterFeatureRadius} ";
            }

            // Fields
            string varietyLayersSql =
                $@"select fields.variety                                 as name,
                           LOWER(fields.variety)                         as slug,
                           'Fields'                                      as 'group',
                           'polygon'                                     as 'icon_shape',
                           'geojson'                                     as 'format',
                           'true'                                        as 'show_in_menu',
                           'true'                                        as 'show_by_default',
                           'true'                                        as 'clickable',
                           '0'                                           as 'order',
                           'fill'                                        as 'type',
                           'true'                                        as 'show_labels',
                           ifnull(map_variety_styles.color, ""#002b49"") as 'color',
                           count(*)                                      as 'field_count',
                           count(idle)                                   as 'idle_field_count',
                           count(fallow)                                 as 'fallow_field_count'
                    from (SELECT vwSimplifiedGrowerfields.id,
                                 vwSimplifiedGrowerfields.grower_id,
                                 case
                                     when field_year_specific_meta_data.idle is not null and field_year_specific_meta_data.idle is true
                                         then 1
                                     end                                          as idle,
                                 case
                                     when field_year_specific_meta_data.fallow is not null and field_year_specific_meta_data.fallow is true
                                         then 1
                                     end                                          as fallow,
                                 ifnull((select variety
                                         from field_meta_data
                                         where field_meta_data.field_id = vwSimplifiedGrowerfields.id
                                         order by year desc
                                         limit 1),
                                        (select group_concat(distinct ifnull(variety_id, variety_other))
                                         from growerlots
                                                  left join field_lot on field_lot.lot_id = growerlots.id
                                         where field_lot.field_id = vwSimplifiedGrowerfields.id
                                         group by field_lot.field_id, crop_year
                                         order by crop_year desc
                                         limit 1))                                as variety
                          FROM vwSimplifiedGrowerfields
                                   left join field_meta_data
                                             on vwSimplifiedGrowerfields.id = field_meta_data.field_id
                                   left join field_year_specific_meta_data
                                             on vwSimplifiedGrowerfields.id = field_year_specific_meta_data.field_id
                                   left join field_lot on vwSimplifiedGrowerfields.id = field_lot.field_id
                                   left join growerlots on growerlots.id = field_lot.lot_id
                          WHERE shape is not null
                               {growerFieldVarietyFilterSql}
                             and st_isvalid(st_geomfromtext(st_astext(shape)))
                               {featureFieldVarietyFilterSql}
                             group by vwSimplifiedGrowerfields.id
                             ORDER BY vwSimplifiedGrowerfields.id ASC
                         ) as fields
                             left join map_variety_styles on fields.variety = map_variety_styles.variety_code
                    group by fields.variety
                    order by field_count desc";

            var fieldVarietyLayers = await db.SqlQueryToList(varietyLayersSql).ConfigureAwait(false);

            var idleFields = fieldVarietyLayers
                .Select(varietyLayer => Int32.Parse(varietyLayer["idle_field_count"].ToString() ?? "0")).Sum();
            var fallowFields = fieldVarietyLayers
                .Select(varietyLayer => Int32.Parse(varietyLayer["fallow_field_count"].ToString() ?? "0")).Sum();
            fieldVarietyLayers.Add(
                new Dictionary<string, object>()
                {
                    { "name", "idle" },
                    { "slug", "idle" },
                    { "group", "Fields" },
                    { "icon_shape", "polygon" },
                    { "format", "geojson" },
                    { "show_in_menu", true },
                    { "show_by_default", true },
                    { "clickable", true },
                    { "order", 0 },
                    { "type", "fill" },
                    { "show_labels", true },
                    { "color", "#EEE" },
                    { "field_count", idleFields },
                }
            );
            fieldVarietyLayers.Add(
                new Dictionary<string, object>()
                {
                    { "name", "fallow" },
                    { "slug", "fallow" },
                    { "group", "Fields" },
                    { "icon_shape", "polygon" },
                    { "format", "geojson" },
                    { "show_in_menu", true },
                    { "show_by_default", true },
                    { "clickable", true },
                    { "order", 0 },
                    { "type", "fill" },
                    { "show_labels", true },
                    { "color", "#EEE" },
                    { "field_count", fallowFields },
                }
            );

            string varietyColorsSql =
                @"select variety_code as input, color as output from map_variety_styles";
            var varietyColors = await db.SqlQueryToList(varietyColorsSql).ConfigureAwait(false);


            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

            var fieldsByVariety = await GetGrowerFieldsGeojsonByVariety(growerId, "polygon", center, radius);

            foreach (var fieldVarietyLayer in fieldVarietyLayers)
            {
                var geojson = fieldsByVariety.Where(x => x.Key.ToString() == fieldVarietyLayer["name"].ToString())
                    .FirstOrDefault();

                fieldVarietyLayer["geojson"] = geojson.Value;


                fieldVarietyLayer["colorConditions"] = varietyColors;

                // Ensure we don't have any empty variety codes
                fieldVarietyLayer["name"] = string.IsNullOrEmpty(fieldVarietyLayer["name"].ToString())
                    ? "Other"
                    : fieldVarietyLayer["name"];

                // update the names to be variety names not variety codes
                var fieldVariety = varietyList.Where(x => x.Key.ToString() == fieldVarietyLayer["name"].ToString());
                fieldVarietyLayer["name"] =
                    fieldVariety.Any() ? fieldVariety.First().Value : fieldVarietyLayer["name"].ToString();

                if (Convert.ToInt64(fieldVarietyLayer["field_count"]) > 0)
                {
                    layers.Add(fieldVarietyLayer);
                }
            }

            // Add Field Point Layer
            Dictionary<string, object> fieldPoints = new Dictionary<string, object>();
            fieldPoints["name"] = "All Field Points";
            fieldPoints["slug"] = "fieldPoints";
            fieldPoints["group"] = "Fields";
            fieldPoints["icon_shape"] = "circle";
            fieldPoints["geojson"] = await GetGrowerFieldsGeojson(growerId, null, "point", center, radius);
            fieldPoints["format"] = "geojson";
            fieldPoints["feature_count"] = 1;
            fieldPoints["show_in_menu"] = true;
            fieldPoints["show_by_default"] = false;
            fieldPoints["clickable"] = false;
            fieldPoints["order"] = 100;
            fieldPoints["icon"] = null;
            fieldPoints["type"] = "circle";
            fieldPoints["allow_overlap"] = true;
            fieldPoints["show_labels"] = false;
            fieldPoints["color"] = "#999";
            fieldPoints["colorConditions"] = varietyColors;
            layers.Add(fieldPoints);

            string growerSubAreaFilterSql = "";
            string featureSubAreaFilterSql = "";

            if (!string.IsNullOrEmpty(growerId))
            {
                growerSubAreaFilterSql = $" and growers.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featureSubAreaFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({center})', 0)) < {radius ?? FilterFeatureRadius} ";
            }

            // Sub areas
            string subAreaSql =
                $@"select sub_area_types.name,
                       sub_area_types.id   as sub_area_type_id,
                       'geojson'           as format,
                       sub_area_types.`default` as show_by_default,
                       sub_area_types.`show_in_menu`,
                       sub_area_types.`clickable`,
                       sub_area_types.`order`, 
                       'Layer'              as 'group',
                       'fill'              as type,
                       ''                  as icon,
                       ''                  as allow_overlap,
                       sub_area_types.`color`,
                       sub_area_types.`show_labels`,
                       count(sub_areas.id) as feature_count
                from sub_area_types
                         join sub_areas on sub_area_types.id = sub_areas.sub_area_type_id
                         join growers on sub_areas.grower_id = growers.id
                where sub_areas.deleted_at is null
                  and st_isvalid(st_geomfromtext(st_astext(shape)))
                  and sub_area_types.deleted_at is null
                  {growerSubAreaFilterSql}
                  {featureSubAreaFilterSql}
                  and sub_areas.crop_year = ?
                group by sub_area_types.id;";
            var subAreaLayers = await db.SqlQueryToList(subAreaSql, growerId, year).ConfigureAwait(false);
            foreach (var subAreaLayer in subAreaLayers)
            {
                subAreaLayer["icon_shape"] = "polygon";
                subAreaLayer["url"] = "growers/" + growerId + "/sub-areas/" + subAreaLayer["sub_area_type_id"] +
                                      "/geojson";
                subAreaLayer["slug"] = (subAreaLayer["name"].ToString() + "_" + year.ToString()).Replace(" ", "_")
                    .ToLower(); // Slugify name
                subAreaLayer["geojson"] = await GetGrowerSubAreasGeojson(growerId,
                    Int32.Parse(subAreaLayer["sub_area_type_id"].ToString()), year, center, radius);
                if (Convert.ToInt64(subAreaLayer["feature_count"]) > 0)
                {
                    layers.Add(subAreaLayer);
                }
            }

            string growerPointFilterSql = "";
            string featurePointFilterSql = "";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerPointFilterSql = $" and growers.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featurePointFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(map_points.location), 0), ST_GeomFromText('POINT({center})', 0)) < {radius ?? FilterFeatureRadius} ";
            }


            // Points
            string pointSql =
                $@"select map_point_types.name,
                           'geojson'              as format,
                           map_point_types.`id` as map_point_type_id,
                           map_point_types.`default` as show_by_default,
                           map_point_types.`show_in_menu`,
                           map_point_types.`clickable`,
                           map_point_types.`order`,
                           'Layer'                 as 'group',
                           'circle'                 as type,
                           ''                 as show_labels,
                           map_point_types.`color`,
                           map_point_types.`icon`,
                           map_point_types.`allow_overlap`,
                           count(map_points.id)   as feature_count
                    from map_point_types
                             join map_points on map_point_types.id = map_points.type_id
                             join growers on map_points.grower_id = growers.id
                    where map_points.deleted_at is null
                      and map_point_types.deleted_at is null
                      {growerPointFilterSql}
                      {featurePointFilterSql}
                      and map_points.year = ?
                    group by map_point_types.id;";
            var pointLayers = await db.SqlQueryToList(pointSql, year).ConfigureAwait(false);
            foreach (var pointLayer in pointLayers)
            {
                pointLayer["icon_shape"] = "circle";
                pointLayer["slug"] =
                    (pointLayer["name"].ToString() + "_" + year.ToString()).Replace(" ", "_").ToLower(); // Slugify name
                if (!pointLayer["icon"].ToString().IsNullOrEmpty())
                {
                    pointLayer["type"] = "symbol";
                }

                pointLayer["order"] = 0;

                pointLayer["geojson"] = await GetGrowerPointsGeojson(growerId,
                    Int32.Parse(pointLayer["map_point_type_id"].ToString()), year, center, radius);

                layers.Add(pointLayer);
            }

            return layers;
        }

        public async Task<IEnumerable> GetFeatureByCoordinates(string growerId, double latitude, double longitude,
            double zoom, int year, List<string> exclude)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFeatureByCoordinates: {growerId} {latitude} {longitude} {zoom} {string.Join(", ", exclude)}"));

            // This calculates the size of the touch target on the map
            // this needs to be balanced with zoom if zoom is less than 18 and greater than 12,
            // calculate the appropriate tolerance

            double growthFactor = 0.5000003506;
            double intercept = 5.310920006;
            double tolerance = intercept * Math.Pow(growthFactor, zoom);

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFeatureByCoordinates tolerance: {tolerance}"));

            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();
            var additionalExcludes = new List<string>();
            foreach (var excluded in exclude)
            {
                var fieldVariety = varietyList.Where(x => x.Value.ToString().ToLower().Replace(" ", "_") == excluded);
                if (fieldVariety.Any())
                {
                    additionalExcludes.Add(fieldVariety.First().Key.ToString());
                }
            }

            string growerFeatureFilterSql = "";
            string growerFieldFilterSql = "";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerFeatureFilterSql = $" and growers.grower_id = '{growerId}' ";
                growerFieldFilterSql = $" and growerfields.grower_id = '{growerId}' ";
            }

            exclude.AddRange(additionalExcludes);
            string sql = $@"select * from ((select growerfields.id,
                                growerfields.field_name                                                               as name,
                                'field'                                                                               as type,
                                IFNULL(REPLACE(LOWER(
                                ifnull((select variety from field_meta_data where field_meta_data.field_id = growerfields.id and field_meta_data.deleted_at is null order by year desc limit 1),
                                   (select group_concat(distinct ifnull(variety_id, variety_other))
                                    from growerlots
                                             left join field_lot on field_lot.lot_id = growerlots.id
                                    where field_lot.field_id = growerfields.id
                                    group by field_lot.field_id, crop_year
                                    order by crop_year desc
                                       limit 1)) 
                                    ),' ','_'),'Other') as layer_type,
                                3                                                                                     as priority,
                                ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({latitude} {longitude})',
                                                                   0))                                             as tolerance
                         from growerfields
                              left join field_meta_data
                                        on growerfields.id = field_meta_data.field_id and field_meta_data.deleted_at is null
                              left join field_lot on growerfields.id = field_lot.field_id
                              left join growerlots on growerlots.id = field_lot.lot_id
                         where shape is not null
                           and ST_IsValid(ST_GeomFromText(ST_AsText(shape)))
                           and growerfields.deleted_at is null
                           {growerFieldFilterSql}
                           and ST_intersects(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({latitude} {longitude})', 0)))
                        union
                        (select sub_areas.id,
                                sub_area_types.name as name,
                                'sub_area'          as type,
                                REPLACE(LOWER(sub_area_types.name),' ','_')          as layer_type,
                                2                   as priority,
                                ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({latitude} {longitude})', 0)) as tolerance
                         from sub_areas
                                  join sub_area_types on sub_areas.sub_area_type_id = sub_area_types.id 
                                  join growers on sub_areas.grower_id = growers.id
                         where shape is not null
                           and sub_areas.deleted_at is null
                           and sub_areas.crop_year = ?
                           {growerFeatureFilterSql}
                           and ST_IsValid(ST_GeomFromText(ST_AsText(shape)))
                           and ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({latitude} {longitude})', 0))  < {tolerance})
                        union

                        (select map_points.id,
                                map_point_types.name                                                           as name,
                                'point'                                                                        as type,
                                LOWER(map_point_types.name)                                                    as layer_type,
                                1                                                                              as priority,
                                ST_Distance(ST_GeomFromText(ST_AsText(map_points.location), 0),
                                            ST_GeomFromText('POINT({latitude} {longitude})', 0)) as tolerance
                         from map_points
                                  join map_point_types on map_points.type_id = map_point_types.id
                                  join growers on map_points.grower_id = growers.id
                         where ST_Distance(ST_GeomFromText(ST_AsText(map_points.location), 0),
                                           ST_GeomFromText('POINT({latitude} {longitude})', 0)) < {tolerance}
                           and map_points.deleted_at is null
                           and map_points.year = ?
                           {growerFeatureFilterSql}
                         order by ST_Distance(ST_GeomFromText(ST_AsText(map_points.location), 0),
                                              ST_GeomFromText('POINT({latitude} {longitude})', 0)) asc)) as features
                        where layer_type not in ({string.Join(", ", exclude.Select(s => "?"))})
                        order by priority asc, tolerance asc
                        limit 5;";

            var sqlParameters = exclude;
            sqlParameters.Add(year.ToString());
            sqlParameters.Add(year.ToString());
            sqlParameters.Reverse();

            // use SqlQueryToList to execute query and return results
            var locations = await db.SqlQueryToList(sql, sqlParameters.ToArray()).ConfigureAwait(false);
            var location = new Dictionary<string, object>();

            if (locations.Any())
            {
                location = locations.First();

                location["properties"] = await GetFeaturePropertiesByTypeAndId(
                    location["type"].ToString(),
                    Int32.Parse(location["id"].ToString()),
                    year
                );
            }

            return location;
        }

        public async Task<Dictionary<string, object>> GetFeaturePropertiesByTypeAndId(string type, int id, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFeaturePropertiesByTypeAndId: {type} {id} {year}"));
            Dictionary<string, object> properties = null;
            switch (type)
            {
                case "field":
                    properties = await GetGrowerField(id);
                    properties["points"] = await GetFieldPoints(id, year);
                    properties["scouting_events"] = await GetFieldScoutingEvents(id);
                    properties["active_field_warnings"] = await GetActiveFieldWarnings(id);
                    break;
                case "sub_area":
                    properties = await GetGrowerSubArea(id);
                    break;
                case "point":
                    properties = await GetGrowerPoint(id);
                    break;
            }

            return properties;
        }

        private async Task<Dictionary<string, object>> GetGrowerPoint(int pointId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPoint: {pointId}"));

            string growerPortalBaseUrl = settings[Config.Settings.Api().GrowerPortal().BaseUrl()];

            string sql = $@"select map_points.name,
                           map_points.notes     as description,
                           map_point_types.name as type,
                           map_point_types.color,
                           map_point_types.icon,
                           map_point_types.allow_overlap,
                           map_points.created_at,
                           
                           users.first_name as creator_first_name,
                           users.last_name as creator_last_name,

                           weather.id as weather_id,
                           weather.title as weather_title,
                           weather.description as weather_description,
                           weather.icon as weather_icon,
                           weather.temp as weather_temp,
                           weather.feels_like as weather_feels_like,
                           weather.temp_min as weather_temp_min,
                           weather.temp_max as weather_temp_max,
                           weather.pressure as weather_pressure,
                           weather.humidity as weather_humidity,
                           weather.visibility as weather_visibility,
                           weather.wind_speed as weather_wind_speed,
                           weather.wind_direction as weather_wind_direction,
                           weather.wind_gust as weather_wind_gust,
                           weather.rain_1hr as weather_rain_1hr,
                           weather.rain_3hr as weather_rain_3hr,
                           weather.snow_1hr as weather_snow_1hr,
                           weather.snow_3hr as weather_snow_3hr,
                           weather.created_at as weather_created_at,
                           weather.updated_at as weather_updated_at,
                           
                           replace(
                                   concat('{growerPortalBaseUrl}','/',images.path)
                               ,'api/public','storage') as image,
                           count(*)             as count
                    from map_points
                             join map_point_types on map_points.type_id = map_point_types.id

                             left join users on map_points.created_by = users.id
                             left join weather on map_points.weather_id = weather.id
                        
                             left join imageables on map_points.id = imageables.imageable_id and
                                                     imageables.imageable_type = 'App\\Models\\MapPoint'
                             left join images on images.id = imageables.image_id
                    where map_points.id = ?";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, pointId).ConfigureAwait(false);

            return results.Any() ? results.First() : null;
        }

        public async Task<Dictionary<string, object>> GetGrowerSubArea(int subAreaId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerSubArea: {subAreaId}"));

            string growerPortalBaseUrl = settings[Config.Settings.Api().GrowerPortal().BaseUrl()];

            string sql = $@"select
                            ST_AsGeoJSON(sub_areas.shape) as shape,
                            ST_AsText(sub_areas.shape) as shape_text,
                            ST_x(ST_centroid(st_geometryfromtext(st_astext(sub_areas.shape)))) as x,
                            ST_y(ST_centroid(st_geometryfromtext(st_astext(sub_areas.shape)))) as y,
                            sub_areas.id,
                            sub_areas.crop_year,
                            sub_areas.field_id,
                            sub_areas.grower_id,
                            sub_areas.acres,
                            sub_areas.notes,
                            sub_areas.created_at,

                            users.first_name as creator_first_name,
                            users.last_name as creator_last_name,

                            concat(sub_area_types.name, ' ', sub_areas.id) as title,
                            sub_area_types.id                   as sub_area_type_id,
                            sub_area_types.color                         as fill,
                            sub_area_types.name                         as name,
                            replace(
                                    concat('{growerPortalBaseUrl}','/',images.path)
                                ,'api/public','storage') as image
                        from sub_areas
                                 join sub_area_types  on sub_areas.sub_area_type_id = sub_area_types.id
                                 left join users on sub_areas.created_by = users.id
                                 left join imageables on sub_areas.id = imageables.imageable_id and
                                                         imageables.imageable_type = 'App\\Models\\SubArea'
                                 left join images on images.id = imageables.image_id
                        where sub_areas.id = ?";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, subAreaId).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["field"] = null;
                result["grower"] = null;
                if (!result["field_id"].ToString().IsNullOrEmpty())
                {
                    var fieldId = Int32.Parse(result["field_id"].ToString());
                    result["field"] = await GetGrowerField(fieldId);
                }

                if (!result["grower_id"].ToString().IsNullOrEmpty())
                {
                    var growerId = Int32.Parse(result["grower_id"].ToString());
                    result["grower"] = await GetGrower(growerId);
                }
            }

            return results.Any() ? results.First() : null;
        }

        private async Task<List<Dictionary<string, object>>> GetFieldPoints(int fieldId, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFieldPoints: {fieldId} {year}"));

            string sql = @"select map_point_types.name,
                                   map_point_types.color,
                                   map_point_types.icon,
                                   map_point_types.allow_overlap,
                                   count(*) as count
                            from map_points
                                     join map_point_types on map_points.type_id = map_point_types.id
                            where field_id = ? and year =  ?
                            group by type_id";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, fieldId, year).ConfigureAwait(false);

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetFieldScoutingEvents(int fieldId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFieldScoutingEvents: {fieldId}"));

            string sql = @"select crop_year, completed_at from roguing_logs
                            where field_id = ?
                            order by completed_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, fieldId).ConfigureAwait(false);

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetFieldScoutingEvent(int id)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFieldScoutingEvent: {id}"));

            string sql = @"select crop_year, completed_at from roguing_logs
                            where roguing_logs.id = ?
                            order by completed_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, id.ToString()).ConfigureAwait(false);

            return results;
        }

        private async Task<Dictionary<string, object>> GetGrowerField(int fieldId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerField: {fieldId}"));

            string sql = @"select  fields.shape,
                                    fields.x,
                                    fields.y,
                                    fields.id,
                                    fields.field_name,
                                    fields.notes,
                                    fields.created_at,
                                    fields.creator_first_name,
                                    fields.creator_last_name,
                                    fields.grower_name,
                                    fields.field_name                                as title,
                                    concat(fields.grower_id, ' ', fields.field_name) as description,
                                    round(fields.acres, 2)                           as acres,
                                    fields.variety                                   as variety_code,
                                    fields.idle                                      as idle,
                                    fields.fallow                                    as fallow,
                                    map_variety_styles.color                         as fill,
                                    map_variety_styles.color                         as stroke
                            from (
                                     SELECT vwSimplifiedGrowerfields.id,
                                            ST_AsGeoJSON(vwSimplifiedGrowerfields.shape) as shape,
                                            ST_x(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape)))) as x,
                                            ST_y(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape)))) as y,
                                            vwSimplifiedGrowerfields.field_name,
                                            vwSimplifiedGrowerfields.grower_id,
                                            vwSimplifiedGrowerfields.created_at,
                                            vwSimplifiedGrowerfields.acres,
                                            (select CAST(IF(idle = 1, 'true', 'false') AS JSON) from field_year_specific_meta_data where field_year_specific_meta_data.field_id = vwSimplifiedGrowerfields.id order by year desc limit 1) as idle,
                                            (select CAST(IF(fallow = 1, 'true', 'false') AS JSON) from field_year_specific_meta_data where field_year_specific_meta_data.field_id = vwSimplifiedGrowerfields.id order by year desc limit 1) as fallow,
                                            users.first_name as creator_first_name,
                                            users.last_name as creator_last_name,
                                            growers.name as grower_name,
                                            vwSimplifiedGrowerfields.notes,
                                            ifnull((select variety from field_meta_data where field_meta_data.field_id = vwSimplifiedGrowerfields.id and field_meta_data.deleted_at is null order by year desc limit 1),
                                           (select group_concat(distinct ifnull(variety_id, variety_other))
                                            from growerlots
                                                     left join field_lot on field_lot.lot_id = growerlots.id
                                            where field_lot.field_id = vwSimplifiedGrowerfields.id
                                            group by field_lot.field_id, crop_year
                                            order by crop_year desc
                                               limit 1))  as variety
                                     FROM vwSimplifiedGrowerfields
                                              left join field_meta_data
                                                        on vwSimplifiedGrowerfields.id = field_meta_data.field_id
                                              left join field_lot on vwSimplifiedGrowerfields.id = field_lot.field_id
                                              left join users on vwSimplifiedGrowerfields.created_by_id = users.id
                                              left join growerlots on growerlots.id = field_lot.lot_id
                                              left join growers on growers.grower_id = vwSimplifiedGrowerfields.grower_id
                                     WHERE vwSimplifiedGrowerfields.id = ?
                                     group by vwSimplifiedGrowerfields.id
                                     ORDER BY vwSimplifiedGrowerfields.id ASC
                                 ) as fields
                                     left join map_variety_styles on fields.variety = map_variety_styles.variety_code;";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, fieldId).ConfigureAwait(false);
            var field = results.Any() ? results.First() : null;

            if (field != null)
            {
                var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

                Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();
                var fieldVariety = varietyList.Where(x => x.Key.ToString() == field["variety_code"].ToString());

                field["variety"] = fieldVariety.Any() ? fieldVariety.First().Value : field["variety_code"].ToString();

                Dictionary<string, object> fieldMetadata =
                    (await GetGrowerFieldMetaData(Convert.ToInt32(field["id"].ToString()))).FirstOrDefault();

                field["planting_media"] = null;
                field["planted_date"] = null;

                if (fieldMetadata != null)
                {
                    List<Dictionary<string, object>>
                        meta = (List<Dictionary<string, object>>)fieldMetadata["metadata"];
                    if (meta.Any())
                    {
                        Dictionary<string, object> recentMetaData = meta.FirstOrDefault();
                        if (recentMetaData != null)
                        {
                            field["planted_date"] = recentMetaData["planted"];

                            Dictionary<string, object> media =
                                ((List<Dictionary<string, object>>)recentMetaData["media"]).FirstOrDefault();
                            if (media != null)
                            {
                                field["planting_media_name"] = media["name"];
                            }
                        }

                        field["metadata"] = meta;
                    }
                }
            }

            return field;
        }

        public async Task<IEnumerable> GetUserRoguingSchedules(int userId, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRoguingSchedules: {userId}"));
            string sql = $@"select roguing_schedules.*
                            from roguing_schedules
                                     join roguing_teams on roguing_teams.id = roguing_schedules.team_id
                                     join roguing_team_members on roguing_team_members.team_id = roguing_teams.id
                            where roguing_team_members.user_id = ?
                              and roguing_schedules.published is not null
                              and roguing_schedules.deleted_at is  null";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, userId.ToString()).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["year"] = year;
                result["fields"] = await GetRoguingSchedule(
                    Convert.ToInt32(result["id"].ToString()),
                    year
                );
            }

            return results;
        }

        public async Task<IEnumerable> GetRoguingSchedules(int year, int? teamId, int? userId, DateTime? windowStart,
            DateTime? windowEnd, bool onlyCurrent, bool onlyFinished)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRoguingSchedules: teamId - {teamId}, userId - {userId}, windowStart - {windowStart}, windowEnd - {windowEnd}, onlyCurrent - {onlyCurrent}, onlyFinished - {onlyFinished}"));
            string sql = $@"select roguing_schedules.id,
                                   roguing_schedules.starting_point_id,
                                   roguing_schedules.begin,
                                   roguing_schedules.end,
                                   roguing_schedules.published,
                                   roguing_schedules.team_id,
                                   count(scheduled_fields.id) as num_fields,
                                   sum(case when scheduled_fields.roguing_log_id is not null and scheduled_fields.scouting_report_id is not null then 1 else 0 end) as completed_fields
                            from roguing_schedules
                            left join roguing_teams on roguing_teams.id = roguing_schedules.team_id
                            left join scheduled_fields on roguing_schedules.id = scheduled_fields.schedule_id
                            where roguing_schedules.published is not null
                              and roguing_schedules.deleted_at is  null ";

            List<string> sqlParameters = new List<string>();

            if (teamId != null)
            {
                sql += "and roguing_schedules.team_id = ? ";
                sqlParameters.Add(teamId.ToString());
            }

            if (userId != null)
            {
                sql += "and roguing_schedules.team_id in " +
                       "(" +
                       "select roguing_teams.id " +
                       "from roguing_teams join roguing_team_members on roguing_teams.id = roguing_team_members.team_id " +
                       "where roguing_team_members.user_id = ?" +
                       ") ";
                sqlParameters.Add(userId.ToString());
            }

            if (windowStart != null && windowEnd != null)
            {
                sql += "and (roguing_schedules.begin between ?  and ? " +
                       "or roguing_schedules.end between ? and ? " +
                       "or (roguing_schedules.begin <= ? and roguing_schedules.end >= ?)) ";
                sqlParameters.Add(windowStart.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlParameters.Add(windowEnd.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlParameters.Add(windowStart.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlParameters.Add(windowEnd.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlParameters.Add(windowStart.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                sqlParameters.Add(windowEnd.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            }

            sql += @"group by roguing_schedules.id,
                                   roguing_schedules.starting_point_id,
                                   roguing_schedules.begin,
                                   roguing_schedules.end,
                                   roguing_schedules.team_id ";

            if (onlyCurrent && !onlyFinished)
            {
                sql += "having num_fields <> completed_fields ";
            }

            if (onlyFinished && !onlyCurrent)
            {
                sql += "having num_fields = completed_fields ";
            }

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, sqlParameters.ToArray()).ConfigureAwait(false);

            List<Dictionary<string, object>> teams = await GetRoguingTeams() as List<Dictionary<string, object>>;

            foreach (var result in results)
            {
                result["year"] = year;
                result["team"] = teams.FirstOrDefault(x => x["id"].ToString() == result["team_id"].ToString());
                result["fields"] = await GetRoguingSchedule(
                    Convert.ToInt32(result["id"].ToString()),
                    year
                );
            }

            return results;
        }

        public async Task<IEnumerable> GetRoguingTeams()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRoguingTeams:"));
            string sql = $@"select id, name
                            from roguing_teams
                            where deleted_at is null";
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetRoguingSchedule(int scheduleId, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRoguingSchedule: {scheduleId}"));
            string sql = $@"select growerfields.id,
                               scheduled_fields.`order`                 as                                                      `order`,
                               scheduled_fields.`roguing_log_id`        as                                                      `roguing_log_id`,
                               scheduled_fields.`scouting_report_id`    as                                                      `scouting_report_id`,
                               field_name                               as                                                      field,
                               group_concat(distinct growers.name)      as                                                      grower,
                               acres,
                               JSON_EXTRACT(st_asgeojson(st_centroid(st_geomfromtext(st_astext(shape)))), '$.coordinates') center,
                               ifnull((select variety from field_meta_data where field_meta_data.field_id = growerfields.id order by year desc limit 1),
                                      (select variety
                                       from harvest_datasheets
                                                join growerlots on harvest_datasheets.lot_number = growerlots.lot_number
                                                join field_lot on field_lot.lot_id = growerlots.id
                                                join field_meta_data on field_lot.field_id = field_meta_data.field_id
                                       where field_meta_data.field_id = growerfields.id
                                       order by growerlots.crop_year desc
                                       limit 1))                   as                                                      variety,
                               count(map_points.id)                as                                                      males
                        from roguing_schedules
                                 left join scheduled_fields on roguing_schedules.id = scheduled_fields.schedule_id
                                 left join growerfields on scheduled_fields.field_id = growerfields.id
                                 left join growers on growers.grower_id = growerfields.grower_id
                                 left join map_points on growerfields.id = map_points.field_id and map_points.type_id = 1 and map_points.year = ?
                        where shape is not null
                          and growerfields.deleted_at is null
                          and roguing_schedules.id = ?
                        group by growerfields.id,
                                 scheduled_fields.order,
                                 scheduled_fields.roguing_log_id,
                                 growerfields.field_name,
                                 growerfields.shape,
                                 variety
                        order by scheduled_fields.order asc;";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, year.ToString(), scheduleId.ToString()).ConfigureAwait(false);
            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();
            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

            foreach (var result in results)
            {
                var variety = varietyList
                    .FirstOrDefault(x => x.Key.ToString() == result["variety"].ToString());
                result["variety_name"] = variety.Value;

                if (!result["roguing_log_id"].ToString().IsNullOrEmpty())
                {
                    result["roguing_log"] =
                        await GetFieldScoutingEvent(Convert.ToInt32(result["roguing_log_id"].ToString()));
                }
            }

            return results;
        }

        public async Task<IEnumerable> GetGrowerFieldKilnSampleReports(int fieldId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldKilnSampleReports: {fieldId}"));

            string sql = $@"select id,
                               field_id,
                               facility_id,
                               depth,
                               temperature,
                               notes,
                               created_at,
                               updated_at
                        from kiln_sample_reports
                        where deleted_at is null
                        and field_id = ?
                        order by created_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, fieldId.ToString()).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["field"] = await GetGrowerField(Convert.ToInt32(result["field_id"].ToString()));
                result["facility"] = await GetGrowerFacility(Convert.ToInt32(result["facility_id"].ToString()));
            }

            return results;
        }

        public async Task<IEnumerable> GetKilnSampleReports(int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetKilnSampleReports {year}"));

            string sql = $@"select id,
                               field_id,
                               facility_id,
                               depth,
                               temperature,
                               notes,
                               visited_at,
                               created_by,
                               created_at,
                               updated_at
                        from kiln_sample_reports
                        where deleted_at is null
                        and DATE_FORMAT(created_at,'%Y') = ?
                        order by visited_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, year).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["field"] = await GetGrowerField(Convert.ToInt32(result["field_id"].ToString()));
                result["facility"] = await GetGrowerFacility(Convert.ToInt32(result["facility_id"].ToString()));
                if (!result["created_by"].ToString().IsNullOrEmpty())
                {
                    result["user"] = await GetUser(Convert.ToInt32(result["created_by"].ToString()));
                }
                else
                {
                    result["user"] = null;
                }
            }

            return results;
        }

        public async Task<IEnumerable> GetAgronomyHubUpdates(int year, int? regionId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAgronomyHubUpdates {year} {regionId}"));

            string sql = $@"select * from agronomy_updates  where DATE_FORMAT(created_at, '%Y') = ?";
            if (regionId != null)
            {
                sql += " and region_id = " + regionId + " ";
            }

            sql += " order by created_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, year).ConfigureAwait(false);


            foreach (var result in results)
            {
                result["region"] = await GetRegion(Int32.Parse(result["region_id"].ToString()));
                result["author"] = await GetUser(Int32.Parse(result["author_id"].ToString()));
                result["observations"] = await GetAgronomyHubObservations(Int32.Parse(result["id"].ToString()));
                result["images"] = await GetAgronomyHubImages(Int32.Parse(result["id"].ToString()));
            }

            return results;
        }

        private async Task<IEnumerable> GetAgronomyHubObservations(int agronomyUpdateId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAgronomyHubObservations {agronomyUpdateId}"));

            string sql = $@"select agronomy_update_observations.id,
                                   agronomy_update_id,
                                   ifnull(agronomy_update_pressure_types.name,
                                          pressure_type_other)           as pressure_type,
                                   scouting_report_incidence_levels.name as incidence_level,
                                   agronomy_update_severity_levels.name  as severity_level,
                                   agronomy_update_observations.created_at,
                                   agronomy_update_observations.updated_at
                            from agronomy_update_observations
                                     join agronomy_update_pressure_types
                                          on agronomy_update_observations.pressure_type_id = agronomy_update_pressure_types.id
                                     join scouting_report_incidence_levels
                                          on agronomy_update_observations.incidence_level_id = scouting_report_incidence_levels.id
                                     join agronomy_update_severity_levels
                                          on agronomy_update_observations.severity_level_id = agronomy_update_severity_levels.id
                            where agronomy_update_id = ?
                            order by agronomy_update_observations.created_at desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, agronomyUpdateId).ConfigureAwait(false);

            return results;
        }

        private async Task<IEnumerable> GetAgronomyHubImages(int agronomyUpdateId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAgronomyHubObservations {agronomyUpdateId}"));

            string growerPortalBaseUrl = settings[Config.Settings.Api().GrowerPortal().BaseUrl()];

            string sql = $@"select images.id,
                                replace(concat('{growerPortalBaseUrl}', '/', images.path), 'api', 'storage') as path,
                                   images.image_alt,
                                   images.created_at
                            from agronomy_updates
                                     join imageables on imageables.imageable_id = agronomy_updates.id and
                                                        imageables.imageable_type = 'App\\Models\\AgronomyUpdate'
                                     join images on imageables.image_id = images.id
                            where agronomy_updates.id = ?;";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, agronomyUpdateId).ConfigureAwait(false);

            return results;
        }

        private async Task<Dictionary<string, object>> GetRegion(int regionId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRegion {regionId}"));

            string sql = $@"select * from regions where id = ?";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, regionId).ConfigureAwait(false);

            return results?.First();
        }

        public async Task<List<Dictionary<string, object>>> GetGrowerFieldMetaData(int fieldId, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldMetaData: {fieldId} {year}"));

            string sql = $@"select growerfields.id,
                                   growerfields.field_name as name,
                                   growerfields.grower_id,
                                   ST_AsText(growerfields.shape) as shape_text,
                                   growerfields.acres,
                                   growerfields.notes,
                                   growerfields.locked,
                                   (select idle from field_year_specific_meta_data where field_id = growerfields.id and year = ? and idle is true order by year desc limit 1) as idle_bool,
                                   (select now() from field_year_specific_meta_data where field_id = growerfields.id and year = ? and idle is true order by year desc limit 1) as idle,
                                   field_meta_data.year,
                                   field_meta_data.planted,
                                   field_meta_data.variety,
                                   field_meta_data.amount_planted as rhizomes_planted,
                                   field_meta_data.source,
                                   field_meta_data.traceability_id,
                                   field_meta_data.media_id,
                                   planting_media.name as media_name,
                                   field_meta_data.amount_planted,
                                   field_meta_data.organic,
                                   field_meta_data.notes   as year_notes
                            from growerfields left join field_meta_data
                                               on field_meta_data.field_id = growerfields.id and field_meta_data.year = ?
                            left join planting_media on field_meta_data.media_id = planting_media.id where growerfields.id = ?
                            order by year desc";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, year, year, fieldId).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["grower"] = await GetGrower(result["grower_id"].ToString());
            }

            return results;
        }

        public async Task<List<Dictionary<string, object>>> GetGrowerFieldMetaData(int fieldId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldMetaData All MetaData: {fieldId}"));

            string sql = $@"select growerfields.id,
                                   growerfields.field_name as name,
                                   growerfields.grower_id,
                                   ST_AsText(growerfields.shape) as shape_text,
                                   growerfields.acres,
                                   growerfields.notes,
                                   growerfields.locked,
                                   (select now() from field_year_specific_meta_data where field_id = growerfields.id and idle is true order by year desc limit 1) as idle,
                                   (select idle from field_year_specific_meta_data where field_id = growerfields.id and idle is true order by year desc limit 1) as idle_bool
                            from growerfields
                            where deleted_at is null
                            and growerfields.id = ?";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql, fieldId).ConfigureAwait(false);

            foreach (var result in results)
            {
                result["grower"] = await GetGrower(result["grower_id"].ToString());

                sql = $@"select field_meta_data.id as field_meta_data_id,
                                field_meta_data.year,
                                field_meta_data.planted,
                                field_meta_data.variety,
                                field_meta_data.amount_planted as rhizomes_planted,
                                field_meta_data.source,
                                field_meta_data.traceability_id,
                                field_meta_data.media_id,
                                field_meta_data.amount_planted,
                                field_meta_data.organic,
                                field_meta_data.notes   as year_notes
                            from field_meta_data
                            where deleted_at is null
                            and field_meta_data.field_id = ?
                            order by field_meta_data.year desc";

                var metadataResults = await db.SqlQueryToList(sql, fieldId).ConfigureAwait(false);

                foreach (var metadataResult in metadataResults)
                {
                    metadataResult["media"] = await GetFieldMediaOption(metadataResult["media_id"].ToString());
                }

                result["metadata"] = metadataResults;
                result["active_field_warnings"] = await GetActiveFieldWarnings(fieldId);
            }

            return results;
        }

        private async Task<IEnumerable> GetUser(int userId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetUser: {userId}"));

            // Raw SQL statement
            string sql = @"SELECT id,
                                first_name,
                                last_name,
                                position,
                                email,
                                phone,
                                last_web_login,
                                last_mobile_login,
                                created_at,
                                deleted_at FROM users where id = ?";

            //use SqlQueryToList to execute query and return results
            var users = await db.SqlQueryToList(sql, userId).ConfigureAwait(false);

            return users.FirstOrDefault();
        }

        private async Task<IEnumerable> GetFieldMediaOption(string mediaId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetFieldMediaOption: {mediaId}"));

            // Raw SQL statement
            string sql = @"SELECT id, name FROM planting_media where id = ?";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, mediaId).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetGrowerSubAreasGeojson(string growerId, int SubAreaTypeId, int year,
            string center = null, double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerSubAreasGeojson: {growerId} {year}"));

            string growerSubAreaFilterSql = "";
            string featureSubAreaFilterSql = "";

            if (!string.IsNullOrEmpty(growerId))
            {
                growerSubAreaFilterSql = $" and growers.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featureSubAreaFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(shape), 0), ST_GeomFromText('POINT({center})', 0)) < {radius ?? FilterFeatureRadius} ";
            }


            string sql = $@"select subareas.shape,
                                   subareas.id,
                                   sub_area_types.name  as title,
                                   subareas.notes       as description,
                                   sub_area_types.color as fill,
                                   sub_area_types.color as stroke
                            from (
                                     SELECT sub_areas.id,
                                            ST_AsGeoJSON(sub_areas.shape) as shape,
                                            sub_areas.grower_id,
                                            sub_areas.notes,
                                            sub_areas.sub_area_type_id
                                     FROM sub_areas
                                              JOIN growers on sub_areas.grower_id = growers.id
                                     WHERE sub_areas.crop_year = ?
                                       {growerSubAreaFilterSql}
                                       {featureSubAreaFilterSql}
                                        and sub_areas.deleted_at is null
                                 ) as subareas
                                     join sub_area_types on subareas.sub_area_type_id = sub_area_types.id
                                     where sub_area_types.id = ?;";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results =
                await db.SqlQueryToList(sql, year, SubAreaTypeId).ConfigureAwait(false);
            List<Dictionary<string, object>> features = new List<Dictionary<string, object>>();
            foreach (var result in results)
            {
                if (result["shape"] != null && !result["shape"].ToString().IsNullOrEmpty())
                {
                    Dictionary<string, object> feature = new Dictionary<string, object>();
                    feature["type"] = "Feature";
                    feature["properties"] = result;
                    feature["geometry"] = JObject.Parse(result["shape"].ToString());
                    result.Remove("shape");
                    features.Add(feature);
                }
            }

            Dictionary<string, object> geojson = new Dictionary<string, object>();
            geojson["type"] = "FeatureCollection";
            geojson["features"] = features;

            return geojson;
        }

        private async Task<List<Dictionary<string, object>>> GetGrowerFieldsGeojsonData(string growerId,
            string center = null, string geometryType = null, string varietyId = null, double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldsGeojsonData: {growerId} {varietyId} {geometryType} {center}"));

            string growerFieldFilterSql = "";
            string featureFieldFilterSql = "";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerFieldFilterSql = $" and vwSimplifiedGrowerfields.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featureFieldFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(vwSimplifiedGrowerfields.shape), 0), ST_GeomFromText('POINT({center})',0))  < {radius ?? FilterFeatureRadius} ";
            }

            string sql = $@"select fields.shape,
                                   fields.x,
                                   fields.y,
                                   fields.id,
                                   fields.field_name                                as title,
                                   concat(growers.name, ' - ', fields.field_name)   as description,
                                   growers.name                                     as grower_name,
                                   round(fields.acres, 2)                           as acres,
                                   fields.idle                                      as idle,
                                   CASE
                                       WHEN fields.idle is true then 'idle'
                                       WHEN fields.fallow is true then 'fallow'
                                       ELSE fields.variety
                                    END                                             as variety,
                                   ifnull(global_qc.variety_name,fields.variety)    as variety_name,
                                   map_variety_styles.color                         as fill,
                                   map_variety_styles.color                         as stroke
                            from (
                                     SELECT vwSimplifiedGrowerfields.id,
                                            ST_AsGeoJSON(vwSimplifiedGrowerfields.shape)      as shape,
                                            vwSimplifiedGrowerfields.shape                    as raw_shape,
                                            ST_x(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape)))) as x,
                                            ST_y(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape)))) as y,
                                            vwSimplifiedGrowerfields.field_name,
                                            vwSimplifiedGrowerfields.grower_id,
                                            vwSimplifiedGrowerfields.acres,
                                            ifnull((select variety from field_meta_data where field_meta_data.deleted_at is null and field_meta_data.field_id = vwSimplifiedGrowerfields.id order by year desc limit 1),
                                               (select group_concat(distinct ifnull(variety_id, variety_other))
                                                from growerlots
                                                         left join field_lot on field_lot.lot_id = growerlots.id
                                                where field_lot.field_id = vwSimplifiedGrowerfields.id
                                                group by field_lot.field_id, crop_year
                                                order by crop_year desc
                                                   limit 1))  as variety,                                 
                                            (select idle from field_year_specific_meta_data where field_year_specific_meta_data.field_id = vwSimplifiedGrowerfields.id order by year desc limit 1) as idle,
                                            (select fallow from field_year_specific_meta_data where field_year_specific_meta_data.field_id = vwSimplifiedGrowerfields.id order by year desc limit 1) as fallow
                                        FROM vwSimplifiedGrowerfields
                                              left join field_meta_data
                                                        on vwSimplifiedGrowerfields.id = field_meta_data.field_id
                                              left join field_lot on vwSimplifiedGrowerfields.id = field_lot.field_id
                                              left join growerlots on growerlots.id = field_lot.lot_id
                                     WHERE ST_IsValid(ST_GeomFromText(ST_AsText(vwSimplifiedGrowerfields.shape)))
                                       {growerFieldFilterSql}
                                       {featureFieldFilterSql}
                                       and vwSimplifiedGrowerfields.deleted_at is null
                                     group by vwSimplifiedGrowerfields.id
                                     ORDER BY vwSimplifiedGrowerfields.id ASC
                                 ) as fields
                                     join growers on fields.grower_id = growers.grower_id
                                     left join map_variety_styles on fields.variety = map_variety_styles.variety_code
                                     left join global_qc on global_qc.variety_id = fields.variety
                            where ST_IsValid(ST_GeomFromText(ST_AsText(raw_shape))) = 1" +
                         (!string.IsNullOrEmpty(varietyId) ? " and fields.variety = ?" : "")
                         + ";";

            List<Dictionary<string, object>> fieldData = await db.SqlQueryToList(sql, varietyId).ConfigureAwait(false);

            foreach (var field in fieldData)
            {
                Dictionary<string, object> fieldMetadata =
                    (await GetGrowerFieldMetaData(Convert.ToInt32(field["id"].ToString()))).FirstOrDefault();

                field["planting_media"] = null;
                field["planted_date"] = null;

                if (fieldMetadata != null)
                {
                    List<Dictionary<string, object>>
                        meta = (List<Dictionary<string, object>>)fieldMetadata["metadata"];
                    if (meta.Any())
                    {
                        Dictionary<string, object> recentMetaData = meta.FirstOrDefault();
                        if (recentMetaData != null)
                        {
                            field["planted_date"] = recentMetaData["planted"];

                            Dictionary<string, object> media =
                                ((List<Dictionary<string, object>>)recentMetaData["media"]).FirstOrDefault();
                            if (media != null)
                            {
                                field["planting_media_name"] = media["name"];
                            }
                        }

                        field["metadata"] = meta;
                    }
                }
            }

            // use SqlQueryToList to execute query and return results
            return fieldData;
        }

        public async Task<Dictionary<string, object>> GetGrowerFieldsGeojson(string growerId,
            string varietyId = null, string geometryType = null, string center = null, double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldsGeojson: {growerId} {varietyId} {geometryType} {center}"));

            Dictionary<string, object> growerProperties = await GetGrowerFieldsProperties(growerId);

            List<Dictionary<string, object>> results =
                await GetGrowerFieldsGeojsonData(growerId, center, geometryType, varietyId, radius);

            return FieldsToGeojson(results, geometryType, growerProperties);
        }

        private async Task<Dictionary<string, object>> GetGrowerFieldsGeojsonByVariety(string growerId,
            string geometryType = "polygon", string center = null, double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldsGeojsonByVariety: {growerId} {geometryType} {center}"));

            List<Dictionary<string, object>> results =
                await GetGrowerFieldsGeojsonData(growerId, center, geometryType, radius: radius);


            var varieties = results.Select(field => field["variety"].ToString()).Distinct();

            var growerProperties = await GetGrowerFieldsProperties(growerId);
            Dictionary<string, object> varietyFields = new Dictionary<string, object>();
            foreach (var variety in varieties)
            {
                List<Dictionary<string, object>> fields = results.Where(x => x["variety"].ToString() == variety)
                    .ToList();

                varietyFields[variety] = FieldsToGeojson(fields, geometryType, growerProperties);
            }

            return varietyFields;
        }

        private Dictionary<string, object> FieldsToGeojson(List<Dictionary<string, object>> results,
            string geometryType, Dictionary<string, object> properties)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"FieldsToGeojson: {results.Count} {geometryType}"));
            List<Dictionary<string, object>> features = new List<Dictionary<string, object>>();
            Dictionary<string, object> geojson = new Dictionary<string, object>();

            if (results.Any())
            {
                List<Dictionary<string, object>> bounds =
                    (List<Dictionary<string, object>>)properties.Where(x => x.Key == "bounds").FirstOrDefault().Value;
                var data = bounds.FirstOrDefault();

                var bbox = new List<float>();
                bbox.Add(float.Parse(data["min_y"].ToString()));
                bbox.Add(float.Parse(data["min_x"].ToString()));

                bbox.Add(float.Parse(data["max_y"].ToString()));
                bbox.Add(float.Parse(data["max_x"].ToString()));

                foreach (var result in results)
                {
                    if (result["shape"] != null && !result["shape"].ToString().IsNullOrEmpty())
                    {
                        Dictionary<string, object> feature = new Dictionary<string, object>();
                        JObject geometry = null;
                        if (geometryType == "point")
                        {
                            geometry = new JObject();
                            geometry.Add("type", "Point");
                            geometry.Add("coordinates", new JArray()
                            {
                                result["x"], result["y"]
                            });
                        }
                        else
                        {
                            geometry = JObject.Parse(result["shape"].ToString());
                        }

                        feature["type"] = "Feature";
                        feature["bbox"] = bbox;
                        feature["geometry"] = geometry;
                        result.Remove("shape");
                        feature["properties"] = result;
                        features.Add(feature);
                    }
                }

                geojson["type"] = "FeatureCollection";
                geojson["bbox"] = bbox;
                geojson["properties"] = properties;
                geojson["features"] = features;
            }

            return geojson;
        }

        private async Task<Dictionary<string, object>> GetGrowerFieldsProperties(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldsProperties: {growerId}"));

            string growerFieldVarietyFilterSql = " ";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerFieldVarietyFilterSql = $" and vwSimplifiedGrowerfields.grower_id = '{growerId}' ";
            }

            string sql = @$"SELECT
                                   min(ST_x(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape))))) as min_x,
                                   min(ST_y(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape))))) as min_y,
                                   max(ST_x(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape))))) as max_x,
                                   max(ST_y(ST_centroid(st_geometryfromtext(st_astext(vwSimplifiedGrowerfields.shape))))) as max_y
                            FROM vwSimplifiedGrowerfields
                            WHERE ST_IsValid(ST_GeomFromText(ST_AsText(vwSimplifiedGrowerfields.shape)))
                                {growerFieldVarietyFilterSql}
                            group by vwSimplifiedGrowerfields.grower_id";

            // use SqlQueryToList to execute query and return results
            var bounds = await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);

            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties["bounds"] = bounds;

            return properties;
        }

        public async Task<IEnumerable> GetGrowerPoints(string growerId, int pointTypeId, int year, string center = null,
            double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPoints: {growerId}"));

            string growerPointFilterSql = "";
            string featurePointFilterSql = "";
            if (!string.IsNullOrEmpty(growerId))
            {
                growerPointFilterSql = $" and growers.grower_id = '{growerId}' ";
            }

            if (!string.IsNullOrEmpty(center))
            {
                featurePointFilterSql =
                    $" and ST_Distance(ST_GeomFromText(ST_AsText(map_points.location), 0), ST_GeomFromText('POINT({center})', 0))  < {radius ?? FilterFeatureRadius}";
            }


            string sql = $@"select ST_AsGeoJSON(map_points.location)                     as shape,
                                   map_points.id,
                                   map_points.created_at,
                                   map_points.name                                       as title,
                                   concat(ifnull(growerfields.field_name,growers.name), ' ', map_points.name) as description,
                                   map_point_types.icon                                  as icon,
                                   map_point_types.color                                 as fill,
                                   map_point_types.color                                 as stroke
                            from map_points
                                     join map_point_types on map_points.type_id = map_point_types.id
                                     left join growerfields on map_points.field_id = growerfields.id
                                     left join growers on map_points.grower_id = growers.id
                            where  map_points.deleted_at is null
                              and map_points.type_id = ?
                              and map_points.year = ?
                              {growerPointFilterSql}
                              {featurePointFilterSql}
                              order by created_at desc;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, pointTypeId, year).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerPointsGeojson(string growerId, int pointTypeId, int year,
            string center = null, double? radius = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerPointsGeojson: {growerId} {pointTypeId} {year} {center}"));

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results =
                (List<Dictionary<string, object>>)await GetGrowerPoints(growerId, pointTypeId, year, center, radius)
                    .ConfigureAwait(false);

            List<Dictionary<string, object>> features = new List<Dictionary<string, object>>();
            foreach (var result in results)
            {
                if (result["shape"] != null && !result["shape"].ToString().IsNullOrEmpty())
                {
                    Dictionary<string, object> feature = new Dictionary<string, object>();

                    feature["type"] = "Feature";
                    feature["geometry"] = JObject.Parse(result["shape"].ToString());
                    result.Remove("shape");
                    feature["properties"] = result;
                    features.Add(feature);
                }
            }

            Dictionary<string, object> geojson = new Dictionary<string, object>();
            geojson["type"] = "FeatureCollection";
            geojson["features"] = features;

            return geojson;
        }

        public async Task<IEnumerable> GetGrowerMobileConfig(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerMobileConfig: {growerId}"));

            string growersSql = @"select growers.id,
                                   growers.name,
                                   growers.grower_id,
                                   growers.state,
                                   group_concat(distinct hgas.hga_id)     as hgas
                            from growers
                                     left join grower_hga on growers.id = grower_hga.grower_id
                                     left join hgas on grower_hga.hga_id = hgas.id
                            where growers.grower_id = ?
                            group by growers.id;";
            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> growers =
                await db.SqlQueryToList(growersSql, growerId).ConfigureAwait(false);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (var grower in growers)
            {
                Dictionary<string, object> growerData = new Dictionary<string, object>();

                growerData["id"] = grower["id"].ToString();
                growerData["name"] = grower["name"].ToString();
                growerData["state"] = grower["state"].ToString();
                growerData["hga"] = grower["hgas"].ToString().Split(',');
                growerData["fields"] =
                    (List<Dictionary<string, object>>)await GetGrowerFields(grower["grower_id"].ToString());
                growerData["facilities"] =
                    (List<Dictionary<string, object>>)await GetGrowerFacilities(grower["grower_id"].ToString());
                growerData["spray_licensees"] =
                    (List<Dictionary<string, object>>)await GetGrowerSprayLicensees(growerId);
                growerData["years"] =
                    ((List<Dictionary<string, object>>)await GetAvailableGrowerPortalYears()).Select(x => x["year"]);
                growerData["storage_conditions"] =
                    (List<Dictionary<string, object>>)await GetAvailableGrowerPortalStorageConditions();
                growerData["previous_lot"] = await GetGrowerPreviousLot(growerId);
                growerData["abilities"] = await GetGrowerAbilities(growerId);

                data.Add(growerData);
            }

            return data;
        }

        private async Task<IEnumerable> GetGrowerAbilities(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerAbilities: {growerId}"));

            string sql = @"select permissions.id, permissions.name
                            from model_has_permissions
                                     join growers on growers.id = model_has_permissions.model_id
                                     join permissions on model_has_permissions.permission_id = permissions.id
                            where model_type = 'App\\Models\\Grower'
                              and growers.grower_id = ?;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
        }

        private async Task<String> GetGrowerPreviousLot(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFacilities: {growerId}"));

            string sql = @"select lot_number from growerlots
                            where grower_id = ?
                            and deleted_at is null
                            order by created_at desc
                            limit 1;";

            // use SqlQueryToList to execute query and return results
            var lots = (await db.SqlQueryToList(sql, growerId).ConfigureAwait(false)).Select(x => x["lot_number"]);
            return (lots.Any() ? lots.First().ToString() : "");
        }

        private async Task<IEnumerable> GetGrowerFacilities(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFacilities: {growerId}"));

            string sql =
                @"select facilities.id, facilities.name, facilities.measure_tech_enabled, kiln_fuels.id as kiln_fuel_id, picker_types.id as picker_type_id
                            from facilities
                                     join facility_grower on facilities.id = facility_grower.facility_id
                                     left join kiln_fuels on facilities.kiln_fuel_id = kiln_fuels.id
                                     left join picker_types on facilities.picker_type_id = picker_types.id
                                     join growers on facility_grower.grower_id = growers.id
                            where growers.grower_id = ?                           
                        AND facilities.deleted_at is null";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> facilities =
                await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (var facility in facilities)
            {
                Dictionary<string, object> facilityData = new Dictionary<string, object>();

                facilityData["id"] = facility["id"].ToString();
                facilityData["name"] = facility["name"].ToString();
                facilityData["measure_tech_enabled"] = facility["measure_tech_enabled"];
                facilityData["kiln_fuel"] =
                    ((List<Dictionary<string, object>>)await GetFacilityKilnFuel(facility["kiln_fuel_id"].ToString()))
                    .FirstOrDefault();
                facilityData["picker_type"] =
                    ((List<Dictionary<string, object>>)await GetFacilityPickerType(facility["picker_type_id"]
                        .ToString())).FirstOrDefault();

                data.Add(facilityData);
            }

            return data;
        }

        private async Task<IEnumerable> GetGrowerFacility(int facilityId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFacilities: {facilityId}"));

            string sql =
                @"select facilities.id, 
                           facilities.name, 
                           kiln_fuels.id as kiln_fuel_id, 
                           picker_types.id as picker_type_id
                            from facilities
                                 left join kiln_fuels on facilities.kiln_fuel_id = kiln_fuels.id
                                 left join picker_types on facilities.picker_type_id = picker_types.id
                            where facilities.id = ?                           
                        AND facilities.deleted_at is null";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> facilities =
                await db.SqlQueryToList(sql, facilityId).ConfigureAwait(false);

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (var facility in facilities)
            {
                Dictionary<string, object> facilityData = new Dictionary<string, object>();

                facilityData["id"] = facility["id"].ToString();
                facilityData["name"] = facility["name"].ToString();
                facilityData["kiln_fuel"] =
                    ((List<Dictionary<string, object>>)await GetFacilityKilnFuel(facility["kiln_fuel_id"].ToString()))
                    .FirstOrDefault();
                facilityData["picker_type"] =
                    ((List<Dictionary<string, object>>)await GetFacilityPickerType(facility["picker_type_id"]
                        .ToString())).FirstOrDefault();

                data.Add(facilityData);
            }

            return data.FirstOrDefault();
        }

        private async Task<List<Dictionary<string, object>>> GetFacilityKilnFuel(string kilnFuelId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityKilnFuel: {kilnFuelId}"));

            string sql = @"select id, type
                           from kiln_fuels
                           where id = ?";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, kilnFuelId).ConfigureAwait(false);
        }

        private async Task<List<Dictionary<string, object>>> GetFacilityPickerType(string pickerTypeId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityPickerType: {pickerTypeId}"));

            string sql = @"select id, type 
                           from picker_types
                           where id = ?";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, pickerTypeId).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerFields(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetGrowerFields: {growerId}"));

            string sql = @"select * from ( SELECT growerfields.id,
                                  growerfields.field_name,
                                   ifnull((select variety from field_meta_data where field_meta_data.field_id = growerfields.id order by year desc limit 1),
                                   (select group_concat(distinct ifnull(variety_id, variety_other))
                                    from growerlots
                                             left join field_lot on field_lot.lot_id = growerlots.id
                                    where field_lot.field_id = growerfields.id
                                    group by field_lot.field_id, crop_year
                                    order by crop_year desc
                                       limit 1))  as  predicted_variety,
                                  replace(
                                          replace(
                                                    replace(ST_asText(ST_centroid(st_geometryfromtext(st_astext(shape)))), 'POINT(', '')
                                              , ')', '')
                                      , ' ', ',')
                                                                   as center
                           FROM growerfields
                                    left join field_lot on field_lot.field_id = growerfields.id
                                    left join growerlots on growerlots.id = field_lot.lot_id
                                    left join field_meta_data on field_meta_data.field_id = growerfields.id
                           WHERE (growerfields.shape is null OR ST_IsValid(ST_GeomFromText(ST_AsText(growerfields.shape))))
                             AND growerfields.grower_id = ?
                             AND growerfields.deleted_at is null
                           ORDER BY growerfields.field_name ASC, ifnull(growerlots.crop_year, field_meta_data.year) desc) as fields
                           group by id
                           order by field_name;";
            var fields = await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);

            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();
            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

            foreach (var field in fields)
            {
                var variety = varietyList
                    .FirstOrDefault(x => x.Key.ToString() == field["predicted_variety"].ToString());
                field["predicted_variety_name"] = variety.Value;

                data.Add(field);
            }

            // use SqlQueryToList to execute query and return results
            return data;
        }

        public async Task<IEnumerable> GetFields(string searchTerm, string growerId, string variety,
            string state, int year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFields: {searchTerm} {growerId} {variety} {state} {year}"));

            var whereFilters = new ArrayList();
            var havingFilters = new ArrayList();

            if (!growerId.IsNullOrEmpty())
            {
                whereFilters.Add("growers.grower_id = '" + growerId + "'");
            }

            if (!searchTerm.IsNullOrEmpty())
            {
                whereFilters.Add("field_name like  '%" + searchTerm + "%'");
            }

            if (!variety.IsNullOrEmpty())
            {
                havingFilters.Add("variety like '%" + variety + "%'");
            }

            if (!state.IsNullOrEmpty())
            {
                whereFilters.Add("growers.state = '" + state + "'");
            }


            string sql = @"select growerfields.id,
                          growerfields.locked                                                                                                                                                   as locked,
                           growerfields.field_name                                                                                                                                              as field_name,
                           st_asText(growerfields.shape)                                                                                                                                        as shape_text,
                           acres,
                           (select lot_number
                            from growerlots
                                     left join field_lot on field_lot.lot_id = growerlots.id where field_lot.field_id = growerfields.id
                            order by crop_year desc
                            limit 1)                                                                                                                                               as last_lot_number,
                    
                           group_concat(distinct growers.name)                                                                                                                     as grower_name,
                           (case
                                when shape is not null and st_isvalid(shape) then JSON_EXTRACT(st_asgeojson(st_centroid(st_geomfromtext(st_astext(shape)))), '$.coordinates') end) as center,
                                        ifnull((select variety from field_meta_data where field_meta_data.field_id = growerfields.id order by year desc limit 1),
                                               (select group_concat(distinct ifnull(variety_id, variety_other))
                                                from growerlots
                                                         left join field_lot on field_lot.lot_id = growerlots.id
                                                where field_lot.field_id = growerfields.id
                                                group by field_lot.field_id, crop_year
                                                order by crop_year desc
                                                   limit 1))                                                                                                                       as variety,
                           count(map_points.id)                                                                                                                                    as males
                    from growerfields
                             join growers on growers.grower_id = growerfields.grower_id
                             left join map_points on growerfields.id = map_points.field_id and map_points.type_id = ? and map_points.year = ?
                    
                    where growerfields.deleted_at is null
                    " + (whereFilters.Count > 0
                ? " and " + string.Join("\n and ", whereFilters.Cast<string>().ToArray())
                : null) + @"
                    group by growerfields.id
                    " + (havingFilters.Count > 0
                ? " having " + string.Join("\n and ", havingFilters.Cast<string>().ToArray())
                : null) + @"
                    order by growerfields.grower_id, growerfields.field_name;";

            var maleMapPointTypeId = 1;
            var fields = await db.SqlQueryToList(sql, maleMapPointTypeId, year.ToString()).ConfigureAwait(false);

            var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();
            Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

            foreach (var field in fields)
            {
                var varietyName = varietyList
                    .FirstOrDefault(x => x.Key.ToString() == field["variety"].ToString());
                field["variety_name"] = varietyName.Value;

                data.Add(field);
            }

            // use SqlQueryToList to execute query and return results
            return data;
        }

        private async Task<IEnumerable> GetGrowerSprayLicensees(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerSprayLicensees: {growerId}"));

            string sql = @"select spray_licensees.id,
                                   spray_licensees.grower_id,
                                   spray_licensees.individual_name,
                                   spray_licensees.license_number,
                                   spray_licensees.firm_name
                            from spray_licensees
                                     join growers on spray_licensees.grower_id = growers.id
                            WHERE growers.grower_id = ?
                            AND spray_licensees.deleted_at is null
                            ORDER BY spray_licensees.firm_name, spray_licensees.individual_name ASC;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
        }


        private async Task<IEnumerable> GetAvailableGrowerPortalStorageConditions()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableGrowerPortalStorageConditions"));

            string sql = @"select id, type, notes from storage_conditions;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }


        public async Task<IEnumerable> GetMobileConfig()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetMobileConfig"));

            Dictionary<string, object> config = new Dictionary<string, object>();

            config["content"] =
                (List<Dictionary<string, object>>)await GetContent().ConfigureAwait(false);
            config["regions"] =
                (List<Dictionary<string, object>>)await GetGrowerRegions().ConfigureAwait(false);
            config["years"] =
                ((List<Dictionary<string, object>>)await GetAvailableGrowerPortalYears().ConfigureAwait(false)).Select(
                    x => x["year"]);
            config["storage_conditions"] =
                (List<Dictionary<string, object>>)await GetAvailableGrowerPortalStorageConditions()
                    .ConfigureAwait(false);
            config["map_point_types"] =
                (List<Dictionary<string, object>>)await GetAvailableMapPointTypes().ConfigureAwait(false);
            config["map_area_types"] =
                (List<Dictionary<string, object>>)await GetAvailableMapShapeTypes().ConfigureAwait(false);
            config["measurement_units"] =
                (List<Dictionary<string, object>>)await GetAvailableMeasurementUnits().ConfigureAwait(false);
            config["spray_application_methods"] =
                (List<Dictionary<string, object>>)await GetAvailableSprayApplicationMethods().ConfigureAwait(false);


            var chemicals = (List<Dictionary<string, object>>)await GetAvailableChemicals().ConfigureAwait(false);
            var chemicalTypes =
                (List<Dictionary<string, object>>)await GetAvailableChemicalTypes().ConfigureAwait(false);

            config["chemicalTypes"] = chemicalTypes;

            foreach (var chemical in chemicals)
            {
                chemical["chemical_type_object"] = chemicalTypes
                    .Where(s => s["type"].ToString() == chemical["chemical_type"].ToString()).FirstOrDefault();
            }

            config["chemicals"] = chemicals;

            config["picker_types"] =
                (List<Dictionary<string, object>>)await GetAvailablePickerTypes().ConfigureAwait(false);
            config["kiln_fuels"] =
                (List<Dictionary<string, object>>)await GetAvailableKilnFuels().ConfigureAwait(false);
            config["scouting_report_incident_levels"] =
                (List<Dictionary<string, object>>)await GetScoutingReportIncidentLevels().ConfigureAwait(false);
            config["scouting_report_irrigation_types"] =
                (List<Dictionary<string, object>>)await GetScoutingReportIrrigationTypes().ConfigureAwait(false);
            config["scouting_report_interrow_management"] =
                (List<Dictionary<string, object>>)await GetScoutingReportInterrowManagement().ConfigureAwait(false);
            config["scouting_report_row_directions"] =
                (List<Dictionary<string, object>>)await GetScoutingReportRowDirections().ConfigureAwait(false);
            config["scouting_report_perimeter_management"] =
                (List<Dictionary<string, object>>)await GetScoutingPerimeterManagementOptions().ConfigureAwait(false);
            config["field_media_options"] =
                (List<Dictionary<string, object>>)await GetFieldMediaOptions().ConfigureAwait(false);
            config["facility_assessment_metadata"] =
                (List<Dictionary<string, object>>)await GetFacilityAssessmentMetadata().ConfigureAwait(false);

            return config;
        }

        private async Task<IEnumerable> GetContent()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableMapPointTypes"));

            string sql = @"select `key`, `value` from settings where `key` in ('preharvest_create_notice');";

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> content = await db.SqlQueryToList(sql).ConfigureAwait(false);

            return content;
        }

        private async Task<IEnumerable> GetAvailableMapPointTypes()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableMapPointTypes"));

            string sql =
                @"select id, name, color, `default`, `order`, show_in_menu, clickable, show_labels, icon, allow_overlap from map_point_types
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }


        private async Task<IEnumerable> GetAvailableMapShapeTypes()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableMapPointTypes"));

            string sql =
                @"select id, name, color, `default`, `order`, show_in_menu, clickable, show_labels from sub_area_types
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableMeasurementUnits()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableMeasurementUnits"));

            string sql = @"select id, type from measurement_units;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableSprayApplicationMethods()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableSprayApplicationMethods"));

            string sql = @"select id, name from spray_application_methods
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableChemicals()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableChemicals"));

            string sql =
                @"select id, epa_reg_num, agrian_epa_reg_num, agrian_product_id, common_name, trade_name, chemical_type, label_rate, u_of_m, u_of_m_rate, max_apps_num, max_rate_per_season, interval_days_label, phi_days_label, reentry_hours, organic_certificate, signal_word, cutoff_eu, cutoff_jp, banned_eu, banned_jp, banned_ko, banned_tw, phi_eu, phi_jp, us_mrl, codex_mrl, eu_mrl, jp_mrl, canada_mrl, australia_mrl from chemicals;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableChemicalTypes()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableChemicalTypes"));

            string sql =
                @"select id, type, notes from chemical_types;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailablePickerTypes()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailablePickerTypes"));

            string sql =
                @"select id, 
                         type, 
                         notes, 
                         created_at, 
                         updated_at
                 from picker_types;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableKilnFuels()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAvailableKilnFuels"));

            string sql =
                @"select id, 
                         type, 
                         notes, 
                         created_at, 
                         updated_at
                 from kiln_fuels;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetScoutingReportIncidentLevels()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetScoutingReportIncidentLevels"));

            string sql =
                @"select id, 
                         name
                 from scouting_report_incidence_levels
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetScoutingReportIrrigationTypes()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetScoutingReportIrrigationTypes"));

            string sql =
                @"select id, 
                         name
                 from scouting_report_irrigation_types
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetScoutingReportInterrowManagement()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetScoutingReportInterrowManagement"));

            string sql =
                @"select id, 
                         name
                 from scouting_report_interrow_management
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetScoutingReportRowDirections()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetScoutingReportRowDirections"));

            string sql =
                @"select id, 
                         name
                 from scouting_report_row_directions
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetScoutingPerimeterManagementOptions()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetScoutingPerimeterManagementOptions"));

            string sql =
                @"select id, 
                         name
                 from scouting_report_perimeter_management
                where deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetFieldMediaOptions()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFieldMediaOptions"));

            string sql =
                @"select id, 
                         name
                 from planting_media;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        private async Task<IEnumerable> GetAvailableGrowerPortalYears()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetAvailableGrowerPortalYears"));

            string sql = @"select year from years where active = true AND years.deleted_at is null;";

            // use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql).ConfigureAwait(false);
        }

        public async Task<bool> UpdateGrowerPortalLotAnalytics(string lotNumber, decimal? quantityBales,
            decimal? tempMin,
            decimal? tempMax, decimal? moistMin, decimal? moistMax, decimal? uvAlpha, decimal? uvBeta, decimal? hsi,
            decimal? oilByDist, decimal? moistureOven, decimal? oilAPinene, decimal? oilBPinene, decimal? oilMyrcene,
            decimal? oil2MethylButyl, decimal? oilLimonene, decimal? oilMethylHeptonate, decimal? oilMethylOctonoate,
            decimal? oilLinalool, decimal? oilCaryophyllene, decimal? oilFarnesene, decimal? oilHumulene,
            decimal? oilGeraniol, decimal? oilCaryoxide)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"UpdateGrowerPortalLotAnalytics: {lotNumber}"));

            LotQc lotQc = null;
            // Find existing lot
            string sql = "select id from lot_qc where lot = ? limit 1";
            List<Dictionary<string, object>> existingLotQcData =
                await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);
            Dictionary<string, object> existingLotQc = existingLotQcData.FirstOrDefault();
            bool lotExists = existingLotQc != null && !existingLotQc["id"].ToString().IsNullOrEmpty();

            //Use existing lot or create create new lot with lot number
            if (lotExists && uint.TryParse(existingLotQc["id"].ToString(), out uint existingLotId))
            {
                lotQc = db.LotQc.Find(existingLotId);
            }
            else
            {
                lotQc = new LotQc();
                lotQc.Lot = lotNumber;
            }

            lotQc.QtyBales = quantityBales;
            lotQc.TempMin = tempMin;
            lotQc.TempMax = tempMax;
            lotQc.MoistMin = moistMin;
            lotQc.MoistMax = moistMax;
            lotQc.UvAlpha = uvAlpha;
            lotQc.UvBeta = uvBeta;
            lotQc.Hsi = hsi;
            lotQc.OilByDist = oilByDist;
            lotQc.MoistureOven = moistureOven;
            lotQc.OilAPinene = oilAPinene;
            lotQc.OilBPinene = oilBPinene;
            lotQc.OilMyrcene = oilMyrcene;
            lotQc.Oil2MethylButyl = oil2MethylButyl;
            lotQc.OilLimonene = oilLimonene;
            lotQc.OilMethylHeptonate = oilMethylHeptonate;
            lotQc.OilMethylOctonoate = oilMethylOctonoate;
            lotQc.OilLinalool = oilLinalool;
            lotQc.OilCaryophyllene = oilCaryophyllene;
            lotQc.OilFarnesene = oilFarnesene;
            lotQc.OilHumulene = oilHumulene;
            lotQc.OilGeraniol = oilGeraniol;
            lotQc.OilCaryoxide = oilCaryoxide;

            if (lotExists)
            {
                db.LotQc.Update(lotQc);
            }
            else
            {
                await db.LotQc.AddAsync(lotQc);
            }

            Task<int> success = db.SaveChangesAsync();

            // Return boolean on success
            return success.Result == 1;
        }

        public async Task<IEnumerable> GetLotFarmData(params string[] lotNumbers)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetLotFarmData: {($"'{String.Join("','", lotNumbers)}'")}"));

            string sql = $@"SELECT growers.grower_id,
                                   harvest_datasheets.lot_number,
                                   harvest_datasheets.grown_by,
                                   harvest_datasheets.harvest_start_at,
                                   growers.farm_satellite_image,
                                   st_asGeojson(growers.farm_satellite_image_location) as farm_image_center,
                                   growers.about_text,
                                   growers.owned_by,
                                   growers.founded_date
                            FROM harvest_datasheets
                            LEFT JOIN growers ON growers.grower_id = harvest_datasheets.grower_id 
                            WHERE harvest_datasheets.deleted_at IS NULL
                              AND growers.deleted_at IS NULL
                              AND harvest_datasheets.lot_number IN ({string.Join(", ", lotNumbers.Select(s => "?"))});";

            var lotFarms = await db.SqlQueryToList(sql, lotNumbers).ConfigureAwait(false);

            List<JToken> centerCoords = null;
            foreach (var lotFarm in lotFarms)
            {
                if (lotFarm["farm_image_center"] != null &&
                    !String.IsNullOrEmpty(lotFarm["farm_image_center"].ToString()))
                {
                    var geojsonCenter = JObject.Parse(lotFarm["farm_image_center"].ToString());
                    if (geojsonCenter["coordinates"] != null)
                    {
                        centerCoords = geojsonCenter["coordinates"].ToList();
                    }

                    lotFarm["farm_image_center"] = centerCoords;
                }

                lotFarm["certificates"] =
                    await GetGrowerCertificates(lotFarm["grower_id"].ToString()).ConfigureAwait(false);
            }

            return lotFarms;
        }

        public async Task<IEnumerable> GetGrowerCertificates(string growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerCertificates: {growerId}"));

            string sql = $@"SELECT grower_certifications.name,
                                   grower_certifications.logo
                            FROM growers
                            JOIN growers_grower_certification ON growers.id = growers_grower_certification.grower_id
                            JOIN grower_certifications on growers_grower_certification.grower_certification_id = grower_certifications.id
                            WHERE growers.grower_id = '{growerId}';";

            return await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);
        }

        public async Task<string> LotNumberToGrower(string lotNumber)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"LotNumberToGrower: {lotNumber}"));
            string sql =
                $@"SELECT grower_id
                   FROM growerlots
                   WHERE lot_number = ?";

            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, lotNumber).ConfigureAwait(false);

            if (results == null || results.Count == 0)
            {
                try
                {
                    var growerHga = Regex.Replace(lotNumber.Split("-")[1], "[^0-9]", "");
                    sql =
                        $@"SELECT distinct growers.grower_id
                            FROM hgas
                                     join grower_hga on hgas.id = grower_hga.hga_id
                                     join growers on grower_hga.grower_id = growers.id and growers.deleted_at is null
                            WHERE hgas.hga_id = ?";

                    results = await db.SqlQueryToList(sql, growerHga).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                        $"Unable to extract hga from: {lotNumber}"));
                }
            }


            if (results != null && results.Count > 0)
            {
                return results.First()["grower_id"].ToString();
            }

            throw new ApiValidationException("Lot Number", lotNumber, $"No grower found for Lot Number {lotNumber}");
        }

        public async Task<List<Dictionary<string, object>>> GetFieldNamesFromFieldIds(string[] fieldIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFieldNames: {($"'{String.Join("','", fieldIds)}'")}"));

            string sql = $@"select id, field_name 
                           from growerfields
                           where id in ({string.Join(", ", fieldIds.Select(s => "?"))})";

            return await db.SqlQueryToList(sql, fieldIds).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetHarvestWindows(string state, int cropYear)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetHarvestWindows: {state}, {cropYear}"));

            string sql = $@"select id,
                                   variety_code,
                                   state,
                                   window_opened,
                                   window_closed,
                                   created_at,
                                   updated_at,
                                   deleted_at
                            from harvest_windows
                            where deleted_at is null and state = ? and crop_year = ?
                            order by window_opened, window_closed desc";

            var windows = await db.SqlQueryToList(sql, state, cropYear).ConfigureAwait(false);

            foreach (var window in windows)
            {
                var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

                Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

                window["variety_name"] = varietyList
                    .Where(s => s.Key != null && s.Key.ToString().StartsWith(window["variety_code"].ToString()))
                    .FirstOrDefault().Value;
            }

            return windows;
        }

        public async Task<IEnumerable> GetGrowerHarvestExceptionRequests(string growerId, int cropYear)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerHarvestExceptionRequests: {growerId}, {cropYear}"));

            string sql = $@"select harvest_exception_requests.id,
                                   growers.name as grower_name,
                                   growerfields.field_name,
                                   growerfields.standardized_name as standardized_field_name,
                                   harvest_exception_requests.variety_id as variety_code,
                                   harvest_exception_requests.requested_date,
                                   harvest_exception_requests.approved,
                                   harvest_exception_requests.rejected,
                                   harvest_exception_requests.grower_comments,
                                   concat(created_by_user.first_name, ' ', created_by_user.last_name) as created_by_name,
                                   concat(completed_by_user.first_name, ' ', completed_by_user.last_name) as completed_by_name,
                                   harvest_exception_requests.created_at,
                                   harvest_exception_requests.updated_at,
                                   harvest_exception_requests.deleted_at,
                                   harvest_windows.window_opened,
                                   harvest_windows.window_closed
                            from harvest_exception_requests
                                     left join growers on harvest_exception_requests.grower_id = growers.id
                                     left join growerfields on harvest_exception_requests.field_id = growerfields.id
                                     left join users created_by_user on harvest_exception_requests.created_by = created_by_user.id
                                     left join users completed_by_user on harvest_exception_requests.completed_by = completed_by_user.id
                                     left join harvest_windows on growers.state = harvest_windows.state
                                                              and harvest_exception_requests.variety_id = harvest_windows.variety_code
                                                              and harvest_windows.crop_year = ?
                            where growers.grower_id = ?
                                and harvest_exception_requests.deleted_at is null 
                                and harvest_exception_requests.requested_date >= '{cropYear}-01-01'
                                and harvest_exception_requests.requested_date < '{cropYear + 1}-01-01'";

            var requests = await db.SqlQueryToList(sql, cropYear, growerId).ConfigureAwait(false);

            foreach (var window in requests)
            {
                var varieties = (object[])await pimService.GetVarietyCodeAndNameMap();

                Dictionary<object, object> varietyList = (Dictionary<object, object>)varieties.First();

                window["variety_name"] = varietyList
                    .Where(s => s.Key != null && s.Key.ToString().StartsWith(window["variety_code"].ToString()))
                    .FirstOrDefault().Value;
            }

            return requests;
        }

        public async Task<IEnumerable> GetGrowerFieldWarnings(string growerId, int? year)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetGrowerFieldWarnings: {growerId} {year}"));

            string sql = $@"select field_warnings.*,
                                   growers.grower_id as grower_code,
                                   growers.name as grower_name,
                                   growerfields.field_name,
                                   chemicals.common_name as chemical_common_name,
                                   chemicals.trade_name as chemical_trade_name,
                                   chemicals.epa_reg_num as chemical_epa_reg_num,
                                   chemicals.chemical_type,
                                   field_warnings.chemical_other
                            from field_warnings
                            left join growers on field_warnings.grower_id = growers.id
                            left join growerfields on field_warnings.field_id = growerfields.id
                            left join chemicals on field_warnings.chemical_id = chemicals.id
                            where field_warnings.deleted_at is null
                                and growers.grower_id = '{growerId}' " +
                         (year != null
                             ? $"and field_warnings.chemical_applied_at >= '{year}-01-01 00:00:00.000' and field_warnings.chemical_applied_at < '{year + 1}-01-01 00:00:00.000'"
                             : "");

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>>
                results = await db.SqlQueryToList(sql).ConfigureAwait(false);

            return results;
        }

        private async Task<List<Dictionary<string, object>>> GetActiveFieldWarnings(int? fieldId = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetActiveFieldWarnings: {fieldId}"));

            string sql = $@"select field_warnings.*,
                                   growers.grower_id as grower_code,
                                   growers.name as grower_name,
                                   growerfields.id as field_id,
                                   growerfields.field_name,
                                   chemicals.common_name as chemical_common_name,
                                   chemicals.trade_name as chemical_trade_name,
                                   chemicals.epa_reg_num as chemical_epa_reg_num,
                                   chemicals.chemical_type,
                                   field_warnings.chemical_other
                            from field_warnings
                            left join growers on field_warnings.grower_id = growers.id
                            left join growerfields on field_warnings.field_id = growerfields.id
                            left join chemicals on field_warnings.chemical_id = chemicals.id
                            where field_warnings.deleted_at is null
                                and date_add(field_warnings.chemical_applied_at, interval field_warnings.reentry_hours hour) >= now() "
                         + (fieldId != null ? $"and field_warnings.field_id = ? " : "");

            // use SqlQueryToList to execute query and return results
            List<Dictionary<string, object>> results = await db.SqlQueryToList(sql, fieldId).ConfigureAwait(false);

            return results;
        }

        private async Task<IEnumerable> GetFacilityAssessmentMetadata()
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityAssessmentMetadata"));

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityAssessmentCategories"));

            string sql =
                @"select id, 
                         name,
                         `order` as 'order'
                 from facility_assessment_categories
                 where deleted_at is null";

            // use SqlQueryToList to execute query and return results
            var categories = await db.SqlQueryToList(sql).ConfigureAwait(false);

            foreach (var category in categories)
            {
                category["groups"] = await GetFacilityAssessmentCategoryGroups(category["id"].ToString());
            }

            return categories;
        }

        private async Task<IEnumerable> GetFacilityAssessmentCategoryGroups(string categoryId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityAssessmentCategoryGroups"));

            string sql =
                @"select id,
                         category_id,
                         name,
                         `order` as 'order',
                         number_questions
                 from facility_assessment_groups
                 where category_id = ? and deleted_at is null";

            // use SqlQueryToList to execute query and return results
            var groups = await db.SqlQueryToList(sql, categoryId).ConfigureAwait(false);

            foreach (var group in groups)
            {
                group["questions"] = await GetFacilityAssessmentGroupQuestions(group["id"].ToString());
            }

            return groups;
        }

        private async Task<IEnumerable> GetFacilityAssessmentGroupQuestions(string groupId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityAssessmentCategoryGroups"));

            string sql =
                @"select id,
                         group_id,
                         text,
                         `order` as 'order',
                         has_notes
                 from facility_assessment_questions
                 where group_id = ? and deleted_at is null";

            // use SqlQueryToList to execute query and return results
            var questions = await db.SqlQueryToList(sql, groupId).ConfigureAwait(false);

            return questions;
        }

        public async Task<IEnumerable> GetUserDraftedFacilityAssessments(int userId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetUserDraftedFacilityAssessments {userId}"));

            string sql =
                @"select id,
                         conducted_by,
                         facility_id,
                         grower_id,
                         score,
                         lead_staff,
                         notes,
                         conducted_at,
                         published,
                         created_at,
                         updated_at
                 from facility_assessments
                 where conducted_by = ? and published is null and deleted_at is null
                 order by created_at desc";

            // use SqlQueryToList to execute query and return results
            var assessments = await db.SqlQueryToList(sql, userId).ConfigureAwait(false);

            var assessmentsObject = await RawFacilityAssessmentsToObject(assessments).ConfigureAwait(false);

            return assessmentsObject;
        }

        public async Task<IEnumerable> GetGrowerCompletedFacilityAssessments(int growerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetUserDraftedFacilityAssessments {growerId}"));

            string sql =
                @"select id,
                         conducted_by,
                         facility_id,
                         grower_id,
                         score,
                         lead_staff,
                         notes,
                         conducted_at,
                         published,
                         created_at,
                         updated_at
                 from facility_assessments
                 where grower_id = ? and published is not null and deleted_at is null
                 order by conducted_at desc";

            // use SqlQueryToList to execute query and return results
            var assessments = await db.SqlQueryToList(sql, growerId).ConfigureAwait(false);

            var assessmentsObject = await RawFacilityAssessmentsToObject(assessments).ConfigureAwait(false);

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, assessmentsObject.ToString()));

            return assessmentsObject;
        }

        private async Task<IEnumerable> RawFacilityAssessmentsToObject(List<Dictionary<string, object>> assessments)
        {
            foreach (var assessment in assessments)
            {
                assessment["conducted_by_user"] =
                    await GetUser(Convert.ToInt32(assessment["conducted_by"])).ConfigureAwait(false);
                assessment["grower"] = await GetGrowerFromPrimaryKey(Convert.ToInt32(assessment["grower_id"]))
                    .ConfigureAwait(false);
                assessment["facility"] =
                    await GetGrowerFacility(Convert.ToInt32(assessment["facility_id"])).ConfigureAwait(false);
                assessment["answers"] = await GetFacilityAssessmentAnswers(Convert.ToInt32(assessment["id"]))
                    .ConfigureAwait(false);
            }

            return assessments;
        }

        private async Task<IEnumerable> GetFacilityAssessmentAnswers(int assessmentId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetUserDraftedFacilityAssessments {assessmentId}"));

            string sql =
                @$"select id,
                          facility_assessment_id,
                          facility_assessment_question_id,
                          answer,
                          notes,
                          created_at,
                          updated_at
                    from facility_assessment_answers
                    where facility_assessment_id = ? and deleted_at is null";

            // use SqlQueryToList to execute query and return results
            var answers = await db.SqlQueryToList(sql, assessmentId).ConfigureAwait(false);

            foreach (var answer in answers)
            {
                answer["image_urls"] = await GetFacilityAssessmentAnswerImages(Convert.ToInt32(answer["id"]));
            }

            return answers;
        }

        private async Task<IEnumerable> GetFacilityAssessmentAnswerImages(int answerId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetFacilityAssessmentAnswerImages {answerId}"));

            string growerPortalBaseUrl = settings[Config.Settings.Api().GrowerPortal().BaseUrl()];

            string sql =
                @$"select replace(concat('{growerPortalBaseUrl}', '/', images.path), 'api/public', 'storage') as imageUrl
                   from imageables
                   left join images on imageables.image_id = images.id
                   where imageables.imageable_id = ? and imageable_type = 'App\\Models\\FacilityAssessmentAnswer'
                     and images.deleted_at is null";

            // use SqlQueryToList to execute query and return results
            var images = await db.SqlQueryToList(sql, answerId).ConfigureAwait(false);
            return images.Select(s => s.First().Value);
        }
    }
}