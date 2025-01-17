using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Logging;

namespace YchApiFunctions.Fusion
{
    public abstract class FusionFunction<T> : ApiFunction where T : class, IApiSystemService
    {
        public class GenericResults : ArrayList
        {
            public GenericResults() { }

            public GenericResults(params IEnumerable[] resultSets)
            {
                if (resultSets != null)
                {
                    foreach (IEnumerable resultSet in resultSets)
                    {
                        AddResults(resultSet);
                    }
                }
            }

            public void AddResults(IEnumerable enumerable)
            {
                if (enumerable == null) return;

                IEnumerator enm = enumerable.GetEnumerator();

                while (enm.MoveNext())
                {
                    Add(enm.Current);
                }
            }
        }

        protected const string DataSourceParameter = "dataSource";
        protected const string NormalizeParameter = "normalize";

        protected virtual string[] DefaultDataSources => null; // By default return all data sources
        protected virtual bool DefaultNormalizeDataSets => true;

        private IApiSystemServiceProvider serviceProvider;
        private IValidationService validation;

        protected FusionFunction(IApiSystemServiceProvider serviceProvider, IValidationService validation, 
            ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.serviceProvider = serviceProvider;
            this.validation = validation;
        }

        protected virtual void OnValidate(IValidationService validation) { }

        protected Task<IActionResult> ProcessFusionRequest(HttpRequest req, Func<T, Task<IEnumerable>> serviceHandler, 
            Action<IValidationService> validationHandler = null)
        {
            return ProcessRequest(req, async () =>
            {
                validationHandler?.Invoke(validation);

                OnValidate(validation);

                // TODO: Add error handling per serviceHandler here to prevent one data source from causing the whole endpoint to fail.
                var tasks = serviceProvider.GetServices<T>(GetDataSources(req)).Select(s => serviceHandler(s)).ToArray();

                await Task.WhenAll(tasks);

                return SuccessResponse(new GenericResults(HandleTaskResults(tasks, GetBool(req, NormalizeParameter, DefaultNormalizeDataSets))));
            });
        }

        protected virtual IEnumerable[] HandleTaskResults(Task<IEnumerable>[] tasks, bool normalizeDataSets)
        {
            var dataSets = tasks.Select(s => s.Result);
            
            if (normalizeDataSets)
            {
                List<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();
                HashSet<string> keys = new HashSet<string>();

                foreach (IEnumerable dataSet in dataSets)
                {
                    if (dataSet is List<Dictionary<string, object>> row && row.Any())
                    {
                        dictionaries.AddRange(row);
                        
                        foreach (string key in row.First().Keys)
                        {
                            keys.Add(key);
                        }
                    }
                }

                foreach (var dict in dictionaries)
                {
                    foreach (string key in keys)
                    {
                        if (!dict.ContainsKey(key))
                        {
                            dict.Add(key, null);
                        }
                    }
                }
            }

            return dataSets.ToArray();
        }

        protected string[] GetDataSources(HttpRequest req)
        {
            string[] result;

            if (req.Query.TryGetValue(DataSourceParameter, out var dataSource))
            {
                result = dataSource.ToString().Split(',');
            }
            else
            {
                result = DefaultDataSources;
            }

            return result;
        }
    }
}
