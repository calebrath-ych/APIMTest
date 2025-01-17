using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Files
    {
        public Files()
        {
            Fileables = new HashSet<Fileables>();
            LegalProceedingsInventoryOwedFile = new HashSet<LegalProceedings>();
            LegalProceedingsPastDueBalanceFile = new HashSet<LegalProceedings>();
        }

        public ulong Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Fileables> Fileables { get; set; }
        public virtual ICollection<LegalProceedings> LegalProceedingsInventoryOwedFile { get; set; }
        public virtual ICollection<LegalProceedings> LegalProceedingsPastDueBalanceFile { get; set; }
    }
}
