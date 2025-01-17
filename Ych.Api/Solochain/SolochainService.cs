using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Data.Solochain;
using Ych.Api.Data.Solochain.Views;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Logging;

namespace Ych.Api.Solochain
{
    /// <summary>
    /// Contract for a DI compatible component/service. DI requires an interface and one or more implementations.
    /// For single implementation contracts, the interface and class can remain in the same file for simplicity.
    /// For interfaces with multiple implementations, the interface and all implementations should exist in separate files.
    /// </summary>
    public interface ISolochainService : IHealthyService
    {
        Task<IEnumerable> GetLotLocations(int year);
        Task<IEnumerable> GetSingleLotLocation(string lotNumber);
        Task<IEnumerable> GetGrowerDeliveries(string growerId, int year, string status);
        Task<GrowerAllWmsDelivery[]> GetGenericReceiptsWms(int year);
        Task<IEnumerable> GetMultiLotIsValidWms(string[] lotNumberList);
    }

    /// <summary>
    /// Implementation of ISolochainService contract. Business logic should exist in services and components like these
    /// rather than directly in the Azure functions. These services can then be injected into other services so long as they don't
    /// create circular dependencies.
    /// </summary>
    public class SolochainService : ApiDataService, ISolochainService
    {
        public const string SolochainSystemName = "Solochain";
        public override string SystemName => SolochainSystemName;

        protected override ApiDataSource Db => db;

        private SolochainDataSource db;
        private ILogWriter log;

        public SolochainService(SolochainDataSource db, ILogWriter log)
        {
            this.db = db;
            this.log = log;
        }

        public async Task<IEnumerable> GetLotLocations(int year)
        {
            return await db.SprocQueryToList("API_LIVE_LotLocations", new QueryParameter[] { new QueryParameter { Name = "crpyr", Value = year } }).ConfigureAwait(false);
        }
        
        public async Task<IEnumerable> GetSingleLotLocation(string lotNumber)
        {
            return await db.SprocQueryToList("API_LIVE_SingleLotLocation", new QueryParameter[] { new QueryParameter { Name = "lotNumber", Value = lotNumber } }).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetGrowerDeliveries(string growerId, int year, string status)
        {
            string storedProc = "";
            string[] parameters;

            // TEMPORARY until stored procedure is updated
            switch (growerId)
            {

                case "SEL001":
                    storedProc = "API_LIVE_DeliveryByLot_SBG";
                    parameters = new string[] { year.ToString() };
                    break;
                case "VIR001":
                    storedProc = "API_LIVE_DeliveryByLot_VGF";
                    parameters = new string[] { year.ToString() };
                    break;
                default:
                    storedProc = "API_LIVE_DeliveryByLot"; 
                    parameters = new string[] { growerId, year.ToString() };
                    break;
            }

            // Creates a parameter list for the end of the sql command ex {0}, {1}
            string parameterIndexList = string.Join(", ", parameters.Select((s, i) => "{" + i + "}"));

            IEnumerable results;

            if (status != null && status.ToUpper() == "OPEN")
            {
                results = await db.GrowerOpenDeliveries.FromSqlRaw("exec " + storedProc + " " + parameterIndexList, parameters).ToArrayAsync().ConfigureAwait(false);
            }
            else
            {
                results = await db.GrowerAllWmsDeliveries.FromSqlRaw("exec " + storedProc + "_All " + parameterIndexList, parameters).ToArrayAsync().ConfigureAwait(false);
            }

            return results;
        }

        public async Task<GrowerAllWmsDelivery[]> GetGenericReceiptsWms(int year)
        {
            return await db.GrowerAllWmsDeliveries.FromSqlRaw("exec API_LIVE_DeliveryByLot_AllGrowers {0}", year).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetMultiLotIsValidWms(string[] lotNumberList)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            foreach(string lot in lotNumberList)
            {
                List<Dictionary<string, object>> resultList = await db.SprocQueryToList("API_LIVE_LotValid", new QueryParameter[] { new QueryParameter { Name = "lot", Value = lot } }).ConfigureAwait(false);
                results.Add(resultList.First());
            }

            return results;
        }
    }
}