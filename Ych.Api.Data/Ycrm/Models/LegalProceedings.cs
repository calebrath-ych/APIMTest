using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class LegalProceedings
    {
        public LegalProceedings()
        {
            LegalActions = new HashSet<LegalActions>();
            LegalOwed = new HashSet<LegalOwed>();
        }

        public ulong Id { get; set; }
        public ulong StatusId { get; set; }
        public ulong CustomerId { get; set; }
        public ulong CurrencyId { get; set; }
        public ulong? InventoryOwedFileId { get; set; }
        public ulong? PastDueBalanceFileId { get; set; }
        public decimal Charges { get; set; }
        public decimal UnpaidInvoice { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Currencies Currency { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Files InventoryOwedFile { get; set; }
        public virtual Files PastDueBalanceFile { get; set; }
        public virtual LegalStatuses Status { get; set; }
        public virtual ICollection<LegalActions> LegalActions { get; set; }
        public virtual ICollection<LegalOwed> LegalOwed { get; set; }
    }
}
