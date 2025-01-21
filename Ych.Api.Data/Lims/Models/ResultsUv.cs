using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class ResultsUv
    {
        public ulong Id { get; set; }
        public string SampleCode { get; set; }
        public double? Alpha { get; set; }
        public double? Beta { get; set; }
        public double? Hsi { get; set; }
        public string Notes { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
