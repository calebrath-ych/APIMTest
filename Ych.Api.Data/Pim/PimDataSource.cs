using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Pim.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Pim
{
    public partial class PimDataSource : ApiDataSource
    {
        protected override string Name => "Pim";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public PimDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}
