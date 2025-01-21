using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.Logging;
using Ych.Data.Templating;
using Ych.Logging;
using Ych.Api.Statistics;

namespace YchApiFunctions.Query
{
    /// <summary>
    /// ResourceQuery is a single endpoint that allows you to dynamically formulate a resource query using url and query parameters.
    /// </summary>
    public class ResourceQuery : ApiFunction
    {
        private IResourceQueryService queryService;

        public ResourceQuery(IResourceQueryService queryService, ILogWriter log, IApiStatisticsService statistics) : base(log, statistics)
        {
            this.queryService = queryService;

            // TODO: Find a more elegant way to load/cache resource templates that depend on dataAccessProvider/DI?
            ApiResourceTemplates.LoadTemplates(queryService);
        }

        [Function(nameof(ResourceQuery))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "query/{resource}/{resourceId?}")] HttpRequest req, string resource, string resourceId)
        {
            return await ProcessRequest(req, async () =>
            {
                return SuccessResponse(await queryService.ExecuteResourceQuery(Ych.Data.Templating.ResourceQuery.Create(
                    resource, resourceId, new QueryStringProvider(req))));
            });
        }
    }
}
