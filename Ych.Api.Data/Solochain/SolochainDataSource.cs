using Microsoft.EntityFrameworkCore;
using Ych.Api.Data.Solochain.Views;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Solochain
{
    public class SolochainDataSource : ApiDataSource
    {
        public DbSet<GrowerAllWmsDelivery> GrowerAllWmsDeliveries { get; set; }
        public DbSet<GrowerOpenDelivery> GrowerOpenDeliveries { get; set; }

        protected override string Name => "Solochain";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.SqlServer;

        public SolochainDataSource(ISettingsProvider settings) : base(settings)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrowerAllWmsDelivery>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Lot").HasColumnName("lot");
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("VarietyId").HasColumnName("variety_id");
            });

            modelBuilder.Entity<GrowerOpenDelivery>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("DateReceived").HasColumnName("date_received");
                entity.Property("Lot").HasColumnName("lot");
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("QtyBalesDlv").HasColumnName("qty_bales_dlv");
            });
        }
    }
}