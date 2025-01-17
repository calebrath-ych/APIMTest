using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class SpraysUnusableImports
    {
        public uint Id { get; set; }
        public string AgrianPurId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Comments { get; set; }
        public string AgrianViewUrl { get; set; }
        public DateTime? AgrianReportedAt { get; set; }
        public string Exception { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
