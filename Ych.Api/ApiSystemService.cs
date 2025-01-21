using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Data;
using Ych.Api.GrowerPortal;
using Ych.Api.Lims;
using Ych.Api.Magento;
using Ych.Api.Pim;
using Ych.Api.Scada;
using Ych.Api.Selection;
using Ych.Api.Solochain;
using Ych.Api.X3;
using Ych.Api.Ycrm;
using Ych.Configuration;

namespace Ych.Api
{
    public interface IApiSystemService
    {
        string SystemName { get; }
    }

    public interface ICustomerContactsService : IApiSystemService
    {
        Task<IEnumerable> GetCustomerContacts(string customerCode);
    }

    public interface ICustomerAddressesService : IApiSystemService
    {
        Task<IEnumerable> GetCustomerAddresses(string customerCode);
    }

    public interface IHealthyService : IApiSystemService
    {
        Task<IEnumerable> ServiceHealth();
    }

    public abstract class ApiDataService : ApiSystemService
    {
        protected abstract ApiDataSource Db { get; }

        public virtual async Task<IEnumerable> ServiceHealth()
        {
            var dbResponse = await Db.CanConnect();

            return new List<Dictionary<string, object>>() {
                new Dictionary<string, object>()
                {
                    {"system", SystemName },
                    {"database_connection", Db.Settings.GetValue(ApiSettings.GetSystem<IHealthySystem>(SystemName)?.CheckServiceHealth(), true) ? dbResponse.CanConnect ? "succeeded" : "failed" : "skipped" },
                    {"database_connection_errors", dbResponse.Errors }
                }
            };
        }
    }

    public abstract class ApiSystemService : IApiSystemService
    {
        public const string SourceSystemField = "source_system";

        public abstract string SystemName { get; }

        protected virtual List<Dictionary<string, object>> SetSourceSystem(List<Dictionary<string, object>> results)
        {
            foreach (var row in results)
            {
                row.Add(SourceSystemField, SystemName);
            }

            return results;
        }
    }

    public interface IApiSystemServiceProvider
    {
        T GetService<T>(string systemName) where T : class, IApiSystemService;
        T[] GetServices<T>(params string[] systemNames) where T : class, IApiSystemService;
    }

    public class ApiSystemServiceProvider : IApiSystemServiceProvider
    {
        private Dictionary<string, IApiSystemService> services = new Dictionary<string, IApiSystemService>();

        public ApiSystemServiceProvider(IGrowerPortalService gpService,ILimsService limsService, IMagentoService magentoService, IPimService pimService,
            IScadaService scadaService, ISelectionService selectionService, ISolochainService solochainService,IX3Service x3Service, IYcrmService ycrmService)
        {
            RegisterService(gpService);
            RegisterService(limsService);
            RegisterService(magentoService);
            RegisterService(pimService);
            RegisterService(scadaService);
            RegisterService(selectionService);
            RegisterService(solochainService);
            RegisterService(x3Service);
            RegisterService(ycrmService);
        }

        private void RegisterService(IApiSystemService service)
        {
            services.Add(service.SystemName, service);
        }

        public T GetService<T>(string systemName) where T : class, IApiSystemService
        {
            return services.FirstOrDefault(s => s.Key.Equals(systemName, StringComparison.InvariantCultureIgnoreCase)).Value as T;
        }

        public T[] GetServices<T>(params string[] systemNames) where T : class, IApiSystemService
        {
            if (systemNames == null)
            {
                // If system is unspecified, return results from all valid systems
                return services.Where(s => s.Value is T).Select(s => s.Value as T).ToArray();
            }
            else
            {
                return systemNames.Select(s => GetService<T>(s)).Where(s => s != null).ToArray();
            }
        }
    }
}
