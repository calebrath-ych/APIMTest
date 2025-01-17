using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SamplesArchive
    {
        public uint Id { get; set; }
        public uint? VarietyId { get; set; }
        public uint? BinId { get; set; }
        public string LotNumber { get; set; }
        public bool LotValid { get; set; }
        public uint CoresReceived { get; set; }
        public uint CoresReserved { get; set; }
        public uint CoresConsumed { get; set; }
        public uint CoresAvailable { get; set; }
        public string Notes { get; set; }
        public string Comments { get; set; }
        public byte Rating { get; set; }
        public bool Appearance { get; set; }
        public byte Shatter { get; set; }
        public byte Greenness { get; set; }
        public string AromaProfile { get; set; }
        public bool Rejected { get; set; }
        public uint CropYear { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Bins Bin { get; set; }
        public virtual Varieties Variety { get; set; }
    }
}
