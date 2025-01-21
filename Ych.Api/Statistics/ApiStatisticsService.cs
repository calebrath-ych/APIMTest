using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Logging;

namespace Ych.Api.Statistics
{
    public interface IApiStatisticsService
    {
        Task<ApiRequestStatistics[]> GetRequestStatistics(string type = null, string name = null);
        void IncrementRequestStatistics(string operationName, string route);
        void IncrementValidationStatistics(string operationName, string route);
        void IncrementErrorStatistics(string operationName, string route);
    }

    public class ApiStatisticsService : ApiDataService, IApiStatisticsService
    {
        public const string ApiStatisticsSystemName = "ApiStatistics";
        public override string SystemName => ApiStatisticsSystemName;

        // These are config driven and only need to be set once since settings changes cause app recycling
        private static string databaseTableName;
        private static Dictionary<string, string> apiLookup;

        protected override ApiDataSource Db => db;

        private ApiStatisticsDataSource db;
        private ILogWriter log;
        private string logSource => GetType().Name;
        private bool isDisabled;

        public ApiStatisticsService(ApiStatisticsDataSource db, ILogWriter log, ISettingsProvider settings)
        {
            this.db = db;
            this.log = log;

            if (!settings.TryGetValue(Config.Settings.Api().RequestStatistics().Disabled(), out isDisabled))
            {
                // Default to enabled, should never be disabled in prod
                isDisabled = false; 
            }

            if (!isDisabled)
            {
                if (databaseTableName == null)
                {
                    // Allow this to be overridden per environment in case they exist on the same DB
                    if (!settings.TryGetValue(Config.Settings.Api().RequestStatistics().TableName(), out databaseTableName))
                    {
                        // Default table name in case we don't need to configure it
                        databaseTableName = "ApiRequestStatistics";
                    }
                }

                if (apiLookup == null)
                {
                    if (settings.TryGetValue(Config.Settings.Api().LookupTable(), out string apiLookupString))
                    {
                        try
                        {
                            apiLookup = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiLookupString);
                        }
                        catch (Exception ex)
                        {
                            isDisabled = true;

                            log.Write(new ApiLogEntry
                            {
                                Message = $"Failed to parse ApiLookupTable from AppSettings: {ex.Message}",
                                Severity = LogSeverities.Error,
                                Source = logSource
                            });
                        }
                    }
                    else
                    {
                        isDisabled = true;

                        log.Write(new ApiLogEntry
                        {
                            Message = $"AppSetting ApiLookupTable is missing and request statistics will not function.",
                            Severity = LogSeverities.Error,
                            Source = logSource
                        });
                    }
                }
            }
        }

        public async Task<ApiRequestStatistics[]> GetRequestStatistics(string api = null, string operation = null)
        {
            if (string.IsNullOrWhiteSpace(api))
            {
                api = null;
            }

            if (string.IsNullOrWhiteSpace(operation))
            {
                operation = null;
            }

            return await db.ApiRequestAnalytics.Where(s =>
                (api == null || s.Api == api) &&
                (operation == null || s.Operation == operation)).ToArrayAsync().ConfigureAwait(false);
        }

        public void IncrementRequestStatistics(string operation, string route)
        {
            if (isDisabled)
            {
                return;
            }

            try
            {
                IncrementAnalytics(operation, GetApiName(route), "Requests");
            }
            catch (Exception ex)
            {
                log.Write(new ApiLogEntry
                {
                    Message = $"Unhandled exception in {nameof(IncrementRequestStatistics)}.",
                    Exception = ex,
                    Severity = LogSeverities.Error,
                    Source = logSource
                });
            }
        }

        public void IncrementErrorStatistics(string operation, string route)
        {
            if (isDisabled)
            {
                return;
            }

            try
            {
                IncrementAnalytics(operation, GetApiName(route), "Errors");
            }
            catch (Exception ex)
            {
                log.Write(new ApiLogEntry
                {
                    Message = $"Unhandled exception in {nameof(IncrementErrorStatistics)}.",
                    Exception = ex,
                    Severity = LogSeverities.Error,
                    Source = logSource
                });
            }
        }

        public void IncrementValidationStatistics(string operation, string route)
        {
            if (isDisabled)
            {
                return;
            }

            try
            {
                IncrementAnalytics(operation, GetApiName(route), "ValidationFailures");
            }
            catch (Exception ex)
            {
                log.Write(new ApiLogEntry
                {
                    Message = $"Unhandled exception in {nameof(IncrementValidationStatistics)}.",
                    Exception = ex,
                    Severity = LogSeverities.Error,
                    Source = logSource
                });
            }
        }

        private void IncrementAnalytics(string operationName, string apiName, string column)
        {
            // Run in a task so we don't block the API thread and slow down API calls
            Task.Run(async () =>
            {
                try
                {
                    // Stats roll over daily so only upsert stats for current day
                    DateTime startDate = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day), DateTimeKind.Utc);

                    int affected = await db.ExecuteSqlNonQuery($"UPDATE [{databaseTableName}] SET [{column}] = [{column}] + 1 WHERE [Api] = @api AND [Operation] = @operation AND CaptureDate = @startDate", 
                        new QueryParameter { Name = "api", Value = apiName },
                        new QueryParameter { Name = "operation", Value = operationName },
                        new QueryParameter { Name = "startDate", Value = startDate }).ConfigureAwait(false);

                    if (affected < 1)
                    {
                        // If nothing was updated, a record may not exist yet for today.
                        if (await db.ExecuteSqlNonQuery($"INSERT INTO [{databaseTableName}] ([Api], [Operation], [{column}]) VALUES (@api, @operation, 1)",
                            new QueryParameter { Name = "api", Value = apiName },
                            new QueryParameter { Name = "operation", Value = operationName }).ConfigureAwait(false) < 1)
                        {
                            log.Write(new ApiLogEntry
                            {
                                Message = $"Failed to insert into {databaseTableName} for API {apiName}, Operation {operationName}. It may already exist.",
                                Severity = LogSeverities.Warning,
                                Source = logSource
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Write(new ApiLogEntry
                    {
                        Message = $"Unhandled exception in {nameof(IncrementAnalytics)}.",
                        Exception = ex,
                        Severity = LogSeverities.Error,
                        Source = logSource
                    });
                }
            });
        }

        private string GetApiName(string route)
        {
            string suffix = route.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(s => s != "/");

            if (apiLookup.ContainsKey(suffix))
            {
                return apiLookup[suffix];
            }
            else
            {
                return "Dev";
            }
        }
    }
}
