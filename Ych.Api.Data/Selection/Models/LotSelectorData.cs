using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class LotSelectorData
    {
        public string LotNumber { get; set; }
        public uint CoresReceived { get; set; }
        public uint CoresReserved { get; set; }
        public uint CoresConsumed { get; set; }
        public uint CoresAvailable { get; set; }
        public string Notes { get; set; }
        public bool Appearance { get; set; }
        public byte Shatter { get; set; }
        public byte Greenness { get; set; }
        public string AromaProfile { get; set; }
        public string BinName { get; set; }
        public bool Rejected { get; set; }
        public uint? LbUnspecified { get; set; }
        public uint? LbCone { get; set; }
        public uint? LbT90 { get; set; }
        public uint? LbCryo { get; set; }
    }
}
