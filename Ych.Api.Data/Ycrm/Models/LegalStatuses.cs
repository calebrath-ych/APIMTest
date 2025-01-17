using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class LegalStatuses
    {
        public LegalStatuses()
        {
            LegalProceedings = new HashSet<LegalProceedings>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<LegalProceedings> LegalProceedings { get; set; }
    }
}
