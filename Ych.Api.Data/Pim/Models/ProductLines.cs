using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class ProductLines
    {
        public ProductLines()
        {
            BrewingValues = new HashSet<BrewingValues>();
        }

        public uint Id { get; set; }
        public string ProductLineCode { get; set; }
        public string ProductLineName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }
        public uint? MagentoProductLineId { get; set; }

        public virtual ICollection<BrewingValues> BrewingValues { get; set; }
    }
}
