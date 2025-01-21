using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Audits
    {
        public ulong Id { get; set; }
        public string UserType { get; set; }
        public ulong? UserId { get; set; }
        public string Event { get; set; }
        public string AuditableType { get; set; }
        public ulong AuditableId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string Url { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Tags { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
