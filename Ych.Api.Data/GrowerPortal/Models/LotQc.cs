using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class LotQc
    {
        public uint Id { get; set; }
        public DateTime? DateReceived { get; set; }
        public decimal? CropYear { get; set; }
        public string Lot { get; set; }
        public decimal? QtyBales { get; set; }
        public string X3Status { get; set; }
        public decimal? TempMin { get; set; }
        public decimal? TempMax { get; set; }
        public decimal? MoistMin { get; set; }
        public decimal? MoistMax { get; set; }
        public decimal? UvAlpha { get; set; }
        public decimal? UvBeta { get; set; }
        public decimal? Hsi { get; set; }
        public decimal? MoistureOven { get; set; }
        public decimal? MoistureMeter { get; set; }
        public decimal? OilByDist { get; set; }
        public decimal? OilAPinene { get; set; }
        public decimal? OilBPinene { get; set; }
        public decimal? OilMyrcene { get; set; }
        public decimal? Oil2MethylButyl { get; set; }
        public decimal? OilLimonene { get; set; }
        public decimal? OilMethylHeptonate { get; set; }
        public decimal? OilMethylOctonoate { get; set; }
        public decimal? OilLinalool { get; set; }
        public decimal? OilCaryophyllene { get; set; }
        public decimal? OilFarnesene { get; set; }
        public decimal? OilHumulene { get; set; }
        public decimal? OilGeraniol { get; set; }
        public decimal? OilCaryoxide { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
