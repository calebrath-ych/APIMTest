using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class Brands
    {
        public Brands()
        {
            Varieties = new HashSet<Varieties>();
        }

        public uint Id { get; set; }
        public string BrandName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Varieties> Varieties { get; set; }
    }
}
