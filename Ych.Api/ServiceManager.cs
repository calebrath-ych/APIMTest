using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Ych.Api.Data;
using Ych.Api.Data.GrowerPortal;
using Ych.Api.Data.Lims;
using Ych.Api.Data.Pim;
using Ych.Api.Data.Selection;
using Ych.Api.Data.Solochain;
using Ych.Api.Data.X3;
using Ych.Api.Data.Ycrm;
using Ych.Api.GrowerPortal;
using Ych.Api.Pim;
using Ych.Api.Lims;
using Ych.Api.Logging;
using Ych.Api.Selection;
using Ych.Api.Solochain;
using Ych.Api.X3;
using Ych.Api.Ycrm;
using Ych.Configuration;
using Ych.Data;
using Ych.Data.Templating;
using Ych.Logging;
using Ych.Api.Statistics;
using Ych.Api.Notification;
using Ych.Api.Configuration;
using Ych.Api.Magento;
using Ych.Magento;
using Ych.Api.Data.Magento;
using Ych.Api.Fusion;
using Ych.Api.Scada;
using Ych.Api.Data.Scada;
using Ych.Api.Sensory;
using Ych.Api.Data.Sensory;

namespace Ych.Api
{
    /// <summary>
    /// Utility methods for application startup and dependency injection.
    /// </summary>
    public static class ServiceManager
    {
        /// <summary>
        /// Used to initialize any global components and configuration.
        /// </summary>
        public static void Initialize()
        {
            Config.Initialize(EnvironmentVariableProvider.Instance);
            ApiSettings.InitializeSystems();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy(true, false) }
            };
        }

        /// <summary>
        /// Used to register any dependency injection components.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ISettingsProvider), EnvironmentVariableProvider.Instance);
            // TODO: Could this be a singleton? Would reduce overhead and the possibility of a log entry
            // being dropped because azure cleans up this function instance before logging has flushed
            services.AddScoped<ILogWriter, ApiLogWriter>(); 
            services.AddDbContext<GrowerPortalDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<X3DataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<SolochainDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<SelectionDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<SensoryDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<PimDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<LimsDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<YcrmDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<ApiStatisticsDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<MagentoDataSource>(ServiceLifetime.Scoped);
            services.AddDbContext<ScadaDataSource>(ServiceLifetime.Scoped);
            services.AddScoped<DataSourceProvider, ApiDataSourceProvider>();
            services.AddScoped<IResourceQueryService, ResourceQueryService>();
            services.AddScoped<IExampleGrowerPortalService, ExampleGrowerPortalService>();
            services.AddScoped<IGrowerPortalService, GrowerPortalService>();
            services.AddScoped<ISelectionService, SelectionService>();
            services.AddScoped<ISensoryService, SensoryService>();
            services.AddScoped<ILimsService, LimsService>();
            services.AddScoped<IPimService, PimService>();
            services.AddScoped<IX3Service, X3Service>();
            services.AddScoped<ISolochainService, SolochainService>();
            services.AddScoped<IYcrmService, YcrmService>();
            services.AddScoped<IMagentoService, MagentoService>();
            services.AddScoped<IFusionService, FusionService>();
            services.AddScoped<IScadaService, ScadaService>();
            services.AddScoped<IProxyRoutingService, ProxyRoutingService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IApiStatisticsService, ApiStatisticsService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IVersionedSettingsProvider, TableStorageVersionedSettingsProvider>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IMagentoApiClient, MagentoApiClient>();
            services.AddScoped<IApiSystemServiceProvider, ApiSystemServiceProvider>();
        }
    }
}
