using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class PaymentPlans
    {
        public PaymentPlans()
        {
            Payments = new HashSet<Payments>();
        }

        public ulong Id { get; set; }
        public ulong CustomerId { get; set; }
        public ulong PaymentPlanStatusId { get; set; }
        public ulong CurrencyId { get; set; }
        public string Name { get; set; }
        public bool FullyShipped { get; set; }
        public bool Autopay { get; set; }
        public uint Installments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public decimal DownPayment { get; set; }
        public decimal TotalValue { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Currencies Currency { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual PaymentPlanStatuses PaymentPlanStatus { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
