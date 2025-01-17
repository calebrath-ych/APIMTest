using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class AllocationsLog
    {
        public uint Id { get; set; }
        public string LotNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
