using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class LotAllocations
    {
        public ulong Id { get; set; }
        public string LotNumber { get; set; }
        public uint Unspecified { get; set; }
        public uint WholeCone { get; set; }
        public uint T90 { get; set; }
        public uint Cryo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
