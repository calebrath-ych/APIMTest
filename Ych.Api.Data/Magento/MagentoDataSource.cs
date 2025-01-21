using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Magento
{
    public class MagentoDataSource : ApiDataSource
    {
        public MagentoDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        protected override string Name => "Magento";

        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;
    }
}
