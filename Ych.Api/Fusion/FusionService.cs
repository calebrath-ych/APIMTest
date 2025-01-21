using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Magento;
using Ych.Api.X3;
using Ych.Logging;

namespace Ych.Api.Fusion
{
    public interface IFusionService
    {
        Task<IEnumerable> GetCustomerContacts(string customerCode);
    }

    public class FusionService : IFusionService
    {
        private IX3Service x3Service;
        private IMagentoService magentoService;
        private ILogWriter log;

        public FusionService(IX3Service x3Service, IMagentoService magentoService, ILogWriter log)
        {
            this.x3Service = x3Service;
            this.magentoService = magentoService;
            this.log = log;
        }

        public async Task<IEnumerable> GetCustomerContacts(string customerCode)
        {
            var x3Task = x3Service.GetCustomerContacts(customerCode);
            var magentoTask = magentoService.GetCustomerContacts(customerCode);

            await Task.WhenAll(x3Task, magentoTask).ConfigureAwait(false);

            var x3Contacts = x3Task.Result;
            var magentoContacts = magentoTask.Result;

            return new object[]
            {
                new
                {
                    X3Contacts = x3Contacts,
                    MagentoContacts = magentoContacts
                }
            };
        }
    }
}
