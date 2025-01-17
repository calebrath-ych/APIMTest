using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Payments
    {
        public ulong Id { get; set; }
        public ulong PaymentPlanId { get; set; }
        public ulong PaymentStatusId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual PaymentPlans PaymentPlan { get; set; }
        public virtual PaymentStatuses PaymentStatus { get; set; }
    }
}
