using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class BeerTypes
    {
        public BeerTypes()
        {
            BeerTypesVarieties = new HashSet<BeerTypesVarieties>();
        }

        public uint Id { get; set; }
        public string BeerType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BeerTypesVarieties> BeerTypesVarieties { get; set; }
    }
}
