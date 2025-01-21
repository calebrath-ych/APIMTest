using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SelectionTransactions
    {
        public SelectionTransactions()
        {
            SelectionsTransactions = new HashSet<SelectionsTransactions>();
        }

        public uint Id { get; set; }
        public uint CreatedById { get; set; }
        public string StartStatus { get; set; }
        public string EndStatus { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CreatedBy { get; set; }
        public virtual ICollection<SelectionsTransactions> SelectionsTransactions { get; set; }
    }
}
