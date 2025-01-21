using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class BeerSampleVarieties
    {
        public ulong Id { get; set; }
        public int BeerSampleId { get; set; }
        public string VarietyCode { get; set; }
        public string ProductLineCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
