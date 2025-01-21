using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ych.Api.Data.GrowerPortal.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.GrowerPortal
{
    public partial class GrowerPortalDataSource : ApiDataSource
    {
        public DbSet<HarvestInformation> HarvestInformation { get; set; }

        protected override string Name => "GrowerPortal";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public GrowerPortalDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder builder)
        {
            // HarvestInformation is not a table, it is the result of an adhoc SQL query
            builder.Entity<HarvestInformation>().HasNoKey();
        }
    }
}
