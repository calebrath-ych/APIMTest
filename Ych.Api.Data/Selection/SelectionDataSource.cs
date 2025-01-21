using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Selection.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Selection
{
    public partial class SelectionDataSource : ApiDataSource
    {
        protected override string Name => "Selection";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public SelectionDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}
