using Microsoft.EntityFrameworkCore;
using Ych.Api.Data.GrowerPortal.Models;

namespace Ych.Api.Data.GrowerPortal
{
    public partial class GrowerPortalDataSource
    {
        public virtual DbSet<ActionEvents> ActionEvents { get; set; }
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<BrewerFeedback> BrewerFeedback { get; set; }
        public virtual DbSet<Cache> Cache { get; set; }
        public virtual DbSet<ChemicalType> ChemicalTypes { get; set; }
        public virtual DbSet<Chemicals> Chemicals { get; set; }
        public virtual DbSet<Facilities> Facilities { get; set; }
        public virtual DbSet<FacilityGrower> FacilityGrower { get; set; }
        public virtual DbSet<FieldLot> FieldLot { get; set; }
        public virtual DbSet<FieldMetaData> FieldMetaData { get; set; }
        public virtual DbSet<FieldSpray> FieldSpray { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FilesComments> FilesComments { get; set; }
        public virtual DbSet<FinancialRecords> FinancialRecords { get; set; }
        public virtual DbSet<FinancialUploads> FinancialUploads { get; set; }
        public virtual DbSet<GlobalQc> GlobalQc { get; set; }
        public virtual DbSet<GrowerHga> GrowerHga { get; set; }
        public virtual DbSet<GrowerUser> GrowerUser { get; set; }
        public virtual DbSet<Growerfields> Growerfields { get; set; }
        public virtual DbSet<Growerlots> Growerlots { get; set; }
        public virtual DbSet<Growers> Growers { get; set; }
        public virtual DbSet<HarvestDatasheets> HarvestDatasheets { get; set; }
        public virtual DbSet<Hgas> Hgas { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<KilnFuels> KilnFuels { get; set; }
        public virtual DbSet<LotQc> LotQc { get; set; }
        public virtual DbSet<MapBookmarks> MapBookmarks { get; set; }
        public virtual DbSet<MapPointTypes> MapPointTypes { get; set; }
        public virtual DbSet<MapPoints> MapPoints { get; set; }
        public virtual DbSet<MapVarietyStyles> MapVarietyStyles { get; set; }
        public virtual DbSet<MeasurementUnits> MeasurementUnits { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<ModelHasPermissions> ModelHasPermissions { get; set; }
        public virtual DbSet<ModelHasRoles> ModelHasRoles { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<OfficialFeedback> OfficialFeedback { get; set; }
        public virtual DbSet<PasswordResets> PasswordResets { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PickerTypes> PickerTypes { get; set; }
        public virtual DbSet<ReportNotifications> ReportNotifications { get; set; }
        public virtual DbSet<RoleHasPermissions> RoleHasPermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<SocialAccounts> SocialAccounts { get; set; }
        public virtual DbSet<SprayApplicationMethods> SprayApplicationMethods { get; set; }
        public virtual DbSet<SprayLicensees> SprayLicensees { get; set; }
        public virtual DbSet<Sprays> Sprays { get; set; }
        public virtual DbSet<SpraysPollLogs> SpraysPollLogs { get; set; }
        public virtual DbSet<SpraysUnusableImports> SpraysUnusableImports { get; set; }
        public virtual DbSet<StorageConditions> StorageConditions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vwbaledeliverylotsandvarieties> Vwbaledeliverylotsandvarieties { get; set; }
        public virtual DbSet<Vwharvestviewdatabyfield2020> Vwharvestviewdatabyfield2020 { get; set; }
        public virtual DbSet<Vwharvestviewdatabylot2020> Vwharvestviewdatabylot2020 { get; set; }
        public virtual DbSet<WashingtonPoliticalBoundariesCounties> WashingtonPoliticalBoundariesCounties { get; set; }
        public virtual DbSet<WashingtonPoliticalBoundariesSections> WashingtonPoliticalBoundariesSections { get; set; }
        public virtual DbSet<WashingtonPoliticalBoundariesTownships> WashingtonPoliticalBoundariesTownships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionEvents>(entity =>
            {
                entity.ToTable("action_events");

                entity.HasIndex(e => e.UserId)
                    .HasName("action_events_user_id_index");

                entity.HasIndex(e => new { e.ActionableType, e.ActionableId })
                    .HasName("action_events_actionable_type_actionable_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => new { e.BatchId, e.ModelType, e.ModelId })
                    .HasName("action_events_batch_id_model_type_model_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ActionableId)
                    .HasColumnName("actionable_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ActionableType)
                    .IsRequired()
                    .HasColumnName("actionable_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BatchId)
                    .HasColumnName("batch_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Changes)
                    .HasColumnName("changes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Exception)
                    .IsRequired()
                    .HasColumnName("exception")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Fields)
                    .IsRequired()
                    .HasColumnName("fields")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ModelType)
                    .IsRequired()
                    .HasColumnName("model_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Original)
                    .HasColumnName("original")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("'running'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TargetId)
                    .HasColumnName("target_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TargetType)
                    .IsRequired()
                    .HasColumnName("target_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.ToTable("audit_logs");

                entity.HasIndex(e => e.UserId)
                    .HasName("audit_logs_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasColumnType("blob");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("audit_logs_user_id_foreign");
            });

            modelBuilder.Entity<BrewerFeedback>(entity =>
            {
                entity.ToTable("brewer_feedback");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Agent)
                    .IsRequired()
                    .HasColumnName("agent")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Berry)
                    .HasColumnName("berry")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BurntRubber)
                    .HasColumnName("burnt_rubber")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Cardboard)
                    .HasColumnName("cardboard")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Catty)
                    .HasColumnName("catty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Cheesy)
                    .HasColumnName("cheesy")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Citrus)
                    .HasColumnName("citrus")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Customer)
                    .IsRequired()
                    .HasColumnName("customer")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Diesel)
                    .HasColumnName("diesel")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Earthy)
                    .HasColumnName("earthy")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Evergreen)
                    .HasColumnName("evergreen")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Floral)
                    .HasColumnName("floral")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Fruity)
                    .HasColumnName("fruity")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Grassy)
                    .HasColumnName("grassy")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Herbal)
                    .HasColumnName("herbal")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Melon)
                    .HasColumnName("melon")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Nutty)
                    .HasColumnName("nutty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Og)
                    .HasColumnName("og")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Plastic)
                    .HasColumnName("plastic")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Pomme)
                    .HasColumnName("pomme")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Ranking)
                    .HasColumnName("ranking")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Selected)
                    .HasColumnName("selected")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SelectionNotes)
                    .HasColumnName("selection_notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SensoryNotes)
                    .HasColumnName("sensory_notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Smoky)
                    .HasColumnName("smoky")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spicy)
                    .HasColumnName("spicy")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StoneFruit)
                    .HasColumnName("stone_fruit")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Submitted)
                    .HasColumnName("submitted")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sulfur)
                    .HasColumnName("sulfur")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Sweaty)
                    .HasColumnName("sweaty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SweetAromatic)
                    .HasColumnName("sweet_aromatic")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Tropical)
                    .HasColumnName("tropical")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Vegetal)
                    .HasColumnName("vegetal")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Woody)
                    .HasColumnName("woody")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Cache>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cache");

                entity.HasIndex(e => e.Key)
                    .HasName("cache_key_unique")
                    .IsUnique();

                entity.Property(e => e.Expiration)
                    .HasColumnName("expiration")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<ChemicalType>(entity =>
            {
                entity.ToTable("chemical_types");

                entity.HasIndex(e => e.Type)
                    .HasName("chemical_types_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Chemicals>(entity =>
            {
                entity.ToTable("chemicals");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AgrianEpaRegNum)
                    .HasColumnName("agrian_epa_reg_num")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AgrianProductId)
                    .HasColumnName("agrian_product_id")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AustraliaMrl)
                    .HasColumnName("australia_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BannedEu).HasColumnName("banned_eu");

                entity.Property(e => e.BannedJp).HasColumnName("banned_jp");

                entity.Property(e => e.BannedKo)
                    .HasColumnName("banned_ko")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BannedTw)
                    .HasColumnName("banned_tw")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CanadaMrl)
                    .HasColumnName("canada_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ChemicalType)
                    .HasColumnName("chemical_type")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CodexMrl)
                    .HasColumnName("codex_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CommonName)
                    .HasColumnName("common_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CutoffEu)
                    .HasColumnName("cutoff_eu")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CutoffJp)
                    .HasColumnName("cutoff_jp")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EpaRegNum)
                    .HasColumnName("epa_reg_num")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EuMrl)
                    .HasColumnName("eu_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IntervalDaysLabel)
                    .HasColumnName("interval_days_label")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JpMrl)
                    .HasColumnName("jp_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LabelRate)
                    .HasColumnName("label_rate")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MaxAppsNum)
                    .HasColumnName("max_apps_num")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MaxRatePerSeason)
                    .HasColumnName("max_rate_per_season")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OrganicCertificate)
                    .HasColumnName("organic_certificate")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PhiDaysLabel)
                    .HasColumnName("phi_days_label")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PhiEu)
                    .HasColumnName("phi_eu")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PhiJp)
                    .HasColumnName("phi_jp")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ReentryHours)
                    .HasColumnName("reentry_hours")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SignalWord)
                    .HasColumnName("signal_word")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TradeName)
                    .HasColumnName("trade_name")
                    .HasColumnType("varchar(75)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UOfM)
                    .HasColumnName("u_of_m")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UOfMRate)
                    .HasColumnName("u_of_m_rate")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UsMrl)
                    .HasColumnName("us_mrl")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Facilities>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.KilnFuelId)
                    .HasColumnName("kiln_fuel_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PickerTypeId)
                    .HasColumnName("picker_type_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<FacilityGrower>(entity =>
            {
                entity.HasKey(e => new { e.FacilityId, e.GrowerId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("facility_grower");

                entity.Property(e => e.FacilityId)
                    .HasColumnName("facility_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<FieldLot>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.LotId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("field_lot");

                entity.Property(e => e.FieldId)
                    .HasColumnName("field_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LotId)
                    .HasColumnName("lot_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<FieldMetaData>(entity =>
            {
                entity.ToTable("field_meta_data");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FieldId)
                    .HasColumnName("field_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Planted)
                    .HasColumnName("planted")
                    .HasColumnType("datetime");

                entity.Property(e => e.AmountPlanted)
                    .HasColumnName("amount_planted")
                    .HasColumnType("int(11)");
                
                entity.Property(e => e.MediaId)
                    .HasColumnName("media_id")
                    .HasColumnType("int(11)");
                
                entity.Property(e => e.TraceabilityId)
                    .HasColumnName("traceability _id")
                    .HasColumnType("utf8mb4_unicode_ci");
                
                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("utf8mb4_unicode_ci");
                
                entity.Property(e => e.Organic)
                    .HasColumnName("organic")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Variety)
                    .IsRequired()
                    .HasColumnName("variety")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<FieldSpray>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.SprayId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("field_spray");

                entity.Property(e => e.FieldId)
                    .HasColumnName("field_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SprayId)
                    .HasColumnName("spray_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.ToTable("files");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'2019'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Received)
                    .HasColumnName("received")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceivedById)
                    .HasColumnName("received_by_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UploadedByAdmin).HasColumnName("uploaded_by_admin");
            });

            modelBuilder.Entity<FilesComments>(entity =>
            {
                entity.ToTable("files_comments");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("files_comments_author_id_foreign");

                entity.HasIndex(e => e.FileId)
                    .HasName("files_comments_file_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FileId)
                    .HasColumnName("file_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.FilesComments)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("files_comments_author_id_foreign");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.FilesComments)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("files_comments_file_id_foreign");
            });

            modelBuilder.Entity<FinancialRecords>(entity =>
            {
                entity.ToTable("financial_records");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AvgReturn)
                    .HasColumnName("avg_return")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FinancialUploadId)
                    .HasColumnName("financial_upload_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LbsDelivered)
                    .HasColumnName("lbs_delivered")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.LbsSettled)
                    .HasColumnName("lbs_settled")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.LbsUnsettled)
                    .HasColumnName("lbs_unsettled")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.MonthlyAvgReturn)
                    .HasColumnName("monthly_avg_return")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.MonthlyLbsSettled)
                    .HasColumnName("monthly_lbs_settled")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.MonthlyRevenue)
                    .HasColumnName("monthly_revenue")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.PaymentsReceived)
                    .HasColumnName("payments_received")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.RevenueEarned)
                    .HasColumnName("revenue_earned")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Variety)
                    .IsRequired()
                    .HasColumnName("variety")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<FinancialUploads>(entity =>
            {
                entity.ToTable("financial_uploads");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ReportTitle)
                    .IsRequired()
                    .HasColumnName("report_title")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<GlobalQc>(entity =>
            {
                entity.ToTable("global_qc");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("global_qc_variety_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AlphaAvg)
                    .HasColumnName("alpha_avg")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.AlphaBeta)
                    .HasColumnName("alpha_beta")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.AlphaHigh)
                    .HasColumnName("alpha_high")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.AlphaLow)
                    .HasColumnName("alpha_low")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.AromaShort)
                    .HasColumnName("aroma_short")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BPinene)
                    .IsRequired()
                    .HasColumnName("b_pinene")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BetaAvg)
                    .HasColumnName("beta_avg")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.BetaHigh)
                    .HasColumnName("beta_high")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.BetaLow)
                    .HasColumnName("beta_low")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Caryophyllene)
                    .IsRequired()
                    .HasColumnName("caryophyllene")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoH)
                    .HasColumnName("co_h")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Farnesene)
                    .IsRequired()
                    .HasColumnName("farnesene")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Geraniol)
                    .IsRequired()
                    .HasColumnName("geraniol")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Humulene)
                    .IsRequired()
                    .HasColumnName("humulene")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Linalool)
                    .IsRequired()
                    .HasColumnName("linalool")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Myrcene)
                    .IsRequired()
                    .HasColumnName("myrcene")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TotalOilAvg)
                    .HasColumnName("total_oil_avg")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.TotalOilHigh)
                    .HasColumnName("total_oil_high")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.TotalOilLow)
                    .HasColumnName("total_oil_low")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .IsRequired()
                    .HasColumnName("variety_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyName)
                    .IsRequired()
                    .HasColumnName("variety_name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<GrowerHga>(entity =>
            {
                entity.HasKey(e => new { e.GrowerId, e.HgaId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("grower_hga");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HgaId)
                    .HasColumnName("hga_id")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<GrowerUser>(entity =>
            {
                entity.HasKey(e => new { e.GrowerId, e.UserId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("grower_user");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Growerfields>(entity =>
            {
                entity.ToTable("growerfields");

                entity.HasIndex(e => new { e.FieldName, e.GrowerId })
                    .HasName("growerfields_field_name_grower_id_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Acres).HasColumnName("acres");

                entity.Property(e => e.AgrianSiteUuid)
                    .HasColumnName("agrian_site_uuid")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasColumnName("field_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Growerlots>(entity =>
            {
                entity.ToTable("growerlots");

                entity.HasIndex(e => e.LotNumber)
                    .HasName("growerlots_lot_number_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Mrl)
                    .HasColumnName("mrl")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MrlGeneratedAt)
                    .HasColumnName("mrl_generated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.MrlReasonText)
                    .HasColumnName("mrl_reason_text")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyOther)
                    .HasColumnName("variety_other")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Growers>(entity =>
            {
                entity.ToTable("growers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AgrianGrowerId)
                    .HasColumnName("agrian_grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AgrianGrowerUuid)
                    .HasColumnName("agrian_grower_uuid")
                    .HasColumnType("varchar(36)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FarmSatelliteImage)
                    .HasColumnName("farm_satellite_image")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<HarvestDatasheets>(entity =>
            {
                entity.ToTable("harvest_datasheets");

                entity.HasIndex(e => new { e.GrowerId, e.LotNumber })
                    .HasName("harvest_datasheets_grower_id_lot_number_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BaleMoistureLow)
                    .IsRequired()
                    .HasColumnName("bale_moisture_low")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BaleMoistureHigh)
                    .IsRequired()
                    .HasColumnName("bale_moisture_high")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BaleStorageConditions)
                    .HasColumnName("bale_storage_conditions")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoolingHoursBeforeBaler)
                    .IsRequired()
                    .HasColumnName("cooling_hours_before_baler")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoolingHoursInKiln)
                    .IsRequired()
                    .HasColumnName("cooling_hours_in_kiln")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DryingHours)
                    .IsRequired()
                    .HasColumnName("drying_hours")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DryingTempF)
                    .IsRequired()
                    .HasColumnName("drying_temp_f")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FacilityId)
                    .HasColumnName("facility_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FacilityOther)
                    .HasColumnName("facility_other")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrownBy)
                    .HasColumnName("grown_by")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HarvestEndAt)
                    .HasColumnName("harvest_end_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.HarvestStartAt)
                    .HasColumnName("harvest_start_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Humidified).HasColumnName("humidified");

                entity.Property(e => e.KilnDepthIn)
                    .IsRequired()
                    .HasColumnName("kiln_depth_in")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.KilnFuelId)
                    .HasColumnName("kiln_fuel_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PickerTypeId)
                    .HasColumnName("picker_type_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TotalBales)
                    .IsRequired()
                    .HasColumnName("total_bales")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyAgrian)
                    .HasColumnName("variety_agrian")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyOther)
                    .HasColumnName("variety_other")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Hgas>(entity =>
            {
                entity.ToTable("hgas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.HgaId)
                    .HasColumnName("hga_id")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.ToTable("jobs");

                entity.HasIndex(e => e.Queue)
                    .HasName("jobs_queue_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Attempts)
                    .HasColumnName("attempts")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.AvailableAt)
                    .HasColumnName("available_at")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasColumnName("queue")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ReservedAt)
                    .HasColumnName("reserved_at")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<KilnFuels>(entity =>
            {
                entity.ToTable("kiln_fuels");

                entity.HasIndex(e => e.Type)
                    .HasName("kiln_fuels_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<LotQc>(entity =>
            {
                entity.ToTable("lot_qc");

                entity.HasIndex(e => e.Lot)
                    .HasName("lot_qc_lot_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("decimal(4,0)");

                entity.Property(e => e.DateReceived)
                    .HasColumnName("date_received")
                    .HasColumnType("date");

                entity.Property(e => e.Hsi)
                    .HasColumnName("hsi")
                    .HasColumnType("decimal(5,3)");

                entity.Property(e => e.Lot)
                    .IsRequired()
                    .HasColumnName("lot")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MoistMax)
                    .HasColumnName("moist_max")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.MoistMin)
                    .HasColumnName("moist_min")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.MoistureMeter)
                    .HasColumnName("moisture_meter")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.MoistureOven)
                    .HasColumnName("moisture_oven")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Oil2MethylButyl)
                    .HasColumnName("oil_2_methyl_butyl")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilAPinene)
                    .HasColumnName("oil_a_pinene")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OilBPinene)
                    .HasColumnName("oil_b_pinene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilByDist)
                    .HasColumnName("oil_by_dist")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilCaryophyllene)
                    .HasColumnName("oil_caryophyllene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilCaryoxide)
                    .HasColumnName("oil_caryoxide")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilFarnesene)
                    .HasColumnName("oil_farnesene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilGeraniol)
                    .HasColumnName("oil_geraniol")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilHumulene)
                    .HasColumnName("oil_humulene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilLimonene)
                    .HasColumnName("oil_limonene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilLinalool)
                    .HasColumnName("oil_linalool")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilMethylHeptonate)
                    .HasColumnName("oil_methyl_heptonate")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilMethylOctonoate)
                    .HasColumnName("oil_methyl_octonoate")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.OilMyrcene)
                    .HasColumnName("oil_myrcene")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.QtyBales)
                    .HasColumnName("qty_bales")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.TempMax)
                    .HasColumnName("temp_max")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.TempMin)
                    .HasColumnName("temp_min")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UvAlpha)
                    .HasColumnName("uv_alpha")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.UvBeta)
                    .HasColumnName("uv_beta")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.X3Status)
                    .HasColumnName("x3_status")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<MapBookmarks>(entity =>
            {
                entity.ToTable("map_bookmarks");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BaseMap)
                    .IsRequired()
                    .HasColumnName("base_map")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("bigint(20)");

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

            modelBuilder.Entity<MapPointTypes>(entity =>
            {
                entity.ToTable("map_point_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AllowOverlap).HasColumnName("allow_overlap");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasColumnType("text")
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

            modelBuilder.Entity<MapPoints>(entity =>
            {
                entity.ToTable("map_points");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FieldId)
                    .HasColumnName("field_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MapVarietyStyles>(entity =>
            {
                entity.ToTable("map_variety_styles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<MeasurementUnits>(entity =>
            {
                entity.ToTable("measurement_units");

                entity.HasIndex(e => e.Type)
                    .HasName("measurement_units_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id")
                    .HasColumnType("int(10) unsigned");

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
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id")
                    .HasColumnType("int(10) unsigned");

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

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("news_author_id_foreign");

                entity.HasIndex(e => e.Slug)
                    .HasName("news_slug_unique")
                    .IsUnique();

                entity.HasIndex(e => e.Title)
                    .HasName("news_title_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AttachmentLocation)
                    .HasColumnName("attachment_location")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ImageLocation)
                    .HasColumnName("image_location")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdateAuthorId)
                    .HasColumnName("update_author_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("news_author_id_foreign");
            });

            modelBuilder.Entity<OfficialFeedback>(entity =>
            {
                entity.ToTable("official_feedback");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AromaProfile)
                    .IsRequired()
                    .HasColumnName("aroma_profile")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Greenness)
                    .HasColumnName("greenness")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Shatter)
                    .HasColumnName("shatter")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
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
                    .HasColumnType("int(10) unsigned");

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

            modelBuilder.Entity<PickerTypes>(entity =>
            {
                entity.ToTable("picker_types");

                entity.HasIndex(e => e.Type)
                    .HasName("picker_types_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ReportNotifications>(entity =>
            {
                entity.ToTable("report_notifications");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Frequency)
                    .HasColumnName("frequency")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReportTypeId)
                    .HasColumnName("report_type_id")
                    .HasColumnType("int(11)");

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
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(10) unsigned");

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
                    .HasColumnType("int(10) unsigned");

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

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sessions");

                entity.HasIndex(e => e.Id)
                    .HasName("sessions_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LastActivity)
                    .HasColumnName("last_activity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SocialAccounts>(entity =>
            {
                entity.ToTable("social_accounts");

                entity.HasIndex(e => e.UserId)
                    .HasName("social_accounts_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasColumnName("provider")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProviderId)
                    .IsRequired()
                    .HasColumnName("provider_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SocialAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("social_accounts_user_id_foreign");
            });

            modelBuilder.Entity<SprayApplicationMethods>(entity =>
            {
                entity.ToTable("spray_application_methods");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

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

            modelBuilder.Entity<SprayLicensees>(entity =>
            {
                entity.ToTable("spray_licensees");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FirmName)
                    .HasColumnName("firm_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IndividualName)
                    .IsRequired()
                    .HasColumnName("individual_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LicenseNumber)
                    .IsRequired()
                    .HasColumnName("license_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Sprays>(entity =>
            {
                entity.ToTable("sprays");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AgrianPurId)
                    .HasColumnName("agrian_pur_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AgrianReportedAt)
                    .HasColumnName("agrian_reported_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.AgrianViewUrl)
                    .HasColumnName("agrian_view_url")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnName("application_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApplicationTimeEnd)
                    .HasColumnName("application_time_end")
                    .HasColumnType("time");

                entity.Property(e => e.ApplicationTimeStart)
                    .HasColumnName("application_time_start")
                    .HasColumnType("time");

                entity.Property(e => e.ChemicalId)
                    .HasColumnName("chemical_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ConcentrationApplied)
                    .HasColumnName("concentration_applied")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CropYear)
                    .IsRequired()
                    .HasColumnName("crop_year")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CustomChemApproved)
                    .HasColumnName("custom_chem_approved")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomChemApprovedById)
                    .HasColumnName("custom_chem_approved_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrowerId)
                    .IsRequired()
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MeasurementUnitId)
                    .HasColumnName("measurement_unit_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MeasurementUnitOther)
                    .HasColumnName("measurement_unit_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OtherChemicalType)
                    .HasColumnName("other_chemical_type")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.OtherChemicalTypeName)
                    .HasColumnName("other_chemical_type_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OtherCommonName)
                    .HasColumnName("other_common_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OtherEpaRegNum)
                    .HasColumnName("other_epa_reg_num")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OtherTradeName)
                    .HasColumnName("other_trade_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RatePerAcre)
                    .IsRequired()
                    .HasColumnName("rate_per_acre")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SprayApplicationMethodId)
                    .HasColumnName("spray_application_method_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SprayLicenseeId)
                    .HasColumnName("spray_licensee_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.TemperatureRangeHigh)
                    .HasColumnName("temperature_range_high")
                    .HasColumnType("decimal(4,1) unsigned");

                entity.Property(e => e.TemperatureRangeLow)
                    .HasColumnName("temperature_range_low")
                    .HasColumnType("decimal(4,1) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WindVector)
                    .HasColumnName("wind_vector")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<SpraysPollLogs>(entity =>
            {
                entity.ToTable("sprays_poll_logs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DateEnd)
                    .HasColumnName("date_end")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateStart)
                    .HasColumnName("date_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Exception)
                    .HasColumnName("exception")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TotalSpraysFound)
                    .HasColumnName("total_sprays_found")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UnusableSprays)
                    .HasColumnName("unusable_sprays")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UsableSprays)
                    .HasColumnName("usable_sprays")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpraysUnusableImports>(entity =>
            {
                entity.ToTable("sprays_unusable_imports");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AgrianPurId)
                    .HasColumnName("agrian_pur_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AgrianReportedAt)
                    .HasColumnName("agrian_reported_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.AgrianViewUrl)
                    .HasColumnName("agrian_view_url")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnName("application_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Exception)
                    .HasColumnName("exception")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<StorageConditions>(entity =>
            {
                entity.ToTable("storage_conditions");

                entity.HasIndex(e => e.Type)
                    .HasName("storage_conditions_type_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
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
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AvatarLocation)
                    .HasColumnName("avatar_location")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AvatarType)
                    .IsRequired()
                    .HasColumnName("avatar_type")
                    .HasColumnType("varchar(191)")
                    .HasDefaultValueSql("'gravatar'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ConfirmationCode)
                    .HasColumnName("confirmation_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Confirmed)
                    .IsRequired()
                    .HasColumnName("confirmed")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PasswordChangedAt)
                    .HasColumnName("password_changed_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RememberToken)
                    .HasColumnName("remember_token")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Timezone)
                    .IsRequired()
                    .HasColumnName("timezone")
                    .HasColumnType("varchar(191)")
                    .HasDefaultValueSql("'AmericaLos_Angeles'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Vwbaledeliverylotsandvarieties>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwbaledeliverylotsandvarieties");

                entity.Property(e => e.Lot)
                    .IsRequired()
                    .HasColumnName("lot")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Variety)
                    .IsRequired()
                    .HasColumnName("variety")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Vwharvestviewdatabyfield2020>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwharvestviewdatabyfield2020");

                entity.Property(e => e.Acres).HasColumnName("acres");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasColumnName("field_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerName)
                    .HasColumnName("grower_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HarvestEndAt)
                    .HasColumnName("harvest_end_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.HarvestStartAt)
                    .HasColumnName("harvest_start_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LotCount)
                    .HasColumnName("lot_count")
                    .HasColumnType("bigint(21)");

                entity.Property(e => e.Lots)
                    .HasColumnName("lots")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.MaleHopPlantCount)
                    .HasColumnName("male_hop_plant_count")
                    .HasColumnType("bigint(21)");
            });

            modelBuilder.Entity<Vwharvestviewdatabylot2020>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwharvestviewdatabylot2020");

                entity.Property(e => e.BaleMoistureLow)
                    .IsRequired()
                    .HasColumnName("bale_moisture_low")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BaleMoistureHigh)
                    .IsRequired()
                    .HasColumnName("bale_moisture_high")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BaleStorageConditions)
                    .HasColumnName("bale_storage_conditions")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoolingHoursBeforeBaler)
                    .IsRequired()
                    .HasColumnName("cooling_hours_before_baler")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoolingHoursInKiln)
                    .IsRequired()
                    .HasColumnName("cooling_hours_in_kiln")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DryingHours)
                    .IsRequired()
                    .HasColumnName("drying_hours")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DryingTempF)
                    .IsRequired()
                    .HasColumnName("drying_temp_f")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Facility)
                    .HasColumnName("facility")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerId)
                    .HasColumnName("grower_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrowerName)
                    .HasColumnName("grower_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GrownBy)
                    .HasColumnName("grown_by")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.HarvestEndAt)
                    .HasColumnName("harvest_end_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.HarvestStartAt)
                    .HasColumnName("harvest_start_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Humidified).HasColumnName("humidified");

                entity.Property(e => e.KilnDepthIn)
                    .IsRequired()
                    .HasColumnName("kiln_depth_in")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.KilnFuel)
                    .HasColumnName("kiln_fuel")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PickerType)
                    .HasColumnName("picker_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TotalBales)
                    .IsRequired()
                    .HasColumnName("total_bales")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Variety)
                    .HasColumnName("variety")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<WashingtonPoliticalBoundariesCounties>(entity =>
            {
                entity.ToTable("washington_political_boundaries_counties");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.EditDate)
                    .HasColumnName("edit_date")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditStatus)
                    .HasColumnName("edit_status")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditWho)
                    .HasColumnName("edit_who")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictDesgCd)
                    .HasColumnName("jurisdict_desg_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictFipsDesgCd)
                    .HasColumnName("jurisdict_fips_desg_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictLabelNm)
                    .HasColumnName("jurisdict_label_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictNm)
                    .HasColumnName("jurisdict_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictSystId)
                    .HasColumnName("jurisdict_syst_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JurisdictTypeCd)
                    .HasColumnName("jurisdict_type_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<WashingtonPoliticalBoundariesSections>(entity =>
            {
                entity.ToTable("washington_political_boundaries_sections");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AquaticLandFlg)
                    .HasColumnName("aquatic_land_flg")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.EditDate)
                    .HasColumnName("edit_date")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditStatus)
                    .HasColumnName("edit_status")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditWho)
                    .HasColumnName("edit_who")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescDupStatusFlg)
                    .HasColumnName("legal_desc_dup_status_flg")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescLabelNm)
                    .HasColumnName("legal_desc_label_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescNm)
                    .HasColumnName("legal_desc_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescSystId)
                    .HasColumnName("legal_desc_syst_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescTypeCd)
                    .HasColumnName("legal_desc_type_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsMeridianCd)
                    .HasColumnName("pls_meridian_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngDirCd)
                    .HasColumnName("pls_rng_dir_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngFractCd)
                    .HasColumnName("pls_rng_fract_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngNo)
                    .HasColumnName("pls_rng_no")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpDirCd)
                    .HasColumnName("pls_twp_dir_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpFractCd)
                    .HasColumnName("pls_twp_fract_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpNo)
                    .HasColumnName("pls_twp_no")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpSubdivNo)
                    .HasColumnName("pls_twp_subdiv_no")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpSubdivTypeCd)
                    .HasColumnName("pls_twp_subdiv_type_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ShapeArea)
                    .HasColumnName("shape_area")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ShapeLen)
                    .HasColumnName("shape_len")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<WashingtonPoliticalBoundariesTownships>(entity =>
            {
                entity.ToTable("washington_political_boundaries_townships");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AquaticLandFlg)
                    .HasColumnName("aquatic_land_flg")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.EditDate)
                    .HasColumnName("edit_date")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditStatus)
                    .HasColumnName("edit_status")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EditWho)
                    .HasColumnName("edit_who")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescDupStatusFlg)
                    .HasColumnName("legal_desc_dup_status_flg")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescEstDt)
                    .HasColumnName("legal_desc_est_dt")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescLabelNm)
                    .HasColumnName("legal_desc_label_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescNm)
                    .HasColumnName("legal_desc_nm")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescSystId)
                    .HasColumnName("legal_desc_syst_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalDescTypeCd)
                    .HasColumnName("legal_desc_type_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsMeridianCd)
                    .HasColumnName("pls_meridian_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngDirCd)
                    .HasColumnName("pls_rng_dir_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngFractCd)
                    .HasColumnName("pls_rng_fract_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsRngNo)
                    .HasColumnName("pls_rng_no")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpDirCd)
                    .HasColumnName("pls_twp_dir_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpFractCd)
                    .HasColumnName("pls_twp_fract_cd")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PlsTwpNo)
                    .HasColumnName("pls_twp_no")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ShapeArea)
                    .HasColumnName("shape_area")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ShapeLength)
                    .HasColumnName("shape_length")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
