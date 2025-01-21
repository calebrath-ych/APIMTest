using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class CountryCodes
    {
        public CountryCodes()
        {
            Varieties = new HashSet<Varieties>();
        }

        public uint Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }
        public uint? MagentoCountryCodeId { get; set; }

        public virtual ICollection<Varieties> Varieties { get; set; }
    }
}
