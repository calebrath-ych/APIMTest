using Ych.Api.Data.GrowerPortal;
using Ych.Api.Data.Lims;
using Ych.Api.Data.Pim;
using Ych.Api.Data.Scada;
using Ych.Api.Data.Selection;
using Ych.Api.Data.Sensory;
using Ych.Api.Data.X3;
using Ych.Api.Data.Ycrm;
using Ych.Data;

namespace Ych.Api.Data
{
    public class ApiDataSourceProvider : DataSourceProvider
    {
        public ApiDataSourceProvider(GrowerPortalDataSource growerPortal, X3DataSource x3DataSource, SelectionDataSource selection, 
            SensoryDataSource sensory, PimDataSource pim, LimsDataSource lims, YcrmDataSource ycrm, ScadaDataSource scada)
        {
            RegisterDataSource(growerPortal);
            RegisterDataSource(x3DataSource);
            RegisterDataSource(selection);
            RegisterDataSource(sensory);
            RegisterDataSource(pim);
            RegisterDataSource(lims);
            RegisterDataSource(ycrm);
            RegisterDataSource(scada);
        }
    }
}
