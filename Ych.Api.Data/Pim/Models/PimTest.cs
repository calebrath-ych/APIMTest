using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class PimTest
    {
        public string ProductLineName { get; set; }
        public string DisplayName { get; set; }
        public int IsnullAlphaLow { get; set; }
        public double? AlphaAve { get; set; }
        public double? AlphaHigh { get; set; }
        public double? BetaLow { get; set; }
        public double? BetaAve { get; set; }
        public double? HumuleneAve { get; set; }
        public double? CaryophylleneAve { get; set; }
        public double? BPineneHigh { get; set; }
    }
}
