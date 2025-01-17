using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Data.GrowerPortal;
using Ych.Api.Data.GrowerPortal.Models;
using Ych.Api.Logging;
using Ych.Data.Templating;
using Ych.Logging;

namespace Ych.Api.GrowerPortal
{
    /// <summary>
    /// Contract for a DI compatible component/service. DI requires an interface and one or more implementations.
    /// For single implementation contracts, the interface and class can remain in the same file for simplicity.
    /// For interfaces with multiple implementations, the interface and all implementations should exist in separate files.
    /// </summary>
    public interface IExampleGrowerPortalService
    {
        /// <summary>
        /// Implementation of GetHarvestInformationByLot containing examples of data access using raw SQL, EF ORM, and resource templates.
        /// </summary>
        Task<IEnumerable> GetHarvestInformationByLot(string lotNumber);
    }

    /// <summary>
    /// Implementation of IExampleGrowerPortalService contract. Business logic should exist in services and components like these
    /// rather than directly in the Azure functions. These services can then be injected into other services so long as they don't
    /// create circular dependencies.
    /// </summary>
    public class ExampleGrowerPortalService : IExampleGrowerPortalService
    {
        private IResourceQueryService queryService;
        private GrowerPortalDataSource db;
        private ILogWriter log;
        private string logSource => GetType().Name;

        public ExampleGrowerPortalService(IResourceQueryService queryService, GrowerPortalDataSource db, ILogWriter log)
        {
            this.queryService = queryService;
            this.db = db;
            this.log = log;
            ApiResourceTemplates.LoadTemplates(queryService); // Ensure resource templates have been loaded
        }

        public async Task<IEnumerable> GetHarvestInformationByLot(string lotNumber)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"GetHarvestInformationByLot: {lotNumber}"));

            // Validate lot exists in the growerlots table if necessary
            await ValidateLotExists(lotNumber);

            // Uncomment an implementation below to see the different data access behaviors
            return await GetHarvestInformationByLot_RawSql_Object(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_RawSql_Dictionary(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_RawSql_EF(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_Linq_EF(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_Template_EF(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_Template_HarvestDatasheets(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_Template_HarvestInformation(lotNumber).ConfigureAwait(false);
            //return await GetHarvestInformationByLot_Template_QueryService(lotNumber).ConfigureAwait(false);
        }

        /// <summary>
        /// This example shows how to execute a raw SQL query, and manually map the results from a DataReader to a custom object.
        /// This is the lowest level data access method and requires no EF, Linq or templates.
        /// The same result can be achieved with a sproc using the DataSource.ExecuteSprocQuery method.
        /// </summary>
        private async Task<IEnumerable> GetHarvestInformationByLot_RawSql_Object(string lotNumber)
        {
            // Raw SQL statement
            string sql = @"
                SELECT h.grower_id,
                       h.grown_by,
                       h.bale_moisture_low,
                       h.bale_moisture_high,
                       s.type AS storage_conditions,
                       h.cooling_hours_before_baler,
                       h.cooling_hours_in_kiln,
                       h.drying_hours,
                       h.drying_temp_f,
                       h.humidified,
                       h.kiln_depth_in,
                       f.name AS facility_name,
                       h.facility_other,
                       k.type AS kiln_fuel_type,
                       p.type AS picker_type,
                       h.harvest_start_at,
                       h.harvest_end_at
                FROM harvest_datasheets h
                         LEFT JOIN storage_conditions s ON h.bale_storage_conditions = s.id
                         LEFT JOIN facilities f ON h.facility_id = f.id
                         LEFT JOIN kiln_fuels k ON h.kiln_fuel_id = k.id
                         LEFT JOIN picker_types p ON h.picker_type_id = p.id
                WHERE h.lot_number = ?";

            // List to hold our results
            ArrayList results = new ArrayList();

            // Use DataSource.ExecuteSqlQuery, passing in any parameters (or null) and a callback to handle the raw results.
            await db.ExecuteSqlQuery(sql, new object[] { lotNumber }, (reader) =>
            {
                // Iterate through each row of data from the reader
                while (reader.Read())
                {
                    // Manually map db columns to a new dynamic object type.
                    // This same logic can be used to map to a concrete type (i.e. new HarvestInformation)
                    results.Add(new
                    {
                        bale_moisture_low = reader["bale_moisture_low"],
                        bale_moisture_high = reader["bale_moisture_high"],
                        cooling_hours_before_baler = reader["cooling_hours_before_baler"],
                        cooling_hours_in_kiln = reader["cooling_hours_in_kiln"],
                        drying_hours = reader["drying_hours"],
                        drying_temp_f = reader["drying_temp_f"],
                        facility_name = reader["facility_name"],
                        facility_other = reader["facility_other"],
                        grown_by = reader["grown_by"],
                        harvest_end_at = reader["harvest_end_at"],
                        harvest_start_at = reader["harvest_start_at"],
                        humidified = reader["humidified"],
                        kiln_depth_in = reader["kiln_depth_in"],
                        kiln_fuel_type = reader["kiln_fuel_type"],
                        picker_type = reader["picker_type"],
                        storage_conditions = reader["storage_conditions"]
                    });
                }
            });

            // Return our mapped results
            return results;
        }

        /// <summary>
        /// This example shows how to execute a raw SQL query, and turn the results into a generic list of dictionaries.
        /// This is useful if the results match the payload exactly, or if you need to dynamically manipulate the results.
        /// The same result can be achieved with a sproc using the DataSource.ExecuteSprocQuery method.
        /// </summary>
        private async Task<IEnumerable> GetHarvestInformationByLot_RawSql_Dictionary(string lotNumber)
        {
            string sql = @"
                SELECT h.grower_id,
                       h.grown_by,
                       h.bale_moisture_low,
                       h.bale_moisture_high,
                       s.type AS storage_conditions,
                       h.cooling_hours_before_baler,
                       h.cooling_hours_in_kiln,
                       h.drying_hours,
                       h.drying_temp_f,
                       h.humidified,
                       h.kiln_depth_in,
                       f.name AS facility_name,
                       h.facility_other,
                       k.type AS kiln_fuel_type,
                       p.type AS picker_type,
                       h.harvest_start_at,
                       h.harvest_end_at
                FROM harvest_datasheets h
                         LEFT JOIN storage_conditions s ON h.bale_storage_conditions = s.id
                         LEFT JOIN facilities f ON h.facility_id = f.id
                         LEFT JOIN kiln_fuels k ON h.kiln_fuel_id = k.id
                         LEFT JOIN picker_types p ON h.picker_type_id = p.id
                WHERE h.lot_number = ?";

            List<Dictionary<string, object>> results = null;

            await db.ExecuteSqlQuery(sql, new object[] { lotNumber }, (reader) =>
            {
                // Use the IDbReader.ToList extension method to map the results to a list of generic dictionaries.
                results = reader.ToList();
            });

            return results;
        }

        /// <summary>
        /// This example shows how to execute a raw SQL query and use EF to map the results directly to a custom model/view that
        /// matches the schema of the response payload schema.
        /// </summary>
        private async Task<IEnumerable> GetHarvestInformationByLot_RawSql_EF(string lotNumber)
        {
            string sql = @"
                SELECT h.grower_id,
                       h.grown_by,
                       h.bale_moisture_low,
                       h.bale_moisture_high,
                       s.type AS storage_conditions,
                       h.cooling_hours_before_baler,
                       h.cooling_hours_in_kiln,
                       h.drying_hours,
                       h.drying_temp_f,
                       h.humidified,
                       h.kiln_depth_in,
                       f.name AS facility_name,
                       h.facility_other,
                       k.type AS kiln_fuel_type,
                       p.type AS picker_type,
                       h.harvest_start_at,
                       h.harvest_end_at
                FROM harvest_datasheets h
                         LEFT JOIN storage_conditions s ON h.bale_storage_conditions = s.id
                         LEFT JOIN facilities f ON h.facility_id = f.id
                         LEFT JOIN kiln_fuels k ON h.kiln_fuel_id = k.id
                         LEFT JOIN picker_types p ON h.picker_type_id = p.id
                WHERE h.lot_number = {0};";

            // Use a custom EF DbSet to automatically map the raw query results to HarvestInformation
            return await db.HarvestInformation.FromSqlRaw(sql, lotNumber).ToArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This example shows how to build a complex join query using EF Linq methods, and manually map the results
        /// to a custom model/view that matches the response payload schema.
        /// </summary>
        private async Task<IEnumerable> GetHarvestInformationByLot_Linq_EF(string lotNumber)
        {
            // Using Linq query syntax, you can join multiple DbSets together and select a custom result combining fields
            // from all joined tables
            var query = from h in db.HarvestDatasheets.Where(h => h.LotNumber == lotNumber)
                        from s in db.StorageConditions.Where(s => s.Id.ToString() == h.BaleStorageConditions)
                        from f in db.Facilities.Where(f => f.Id == h.FacilityId)
                        from k in db.KilnFuels.Where(k => k.Id == h.KilnFuelId)
                        from p in db.PickerTypes.Where(p => p.Id == h.PickerTypeId)
                        select new HarvestInformation
                        {
                            bale_moisture_low = h.BaleMoistureLow,
                            bale_moisture_high = h.BaleMoistureHigh,
                            cooling_hours_before_baler = h.CoolingHoursBeforeBaler,
                            cooling_hours_in_kiln = h.CoolingHoursInKiln,
                            drying_hours = h.DryingHours,
                            drying_temp_f = h.DryingTempF,
                            facility_name = f.Name,
                            facility_other = h.FacilityOther,
                            grown_by = h.GrownBy,
                            harvest_end_at = h.HarvestEndAt,
                            harvest_start_at = h.HarvestStartAt,
                            humidified = h.Humidified,
                            kiln_depth_in = h.KilnDepthIn,
                            kiln_fuel_type = k.Type,
                            picker_type = p.Type,
                            storage_conditions = s.Type
                        };

            // The query does not actually execute until you call a 'To' method (ToArray, ToList, etc.)
            return await query.ToArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This example shows how to use a ResourceTemplate to generate raw SQL that is then used with EF to 
        /// map the results HarvestInformation models, which can then be manipulated or directly returned.
        /// </summary>
        private async Task<IEnumerable> GetHarvestInformationByLot_Template_EF(string lotNumber)
        {
            // Find the desired resource template by name
            ResourceTemplate template = ResourceTemplate.Find("harvest-datasheets");

            // ResourceTemplate.CompileQuery will generate SQL that can be used in a manual query or with EF.
            string sql = template.CompileQuery(resourceId: lotNumber, view: "harvest-information");

            // You can add debug logging calls to view compiled SQL in the AzureFunctionTools console.
            log.Write(new ApiLogEntry(logSource, LogSeverities.Debug, sql));

            // Use EF to execute the raw SQL and map results to HarvestInformation
            return await db.HarvestInformation.FromSqlRaw(sql, lotNumber).ToArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This example shows how to execute a resource query and get back a list of generic dictionary results.
        /// This example uses the harvest-datasheets resource (a 1:1 table representation) and specifies the 
        /// harvest-information view to combine columns from other resources and customize the resulting object schema.
        /// </summary>
        public async Task<IEnumerable> GetHarvestInformationByLot_Template_HarvestDatasheets(string lotNumber)
        {
            // Use DataSource.ExecuteResourceQuery to execute a query directly against that DataSource. The requested resources
            // must exist in that DataSource or the query will fail. To dynamically execute a resource query against one or more 
            // DataSources, use IResourceQueryService.ExecuteResourceQuery.
            return await db.ExecuteResourceQuery(new ResourceQuery
            {
                Resource = "harvest-datasheets",
                //ResourceId = lotNumber, // Will produce same results as the lot-number filter, can use either.
                View = "harvest-information", // This view will customize the results, adding/removing columns and joining other resources.
                Include = null, // Can also manually include other resources/views. Included columns will be merged with the main result object.
                Attach = null, // Can also attach other resources/views. Attached resources will be added as a property to the main result object.
                Filters = new ResourceQueryFilter[] // Filters to refine the results.
                {
                    new ResourceQueryFilter
                    {
                        FilterName = "lot-number",
                        Value = lotNumber
                    }
                }
            });
        }

        /// <summary>
        /// This example shows how to use a resource that is not a 1:1 mapping to a table. 
        /// It creates the harvest-information view from harvest-datasheets as a top level resource that can be queried.
        /// It also defines the primaryKey as lot-number to allow for more natural querying.
        /// </summary>
        public async Task<IEnumerable> GetHarvestInformationByLot_Template_HarvestInformation(string lotNumber)
        {
            return await db.ExecuteResourceQuery(new ResourceQuery
            {
                Resource = "harvest-information", // harvest-information is a resource based on the harvest_datasheets table. 
                ResourceId = lotNumber, // lot-number has been mapped as the primaryKey for this view to allow more natural querying
                View = null, // No view needs to be specified here, this resource is already the view we are after.
                Filters = null // No filters required, ResourceId will get us the record we are after
            });
        }

        /// <summary>
        /// This example shows how to use the IResourceQueryService to execute resource query across DataSources. 
        /// bale-inventories is an X3 resource, so the ResourceQueryService will break this into 2 queries and
        /// combine the results based on relationship key bindings.
        /// </summary>
        public async Task<IEnumerable> GetHarvestInformationByLot_Template_QueryService(string lotNumber)
        {
            return await queryService.ExecuteResourceQuery(new ResourceQuery
            {
                Resource = "harvest-information", // harvest-information is a resource based on the harvest_datasheets table. 
                ResourceId = lotNumber, // lot-number has been mapped as the primaryKey for this view to allow more natural querying
                Attach = new string[] { "bale-inventories" }
            });
        }

        /// <summary>
        /// Helper function to throw an exception if the specified lot does not exist (in the growerlots table).
        /// This type of validation is system specific so may not belong on the same IValidationService as request parameter
        /// validation. It could live here, or maybe it belongs on a GrowerPortal specidic validation component that can be shared?
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        private async Task ValidateLotExists(string lotNumber)
        {
            if (!await LotExists(lotNumber))
            {
                throw new ApiException($"Lot {lotNumber} does not exist.", ApiErrorCode.InvalidParameter_0x7100, ApiResponseCodes.InvalidParameter);
            }
        }

        /// <summary>
        /// Helper function to check for the existence of a GrowerLot
        /// </summary>
        /// <param name="lotNumber"></param>
        /// <returns></returns>
        private async Task<bool> LotExists(string lotNumber)
        {
            return await db.Growerlots.FirstOrDefaultAsync(s => s.LotNumber == lotNumber).ConfigureAwait(false) != null;
        }
    }
}
