using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class MapBookmarks
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public long GrowerId { get; set; }
        public string BaseMap { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
