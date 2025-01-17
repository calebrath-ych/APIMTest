using Ych.Cli;

namespace Ych.Apim.Commands
{
    public class AddOrUpdateEndpointsToApis : CliCommand
    {
        public override string Description => "Modifies supporting structure for APIM APIs to add or update endpoints.";
        
        protected override CommandPersistenceTypes PersistenceType => CommandPersistenceTypes.Singleton;
        
        public override string[] Synonyms => ["add-update-endpoints-to-apis"];
        public override CommandParameter[] Parameters =>
        [
            endpointsParameter,
            apisParameter
        ];
        
        private readonly CommandParameter endpointsParameter;
        private readonly CommandParameter apisParameter;
        
        private readonly ApimDefinitionManager apimDefinitionManager;

        public AddOrUpdateEndpointsToApis()
        {
            apimDefinitionManager = new ApimDefinitionManager();

            endpointsParameter = new CommandParameter()
            {
                Index = 0,
                Name = "Endpoints",
                IsLooping = true,
                Description = "Endpoints to be added/updated.",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListEndpoints().Select(s => s.FunctionName).ToArray()
            };
            
            apisParameter = new CommandParameter
            {
                Index = 1,
                Name = "APIs",
                IsLooping = true,
                Description = "APIs to be updated.",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListApis().ToArray()
            };
        }

        public override Task Execute(CommandInvocation invocation)
        {
            var endpoints = invocation.GetValues<string>(endpointsParameter);
            var apis = invocation.GetValues<string>(apisParameter);
            
            try
            {
                apimDefinitionManager.AddOrUpdateEndpointsToApis(apis, endpoints);
                Console.WriteLine($"Endpoints ({string.Join(",", endpoints)}) added to the following APIs ({string.Join(",", apis)}).", ConsoleColor.Green);
            }
            catch (Exception e)
            {
                Log.Error(nameof(AddOrUpdateEndpointsToApis), $"Failed to apply endpoints to APIs: ", e);
            }

            return Task.CompletedTask;
        }
    }
}
