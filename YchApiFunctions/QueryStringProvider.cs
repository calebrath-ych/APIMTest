using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Ych.Data.Templating;

namespace YchApiFunctions
{
    /// <summary>
    /// Used to supply request query parameters to a ResourceQueryService.
    /// </summary>
    public class QueryStringProvider : IResourceQueryProvider
    {
        public string this[string key] => query[key].ToString();
        public IEnumerable<string> Keys => query.Keys;

        private IQueryCollection query;

        public QueryStringProvider(HttpRequest req) : this(req.Query)
        {
        }

        public QueryStringProvider(IQueryCollection query)
        {
            this.query = query;
        }
    }
}
