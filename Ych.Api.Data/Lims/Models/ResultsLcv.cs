using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class ResultsLcv
    {
        public ulong Id { get; set; }
        public string SampleCode { get; set; }
        public double? Value75 { get; set; }
        public double? Value74 { get; set; }
        public double? SampleVolume { get; set; }
        public double? CoefficientOfVariation { get; set; }
        public string Notes { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
