using System.Reflection;
using Ych.Cli;
using Ych.Configuration;

namespace Ych.Apim
{
    public class ApimConsole : CliConsole
    {
        public ApimConsole()
        {
            JsonSettingsProvider.GetSettingsStreamHandler = () =>
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream($"Ych.Apim.settings.json");
            };
        }
    }
}
