using System.Reflection;
using Ych.Data.Templating;

namespace Ych.Api.Data
{
    public static class ApiResourceTemplates
    {
        /// <summary>
        /// Loads all resource templates from the Ych.Api.Data assembly.
        /// </summary>
        /// <param name="queryService"></param>
        public static void LoadTemplates(IResourceQueryService queryService)
        {
            queryService.ExtractTemplates(Assembly.GetExecutingAssembly());
        }
    }
}
