using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Currencies
    {
        public Currencies()
        {
            LegalProceedings = new HashSet<LegalProceedings>();
            PaymentPlans = new HashSet<PaymentPlans>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<LegalProceedings> LegalProceedings { get; set; }
        public virtual ICollection<PaymentPlans> PaymentPlans { get; set; }
    }
}
