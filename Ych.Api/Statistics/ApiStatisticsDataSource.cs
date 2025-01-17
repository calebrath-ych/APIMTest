using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ych.Api.Data;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Statistics
{
    public class ApiStatisticsDataSource : ApiDataSource
    {
        public DbSet<ApiRequestStatistics> ApiRequestAnalytics { get; set; }

        protected override string Name => "RequestStatistics";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.SqlServer;

        public ApiStatisticsDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApiRequestStatistics>()
                .ToTable(typeof(ApiRequestStatistics).Name)
                .HasKey(s => s.Id);
        }
    }
}
