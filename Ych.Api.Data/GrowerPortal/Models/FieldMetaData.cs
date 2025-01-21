using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class FieldMetaData
    {
        public ulong Id { get; set; }
        public int FieldId { get; set; }
        public int Year { get; set; }
        public DateTime Planted { get; set; }
        public string Variety { get; set; }
        public int? AmountPlanted { get; set; }
        public int? MediaId { get; set; }
        public string TraceabilityId { get; set; }
        public string Source { get; set; }
        public bool? Organic { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
