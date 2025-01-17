using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class MapPointTypes
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public bool AllowOverlap { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
