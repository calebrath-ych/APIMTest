using Microsoft.EntityFrameworkCore;
using Ych.Api.Data.Ycrm.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Ycrm
{
    public partial class YcrmDataSource
    {
        public virtual DbSet<ActionEvents> ActionEvents { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Audits> Audits { get; set; }
        public virtual DbSet<Barrellages> Barrellages { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<CustomerProfiles> CustomerProfiles { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomersGrades> CustomersGrades { get; set; }
        public virtual DbSet<FailedJobs> FailedJobs { get; set; }
        public virtual DbSet<Fileables> Fileables { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<InteractionTypes> InteractionTypes { get; set; }
        public virtual DbSet<Interactions> Interactions { get; set; }
        public virtual DbSet<LegalActions> LegalActions { get; set; }
        public virtual DbSet<LegalOwed> LegalOwed { get; set; }
        public virtual DbSet<LegalProceedings> LegalProceedings { get; set; }
        public virtual DbSet<LegalStatuses> LegalStatuses { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<ModelHasPermissions> ModelHasPermissions { get; set; }
        public virtual DbSet<ModelHasRoles> ModelHasRoles { get; set; }
        public virtual DbSet<PasswordResets> PasswordResets { get; set; }
        public virtual DbSet<PaymentPlanStatuses> PaymentPlanStatuses { get; set; }
        public virtual DbSet<PaymentPlans> PaymentPlans { get; set; }
        public virtual DbSet<PaymentStatuses> PaymentStatuses { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PersonalAccessTokens> PersonalAccessTokens { get; set; }
        public virtual DbSet<RoleHasPermissions> RoleHasPermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Taggables> Taggables { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }
        public virtual DbSet<TerritoryUser> TerritoryUser { get; set; }
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

            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.ToTable("addresses");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("addresses_customer_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Primary).HasColumnName("primary");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Zip)
                    .HasColumnName("zip")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("addresses_customer_id_foreign");
            });

            modelBuilder.Entity<Audits>(entity =>
            {
                entity.ToTable("audits");

                entity.HasIndex(e => new { e.AuditableType, e.AuditableId })
                    .HasName("audits_auditable_type_auditable_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => new { e.UserId, e.UserType })
                    .HasName("audits_user_id_user_type_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AuditableId)
                    .HasColumnName("auditable_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AuditableType)
                    .IsRequired()
                    .HasColumnName("auditable_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasColumnName("event")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.NewValues)
                    .HasColumnName("new_values")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.OldValues)
                    .HasColumnName("old_values")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasColumnType("varchar(1023)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Barrellages>(entity =>
            {
                entity.ToTable("barrellages");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("barrellages_created_by_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("barrellages_customer_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Barrellages)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("barrellages_created_by_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Barrellages)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("barrellages_customer_id_foreign");
            });

            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.ToTable("currencies");

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

            modelBuilder.Entity<CustomerProfiles>(entity =>
            {
                entity.ToTable("customer_profiles");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("customer_profiles_created_by_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("customer_profiles_customer_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BarrelAgeing).HasColumnName("barrel_ageing");

                entity.Property(e => e.Bottles).HasColumnName("bottles");

                entity.Property(e => e.Cans).HasColumnName("cans");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Crowlers).HasColumnName("crowlers");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Draft).HasColumnName("draft");

                entity.Property(e => e.Growlers).HasColumnName("growlers");

                entity.Property(e => e.LocationsCount)
                    .HasColumnName("locations_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PilotSystem).HasColumnName("pilot_system");

                entity.Property(e => e.SelfDistribution).HasColumnName("self_distribution");

                entity.Property(e => e.SourProgram).HasColumnName("sour_program");

                entity.Property(e => e.TaproomCount)
                    .HasColumnName("taproom_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WholesaleDistribution).HasColumnName("wholesale_distribution");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CustomerProfiles)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_profiles_created_by_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerProfiles)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_profiles_customer_id_foreign");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.CustomerServiceSpecialistId)
                    .HasName("customers_customer_service_specialist_id_foreign");

                entity.HasIndex(e => e.RegionalSalesManagerId)
                    .HasName("customers_regional_sales_manager_id_foreign");

                entity.HasIndex(e => e.TerritoryId)
                    .HasName("customers_territory_id_foreign");

                entity.HasIndex(e => e.X3Id)
                    .HasName("customers_x3_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CustomerServiceSpecialistId)
                    .HasColumnName("customer_service_specialist_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DoingBusinessAs)
                    .HasColumnName("doing_business_as")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalName)
                    .HasColumnName("legal_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RegionalSalesManagerId)
                    .HasColumnName("regional_sales_manager_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("territory_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.X3GroupId)
                    .HasColumnName("x3_group_id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.X3Id)
                    .HasColumnName("x3_id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.CustomerServiceSpecialist)
                    .WithMany(p => p.CustomersCustomerServiceSpecialist)
                    .HasForeignKey(d => d.CustomerServiceSpecialistId)
                    .HasConstraintName("customers_customer_service_specialist_id_foreign");

                entity.HasOne(d => d.RegionalSalesManager)
                    .WithMany(p => p.CustomersRegionalSalesManager)
                    .HasForeignKey(d => d.RegionalSalesManagerId)
                    .HasConstraintName("customers_regional_sales_manager_id_foreign");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.TerritoryId)
                    .HasConstraintName("customers_territory_id_foreign");
            });

            modelBuilder.Entity<CustomersGrades>(entity =>
            {
                entity.ToTable("customers_grades");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("customers_grades_created_by_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("customers_grades_customer_id_foreign");

                entity.HasIndex(e => e.GradeId)
                    .HasName("customers_grades_grade_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.GradeId)
                    .HasColumnName("grade_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CustomersGrades)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customers_grades_created_by_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersGrades)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customers_grades_customer_id_foreign");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.CustomersGrades)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customers_grades_grade_id_foreign");
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

            modelBuilder.Entity<Fileables>(entity =>
            {
                entity.ToTable("fileables");

                entity.HasIndex(e => new { e.FileableType, e.FileableId })
                    .HasName("fileables_fileable_type_fileable_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => new { e.FileId, e.FileableId, e.FileableType })
                    .HasName("fileables_file_id_fileable_id_fileable_type_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.FileId)
                    .HasColumnName("file_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.FileableId)
                    .HasColumnName("fileable_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.FileableType)
                    .IsRequired()
                    .HasColumnName("fileable_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.Fileables)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fileables_file_id_foreign");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.ToTable("files");

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

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Grades>(entity =>
            {
                entity.ToTable("grades");

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

            modelBuilder.Entity<InteractionTypes>(entity =>
            {
                entity.ToTable("interaction_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasColumnName("icon")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<Interactions>(entity =>
            {
                entity.ToTable("interactions");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("interactions_created_by_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("interactions_customer_id_foreign");

                entity.HasIndex(e => e.TypeId)
                    .HasName("interactions_type_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.InteractionAt)
                    .HasColumnName("interaction_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Interactions)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interactions_created_by_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Interactions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interactions_customer_id_foreign");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Interactions)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interactions_type_id_foreign");
            });

            modelBuilder.Entity<LegalActions>(entity =>
            {
                entity.ToTable("legal_actions");

                entity.HasIndex(e => e.LegalProceedingId)
                    .HasName("legal_actions_legal_proceeding_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LegalProceedingId)
                    .HasColumnName("legal_proceeding_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.LegalProceeding)
                    .WithMany(p => p.LegalActions)
                    .HasForeignKey(d => d.LegalProceedingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("legal_actions_legal_proceeding_id_foreign");
            });

            modelBuilder.Entity<LegalOwed>(entity =>
            {
                entity.ToTable("legal_owed");

                entity.HasIndex(e => e.LegalProceedingId)
                    .HasName("legal_owed_legal_proceeding_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("double(15,2) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.LegalProceedingId)
                    .HasColumnName("legal_proceeding_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.LegalProceeding)
                    .WithMany(p => p.LegalOwed)
                    .HasForeignKey(d => d.LegalProceedingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("legal_owed_legal_proceeding_id_foreign");
            });

            modelBuilder.Entity<LegalProceedings>(entity =>
            {
                entity.ToTable("legal_proceedings");

                entity.HasIndex(e => e.CurrencyId)
                    .HasName("legal_proceedings_currency_id_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("legal_proceedings_customer_id_foreign");

                entity.HasIndex(e => e.InventoryOwedFileId)
                    .HasName("legal_proceedings_inventory_owed_file_id_foreign");

                entity.HasIndex(e => e.PastDueBalanceFileId)
                    .HasName("legal_proceedings_past_due_balance_file_id_foreign");

                entity.HasIndex(e => e.StatusId)
                    .HasName("legal_proceedings_status_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Charges)
                    .HasColumnName("charges")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CurrencyId)
                    .HasColumnName("currency_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.InventoryOwedFileId)
                    .HasColumnName("inventory_owed_file_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PastDueBalanceFileId)
                    .HasColumnName("past_due_balance_file_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UnpaidInvoice)
                    .HasColumnName("unpaid_invoice")
                    .HasColumnType("decimal(8,2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.LegalProceedings)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("legal_proceedings_currency_id_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LegalProceedings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("legal_proceedings_customer_id_foreign");

                entity.HasOne(d => d.InventoryOwedFile)
                    .WithMany(p => p.LegalProceedingsInventoryOwedFile)
                    .HasForeignKey(d => d.InventoryOwedFileId)
                    .HasConstraintName("legal_proceedings_inventory_owed_file_id_foreign");

                entity.HasOne(d => d.PastDueBalanceFile)
                    .WithMany(p => p.LegalProceedingsPastDueBalanceFile)
                    .HasForeignKey(d => d.PastDueBalanceFileId)
                    .HasConstraintName("legal_proceedings_past_due_balance_file_id_foreign");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.LegalProceedings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("legal_proceedings_status_id_foreign");
            });

            modelBuilder.Entity<LegalStatuses>(entity =>
            {
                entity.ToTable("legal_statuses");

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
                    .HasColumnType("varchar(255)")
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
                    .HasColumnType("varchar(255)")
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

            modelBuilder.Entity<PaymentPlanStatuses>(entity =>
            {
                entity.ToTable("payment_plan_statuses");

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

            modelBuilder.Entity<PaymentPlans>(entity =>
            {
                entity.ToTable("payment_plans");

                entity.HasIndex(e => e.CurrencyId)
                    .HasName("payment_plans_currency_id_foreign");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("payment_plans_customer_id_foreign");

                entity.HasIndex(e => e.PaymentPlanStatusId)
                    .HasName("payment_plans_payment_plan_status_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Autopay).HasColumnName("autopay");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CurrencyId)
                    .HasColumnName("currency_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DownPayment)
                    .HasColumnName("down_payment")
                    .HasColumnType("decimal(15,2) unsigned");

                entity.Property(e => e.FullyShipped).HasColumnName("fully_shipped");

                entity.Property(e => e.InstallmentAmount)
                    .HasColumnName("installment_amount")
                    .HasColumnType("decimal(15,2) unsigned");

                entity.Property(e => e.Installments)
                    .HasColumnName("installments")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PaymentPlanStatusId)
                    .HasColumnName("payment_plan_status_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TotalValue)
                    .HasColumnName("total_value")
                    .HasColumnType("decimal(15,2) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.PaymentPlans)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payment_plans_currency_id_foreign");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PaymentPlans)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payment_plans_customer_id_foreign");

                entity.HasOne(d => d.PaymentPlanStatus)
                    .WithMany(p => p.PaymentPlans)
                    .HasForeignKey(d => d.PaymentPlanStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payment_plans_payment_plan_status_id_foreign");
            });

            modelBuilder.Entity<PaymentStatuses>(entity =>
            {
                entity.ToTable("payment_statuses");

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

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.PaymentPlanId)
                    .HasName("payments_payment_plan_id_foreign");

                entity.HasIndex(e => e.PaymentStatusId)
                    .HasName("payments_payment_status_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DueAt)
                    .HasColumnName("due_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentPlanId)
                    .HasColumnName("payment_plan_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.PaymentStatusId)
                    .HasColumnName("payment_status_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.PaymentPlan)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payments_payment_plan_id_foreign");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payments_payment_status_id_foreign");
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<PersonalAccessTokens>(entity =>
            {
                entity.ToTable("personal_access_tokens");

                entity.HasIndex(e => e.Token)
                    .HasName("personal_access_tokens_token_unique")
                    .IsUnique();

                entity.HasIndex(e => new { e.TokenableType, e.TokenableId })
                    .HasName("personal_access_tokens_tokenable_type_tokenable_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Abilities)
                    .HasColumnName("abilities")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.LastUsedAt)
                    .HasColumnName("last_used_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TokenableId)
                    .HasColumnName("tokenable_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TokenableType)
                    .IsRequired()
                    .HasColumnName("tokenable_type")
                    .HasColumnType("varchar(255)")
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
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<Taggables>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("taggables");

                entity.HasIndex(e => new { e.TaggableType, e.TaggableId })
                    .HasName("taggables_taggable_type_taggable_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => new { e.TagId, e.TaggableId, e.TaggableType })
                    .HasName("taggables_tag_id_taggable_id_taggable_type_unique")
                    .IsUnique()
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TaggableId)
                    .HasColumnName("taggable_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TaggableType)
                    .IsRequired()
                    .HasColumnName("taggable_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("taggables_tag_id_foreign");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("json");

                entity.Property(e => e.OrderColumn)
                    .HasColumnName("order_column")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasColumnType("json");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.ToTable("territories");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<TerritoryUser>(entity =>
            {
                entity.ToTable("territory_user");

                entity.HasIndex(e => e.TerritoryId)
                    .HasName("territory_user_territory_id_foreign");

                entity.HasIndex(e => e.UserId)
                    .HasName("territory_user_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TerritoryId)
                    .HasColumnName("territory_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.TerritoryUser)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("territory_user_territory_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TerritoryUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("territory_user_user_id_foreign");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_unique")
                    .IsUnique();

                entity.HasIndex(e => e.X3Id)
                    .HasName("users_x3_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AvatarPath)
                    .HasColumnName("avatar_path")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.JobTitle)
                    .HasColumnName("job_title")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
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

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.X3Id)
                    .HasColumnName("x3_id")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
