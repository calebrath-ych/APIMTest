using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class PaymentStatuses
    {
        public PaymentStatuses()
        {
            Payments = new HashSet<Payments>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Payments> Payments { get; set; }
    }
}
