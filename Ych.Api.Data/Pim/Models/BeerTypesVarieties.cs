using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class BeerTypesVarieties
    {
        public uint Id { get; set; }
        public uint BeerTypeId { get; set; }
        public uint VarietyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual BeerTypes BeerType { get; set; }
        public virtual Varieties Variety { get; set; }
    }
}
