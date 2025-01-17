using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ych.Api.Data.X3.Views;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.X3
{
    public class X3DataSource : ApiDataSource
    {
        public DbSet<BaleInventory> BaleInventories { get; set; }
        public DbSet<VarietyAnalytic> VarietyAnalytics { get; set; }
        public DbSet<LotAnalytic> LotAnalytics { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<VarietyLotCount> VarietyLotCounts { get; set; }
        public DbSet<POVarietySummary> POVarietySummaries { get; set; }
        public DbSet<GrowerAllErpDelivery> GrowerAllErpDeliveries { get; set; }

        protected override string Name => "X3";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.SqlServer;

        public X3DataSource(ISettingsProvider settings) : base(settings)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define an entity that has no table, it is the result of a sproc
            modelBuilder.Entity<BaleInventory>((entity) => 
            {
                entity.HasNoKey();
                entity.Property("Lot").HasColumnName("lot");
                entity.Property("ProductLine").HasColumnName("product_line");
                entity.Property("ProductLineId").HasColumnName("product_line_id");
                entity.Property("Lbs").HasColumnName("lbs");
            });

            modelBuilder.Entity<VarietyAnalytic>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Grower").HasColumnName("grower");
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("VarietyId").HasColumnName("variety_id");
                entity.Property("AvgUvAlpha").HasColumnName("avg_uv_alpha");
                entity.Property("AvgUvBeta").HasColumnName("avg_uv_beta");
                entity.Property("AvgHsi").HasColumnName("avg_hsi");
                entity.Property("AvgOil").HasColumnName("avg_oil");
                entity.Property("AvgTemp").HasColumnName("avg_temp");
                entity.Property("AvgMois").HasColumnName("avg_mois");
                entity.Property("LotCount").HasColumnName("lot_count");
                entity.Property("YchAvgUvAlpha").HasColumnName("ych_avg_uv_alpha");
                entity.Property("YchAvgUvBeta").HasColumnName("ych_avg_uv_beta");
                entity.Property("YchAvgHsi").HasColumnName("ych_avg_hsi");
                entity.Property("YchAvgOil").HasColumnName("ych_avg_oil");
                entity.Property("YchAvgTemp").HasColumnName("ych_avg_temp");
                entity.Property("YchAvgMois").HasColumnName("ych_avg_mois");
            });

            modelBuilder.Entity<LotAnalytic>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Lot").HasColumnName("lot");
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("VarietyId").HasColumnName("variety_id");
                entity.Property("OilByDist").HasColumnName("oil_by_dist");
                entity.Property("OilBPinene").HasColumnName("oil_b_pinene");
                entity.Property("OilMyrcene").HasColumnName("oil_myrcene");
                entity.Property("OilLinalool").HasColumnName("oil_linalool");
                entity.Property("OilCaryophyllene").HasColumnName("oil_caryophyllene");
                entity.Property("OilFarnesene").HasColumnName("oil_farnesene");
                entity.Property("OilHumulene").HasColumnName("oil_humulene");
                entity.Property("OilGeraniol").HasColumnName("oil_geraniol");
                entity.Property("UvAlpha").HasColumnName("uv_alpha");
                entity.Property("UvBeta").HasColumnName("uv_beta");
                entity.Property("Hsi").HasColumnName("hsi");
                entity.Property("MoistMin").HasColumnName("moist_min");
                entity.Property("MoistMax").HasColumnName("moist_max");
                entity.Property("TempMin").HasColumnName("temp_min");
                entity.Property("TempMax").HasColumnName("temp_max");
            });

            modelBuilder.Entity<Contact>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Name").HasColumnName("name");
                entity.Property("Email").HasColumnName("email");
                entity.Property("Phone").HasColumnName("phone");
                entity.Property("Mobile").HasColumnName("mobile");
                entity.Property("Prefix").HasColumnName("prefix");
                entity.Property("LastName").HasColumnName("last_name");
                entity.Property("FirstName").HasColumnName("first_name");
                entity.Property("Function").HasColumnName("function");
                entity.Property("Role").HasColumnName("role");
                entity.Property("Street").HasColumnName("street");
                entity.Property("City").HasColumnName("city");
                entity.Property("PostalCode").HasColumnName("postal_code");
                entity.Property("Region").HasColumnName("region");
            });

            modelBuilder.Entity<VarietyLotCount>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("Lots").HasColumnName("lots");
            });

            modelBuilder.Entity<POVarietySummary>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("LbsPurchased").HasColumnName("lbs_purchased");
                entity.Property("LbsReceived").HasColumnName("lbs_received");
                entity.Property("LbsBalance").HasColumnName("lbs_balance");
            });

            modelBuilder.Entity<GrowerAllErpDelivery>((entity) =>
            {
                entity.HasNoKey();
                entity.Property("DateReceived").HasColumnName("date_received");
                entity.Property("Lot").HasColumnName("lot");
                entity.Property("Variety").HasColumnName("variety");
                entity.Property("QtyBalesDlv").HasColumnName("qty_bales_dlv");
                entity.Property("QtylbsDlv").HasColumnName("qty_lbs_dlv");
                entity.Property("BaleWeight").HasColumnName("bale_wt");
                entity.Property("LeafStem").HasColumnName("leaf_stem");
                entity.Property("Seed").HasColumnName("seed");
                entity.Property("Owner").HasColumnName("owner");
                entity.Property("Alpha").HasColumnName("alpha");
                entity.Property("BaleType").HasColumnName("bale_type");
                entity.Property("AlphaPounds").HasColumnName("alpha_pounds");
            });

            
    }
    }
}
