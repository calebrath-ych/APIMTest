using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class Cultivars
    {
        public Cultivars()
        {
            Varieties = new HashSet<Varieties>();
        }

        public uint Id { get; set; }
        public string Cultivar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }
        public uint? MagentoCultivarId { get; set; }

        public virtual ICollection<Varieties> Varieties { get; set; }
    }
}
