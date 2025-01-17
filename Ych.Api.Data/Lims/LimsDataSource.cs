using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Lims.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Lims
{
    public partial class LimsDataSource : ApiDataSource
    {
        protected override string Name => "Lims";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public LimsDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}
