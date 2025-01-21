using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class OfficialFeedback
    {
        public uint Id { get; set; }
        public string LotNumber { get; set; }
        public byte Shatter { get; set; }
        public byte Greenness { get; set; }
        public string AromaProfile { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
