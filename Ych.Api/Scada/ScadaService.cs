using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Newtonsoft.Json.Linq;
using Ych.Api.Data;
using Ych.Api.Data.Lims.Models;
using Ych.Api.Data.Scada;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Data.Templating;
using Ych.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace Ych.Api.Scada
{
    public enum ScadaSampleType
    {
        Lab = 1,
        Selection,
        Sensory,
        RandD,
        Organic
    }

    /// <summary>
    /// Contract for Scada Service. 
    /// </summary>
    public interface IScadaService : IApiSystemService, IHealthyService
    {
        /// <summary>
        /// Implementation of GetSampleMeta containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetSampleMeta(string sampleId, int identifier);

        /// <summary>
        /// Implementation of GetSampleMeta containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetSampleMeta(string sampleId);

        /// <summary>
        /// Implementation of GetSampleMeta containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetSamples(string sampleId, int identifier, string? sampleTypeString = null);

        /// <summary>
        /// Implementation of GetSampleMeta containing data access using raw SQL.
        /// </summary>
        Task<IEnumerable> GetSamples(string sampleId, string? sampleTypeString = null);
    }

    /// <summary>
    /// Implementation of IScadaService contract. 
    /// </summary>
    public class ScadaService : ApiDataService, IScadaService
    {
        public const string ScadaSystemName = "Scada";
        public override string SystemName => ScadaSystemName;

        protected override ApiDataSource Db => db;

        private ISettingsProvider settings;
        private ScadaDataSource db;
        private ILogWriter log;

        public ScadaService(ISettingsProvider settings, ScadaDataSource db, ILogWriter log)
        {
            this.settings = settings;
            this.db = db;
            this.log = log;
        }

        [Obsolete("GetSampleMeta is deprecated, please use GetSamples instead.")]
        public async Task<IEnumerable> GetSampleMeta(string sampleId)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetSampleMeta {sampleId}"));
            string sql =
                $@"{ScadaQuerySelect()}
                        where UPPER(mes.bar.sample_meta.lot_num) = @sampleId
                        and mes.bar.sample_meta.sample_type_id = {(int)ScadaSampleType.Lab};";

            // use SqlQueryToList to execute query and return results
            var results = await db.SqlQueryToList(sql, new QueryParameter("@sampleId", sampleId.ToUpper()))
                .ConfigureAwait(false);
            return results;
        }

        [Obsolete("GetSampleMeta is deprecated, please use GetSamples instead.")]
        public async Task<IEnumerable> GetSampleMeta(string sampleId, int identifier)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetSampleMeta {sampleId} {identifier}"));
            string sql =
                $@"{ScadaQuerySelect()}
                    where UPPER(mes.bar.sample_meta.lot_num) = @lotNumber 
                    and (mes.bar.sample_meta.truck_num = @identifier or mes.bar.sample_meta.pallet_num = @identifier)
                    and mes.bar.sample_meta.sample_type_id = {(int)ScadaSampleType.Lab};";

            // use SqlQueryToList to execute query and return results
            var results = await db.SqlQueryToList(sql,
                    new QueryParameter("@lotNumber", sampleId.ToUpper()),
                    new QueryParameter("@identifier", identifier)
                )
                .ConfigureAwait(false);
            return results;
        }

        public async Task<IEnumerable> GetSamples(string sampleId, string? sampleTypeString = null)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetSampleMeta {sampleId}"));
            Enum.TryParse(sampleTypeString, true, out ScadaSampleType sampleType);

            string sql = $@"{ScadaQuerySelect()} where UPPER(mes.bar.sample_meta.lot_num) = @sampleId";
            
            if (sampleTypeString != null)
            {
                sql += $@" and mes.bar.sample_meta.sample_type_id = {(int)sampleType};";
            }

            // use SqlQueryToList to execute query and return results
            var results = await db.SqlQueryToList(sql, new QueryParameter("@sampleId", sampleId.ToUpper()))
                .ConfigureAwait(false);
            return results;
        }

        public async Task<IEnumerable> GetSamples(string sampleId, int identifier, string? sampleTypeString)
        {
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetSampleMeta {sampleId}"));
            Enum.TryParse(sampleTypeString, true, out ScadaSampleType sampleType);
            
            string sql = 
                $@"{ScadaQuerySelect()} 
                    where UPPER(mes.bar.sample_meta.lot_num) = @lotNumber
                    and (mes.bar.sample_meta.truck_num = @identifier or mes.bar.sample_meta.pallet_num = @identifier)";
            
            if (sampleTypeString != null)
            {
                sql += $@" and mes.bar.sample_meta.sample_type_id = {(int)sampleType};";
            }

            // use SqlQueryToList to execute query and return results
            var results = await db.SqlQueryToList(sql,
                    new QueryParameter("@lotNumber", sampleId.ToUpper()),
                    new QueryParameter("@identifier", identifier)
                )
                .ConfigureAwait(false);
            return results;
        }

        private static string ScadaQuerySelect()
        {
            return
                $@"SELECT mes.bar.sample_meta.sample_id,
                        mes.bar.lot_sum.tot_bales,
                        mes.bar.bale_rcpt_sum.bales_rcvd as bales_received,
                        mes.bar.sample_meta.prod_type,
                        mes.bar.sample_meta.lot_num,
                        mes.bar.sample_meta.variety,
                        mes.bar.sample_meta.sensory_id,
                        mes.bar.sample_meta.truck_num,
                        mes.bar.sample_meta.pallet_num,
                        mes.bar.sample_type_dim.type_desc,
                        mes.bar.sample_meta.last_samp,
                        mes.bar.sample_meta.moist_min,
                        mes.bar.sample_meta.moist_max,
                        mes.bar.sample_meta.temp_min,
                        mes.bar.sample_meta.temp_max,
                        mes.bar.sample_meta.extraction_num,
                        mes.bar.sample_meta.samp_def,
                        mes.bar.sample_meta.samp_date
                    from mes.bar.sample_meta
                        join mes.bar.sample_type_dim on mes.bar.sample_type_dim.type_id = mes.bar.sample_meta.sample_type_id
                        left join mes.bar.lot_sum on mes.bar.lot_sum.lot_num = mes.bar.sample_meta.lot_num
                        left join mes.bar.bale_rcpt_sum on mes.bar.bale_rcpt_sum.bale_lot_num = mes.bar.sample_meta.lot_num  and mes.bar.bale_rcpt_sum.ship_num = COALESCE(mes.bar.sample_meta.truck_num, mes.bar.sample_meta.pallet_num)";
        }
    }
}