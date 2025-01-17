using Ych.Cli;

namespace Ych.Apim.Commands
{
    public class CreateApi : CliCommand
    {
        public override string Description => "Creates supporting structure for APIM API.";

        protected override CommandPersistenceTypes PersistenceType => CommandPersistenceTypes.Singleton;

        public override string[] Synonyms => new string[] { "api", "apim-api" };

        public override CommandParameter[] Parameters => new CommandParameter[]
        {
            displayNameParameter,
            pathParameter,
            endpointsParameter,
            createSubscriptionParameter
        };
        
        private readonly CommandParameter displayNameParameter;
        private readonly CommandParameter pathParameter;
        private readonly CommandParameter endpointsParameter;
        private readonly CommandParameter createSubscriptionParameter;
        
        private readonly ApimDefinitionManager apimDefinitionManager;

        public CreateApi()
        {
            apimDefinitionManager = new ApimDefinitionManager();
            
            displayNameParameter = new CommandParameter
            {
                Index = 0,
                Name = "DisplayName",
                Description = "Display Name of the API (ex Grower Portal).",
                DataType = typeof(string),
                ValidateValue = (string value) =>
                {
                    List<string> reason = new List<string>();

                    bool empty = string.IsNullOrEmpty(value);

                    if (empty)
                    {
                        reason.Add("Display Name (can't be empty)");
                    }

                    bool exists = apimDefinitionManager.ListApis().Contains(value?.Replace(" ", "-").ToLower());

                    if (exists)
                    {
                        reason.Add("An API with a display name too similar to this already exists.");
                    }

                    return new CommandParameterValidation
                    {
                        IsValid = !empty && !exists,
                        Reason = reason,
                    };
                }
            };
            
            pathParameter = new CommandParameter
            {
                Index = 1,
                Name = "Path",
                IsLooping = false,
                Description = "The path that accesses this api.",
                DataType = typeof(string),
            };
            
            endpointsParameter = new CommandParameter
            {
                Index = 2,
                Name = "Endpoints",
                IsLooping = true,
                Description = "The endpoints that this subscription has access to. Must be an existing Endpoint.",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListEndpoints().Select(s => s.FunctionName).ToArray()
            };
            
            createSubscriptionParameter = new CommandParameter
            {
                Index = 3,
                Name = "CreateSubscription",
                IsLooping = false,
                Description = "Do you want to create a subscription for this API?",
                DataType = typeof(string),
                ValidValues = ["yes", "no", "y", "n"]
            };
        }

        public override Task Execute(CommandInvocation invocation)
        {
            string displayName = invocation.GetValue(displayNameParameter);
            string path = invocation.GetValue(pathParameter);
            List<string> endpoints = invocation.GetValues<string>(endpointsParameter).ToList();
            bool createSubscription = invocation.GetValue(createSubscriptionParameter).StartsWith("y", StringComparison.OrdinalIgnoreCase);
            
            try
            {
                new ApimDefinitionManager().CreateApi(displayName, path, endpoints, $"Created From {nameof(ApimConsole)}:{nameof(CreateApi)} command.");
                Console.WriteLine($"API '{displayName}' created and saved with endpoints: {string.Join(",", endpoints)}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Log.Error(nameof(CreateSubscription), $"Api failed to create: {ex}");
                return Task.CompletedTask;
            }
            
            if (createSubscription)
            {
                try
                {
                    new ApimDefinitionManager().CreateSubscription(displayName, path);
                    Console.WriteLine($"Subscription '{displayName}' created and saved.", ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    Log.Error(nameof(CreateSubscription), $"Subscription failed to create: {ex}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
