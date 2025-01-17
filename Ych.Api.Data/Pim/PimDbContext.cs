using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Pim.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Pim
{
    public partial class PimDataSource
    {
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<Aromas> Aromas { get; set; }
        public virtual DbSet<AromasVarieties> AromasVarieties { get; set; }
        public virtual DbSet<BeerTypes> BeerTypes { get; set; }
        public virtual DbSet<BeerTypesVarieties> BeerTypesVarieties { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<BrewingValues> BrewingValues { get; set; }
        public virtual DbSet<CountryCodes> CountryCodes { get; set; }
        public virtual DbSet<Cultivars> Cultivars { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<ModelHasPermissions> ModelHasPermissions { get; set; }
        public virtual DbSet<ModelHasRoles> ModelHasRoles { get; set; }
        public virtual DbSet<PasswordResets> PasswordResets { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PimTest> PimTest { get; set; }
        public virtual DbSet<ProductLines> ProductLines { get; set; }
        public virtual DbSet<RoleHasPermissions> RoleHasPermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Varieties> Varieties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("activity_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasColumnName("ip_address")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Aromas>(entity =>
            {
                entity.ToTable("aromas");

                entity.HasIndex(e => e.Aroma)
                    .HasName("aromas_aroma_unique")
                    .IsUnique();

                entity.HasIndex(e => e.MagentoAromaId)
                    .HasName("aromas_magento_aroma_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Aroma)
                    .IsRequired()
                    .HasColumnName("aroma")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.MagentoAromaId)
                    .HasColumnName("magento_aroma_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<AromasVarieties>(entity =>
            {
                entity.ToTable("aromas_varieties");

                entity.HasIndex(e => e.AromaId)
                    .HasName("aromas_varieties_aroma_id_index");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("aromas_varieties_variety_id_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AromaId)
                    .HasColumnName("aroma_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Aroma)
                    .WithMany(p => p.AromasVarieties)
                    .HasForeignKey(d => d.AromaId)
                    .HasConstraintName("aromas_varieties_aroma_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.AromasVarieties)
                    .HasForeignKey(d => d.VarietyId)
                    .HasConstraintName("aromas_varieties_variety_id_foreign");
            });

            modelBuilder.Entity<BeerTypes>(entity =>
            {
                entity.ToTable("beer_types");

                entity.HasIndex(e => e.BeerType)
                    .HasName("beer_types_beer_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BeerType)
                    .IsRequired()
                    .HasColumnName("beer_type")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<BeerTypesVarieties>(entity =>
            {
                entity.ToTable("beer_types_varieties");

                entity.HasIndex(e => e.BeerTypeId)
                    .HasName("beer_types_varieties_beer_type_id_index");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("beer_types_varieties_variety_id_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BeerTypeId)
                    .HasColumnName("beer_type_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.BeerType)
                    .WithMany(p => p.BeerTypesVarieties)
                    .HasForeignKey(d => d.BeerTypeId)
                    .HasConstraintName("beer_types_varieties_beer_type_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.BeerTypesVarieties)
                    .HasForeignKey(d => d.VarietyId)
                    .HasConstraintName("beer_types_varieties_variety_id_foreign");
            });

            modelBuilder.Entity<Brands>(entity =>
            {
                entity.ToTable("brands");

                entity.HasIndex(e => e.BrandName)
                    .HasName("brands_brand_name_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasColumnName("brand_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<BrewingValues>(entity =>
            {
                entity.ToTable("brewing_values");

                entity.HasIndex(e => e.ProductLineId)
                    .HasName("brewing_values_product_line_id_foreign");

                entity.HasIndex(e => new { e.VarietyId, e.ProductLineId })
                    .HasName("brewing_values_variety_id_product_line_id_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AlphaAve)
                    .HasColumnName("alpha_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.AlphaHigh)
                    .HasColumnName("alpha_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.AlphaLow)
                    .HasColumnName("alpha_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BPineneAve)
                    .HasColumnName("b_pinene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BPineneHigh)
                    .HasColumnName("b_pinene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BPineneLow)
                    .HasColumnName("b_pinene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BetaAve)
                    .HasColumnName("beta_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BetaHigh)
                    .HasColumnName("beta_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BetaLow)
                    .HasColumnName("beta_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CaryophylleneAve)
                    .HasColumnName("caryophyllene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CaryophylleneHigh)
                    .HasColumnName("caryophyllene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CaryophylleneLow)
                    .HasColumnName("caryophyllene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CoHAve)
                    .HasColumnName("co_h_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CoHHigh)
                    .HasColumnName("co_h_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CoHLow)
                    .HasColumnName("co_h_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FarneseneAve)
                    .HasColumnName("farnesene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.FarneseneHigh)
                    .HasColumnName("farnesene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.FarneseneLow)
                    .HasColumnName("farnesene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.GeraniolAve)
                    .HasColumnName("geraniol_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.GeraniolHigh)
                    .HasColumnName("geraniol_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.GeraniolLow)
                    .HasColumnName("geraniol_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.HumuleneAve)
                    .HasColumnName("humulene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.HumuleneHigh)
                    .HasColumnName("humulene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.HumuleneLow)
                    .HasColumnName("humulene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.LinaloolAve)
                    .HasColumnName("linalool_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.LinaloolHigh)
                    .HasColumnName("linalool_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.LinaloolLow)
                    .HasColumnName("linalool_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.MyrceneAve)
                    .HasColumnName("myrcene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.MyrceneHigh)
                    .HasColumnName("myrcene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.MyrceneLow)
                    .HasColumnName("myrcene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OilAve)
                    .HasColumnName("oil_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OilHigh)
                    .HasColumnName("oil_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OilLow)
                    .HasColumnName("oil_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OtherAve)
                    .HasColumnName("other_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OtherHigh)
                    .HasColumnName("other_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.OtherLow)
                    .HasColumnName("other_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.ProductLineId)
                    .HasColumnName("product_line_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SilineneAve)
                    .HasColumnName("silinene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.SilineneHigh)
                    .HasColumnName("silinene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.SilineneLow)
                    .HasColumnName("silinene_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.ProductLine)
                    .WithMany(p => p.BrewingValues)
                    .HasForeignKey(d => d.ProductLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brewing_values_product_line_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.BrewingValues)
                    .HasForeignKey(d => d.VarietyId)
                    .HasConstraintName("brewing_values_variety_id_foreign");
            });

            modelBuilder.Entity<CountryCodes>(entity =>
            {
                entity.ToTable("country_codes");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("country_codes_country_code_unique")
                    .IsUnique();

                entity.HasIndex(e => e.MagentoCountryCodeId)
                    .HasName("country_codes_magento_country_code_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("country_code")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("country_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.MagentoCountryCodeId)
                    .HasColumnName("magento_country_code_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Cultivars>(entity =>
            {
                entity.ToTable("cultivars");

                entity.HasIndex(e => e.Cultivar)
                    .HasName("cultivars_cultivar_unique")
                    .IsUnique();

                entity.HasIndex(e => e.MagentoCultivarId)
                    .HasName("cultivars_magento_cultivar_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Cultivar)
                    .IsRequired()
                    .HasColumnName("cultivar")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MagentoCultivarId)
                    .HasColumnName("magento_cultivar_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Migrations>(entity =>
            {
                entity.ToTable("migrations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Batch)
                    .HasColumnName("batch")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Migration)
                    .IsRequired()
                    .HasColumnName("migration")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<ModelHasPermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.ModelId, e.ModelType })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("model_has_permissions");

                entity.HasIndex(e => new { e.ModelId, e.ModelType })
                    .HasName("model_has_permissions_model_id_model_type_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ModelType)
                    .HasColumnName("model_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ModelHasPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("model_has_permissions_permission_id_foreign");
            });

            modelBuilder.Entity<ModelHasRoles>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ModelId, e.ModelType })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("model_has_roles");

                entity.HasIndex(e => new { e.ModelId, e.ModelType })
                    .HasName("model_has_roles_model_id_model_type_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ModelType)
                    .HasColumnName("model_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ModelHasRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("model_has_roles_role_id_foreign");
            });

            modelBuilder.Entity<PasswordResets>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("password_resets");

                entity.HasIndex(e => e.Email)
                    .HasName("password_resets_email_index");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.ToTable("permissions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GuardName)
                    .IsRequired()
                    .HasColumnName("guard_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<PimTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("pim_test");

                entity.Property(e => e.AlphaAve)
                    .HasColumnName("alpha_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.AlphaHigh)
                    .HasColumnName("alpha_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BPineneHigh)
                    .HasColumnName("b_pinene_high")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BetaAve)
                    .HasColumnName("beta_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.BetaLow)
                    .HasColumnName("beta_low")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.CaryophylleneAve)
                    .HasColumnName("caryophyllene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HumuleneAve)
                    .HasColumnName("humulene_ave")
                    .HasColumnType("double(4,1)");

                entity.Property(e => e.IsnullAlphaLow)
                    .HasColumnName("ISNULL(alpha_low)")
                    .HasColumnType("int(1)");

                entity.Property(e => e.ProductLineName)
                    .IsRequired()
                    .HasColumnName("product_line_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<ProductLines>(entity =>
            {
                entity.ToTable("product_lines");

                entity.HasIndex(e => e.MagentoProductLineId)
                    .HasName("product_lines_magento_product_line_id_unique")
                    .IsUnique();

                entity.HasIndex(e => e.ProductLineCode)
                    .HasName("product_lines_product_line_code_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.MagentoProductLineId)
                    .HasColumnName("magento_product_line_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ProductLineCode)
                    .IsRequired()
                    .HasColumnName("product_line_code")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductLineName)
                    .IsRequired()
                    .HasColumnName("product_line_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<RoleHasPermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("role_has_permissions");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_has_permissions_role_id_foreign");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RoleHasPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("role_has_permissions_permission_id_foreign");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleHasPermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("role_has_permissions_role_id_foreign");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GuardName)
                    .IsRequired()
                    .HasColumnName("guard_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EmailVerifiedAt)
                    .HasColumnName("email_verified_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RememberToken)
                    .HasColumnName("remember_token")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Varieties>(entity =>
            {
                entity.ToTable("varieties");

                entity.HasIndex(e => e.BrandId)
                    .HasName("varieties_brand_id_foreign");

                entity.HasIndex(e => e.CountryId)
                    .HasName("varieties_country_id_foreign");

                entity.HasIndex(e => e.CultivarId)
                    .HasName("varieties_cultivar_id_foreign");

                entity.HasIndex(e => e.MagentoVarietyId)
                    .HasName("varieties_magento_variety_id_unique")
                    .IsUnique();

                entity.HasIndex(e => new { e.VarietyCode, e.DeletedAt })
                    .HasName("varieties_variety_code_deleted_at_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AromaDescription)
                    .HasColumnName("aroma_description")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Blend).HasColumnName("blend");

                entity.Property(e => e.BrandId)
                    .HasColumnName("brand_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CultivarId)
                    .HasColumnName("cultivar_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Experimental).HasColumnName("experimental");

                entity.Property(e => e.Featured).HasColumnName("featured");

                entity.Property(e => e.MagentoSyncedAt)
                    .HasColumnName("magento_synced_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.MagentoVarietyId)
                    .HasColumnName("magento_variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MarketingDescription)
                    .HasColumnName("marketing_description")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Organic).HasColumnName("organic");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ShowEcommerceGraph)
                    .IsRequired()
                    .HasColumnName("show_ecommerce_graph")
                    .HasDefaultValueSql("'1'");
                
                entity.Property(e => e.AllowMagentoSync)
                    .IsRequired()
                    .HasColumnName("allow_magento_sync")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Varieties)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("varieties_brand_id_foreign");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Varieties)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("varieties_country_id_foreign");

                entity.HasOne(d => d.Cultivar)
                    .WithMany(p => p.Varieties)
                    .HasForeignKey(d => d.CultivarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("varieties_cultivar_id_foreign");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
