using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class ActivityLog
    {
        public uint Id { get; set; }
        public int? UserId { get; set; }
        public string Text { get; set; }
        public string IpAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
