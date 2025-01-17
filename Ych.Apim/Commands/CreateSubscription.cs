using Ych.Cli;

namespace Ych.Apim.Commands
{
    public class CreateSubscription : CliCommand
    {
        public override string Description => "Creates supporting structure for APIM Subscription. Relevant API must exist first.";
        
        protected override CommandPersistenceTypes PersistenceType => CommandPersistenceTypes.Singleton;
        
        public override string[] Synonyms => new string[] { "create-subscription", "subscription", "apim-subscription" };

        public override CommandParameter[] Parameters => new CommandParameter[]
        {
            displayNameParameter,
            scopeParameter,
        };
        
        private readonly CommandParameter displayNameParameter;
        private readonly CommandParameter scopeParameter;
        
        private readonly ApimDefinitionManager apimDefinitionManager;

        public CreateSubscription()
        {
            apimDefinitionManager = new ApimDefinitionManager();
            
            displayNameParameter = new CommandParameter
            {
                Index = 0,
                Name = "DisplayName",
                Description = "Display Name (Pascal Case / ex GrowerPortal). Can't be an existing subscription.",
                DataType = typeof(string),
                ValidateValue = (string value) =>
                {
                    List<string> reason = new List<string>();
                
                    bool noSpecialCharacters = !string.IsNullOrEmpty(value) && value.All(char.IsLetter);

                    if (!noSpecialCharacters)
                    {
                        reason.Add("Display Name (only letters, no spaces or hyphens / ex MobileApps)");
                    }
                
                    bool exists = new ApimDefinitionManager().ListSubscriptions().Contains(value.PascalToKebabCase());

                    if (exists)
                    {
                        reason.Add("A Subscription with this display name already exists.");
                    }
                
                    return new CommandParameterValidation
                    {
                    
                        IsValid = noSpecialCharacters && !exists,
                        Reason = reason,
                    };
                }
            };
            
            scopeParameter = new CommandParameter
            {
                Index = 1,
                Name = "Scope",
                Description = "Scope (the API that this subscription has access to, no special characters besides '-'). Must be an existing subscription",
                DataType = typeof(string),
                ValidValues = apimDefinitionManager.ListApis().ToArray()
            };

        }

        public override Task Execute(CommandInvocation invocation)
        {
            string displayName = invocation.GetValue(displayNameParameter);
            string scope = invocation.GetValue(scopeParameter);

            try
            {
                new ApimDefinitionManager().CreateSubscription(displayName, scope);
                Console.WriteLine($"Subscription '{displayName}' created and saved.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Log.Error(nameof(CreateSubscription), ex);
            }

            return Task.CompletedTask;
        }
    }
}
