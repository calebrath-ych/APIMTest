using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Growerfields
    {
        public uint Id { get; set; }
        public string FieldName { get; set; }
        public string GrowerId { get; set; }
        public string AgrianSiteUuid { get; set; }
        public double Acres { get; set; }
        public string Notes { get; set; }
        public uint? CreatedById { get; set; }
        public uint? DeletedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
