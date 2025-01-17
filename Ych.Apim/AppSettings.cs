using Ych.Apim;
using Ych.Configuration;

[assembly: SettingsAssembly(typeof(ApimConsoleSettings))]

namespace Ych.Apim
{
    public static partial class Extensions
    {
        public static ApimConsoleSettings ApimConsole(this AppSettings appSettings) => ApimConsoleSettings.Instance;
    }

    public class ApimConsoleSettings : IAppSettings
    {
        public static readonly ApimConsoleSettings Instance = new ApimConsoleSettings();
        public string SubscriptionUuid() => "Apim.SubscriptionUuiD";
        public string ResourceName() => "Apim.ResourceName";
    }
}
