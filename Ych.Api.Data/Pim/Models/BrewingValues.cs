using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class BrewingValues
    {
        public uint Id { get; set; }
        public uint ProductLineId { get; set; }
        public uint VarietyId { get; set; }
        public double? AlphaLow { get; set; }
        public double? AlphaAve { get; set; }
        public double? AlphaHigh { get; set; }
        public double? BetaLow { get; set; }
        public double? BetaAve { get; set; }
        public double? BetaHigh { get; set; }
        public double? OilLow { get; set; }
        public double? OilAve { get; set; }
        public double? OilHigh { get; set; }
        public double? CoHLow { get; set; }
        public double? CoHAve { get; set; }
        public double? CoHHigh { get; set; }
        public double? BPineneLow { get; set; }
        public double? BPineneAve { get; set; }
        public double? BPineneHigh { get; set; }
        public double? MyrceneLow { get; set; }
        public double? MyrceneAve { get; set; }
        public double? MyrceneHigh { get; set; }
        public double? LinaloolLow { get; set; }
        public double? LinaloolAve { get; set; }
        public double? LinaloolHigh { get; set; }
        public double? CaryophylleneLow { get; set; }
        public double? CaryophylleneAve { get; set; }
        public double? CaryophylleneHigh { get; set; }
        public double? FarneseneLow { get; set; }
        public double? FarneseneAve { get; set; }
        public double? FarneseneHigh { get; set; }
        public double? HumuleneLow { get; set; }
        public double? HumuleneAve { get; set; }
        public double? HumuleneHigh { get; set; }
        public double? GeraniolLow { get; set; }
        public double? GeraniolAve { get; set; }
        public double? GeraniolHigh { get; set; }
        public double? SilineneLow { get; set; }
        public double? SilineneAve { get; set; }
        public double? SilineneHigh { get; set; }
        public double? OtherLow { get; set; }
        public double? OtherAve { get; set; }
        public double? OtherHigh { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? MagentoSyncedAt { get; set; }

        public virtual ProductLines ProductLine { get; set; }
        public virtual Varieties Variety { get; set; }
    }
}
