using Microsoft.EntityFrameworkCore;
using Ych.Api.Data.Ycrm.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Ycrm
{
    public partial class YcrmDataSource : ApiDataSource
    {
        protected override string Name => "Ycrm";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public YcrmDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}
