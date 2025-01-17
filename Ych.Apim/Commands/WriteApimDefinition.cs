using Ych.Cli;

namespace Ych.Apim.Commands
{
    public class WriteApimDefinition : CliCommand
    {
        public override string Description => "Writes the APIM definition to a text file.";
        
        protected override CommandPersistenceTypes PersistenceType => CommandPersistenceTypes.Singleton;
        
        public override string[] Synonyms => new string[] { "write-apim-definition" };

        public override CommandParameter[] Parameters => [];
        private readonly ApimDefinitionManager apimDefinitionManager;

        public WriteApimDefinition()
        {
            apimDefinitionManager = new ApimDefinitionManager();
        }

        public override Task Execute(CommandInvocation invocation)
        {

            try
            {
                apimDefinitionManager.WriteApimDefinitionToJson();;
            }
            catch (Exception ex)
            {
                Log.Error(nameof(WriteApimDefinition), ex);
            }

            return Task.CompletedTask;
        }
    }
}
