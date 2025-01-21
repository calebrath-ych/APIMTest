using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class Varieties
    {
        public Varieties()
        {
            AromasVarieties = new HashSet<AromasVarieties>();
            BeerTypesVarieties = new HashSet<BeerTypesVarieties>();
            BrewingValues = new HashSet<BrewingValues>();
        }

        public uint Id { get; set; }
        public string DisplayName { get; set; }
        public string VarietyCode { get; set; }
        public bool Blend { get; set; }
        public bool Featured { get; set; }
        public bool Experimental { get; set; }
        public bool Organic { get; set; }
        public string MarketingDescription { get; set; }
        public uint? BrandId { get; set; }
        public uint CultivarId { get; set; }
        public uint CountryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }
        public uint? MagentoVarietyId { get; set; }
        public string AromaDescription { get; set; }
        public bool? ShowEcommerceGraph { get; set; }
        public bool? AllowMagentoSync { get; set; }

        public virtual Brands Brand { get; set; }
        public virtual CountryCodes Country { get; set; }
        public virtual Cultivars Cultivar { get; set; }
        public virtual ICollection<AromasVarieties> AromasVarieties { get; set; }
        public virtual ICollection<BeerTypesVarieties> BeerTypesVarieties { get; set; }
        public virtual ICollection<BrewingValues> BrewingValues { get; set; }
    }
}
