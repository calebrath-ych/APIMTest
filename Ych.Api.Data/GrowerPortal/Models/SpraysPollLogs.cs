using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class SpraysPollLogs
    {
        public uint Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public uint TotalSpraysFound { get; set; }
        public uint UsableSprays { get; set; }
        public uint UnusableSprays { get; set; }
        public string Exception { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
