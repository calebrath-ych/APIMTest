using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class GlobalQc
    {
        public uint Id { get; set; }
        public string VarietyId { get; set; }
        public string VarietyName { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public string AromaShort { get; set; }
        public string Description { get; set; }
        public decimal AlphaAvg { get; set; }
        public decimal AlphaLow { get; set; }
        public decimal AlphaHigh { get; set; }
        public decimal BetaAvg { get; set; }
        public decimal BetaLow { get; set; }
        public decimal BetaHigh { get; set; }
        public decimal AlphaBeta { get; set; }
        public string CoH { get; set; }
        public decimal TotalOilAvg { get; set; }
        public decimal TotalOilLow { get; set; }
        public decimal TotalOilHigh { get; set; }
        public string BPinene { get; set; }
        public string Myrcene { get; set; }
        public string Linalool { get; set; }
        public string Caryophyllene { get; set; }
        public string Farnesene { get; set; }
        public string Humulene { get; set; }
        public string Geraniol { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
