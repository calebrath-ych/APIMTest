using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class LegalActions
    {
        public ulong Id { get; set; }
        public ulong LegalProceedingId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual LegalProceedings LegalProceeding { get; set; }
    }
}
