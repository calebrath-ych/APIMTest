using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class ReportNotifications
    {
        public ulong Id { get; set; }
        public int GrowerId { get; set; }
        public int ReportTypeId { get; set; }
        public int Frequency { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
