using System;
using System.Collections.Generic;
using System.Text;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Logging;
using Ych.Api;
using System.Reflection;
using Ych.Api.X3;
using Ych.Api.Magento;

[assembly: SettingsAssembly(typeof(ApiSettings),
    typeof(ApiSettings.GrowerPortalSettings),
    typeof(ApiSettings.LimsSettings),
    typeof(ApiSettings.OneSignalSettings),
    typeof(ApiSettings.PimSettings),
    typeof(ApiSettings.SelectionSettings),
    typeof(ApiSettings.SolochainSettings),
    typeof(ApiSettings.ScadaSettings),
    typeof(ApiSettings.X3Settings),
    typeof(ApiSettings.MagentoSettings),
    typeof(ApiSettings.RequestStatisticsSettings),
    typeof(ApiSettings.YcrmSettings),
    typeof(ApiSettings.LotLookupSettings),
    typeof(ApiSettings.AppSettingsVersioningSettings),
    typeof(ApiSettings.EisSettings),
    typeof(ApiSettings.MagentoSettings),
    typeof(ApiSettings.ValidationSettings)
    )]

[assembly: LogProviderAssembly(typeof(ApiLogWriter))]

namespace Ych.Api
{
    public static partial class Extensions
    {
        public static ApiSettings Api(this AppSettings appSettings) => ApiSettings.Instance;
    }

    public enum ProxyAuthenticationModes
    {
        BasicAuth,
        Apim,
        None,
    }

    public interface IApiSystem
    {
        string Name { get; }
    }

    public interface IHealthySystem : IDataSystem
    {
        string CheckServiceHealth();
    }

    public interface IDataSystem : IApiSystem
    {
        string ConnectionString();
    }

    public interface IProxySystem : IApiSystem
    {
        string BaseUrl();
        string AuthCredentials();
        string AuthMode();
    }

    public interface IOneSignalSystem : IApiSystem
    {
        string OneSignalAuthCredentials();
        string OneSignalAppId();
    }

    public interface IMobileClientSystem : IApiSystem
    {
        string MobileClientName { get; }
        string CurrentMobileVersion();
    }

    public class ApiSettings : IAppSettings
    {
        public class GrowerPortalSettings : IAppSettings, IDataSystem, IProxySystem, IOneSignalSystem, IMobileClientSystem
        {
            public static readonly GrowerPortalSettings Instance = new GrowerPortalSettings();

            public string Name => "GrowerPortal";
            public string MobileClientName => "GrowerPortalMobile";

            public string ConnectionString() => "Api.GrowerPortal.ConnectionString";
            public string BaseUrl() => "Api.GrowerPortal.BaseUrl";
            public string AuthCredentials() => "Api.GrowerPortal.AuthCredentials";
            public string AuthMode() => "Api.GrowerPortal.AuthMode";
            public string OneSignalAuthCredentials() => "Api.GrowerPortal.OneSignalAuthCredentials";
            public string OneSignalAppId() => "Api.GrowerPortal.OneSignalAppId";
            public string CurrentMobileVersion() => "Api.GrowerPortal.CurrentMobileVersion";
            public string CheckServiceHealth() => "Api.GrowerPortal.CheckSystemHealth";
        }

        public class YcrmSettings : IAppSettings, IDataSystem, IProxySystem, IOneSignalSystem, IMobileClientSystem
        {
            public static readonly YcrmSettings Instance = new YcrmSettings();

            public string Name => "Ycrm";
            public string MobileClientName => "YcrmMobile";

            public string ConnectionString() => "Api.Ycrm.ConnectionString";
            public string BaseUrl() => "Api.Ycrm.BaseUrl";
            public string AuthCredentials() => "Api.Ycrm.AuthCredentials";
            public string AuthMode() => "Api.Ycrm.AuthMode";
            public string OneSignalAuthCredentials() => "Api.Ycrm.OneSignalAuthCredentials";
            public string OneSignalAppId() => "Api.Ycrm.OneSignalAppId";
            public string CurrentMobileVersion() => "Api.Ycrm.CurrentMobileVersion";
            public string CheckServiceHealth() => "Api.Ycrm.CheckSystemHealth";
        }

        public class LotLookupSettings : IAppSettings, IMobileClientSystem
        {
            public static readonly LotLookupSettings Instance = new LotLookupSettings();

            public string Name => "LotLookup";
            public string MobileClientName => "LotLookupMobile";

            public string CurrentMobileVersion() => "Api.LotLookup.CurrentMobileVersion";
        }

        public class EisSettings : IAppSettings, IProxySystem
        {
            public static readonly EisSettings Instance = new EisSettings();

            public string Name => "Eis";

            public string BaseUrl() => "Api.Eis.BaseUrl";
            public string AuthCredentials() => "Api.Eis.AuthCredentials";
            public string AuthMode() => "Api.Eis.AuthMode";
        }

        public class ScadaSettings : IAppSettings, IProxySystem
        {
            public static readonly ScadaSettings Instance = new ScadaSettings();

            public string Name => "Scada";

            public string BaseUrl() => "Api.Scada.BaseUrl";
            public string AuthCredentials() => "Api.Scada.AuthCredentials";
            public string AuthMode() => "Api.Scada.AuthMode";
        }

        public class LimsSettings : IAppSettings, IDataSystem, IProxySystem
        {
            public static readonly LimsSettings Instance = new LimsSettings();

            public string Name => "Lims";

            public string ConnectionString() => "Api.Lims.ConnectionString";
            public string BaseUrl() => "Api.Lims.BaseUrl";
            public string AuthCredentials() => "Api.Lims.AuthCredentials";
            public string AuthMode() => "Api.Lims.AuthMode";
            public string CheckServiceHealth() => "Api.Lims.CheckSystemHealth";
        }

        public class SelectionSettings : IAppSettings, IDataSystem, IProxySystem
        {
            public static readonly SelectionSettings Instance = new SelectionSettings();

            public string Name => "Selection";
            public string BaseUrl() => "Api.Selection.BaseUrl";
            public string AuthCredentials() => "Api.Selection.AuthCredentials";
            public string ConnectionString() => "Api.Selection.ConnectionString";
            public string AuthMode() => "Api.Selection.AuthMode";
            public string CheckServiceHealth() => "Api.Selection.CheckSystemHealth";
        }

        public class PimSettings : IAppSettings, IDataSystem
        {
            public static readonly PimSettings Instance = new PimSettings();

            public string Name => "Pim";

            public string ConnectionString() => "Api.Pim.ConnectionString";
            public string CheckServiceHealth() => "Api.Pim.CheckSystemHealth";
        }

        public class X3Settings : IAppSettings, IDataSystem
        {
            public static readonly X3Settings Instance = new X3Settings();

            public string Name => X3Service.X3SystemName;

            public string ConnectionString() => "Api.X3.ConnectionString";
            public string CheckServiceHealth() => "Api.X3.CheckSystemHealth";
        }

        public class MagentoSettings : IAppSettings, IDataSystem, IProxySystem
        {
            public static readonly MagentoSettings Instance = new MagentoSettings();

            public string Name => MagentoService.MagentoSystemName;

            public string ConnectionString() => "Api.Magento.ConnectionString";
            public string AuthCredentials() => "Api.Magento.AuthCredentials";
            public string BaseUrl() => "Api.Magento.BaseUrl";
            public string AuthMode() => "Api.Magento.AuthMode";
            public string AssetBaseUrl() => "Api.Magento.AssetBaseUrl";
            public string ECommerceBaseUrl() => "Api.Magento.ECommerceBaseUrl";
            public string CheckServiceHealth() => "Api.Magento.CheckSystemHealth";
        }

        public class SolochainSettings : IAppSettings, IDataSystem, IProxySystem
        {
            public static readonly SolochainSettings Instance = new SolochainSettings();

            public string Name => "Solochain";

            public string ConnectionString() => "Api.Solochain.ConnectionString";
            public string BaseUrl() => "Api.Solochain.BaseUrl";
            public string AuthCredentials() => "Api.Solochain.AuthCredentials";
            public string AuthMode() => "Api.Solochain.AuthMode";
            public string CheckServiceHealth() => "Api.Solochain.CheckSystemHealth";
        }

        public class RequestStatisticsSettings : IAppSettings, IDataSystem
        {
            public static readonly RequestStatisticsSettings Instance = new RequestStatisticsSettings();

            public string Name => "RequestStatistics";

            public string ConnectionString() => "Api.RequestStatistics.ConnectionString";
            public string Disabled() => "Api.RequestStatistics.Disabled";
            public string TableName() => "Api.RequestStatistics.TableName";
            public string CheckServiceHealth() => "Api.RequestStatistics.CheckSystemHealth";
        }

        public class AppSettingsVersioningSettings : IAppSettings, IDataSystem
        {
            public static readonly AppSettingsVersioningSettings Instance = new AppSettingsVersioningSettings();

            public string Name => "VersionedSettings";

            public string Enabled() => "Api.VersionedSettings.Enabled";
            public string ConnectionString() => "Api.VersionedSettings.ConnectionString";
            public string TableName() => "Api.VersionedSettings.TableName";
            public string CheckServiceHealth() => "Api.VersionedSettings.CheckSystemHealth";
        }

        public class OneSignalSettings : IAppSettings, IProxySystem
        {
            public static readonly OneSignalSettings Instance = new OneSignalSettings();

            public string Name => "OneSignal";

            public string BaseUrl() => "Api.OneSignal.BaseUrl";
            public string AuthCredentials() => "Api.OneSignal.AuthCredentials";
            public string AuthMode() => "Api.OneSignal.AuthMode";
        }

        public class ValidationSettings
        {
            public static readonly ValidationSettings Instance = new ValidationSettings();

            public string ValidateCustomerCodes() => "Api.Validation.ValidateCustomerCodes";
            public string ValidateCustomerCodeRegex() => "Api.Validation.CustomerCodeRegex";
        }

        public class CdnSettings
        {
            public static readonly CdnSettings Instance = new CdnSettings();

            public string ConeImageBaseUrl() => "Api.Cdn.ConeImageBaseUrl";
            public string ProductImageBaseUrl() => "Api.Cdn.ProductImageBaseUrl";
            public string SpiderGraphBaseUrl() => "Api.Cdn.SpiderGraphBaseUrl";
        }

        public class SensorySettings
        {
            public static readonly SensorySettings Instance = new SensorySettings();

            public string Name => "Sensory";

            public string ConnectionString() => "Api.Sensory.ConnectionString";
            public string BaseUrl() => "Api.Sensory.BaseUrl";
            public string AuthCredentials() => "Api.Sensory.AuthCredentials";
            public string AuthMode() => "Api.Sensory.AuthMode";
            public string OrganizationId() => "Api.Sensory.OrganizationId";
            public string MinPanelistsForLotLookup() => "Api.Sensory.MinPanelistsForLotLookup";
        }

        public static readonly ApiSettings Instance = new ApiSettings();

        private static Dictionary<string, IApiSystem> systems = new Dictionary<string, IApiSystem>();

        public GrowerPortalSettings GrowerPortal() => GrowerPortalSettings.Instance;
        public YcrmSettings Ycrm() => YcrmSettings.Instance;
        public LotLookupSettings LotLookup() => LotLookupSettings.Instance;
        public EisSettings Eis() => EisSettings.Instance;
        public LimsSettings Lims() => LimsSettings.Instance;
        public ScadaSettings Scada() => ScadaSettings.Instance;
        public SelectionSettings Selection() => SelectionSettings.Instance;
        public PimSettings Pim() => PimSettings.Instance;
        public X3Settings X3() => X3Settings.Instance;
        public MagentoSettings Magento() => MagentoSettings.Instance;
        public SolochainSettings Solochain() => SolochainSettings.Instance;
        public RequestStatisticsSettings RequestStatistics() => RequestStatisticsSettings.Instance;
        public AppSettingsVersioningSettings VersionedSettings() => AppSettingsVersioningSettings.Instance;
        public OneSignalSettings OneSignal() => OneSignalSettings.Instance;
        public ValidationSettings Validation() => ValidationSettings.Instance;
        public CdnSettings Cdn() => CdnSettings.Instance;
        public SensorySettings Sensory() => SensorySettings.Instance;

        public string LookupTable() => "Api.LookupTable";
        public string IncludeExceptionDetails() => "Api.IncludeExceptionDetails";
        public string LogRequestBody() => "Api.LogRequestBody";
        public string RequestBufferThreshold() => "Api.RequestBufferThreshold";
        public string RequestBufferLimit() => "Api.RequestBufferLimit";
        public string LogRequestHeaders() => "Api.LogRequestHeaders";
        public string LogResponses() => "Api.LogResponses";
        [AppSetting("If enabled, all proxy routing calls will ignore any certificate errors. Should only be used in local/development environments if needed.", typeof(bool), false, new object[] { true, false })]
        public string IgnoreProxyCertificates() => "Api.IgnoreProxyCertificates";

        public static T GetSystem<T>(string name) where T : class, IApiSystem
        {
            if (systems.ContainsKey(name))
            {
                return systems[name] as T;
            }
            else
            {
                return null;
            }
        }

        public static void InitializeSystems()
        {
            MethodInfo[] methods = typeof(ApiSettings).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                if (method.ReturnType.GetInterface(nameof(IApiSystem)) != null)
                {
                    IApiSystem system = method.Invoke(Config.Settings.Api(), null) as IApiSystem;

                    if (system != null) // Shouldn't be
                    {
                        systems.Add(system.Name, system);
                    }
                }

                if (method.ReturnType.GetInterface(nameof(IMobileClientSystem)) != null)
                {
                    IMobileClientSystem mobileClient = method.Invoke(Config.Settings.Api(), null) as IMobileClientSystem;

                    if (mobileClient != null) // Shouldn't be
                    {
                        systems.Add(mobileClient.MobileClientName, mobileClient);
                    }
                }
            }
        }
    }
}
