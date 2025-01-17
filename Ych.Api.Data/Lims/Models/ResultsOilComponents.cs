using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class ResultsOilComponents
    {
        public ulong Id { get; set; }
        public string SampleCode { get; set; }
        public double? APinene { get; set; }
        public double? BPinene { get; set; }
        public double? Myrcene { get; set; }
        public double? TwoMethylButylIsobutyrate { get; set; }
        public double? Limonene { get; set; }
        public double? MethylHeptanoate { get; set; }
        public double? MethylOctonoate { get; set; }
        public double? Linalool { get; set; }
        public double? Caryophyllene { get; set; }
        public double? Farnesene { get; set; }
        public double? Humulene { get; set; }
        public double? Geraniol { get; set; }
        public double? CaryophylleneOxide { get; set; }
        public string Notes { get; set; }
        public DateTime? AnalysisDate { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
