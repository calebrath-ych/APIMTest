using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class GrowerLot
    {
        public uint Id { get; set; }
        public string LotNumber { get; set; }
        public string CropYear { get; set; }
        public string GrowerId { get; set; }
        public string VarietyId { get; set; }
        public string VarietyOther { get; set; }
        public string Mrl { get; set; }
        public string MrlReasonText { get; set; }
        public string Notes { get; set; }
        public uint? CreatedById { get; set; }
        public uint? DeletedById { get; set; }
        public DateTime? MrlGeneratedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
