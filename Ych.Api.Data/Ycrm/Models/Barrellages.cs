using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Barrellages
    {
        public ulong Id { get; set; }
        public ulong CustomerId { get; set; }
        public ulong CreatedBy { get; set; }
        public uint Year { get; set; }
        public uint Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
