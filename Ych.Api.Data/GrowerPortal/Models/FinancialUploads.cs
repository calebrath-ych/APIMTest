using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class FinancialUploads
    {
        public uint Id { get; set; }
        public int CropYear { get; set; }
        public string ReportTitle { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
