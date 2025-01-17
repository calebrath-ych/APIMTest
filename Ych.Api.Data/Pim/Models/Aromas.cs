using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class Aromas
    {
        public Aromas()
        {
            AromasVarieties = new HashSet<AromasVarieties>();
        }

        public uint Id { get; set; }
        public string Aroma { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }
        public uint? MagentoAromaId { get; set; }

        public virtual ICollection<AromasVarieties> AromasVarieties { get; set; }
    }
}
