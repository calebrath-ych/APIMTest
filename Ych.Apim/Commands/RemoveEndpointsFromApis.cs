using Ych.Cli;

namespace Ych.Apim.Commands
{
    public class RemoveEndpointsFromApis : CliCommand
    {
        public override string Description => "Modifies supporting structure for APIM APIs to remove endpoints.";
        
        protected override CommandPersistenceTypes PersistenceType => CommandPersistenceTypes.Singleton;
        
        public override string[] Synonyms => ["remove-api-endpoints"];

        public override CommandParameter[] Parameters =>
        [
            apisParameter,
            endpointsParameter
        ];

        private readonly CommandParameter apisParameter;
        private readonly CommandParameter endpointsParameter;
        
        private readonly ApimDefinitionManager apimDefinitionManager;

        public RemoveEndpointsFromApis()
        {
            apimDefinitionManager = new ApimDefinitionManager();
            
            apisParameter = new CommandParameter
            {
                Index = 0,
                Name = "APIs",
                IsLooping = true,
                Description = "APIs to remove endpoints from.",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListApis().ToArray()
            };

            endpointsParameter = new CommandParameter()
            {
                Index = 1,
                Name = "Endpoints",
                IsLooping = true,
                Description = "The endpoints to remove some selected APIs.",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListEndpoints().Select(s => s.FunctionName).ToArray()
            };
        }

        public override Task Execute(CommandInvocation invocation)
        {
            var apis = invocation.GetValues<string>(apisParameter);
            var endpoints = invocation.GetValues<string>(endpointsParameter);
            
            try
            {
                apimDefinitionManager.RemoveEndpointsFromApis(apis, endpoints);
                Console.WriteLine($"Removed the following endpoints from {apis.Count()} API(s) ({string.Join(",", endpoints)}).", ConsoleColor.Green);
            }
            catch (Exception e)
            {
                Log.Error(nameof(RemoveEndpointsFromApis), $"Failed to remove endpoints from APIs: ", e);
            }

            return Task.CompletedTask;
        }
    }
}
