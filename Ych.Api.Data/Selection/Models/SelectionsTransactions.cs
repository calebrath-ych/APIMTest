using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SelectionsTransactions
    {
        public uint TransactionId { get; set; }
        public uint SelectionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Selections Selection { get; set; }
        public virtual SelectionTransactions Transaction { get; set; }
    }
}
