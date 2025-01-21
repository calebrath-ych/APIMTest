using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Jobs
    {
        public ulong Id { get; set; }
        public string Queue { get; set; }
        public string Payload { get; set; }
        public byte Attempts { get; set; }
        public uint? ReservedAt { get; set; }
        public uint AvailableAt { get; set; }
        public uint CreatedAt { get; set; }
    }
}
