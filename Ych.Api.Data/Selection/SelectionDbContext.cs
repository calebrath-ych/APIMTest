using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Selection.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Selection
{
    public partial class SelectionDataSource
    {
        public virtual DbSet<AllocationsLog> AllocationsLog { get; set; }
        public virtual DbSet<BeerSampleVarieties> BeerSampleVarieties { get; set; }
        public virtual DbSet<BeerSamples> BeerSamples { get; set; }
        public virtual DbSet<BeerSensory> BeerSensory { get; set; }
        public virtual DbSet<Bins> Bins { get; set; }
        public virtual DbSet<Cache> Cache { get; set; }
        public virtual DbSet<Growers> Growers { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<LotAllocations> LotAllocations { get; set; }
        public virtual DbSet<LotSelectorData> LotSelectorData { get; set; }
        public virtual DbSet<Migrations> Migrations { get; set; }
        public virtual DbSet<ModelHasPermissions> ModelHasPermissions { get; set; }
        public virtual DbSet<ModelHasRoles> ModelHasRoles { get; set; }
        public virtual DbSet<NotesSelections> NotesSelections { get; set; }
        public virtual DbSet<PanelSamples> PanelSamples { get; set; }
        public virtual DbSet<Panels> Panels { get; set; }
        public virtual DbSet<PasswordHistories> PasswordHistories { get; set; }
        public virtual DbSet<PasswordResets> PasswordResets { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<ProductionSamples> ProductionSamples { get; set; }
        public virtual DbSet<ProductionSensory> ProductionSensory { get; set; }
        public virtual DbSet<RoleHasPermissions> RoleHasPermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Samples> Samples { get; set; }
        public virtual DbSet<SamplesArchive> SamplesArchive { get; set; }
        public virtual DbSet<SelectionNotes> SelectionNotes { get; set; }
        public virtual DbSet<SelectionTransactions> SelectionTransactions { get; set; }
        public virtual DbSet<Selections> Selections { get; set; }
        public virtual DbSet<SelectionsTransactions> SelectionsTransactions { get; set; }
        public virtual DbSet<SelectionsWorkorders> SelectionsWorkorders { get; set; }
        public virtual DbSet<Models.Sensory> Sensory { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<SocialAccounts> SocialAccounts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Varieties> Varieties { get; set; }
        public virtual DbSet<Vwcoresamplelotsandvarieties> Vwcoresamplelotsandvarieties { get; set; }
        public virtual DbSet<WorkOrders> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllocationsLog>(entity =>
            {
                entity.ToTable("allocations_log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<BeerSampleVarieties>(entity =>
            {
                entity.ToTable("beer_sample_varieties");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BeerSampleId)
                    .HasColumnName("beer_sample_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.ProductLineCode)
                    .HasColumnName("product_line_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<BeerSamples>(entity =>
            {
                entity.ToTable("beer_samples");

                entity.HasIndex(e => e.SampleCode)
                    .HasName("beer_samples_sample_code_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BeerName)
                    .IsRequired()
                    .HasColumnName("beer_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BeerSource)
                    .IsRequired()
                    .HasColumnName("beer_source")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BeerStyle)
                    .IsRequired()
                    .HasColumnName("beer_style")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DisplayBeerName).HasColumnName("display_beer_name");

                entity.Property(e => e.InResultsBeerName).HasColumnName("in_results_beer_name");

                entity.Property(e => e.InResultsBeerSource).HasColumnName("in_results_beer_source");

                entity.Property(e => e.InResultsBeerStyle).HasColumnName("in_results_beer_style");

                entity.Property(e => e.InResultsGroupStats)
                    .IsRequired()
                    .HasColumnName("in_results_group_stats")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsIndividualStats)
                    .IsRequired()
                    .HasColumnName("in_results_individual_stats")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsNotesForDisplay)
                    .IsRequired()
                    .HasColumnName("in_results_notes_for_display")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsProductLineCode)
                    .IsRequired()
                    .HasColumnName("in_results_product_line_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsSampleCode)
                    .IsRequired()
                    .HasColumnName("in_results_sample_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsVarietyCode)
                    .IsRequired()
                    .HasColumnName("in_results_variety_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IntendedEvalDate)
                    .HasColumnName("intended_eval_date")
                    .HasColumnType("date");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.NotesForDisplay)
                    .HasColumnName("notes_for_display")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RevealOnResults).HasColumnName("reveal_on_results");

                entity.Property(e => e.RevealOnSubmission).HasColumnName("reveal_on_submission");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<BeerSensory>(entity =>
            {
                entity.ToTable("beer_sensory");

                entity.HasIndex(e => e.BeerSampleId)
                    .HasName("beer_sensory_beer_sample_id_foreign");

                entity.HasIndex(e => e.UserId)
                    .HasName("beer_sensory_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Acetaldehyde)
                    .HasColumnName("acetaldehyde")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Alcoholic)
                    .HasColumnName("alcoholic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Almond).HasColumnName("almond");

                entity.Property(e => e.Anise).HasColumnName("anise");

                entity.Property(e => e.Apple).HasColumnName("apple");

                entity.Property(e => e.Apricot).HasColumnName("apricot");

                entity.Property(e => e.Astringent)
                    .HasColumnName("astringent")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Banana).HasColumnName("banana");

                entity.Property(e => e.Barnyard).HasColumnName("barnyard");

                entity.Property(e => e.BeerSampleId)
                    .HasColumnName("beer_sample_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Berry)
                    .HasColumnName("berry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BerryOther)
                    .HasColumnName("berry_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Bitter)
                    .HasColumnName("bitter")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BlackCurrant).HasColumnName("black_currant");

                entity.Property(e => e.BlackPepper).HasColumnName("black_pepper");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bread).HasColumnName("bread");

                entity.Property(e => e.Bubblegum).HasColumnName("bubblegum");

                entity.Property(e => e.ButyricAcid)
                    .HasColumnName("butyric_acid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cabbage).HasColumnName("cabbage");

                entity.Property(e => e.Cantaloupe).HasColumnName("cantaloupe");

                entity.Property(e => e.Caramel).HasColumnName("caramel");

                entity.Property(e => e.Catty)
                    .HasColumnName("catty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cedar).HasColumnName("cedar");

                entity.Property(e => e.Celery).HasColumnName("celery");

                entity.Property(e => e.Cereal)
                    .HasColumnName("cereal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CerealOther)
                    .HasColumnName("cereal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Cherry).HasColumnName("cherry");

                entity.Property(e => e.Chocolate).HasColumnName("chocolate");

                entity.Property(e => e.Cinnamon).HasColumnName("cinnamon");

                entity.Property(e => e.Citrus)
                    .HasColumnName("citrus")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CitrusOther)
                    .HasColumnName("citrus_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Clove).HasColumnName("clove");

                entity.Property(e => e.Coconut).HasColumnName("coconut");

                entity.Property(e => e.Coffee).HasColumnName("coffee");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Cucumber).HasColumnName("cucumber");

                entity.Property(e => e.Dank)
                    .HasColumnName("dank")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DarkMalt).HasColumnName("dark_malt");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Diacetyl)
                    .HasColumnName("diacetyl")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Dill).HasColumnName("dill");

                entity.Property(e => e.Dms)
                    .HasColumnName("dms")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Earthy)
                    .HasColumnName("earthy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EarthyOther)
                    .HasColumnName("earthy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Floral)
                    .HasColumnName("floral")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FloralOther)
                    .HasColumnName("floral_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Garlic).HasColumnName("garlic");

                entity.Property(e => e.Geranium).HasColumnName("geranium");

                entity.Property(e => e.Ginger).HasColumnName("ginger");

                entity.Property(e => e.Grape).HasColumnName("grape");

                entity.Property(e => e.Grapefruit).HasColumnName("grapefruit");

                entity.Property(e => e.Grassy)
                    .HasColumnName("grassy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrassyOther)
                    .HasColumnName("grassy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GreenGrass).HasColumnName("green_grass");

                entity.Property(e => e.GreenOnion).HasColumnName("green_onion");

                entity.Property(e => e.GreenPepper).HasColumnName("green_pepper");

                entity.Property(e => e.Guava).HasColumnName("guava");

                entity.Property(e => e.Hay).HasColumnName("hay");

                entity.Property(e => e.Herbal)
                    .HasColumnName("herbal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HerbalOther)
                    .HasColumnName("herbal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Honey).HasColumnName("honey");

                entity.Property(e => e.Honeydew).HasColumnName("honeydew");

                entity.Property(e => e.Isovaleric)
                    .HasColumnName("isovaleric")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LacticAcid)
                    .HasColumnName("lactic_acid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Lemon).HasColumnName("lemon");

                entity.Property(e => e.Lime).HasColumnName("lime");

                entity.Property(e => e.Mango).HasColumnName("mango");

                entity.Property(e => e.Melon)
                    .HasColumnName("melon")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MelonOther)
                    .HasColumnName("melon_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Mint).HasColumnName("mint");

                entity.Property(e => e.Mushroom).HasColumnName("mushroom");

                entity.Property(e => e.Musty).HasColumnName("musty");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Nutty)
                    .HasColumnName("nutty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.NuttyOther)
                    .HasColumnName("nutty_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Onion).HasColumnName("onion");

                entity.Property(e => e.OnionGarlic)
                    .HasColumnName("onion_garlic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.OnionGarlicOther)
                    .HasColumnName("onion_garlic_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Orange).HasColumnName("orange");

                entity.Property(e => e.Papery)
                    .HasColumnName("papery")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PassionFruit).HasColumnName("passion_fruit");

                entity.Property(e => e.Peach).HasColumnName("peach");

                entity.Property(e => e.Pear).HasColumnName("pear");

                entity.Property(e => e.Pine).HasColumnName("pine");

                entity.Property(e => e.Pineapple).HasColumnName("pineapple");

                entity.Property(e => e.Pomme)
                    .HasColumnName("pomme")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PommeOther)
                    .HasColumnName("pomme_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Popcorn).HasColumnName("popcorn");

                entity.Property(e => e.Raspberry).HasColumnName("raspberry");

                entity.Property(e => e.RawDough).HasColumnName("raw_dough");

                entity.Property(e => e.Roasted)
                    .HasColumnName("roasted")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RoastedOther)
                    .HasColumnName("roasted_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Rose).HasColumnName("rose");

                entity.Property(e => e.Rosemary).HasColumnName("rosemary");

                entity.Property(e => e.RottenVegetables).HasColumnName("rotten_vegetables");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Sawdust).HasColumnName("sawdust");

                entity.Property(e => e.Smoke).HasColumnName("smoke");

                entity.Property(e => e.Soapy).HasColumnName("soapy");

                entity.Property(e => e.Soil).HasColumnName("soil");

                entity.Property(e => e.Sour)
                    .HasColumnName("sour")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spicy)
                    .HasColumnName("spicy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpicyOther)
                    .HasColumnName("spicy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.StoneFruit)
                    .HasColumnName("stone_fruit")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.StoneFruitOther)
                    .HasColumnName("stone_fruit_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Strawberry).HasColumnName("strawberry");

                entity.Property(e => e.Sweet)
                    .HasColumnName("sweet")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromatic)
                    .HasColumnName("sweet_aromatic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromaticOther)
                    .HasColumnName("sweet_aromatic_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Tea).HasColumnName("tea");

                entity.Property(e => e.TeaTree).HasColumnName("tea_tree");

                entity.Property(e => e.Thyme).HasColumnName("thyme");

                entity.Property(e => e.Tobacco).HasColumnName("tobacco");

                entity.Property(e => e.Tropical)
                    .HasColumnName("tropical")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TropicalOther)
                    .HasColumnName("tropical_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Validated).HasColumnName("validated");

                entity.Property(e => e.Vanilla).HasColumnName("vanilla");

                entity.Property(e => e.Vegetal)
                    .HasColumnName("vegetal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VegetalOther)
                    .HasColumnName("vegetal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Walnut).HasColumnName("walnut");

                entity.Property(e => e.Watermelon).HasColumnName("watermelon");

                entity.Property(e => e.Woody)
                    .HasColumnName("woody")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.WoodyOther)
                    .HasColumnName("woody_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.BeerSample)
                    .WithMany(p => p.BeerSensory)
                    .HasForeignKey(d => d.BeerSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("beer_sensory_beer_sample_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BeerSensory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("beer_sensory_user_id_foreign");
            });

            modelBuilder.Entity<Bins>(entity =>
            {
                entity.ToTable("bins");

                entity.HasIndex(e => e.Name)
                    .HasName("bins_name_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Capacity)
                    .HasColumnName("capacity")
                    .HasColumnType("int(11)");

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

            modelBuilder.Entity<Growers>(entity =>
            {
                entity.ToTable("growers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.HgaNumber)
                    .HasColumnName("hga_number")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(191)")
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

            modelBuilder.Entity<LotAllocations>(entity =>
            {
                entity.ToTable("lot_allocations");

                entity.HasIndex(e => e.LotNumber)
                    .HasName("lot_allocations_lot_number_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Cryo)
                    .HasColumnName("cryo")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.T90)
                    .HasColumnName("t_90")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Unspecified)
                    .HasColumnName("unspecified")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WholeCone)
                    .HasColumnName("whole_cone")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<LotSelectorData>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("lot_selector_data");

                entity.Property(e => e.Appearance).HasColumnName("appearance");

                entity.Property(e => e.AromaProfile)
                    .HasColumnName("aroma_profile")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BinName)
                    .IsRequired()
                    .HasColumnName("bin_name")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoresAvailable)
                    .HasColumnName("cores_available")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresConsumed)
                    .HasColumnName("cores_consumed")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReceived)
                    .HasColumnName("cores_received")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReserved)
                    .HasColumnName("cores_reserved")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Greenness)
                    .HasColumnName("greenness")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LbCone)
                    .HasColumnName("lb_cone")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LbCryo)
                    .HasColumnName("lb_cryo")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LbT90)
                    .HasColumnName("lb_t90")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LbUnspecified)
                    .HasColumnName("lb_unspecified")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Rejected).HasColumnName("rejected");

                entity.Property(e => e.Shatter)
                    .HasColumnName("shatter")
                    .HasColumnType("tinyint(3) unsigned");
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

                entity.HasIndex(e => new { e.ModelType, e.ModelId })
                    .HasName("model_has_permissions_model_type_model_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasColumnType("int(10) unsigned");

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

                entity.HasIndex(e => new { e.ModelType, e.ModelId })
                    .HasName("model_has_roles_model_type_model_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(10) unsigned");

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

            modelBuilder.Entity<NotesSelections>(entity =>
            {
                entity.HasKey(e => new { e.NoteId, e.SelectionId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("notes_selections");

                entity.HasIndex(e => e.SelectionId)
                    .HasName("notes_selections_selection_id_foreign");

                entity.Property(e => e.NoteId)
                    .HasColumnName("note_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SelectionId)
                    .HasColumnName("selection_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NotesSelections)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("notes_selections_note_id_foreign");

                entity.HasOne(d => d.Selection)
                    .WithMany(p => p.NotesSelections)
                    .HasForeignKey(d => d.SelectionId)
                    .HasConstraintName("notes_selections_selection_id_foreign");
            });

            modelBuilder.Entity<PanelSamples>(entity =>
            {
                entity.ToTable("panel_samples");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.PanelId)
                    .HasColumnName("panel_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PanelableId)
                    .HasColumnName("panelable_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PanelableType)
                    .IsRequired()
                    .HasColumnName("panelable_type")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Panels>(entity =>
            {
                entity.ToTable("panels");

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

                entity.Property(e => e.PanelistCount)
                    .HasColumnName("panelist_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoundCount)
                    .HasColumnName("round_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<PasswordHistories>(entity =>
            {
                entity.ToTable("password_histories");

                entity.HasIndex(e => e.UserId)
                    .HasName("password_histories_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
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
                    .WithMany(p => p.PasswordHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("password_histories_user_id_foreign");
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

            modelBuilder.Entity<ProductionSamples>(entity =>
            {
                entity.ToTable("production_samples");

                entity.HasIndex(e => e.SampleCode)
                    .HasName("production_samples_sample_code_unique")
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

                entity.Property(e => e.DisplayLotNumber).HasColumnName("display_lot_number");

                entity.Property(e => e.InResultsGroupStats)
                    .IsRequired()
                    .HasColumnName("in_results_group_stats")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsIndividualStats)
                    .IsRequired()
                    .HasColumnName("in_results_individual_stats")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsLotNumber).HasColumnName("in_results_lot_number");

                entity.Property(e => e.InResultsNotesForDisplay)
                    .IsRequired()
                    .HasColumnName("in_results_notes_for_display")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsProductLineCode)
                    .IsRequired()
                    .HasColumnName("in_results_product_line_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsSampleCode)
                    .IsRequired()
                    .HasColumnName("in_results_sample_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.InResultsVarietyCode)
                    .IsRequired()
                    .HasColumnName("in_results_variety_code")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IntendedEvalDate)
                    .HasColumnName("intended_eval_date")
                    .HasColumnType("date");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.NotesForDisplay)
                    .HasColumnName("notes_for_display")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductLineCode)
                    .IsRequired()
                    .HasColumnName("product_line_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RevealOnResults).HasColumnName("reveal_on_results");

                entity.Property(e => e.RevealOnSubmission).HasColumnName("reveal_on_submission");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

            modelBuilder.Entity<ProductionSensory>(entity =>
            {
                entity.ToTable("production_sensory");

                entity.HasIndex(e => e.ProductionSampleId)
                    .HasName("production_sensory_production_sample_id_foreign");

                entity.HasIndex(e => e.UserId)
                    .HasName("production_sensory_user_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Anise).HasColumnName("anise");

                entity.Property(e => e.Apple).HasColumnName("apple");

                entity.Property(e => e.Banana).HasColumnName("banana");

                entity.Property(e => e.Berry)
                    .HasColumnName("berry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BerryOther)
                    .HasColumnName("berry_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BlackCurrant).HasColumnName("black_currant");

                entity.Property(e => e.BlackPepper).HasColumnName("black_pepper");

                entity.Property(e => e.BlackTea).HasColumnName("black_tea");

                entity.Property(e => e.Bubblegum).HasColumnName("bubblegum");

                entity.Property(e => e.BurntRubber)
                    .HasColumnName("burnt_rubber")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cantaloupe).HasColumnName("cantaloupe");

                entity.Property(e => e.Caramel).HasColumnName("caramel");

                entity.Property(e => e.Cardboard)
                    .HasColumnName("cardboard")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Catty)
                    .HasColumnName("catty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cedar).HasColumnName("cedar");

                entity.Property(e => e.Celery).HasColumnName("celery");

                entity.Property(e => e.Cheesy)
                    .HasColumnName("cheesy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cherry).HasColumnName("cherry");

                entity.Property(e => e.Cinnamon).HasColumnName("cinnamon");

                entity.Property(e => e.Citrus)
                    .HasColumnName("citrus")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CitrusOther)
                    .HasColumnName("citrus_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Clove).HasColumnName("clove");

                entity.Property(e => e.Coconut).HasColumnName("coconut");

                entity.Property(e => e.Compost).HasColumnName("compost");

                entity.Property(e => e.Creamy).HasColumnName("creamy");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Cucumber).HasColumnName("cucumber");

                entity.Property(e => e.Dank)
                    .HasColumnName("dank")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Diesel)
                    .HasColumnName("diesel")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Dill).HasColumnName("dill");

                entity.Property(e => e.Earthy)
                    .HasColumnName("earthy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EarthyOther)
                    .HasColumnName("earthy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Floral)
                    .HasColumnName("floral")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FloralOther)
                    .HasColumnName("floral_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Garlic).HasColumnName("garlic");

                entity.Property(e => e.Geosmin).HasColumnName("geosmin");

                entity.Property(e => e.Geranium).HasColumnName("geranium");

                entity.Property(e => e.Ginger).HasColumnName("ginger");

                entity.Property(e => e.Grape).HasColumnName("grape");

                entity.Property(e => e.Grapefruit).HasColumnName("grapefruit");

                entity.Property(e => e.Grassy)
                    .HasColumnName("grassy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrassyOther)
                    .HasColumnName("grassy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GreenGrass).HasColumnName("green_grass");

                entity.Property(e => e.GreenOnion).HasColumnName("green_onion");

                entity.Property(e => e.GreenPepper).HasColumnName("green_pepper");

                entity.Property(e => e.GreenTea).HasColumnName("green_tea");

                entity.Property(e => e.Guava).HasColumnName("guava");

                entity.Property(e => e.Hay).HasColumnName("hay");

                entity.Property(e => e.Herbal)
                    .HasColumnName("herbal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HerbalOther)
                    .HasColumnName("herbal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Honeydew).HasColumnName("honeydew");

                entity.Property(e => e.Lemon).HasColumnName("lemon");

                entity.Property(e => e.Lemongrass).HasColumnName("lemongrass");

                entity.Property(e => e.Lime).HasColumnName("lime");

                entity.Property(e => e.Mango).HasColumnName("mango");

                entity.Property(e => e.Melon)
                    .HasColumnName("melon")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MelonOther)
                    .HasColumnName("melon_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Mint).HasColumnName("mint");

                entity.Property(e => e.Mushroom).HasColumnName("mushroom");

                entity.Property(e => e.Musty).HasColumnName("musty");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Onion).HasColumnName("onion");

                entity.Property(e => e.OnionGarlic)
                    .HasColumnName("onion_garlic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.OnionGarlicOther)
                    .HasColumnName("onion_garlic_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Orange).HasColumnName("orange");

                entity.Property(e => e.PassionFruit).HasColumnName("passion_fruit");

                entity.Property(e => e.Peach).HasColumnName("peach");

                entity.Property(e => e.Pear).HasColumnName("pear");

                entity.Property(e => e.Pine).HasColumnName("pine");

                entity.Property(e => e.Pineapple).HasColumnName("pineapple");

                entity.Property(e => e.PlasticWaxy)
                    .HasColumnName("plastic_waxy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Pomme)
                    .HasColumnName("pomme")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PommeOther)
                    .HasColumnName("pomme_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.ProductionSampleId)
                    .HasColumnName("production_sample_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Raspberry).HasColumnName("raspberry");

                entity.Property(e => e.Resinous).HasColumnName("resinous");

                entity.Property(e => e.Rose).HasColumnName("rose");

                entity.Property(e => e.Rosemary).HasColumnName("rosemary");

                entity.Property(e => e.SampleCode)
                    .IsRequired()
                    .HasColumnName("sample_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Sawdust).HasColumnName("sawdust");

                entity.Property(e => e.Smoky)
                    .HasColumnName("smoky")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Soapy).HasColumnName("soapy");

                entity.Property(e => e.Soil).HasColumnName("soil");

                entity.Property(e => e.Spicy)
                    .HasColumnName("spicy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpicyOther)
                    .HasColumnName("spicy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.StoneFruit)
                    .HasColumnName("stone_fruit")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.StoneFruitOther)
                    .HasColumnName("stone_fruit_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Strawberry).HasColumnName("strawberry");

                entity.Property(e => e.Sulfur)
                    .HasColumnName("sulfur")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Sweaty)
                    .HasColumnName("sweaty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromatic)
                    .HasColumnName("sweet_aromatic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromaticOther)
                    .HasColumnName("sweet_aromatic_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TeaTree).HasColumnName("tea_tree");

                entity.Property(e => e.TomatoPlant).HasColumnName("tomato_plant");

                entity.Property(e => e.Tropical)
                    .HasColumnName("tropical")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TropicalOther)
                    .HasColumnName("tropical_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Validated).HasColumnName("validated");

                entity.Property(e => e.Vanilla).HasColumnName("vanilla");

                entity.Property(e => e.Vegetal)
                    .HasColumnName("vegetal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VegetalOther)
                    .HasColumnName("vegetal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Watermelon).HasColumnName("watermelon");

                entity.Property(e => e.Woody)
                    .HasColumnName("woody")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.WoodyOther)
                    .HasColumnName("woody_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.ProductionSample)
                    .WithMany(p => p.ProductionSensory)
                    .HasForeignKey(d => d.ProductionSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("production_sensory_production_sample_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProductionSensory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("production_sensory_user_id_foreign");
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

            modelBuilder.Entity<Samples>(entity =>
            {
                entity.ToTable("samples");

                entity.HasIndex(e => e.BinId)
                    .HasName("samples_bin_id_foreign");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("samples_variety_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Appearance).HasColumnName("appearance");

                entity.Property(e => e.AromaProfile)
                    .HasColumnName("aroma_profile")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BinId)
                    .HasColumnName("bin_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoresAvailable)
                    .HasColumnName("cores_available")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresConsumed)
                    .HasColumnName("cores_consumed")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReceived)
                    .HasColumnName("cores_received")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReserved)
                    .HasColumnName("cores_reserved")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Greenness)
                    .HasColumnName("greenness")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LotValid).HasColumnName("lot_valid");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rejected).HasColumnName("rejected");

                entity.Property(e => e.Shatter)
                    .HasColumnName("shatter")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Bin)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.BinId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("samples_bin_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.VarietyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("samples_variety_id_foreign");
            });

            modelBuilder.Entity<SamplesArchive>(entity =>
            {
                entity.ToTable("samples_archive");

                entity.HasIndex(e => e.BinId)
                    .HasName("samples_archive_bin_id_foreign");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("samples_archive_variety_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Appearance).HasColumnName("appearance");

                entity.Property(e => e.AromaProfile)
                    .HasColumnName("aroma_profile")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BinId)
                    .HasColumnName("bin_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CoresAvailable)
                    .HasColumnName("cores_available")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresConsumed)
                    .HasColumnName("cores_consumed")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReceived)
                    .HasColumnName("cores_received")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CoresReserved)
                    .HasColumnName("cores_reserved")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'2019'");

                entity.Property(e => e.Greenness)
                    .HasColumnName("greenness")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LotValid).HasColumnName("lot_valid");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rejected).HasColumnName("rejected");

                entity.Property(e => e.Shatter)
                    .HasColumnName("shatter")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Bin)
                    .WithMany(p => p.SamplesArchive)
                    .HasForeignKey(d => d.BinId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("samples_archive_bin_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.SamplesArchive)
                    .HasForeignKey(d => d.VarietyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("samples_archive_variety_id_foreign");
            });

            modelBuilder.Entity<SelectionNotes>(entity =>
            {
                entity.ToTable("selection_notes");

                entity.HasIndex(e => e.CreatedById)
                    .HasName("selection_notes_created_by_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
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

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.SelectionNotes)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selection_notes_created_by_id_foreign");
            });

            modelBuilder.Entity<SelectionTransactions>(entity =>
            {
                entity.ToTable("selection_transactions");

                entity.HasIndex(e => e.CreatedById)
                    .HasName("selection_transactions_created_by_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
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

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.EndStatus)
                    .IsRequired()
                    .HasColumnName("end_status")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.StartStatus)
                    .HasColumnName("start_status")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.SelectionTransactions)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selection_transactions_created_by_id_foreign");
            });

            modelBuilder.Entity<Selections>(entity =>
            {
                entity.ToTable("selections");

                entity.HasIndex(e => e.UserId)
                    .HasName("selections_user_id_foreign");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("selections_variety_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AllocatedLbs)
                    .IsRequired()
                    .HasColumnName("allocated_lbs")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AtcBrewerNotes)
                    .HasColumnName("atc_brewer_notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AtcIsApproved).HasColumnName("atc_is_approved");

                entity.Property(e => e.AtcIsMpsComplete).HasColumnName("atc_is_mps_complete");

                entity.Property(e => e.AtcIsPelleted).HasColumnName("atc_is_pelleted");

                entity.Property(e => e.AtcIsPending).HasColumnName("atc_is_pending");

                entity.Property(e => e.AtcIsSingleLot).HasColumnName("atc_is_single_lot");

                entity.Property(e => e.AtcNotes)
                    .HasColumnName("atc_notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.AtcUserId)
                    .HasColumnName("atc_user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AvgBaleWeight)
                    .HasColumnName("avg_bale_weight")
                    .HasColumnType("decimal(6,2) unsigned");

                entity.Property(e => e.BlendCode)
                    .HasColumnName("blend_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BlendFull).HasColumnName("blend_full");

                entity.Property(e => e.BlendRatio)
                    .HasColumnName("blend_ratio")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BlendSwing).HasColumnName("blend_swing");

                entity.Property(e => e.ConeLbs)
                    .HasColumnName("cone_lbs")
                    .HasColumnType("decimal(8,2) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'2019'");

                entity.Property(e => e.CryoLbs)
                    .HasColumnName("cryo_lbs")
                    .HasColumnType("decimal(8,2) unsigned");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PelletedLotNumber)
                    .HasColumnName("pelleted_lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Rejected).HasColumnName("rejected");

                entity.Property(e => e.SalesOrder)
                    .IsRequired()
                    .HasColumnName("sales_order")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Selected).HasColumnName("selected");

                entity.Property(e => e.SelectedCryo).HasColumnName("selected_cryo");

                entity.Property(e => e.SelectedT90).HasColumnName("selected_t_90");

                entity.Property(e => e.SelectedWholeCone).HasColumnName("selected_whole_cone");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("'pending'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.T90Lbs)
                    .HasColumnName("t90_lbs")
                    .HasColumnType("decimal(8,2) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ValidationOverride).HasColumnName("validation_override");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Warehouse)
                    .HasColumnName("warehouse")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.WorkOrder)
                    .IsRequired()
                    .HasColumnName("work_order")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Selections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selections_user_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.Selections)
                    .HasForeignKey(d => d.VarietyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("selections_variety_id_foreign");
            });

            modelBuilder.Entity<SelectionsTransactions>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.SelectionId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("selections_transactions");

                entity.HasIndex(e => e.SelectionId)
                    .HasName("selections_transactions_selection_id_foreign");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SelectionId)
                    .HasColumnName("selection_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Selection)
                    .WithMany(p => p.SelectionsTransactions)
                    .HasForeignKey(d => d.SelectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selections_transactions_selection_id_foreign");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.SelectionsTransactions)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selections_transactions_transaction_id_foreign");
            });

            modelBuilder.Entity<SelectionsWorkorders>(entity =>
            {
                entity.ToTable("selections_workorders");

                entity.HasIndex(e => e.SelectionId)
                    .HasName("selections_workorders_selection_id_foreign");

                entity.HasIndex(e => e.WorkorderId)
                    .HasName("selections_workorders_workorder_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.SelectionId)
                    .HasColumnName("selection_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WorkorderId)
                    .HasColumnName("workorder_id")
                    .HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.Selection)
                    .WithMany(p => p.SelectionsWorkorders)
                    .HasForeignKey(d => d.SelectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selections_workorders_selection_id_foreign");

                entity.HasOne(d => d.Workorder)
                    .WithMany(p => p.SelectionsWorkorders)
                    .HasForeignKey(d => d.WorkorderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selections_workorders_workorder_id_foreign");
            });

            modelBuilder.Entity<Models.Sensory>(entity =>
            {
                entity.ToTable("sensory");

                entity.HasIndex(e => e.UserId)
                    .HasName("sensory_user_id_foreign");

                entity.HasIndex(e => e.VarietyId)
                    .HasName("sensory_variety_id_foreign");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Anise).HasColumnName("anise");

                entity.Property(e => e.Apple).HasColumnName("apple");

                entity.Property(e => e.Banana).HasColumnName("banana");

                entity.Property(e => e.Berry)
                    .HasColumnName("berry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BerryOther)
                    .HasColumnName("berry_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.BlackCurrant).HasColumnName("black_currant");

                entity.Property(e => e.BlackPepper).HasColumnName("black_pepper");

                entity.Property(e => e.BlackTea).HasColumnName("black_tea");

                entity.Property(e => e.Bubblegum).HasColumnName("bubblegum");

                entity.Property(e => e.BurntRubber)
                    .HasColumnName("burnt_rubber")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cantaloupe).HasColumnName("cantaloupe");

                entity.Property(e => e.Caramel).HasColumnName("caramel");

                entity.Property(e => e.Cardboard)
                    .HasColumnName("cardboard")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Catty)
                    .HasColumnName("catty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cedar).HasColumnName("cedar");

                entity.Property(e => e.Celery).HasColumnName("celery");

                entity.Property(e => e.Cheesy)
                    .HasColumnName("cheesy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cherry).HasColumnName("cherry");

                entity.Property(e => e.Cinnamon).HasColumnName("cinnamon");

                entity.Property(e => e.Citrus)
                    .HasColumnName("citrus")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CitrusOther)
                    .HasColumnName("citrus_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Clove).HasColumnName("clove");

                entity.Property(e => e.Coconut).HasColumnName("coconut");

                entity.Property(e => e.Compost).HasColumnName("compost");

                entity.Property(e => e.Creamy).HasColumnName("creamy");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.CropYear)
                    .HasColumnName("crop_year")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'2018'");

                entity.Property(e => e.Cucumber).HasColumnName("cucumber");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Dank)
                    .HasColumnName("dank")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Diesel)
                    .HasColumnName("diesel")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Dill).HasColumnName("dill");

                entity.Property(e => e.Earthy)
                    .HasColumnName("earthy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EarthyOther)
                    .HasColumnName("earthy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Evergreen)
                    .HasColumnName("evergreen")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Floral)
                    .HasColumnName("floral")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FloralOther)
                    .HasColumnName("floral_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Fruity)
                    .HasColumnName("fruity")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Garlic).HasColumnName("garlic");

                entity.Property(e => e.Geosmin).HasColumnName("geosmin");

                entity.Property(e => e.Geranium).HasColumnName("geranium");

                entity.Property(e => e.Ginger).HasColumnName("ginger");

                entity.Property(e => e.Grape).HasColumnName("grape");

                entity.Property(e => e.Grapefruit).HasColumnName("grapefruit");

                entity.Property(e => e.Grassy)
                    .HasColumnName("grassy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GrassyOther)
                    .HasColumnName("grassy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.GreenGrass).HasColumnName("green_grass");

                entity.Property(e => e.GreenOnion).HasColumnName("green_onion");

                entity.Property(e => e.GreenPepper).HasColumnName("green_pepper");

                entity.Property(e => e.GreenTea).HasColumnName("green_tea");

                entity.Property(e => e.Guava).HasColumnName("guava");

                entity.Property(e => e.Hay).HasColumnName("hay");

                entity.Property(e => e.Herbal)
                    .HasColumnName("herbal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HerbalOther)
                    .HasColumnName("herbal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Honeydew).HasColumnName("honeydew");

                entity.Property(e => e.Lemon).HasColumnName("lemon");

                entity.Property(e => e.Lemongrass).HasColumnName("lemongrass");

                entity.Property(e => e.Lime).HasColumnName("lime");

                entity.Property(e => e.LotNumber)
                    .IsRequired()
                    .HasColumnName("lot_number")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Mango).HasColumnName("mango");

                entity.Property(e => e.Melon)
                    .HasColumnName("melon")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MelonOther)
                    .HasColumnName("melon_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Mint).HasColumnName("mint");

                entity.Property(e => e.Mushroom).HasColumnName("mushroom");

                entity.Property(e => e.Musty).HasColumnName("musty");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Nutty)
                    .HasColumnName("nutty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Og)
                    .HasColumnName("og")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.OgOther)
                    .HasColumnName("og_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Onion).HasColumnName("onion");

                entity.Property(e => e.Orange).HasColumnName("orange");

                entity.Property(e => e.Pass).HasColumnName("pass");

                entity.Property(e => e.PassionFruit).HasColumnName("passion_fruit");

                entity.Property(e => e.Peach).HasColumnName("peach");

                entity.Property(e => e.Pear).HasColumnName("pear");

                entity.Property(e => e.Pine).HasColumnName("pine");

                entity.Property(e => e.Pineapple).HasColumnName("pineapple");

                entity.Property(e => e.Plastic)
                    .HasColumnName("plastic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Pomme)
                    .HasColumnName("pomme")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PommeOther)
                    .HasColumnName("pomme_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Ranking)
                    .HasColumnName("ranking")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Raspberry).HasColumnName("raspberry");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Resinous).HasColumnName("resinous");

                entity.Property(e => e.Rose).HasColumnName("rose");

                entity.Property(e => e.Rosemary).HasColumnName("rosemary");

                entity.Property(e => e.Sawdust).HasColumnName("sawdust");

                entity.Property(e => e.Smoky)
                    .HasColumnName("smoky")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Soapy).HasColumnName("soapy");

                entity.Property(e => e.Soil).HasColumnName("soil");

                entity.Property(e => e.Spicy)
                    .HasColumnName("spicy")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpicyOther)
                    .HasColumnName("spicy_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.StoneFruit)
                    .HasColumnName("stone_fruit")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.StoneFruitOther)
                    .HasColumnName("stone_fruit_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Strawberry).HasColumnName("strawberry");

                entity.Property(e => e.Sulfur)
                    .HasColumnName("sulfur")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Sweaty)
                    .HasColumnName("sweaty")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromatic)
                    .HasColumnName("sweet_aromatic")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SweetAromaticOther)
                    .HasColumnName("sweet_aromatic_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.TeaTree).HasColumnName("tea_tree");

                entity.Property(e => e.TomatoPlant).HasColumnName("tomato_plant");

                entity.Property(e => e.Tropical)
                    .HasColumnName("tropical")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TropicalOther)
                    .HasColumnName("tropical_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Validated).HasColumnName("validated");

                entity.Property(e => e.Vanilla).HasColumnName("vanilla");

                entity.Property(e => e.VarietyId)
                    .HasColumnName("variety_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Vegetal)
                    .HasColumnName("vegetal")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VegetalOther)
                    .HasColumnName("vegetal_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Watermelon).HasColumnName("watermelon");

                entity.Property(e => e.Woody)
                    .HasColumnName("woody")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.WoodyOther)
                    .HasColumnName("woody_other")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sensory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sensory_user_id_foreign");

                entity.HasOne(d => d.Variety)
                    .WithMany(p => p.Sensory)
                    .HasForeignKey(d => d.VarietyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sensory_variety_id_foreign");
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

                entity.Property(e => e.CustomerDisplay)
                    .HasColumnName("customer_display")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

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

                entity.Property(e => e.RememberToken)
                    .HasColumnName("remember_token")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.SensoryValidated).HasColumnName("sensory_validated");

                entity.Property(e => e.Timezone)
                    .IsRequired()
                    .HasColumnName("timezone")
                    .HasColumnType("varchar(191)")
                    .HasDefaultValueSql("'America/Los_Angeles'")
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

            modelBuilder.Entity<Varieties>(entity =>
            {
                entity.ToTable("varieties");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(191)")
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
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Selectable).HasColumnName("selectable");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Vwcoresamplelotsandvarieties>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwcoresamplelotsandvarieties");

                entity.Property(e => e.Lot)
                    .IsRequired()
                    .HasColumnName("lot")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Variety)
                    .IsRequired()
                    .HasColumnName("variety")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.VarietyCode)
                    .HasColumnName("variety_code")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<WorkOrders>(entity =>
            {
                entity.ToTable("work_orders");

                entity.HasIndex(e => e.CreatedById)
                    .HasName("work_orders_created_by_id_foreign");

                entity.HasIndex(e => e.WorkOrderCode)
                    .HasName("work_orders_work_order_code_unique")
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

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(191)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WorkOrderCode)
                    .IsRequired()
                    .HasColumnName("work_order_code")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("work_orders_created_by_id_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
