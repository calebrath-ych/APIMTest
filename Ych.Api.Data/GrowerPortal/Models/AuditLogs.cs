using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class AuditLogs
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public string GrowerId { get; set; }
        public byte[] Changes { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users User { get; set; }
    }
}
