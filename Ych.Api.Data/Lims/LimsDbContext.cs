using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Lims.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Lims
{
    public partial class LimsDataSource 
    {
        public virtual DbSet<ActionEvents> ActionEvents { get; set; }
        public virtual DbSet<FailedJobs> FailedJobs { get; set; }
        public virtual DbSet<MetaHarvests> MetaHarvests { get; set; }
        public virtual DbSet<MetaProduction> MetaProduction { get; set; }
        public virtual DbSet<MetaTimeOfProcessings> MetaTimeOfProcessings { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<PasswordResets> PasswordResets { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<ResultsDryMatter> ResultsDryMatter { get; set; }
        public virtual DbSet<ResultsHplc> ResultsHplc { get; set; }
        public virtual DbSet<ResultsLcv> ResultsLcv { get; set; }
        public virtual DbSet<ResultsOilComponents> ResultsOilComponents { get; set; }
        public virtual DbSet<ResultsOvenMoisture> ResultsOvenMoisture { get; set; }
        public virtual DbSet<ResultsTotalOil> ResultsTotalOil { get; set; }
        public virtual DbSet<ResultsUv> ResultsUv { get; set; }
        public virtual DbSet<Revisions> Revisions { get; set; }
        public virtual DbSet<SampleCodeLists> SampleCodeLists { get; set; }
        public virtual DbSet<SampleTypes> SampleTypes { get; set; }
        public virtual DbSet<Samples> Samples { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Users> Users { get; set; }

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
                    .HasColumnType("varchar(255)")
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<FailedJobs>(entity =>
            {
                entity.ToTable("failed_jobs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Connection)
                    .IsRequired()
                    .HasColumnName("connection")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Exception)
                    .IsRequired()
                    .HasColumnName("exception")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FailedAt)
                    .HasColumnName("failed_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasColumnName("queue")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<MetaHarvests>(entity =>
            {
                entity.ToTable("meta_harvests");

                entity.HasIndex(e => e.SampleId)
                    .HasName("meta_harvests_sample_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BaleCount)
                    .HasColumnName("bale_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BaleLotNumber)
                    .IsRequired()
                    .HasColumnName("bale_lot_number")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Mrl)
                    .HasColumnName("mrl")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProbeMoistureMax)
                    .HasColumnName("probe_moisture_max")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.ProbeMoistureMin)
                    .HasColumnName("probe_moisture_min")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.ProbeTempMax)
                    .HasColumnName("probe_temp_max")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.ProbeTempMin)
                    .HasColumnName("probe_temp_min")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.SampleId)
                    .HasColumnName("sample_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TruckNumber)
                    .HasColumnName("truck_number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Sample)
                    .WithMany(p => p.MetaHarvests)
                    .HasForeignKey(d => d.SampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meta_harvests_sample_id_foreign");
            });

            modelBuilder.Entity<MetaProduction>(entity =>
            {
                entity.ToTable("meta_production");

                entity.HasIndex(e => e.SampleId)
                    .HasName("meta_production_sample_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Mrl)
                    .HasColumnName("mrl")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductionLotNumber)
                    .IsRequired()
                    .HasColumnName("production_lot_number")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleId)
                    .HasColumnName("sample_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TestNumber)
                    .HasColumnName("test_number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Sample)
                    .WithMany(p => p.MetaProduction)
                    .HasForeignKey(d => d.SampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meta_production_sample_id_foreign");
            });

            modelBuilder.Entity<MetaTimeOfProcessings>(entity =>
            {
                entity.ToTable("meta_time_of_processings");

                entity.HasIndex(e => e.SampleId)
                    .HasName("meta_time_of_processings_sample_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BaleLotNumber)
                    .IsRequired()
                    .HasColumnName("bale_lot_number")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Mrl)
                    .HasColumnName("mrl")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductionLotNumber)
                    .IsRequired()
                    .HasColumnName("production_lot_number")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleId)
                    .HasColumnName("sample_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyCode)
                    .IsRequired()
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Sample)
                    .WithMany(p => p.MetaTimeOfProcessings)
                    .HasForeignKey(d => d.SampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("meta_time_of_processings_sample_id_foreign");
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.ToTable("product_types");

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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsDryMatter>(entity =>
            {
                entity.ToTable("results_dry_matter");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ExtractedMoistureVolume).HasColumnName("extracted_moisture_volume");

                entity.Property(e => e.FlaskIdentifier)
                    .HasColumnName("flask_identifier")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleWeight).HasColumnName("sample_weight");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsHplc>(entity =>
            {
                entity.ToTable("results_hplc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Alpha).HasColumnName("alpha");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Beta).HasColumnName("beta");

                entity.Property(e => e.Cohumulone).HasColumnName("cohumulone");

                entity.Property(e => e.Colupulone).HasColumnName("colupulone");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsLcv>(entity =>
            {
                entity.ToTable("results_lcv");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CoefficientOfVariation).HasColumnName("coefficient_of_variation");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleVolume).HasColumnName("sample_volume");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Value74).HasColumnName("value_7_4");

                entity.Property(e => e.Value75).HasColumnName("value_7_5");
            });

            modelBuilder.Entity<ResultsOilComponents>(entity =>
            {
                entity.ToTable("results_oil_components");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.APinene).HasColumnName("a_pinene");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BPinene).HasColumnName("b_pinene");

                entity.Property(e => e.Caryophyllene).HasColumnName("caryophyllene");

                entity.Property(e => e.CaryophylleneOxide).HasColumnName("caryophyllene_oxide");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Farnesene).HasColumnName("farnesene");

                entity.Property(e => e.Geraniol).HasColumnName("geraniol");

                entity.Property(e => e.Humulene).HasColumnName("humulene");

                entity.Property(e => e.Limonene).HasColumnName("limonene");

                entity.Property(e => e.Linalool).HasColumnName("linalool");

                entity.Property(e => e.MethylHeptanoate).HasColumnName("methyl_heptanoate");

                entity.Property(e => e.MethylOctonoate).HasColumnName("methyl_octonoate");

                entity.Property(e => e.Myrcene).HasColumnName("myrcene");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TwoMethylButylIsobutyrate).HasColumnName("two_methyl_butyl_isobutyrate");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsOvenMoisture>(entity =>
            {
                entity.ToTable("results_oven_moisture");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PanWeightDry)
                    .HasColumnName("pan_weight_dry")
                    .HasColumnType("double(8,5)");

                entity.Property(e => e.PanWeightEmpty)
                    .HasColumnName("pan_weight_empty")
                    .HasColumnType("double(8,5)");

                entity.Property(e => e.PanWeightWet)
                    .HasColumnName("pan_weight_wet")
                    .HasColumnType("double(8,5)");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsTotalOil>(entity =>
            {
                entity.ToTable("results_total_oil");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OilVolume).HasColumnName("oil_volume");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleWeight).HasColumnName("sample_weight");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<ResultsUv>(entity =>
            {
                entity.ToTable("results_uv");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Alpha).HasColumnName("alpha");

                entity.Property(e => e.AnalysisDate)
                    .HasColumnName("analysis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Beta).HasColumnName("beta");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Hsi).HasColumnName("hsi");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Revisions>(entity =>
            {
                entity.ToTable("revisions");

                entity.HasIndex(e => new { e.RevisionableId, e.RevisionableType })
                    .HasName("revisions_revisionable_id_revisionable_type_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.NewValue)
                    .HasColumnName("new_value")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OldValue)
                    .HasColumnName("old_value")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RevisionableId)
                    .HasColumnName("revisionable_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RevisionableType)
                    .IsRequired()
                    .HasColumnName("revisionable_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SampleCodeLists>(entity =>
            {
                entity.ToTable("sample_code_lists");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.SampleCodes)
                    .IsRequired()
                    .HasColumnName("sample_codes")
                    .HasColumnType("json");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<SampleTypes>(entity =>
            {
                entity.ToTable("sample_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ExpectsCumulativeDryMatter).HasColumnName("expects_cumulative_dry_matter");

                entity.Property(e => e.ExpectsCumulativeHplc).HasColumnName("expects_cumulative_hplc");

                entity.Property(e => e.ExpectsCumulativeLvc).HasColumnName("expects_cumulative_lvc");

                entity.Property(e => e.ExpectsCumulativeOil).HasColumnName("expects_cumulative_oil");

                entity.Property(e => e.ExpectsCumulativeOilComponents).HasColumnName("expects_cumulative_oil_components");

                entity.Property(e => e.ExpectsCumulativeOvenMoisture).HasColumnName("expects_cumulative_oven_moisture");

                entity.Property(e => e.ExpectsCumulativeUv).HasColumnName("expects_cumulative_uv");

                entity.Property(e => e.ExpectsDryMatter).HasColumnName("expects_dry_matter");

                entity.Property(e => e.ExpectsHplc).HasColumnName("expects_hplc");

                entity.Property(e => e.ExpectsLcv).HasColumnName("expects_lcv");

                entity.Property(e => e.ExpectsOil).HasColumnName("expects_oil");

                entity.Property(e => e.ExpectsOilComponents).HasColumnName("expects_oil_components");

                entity.Property(e => e.ExpectsOvenMoisture).HasColumnName("expects_oven_moisture");

                entity.Property(e => e.ExpectsUv).HasColumnName("expects_uv");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Samples>(entity =>
            {
                entity.ToTable("samples");

                entity.HasIndex(e => e.SampleTypeId)
                    .HasName("samples_sample_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GpSyncedAt)
                    .HasColumnName("gp_synced_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsCumulative).HasColumnName("is_cumulative");

                entity.Property(e => e.NeedsRerun).HasColumnName("needs_rerun");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("product_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SampleTypeId)
                    .HasColumnName("sample_type_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.X3SyncedAt)
                    .HasColumnName("x3_synced_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.SampleType)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.SampleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("samples_sample_type_id_foreign");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.ToTable("settings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
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

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.EmailVerifiedAt)
                    .HasColumnName("email_verified_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RememberToken)
                    .HasColumnName("remember_token")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
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
