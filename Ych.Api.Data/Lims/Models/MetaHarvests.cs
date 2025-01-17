using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class MetaHarvests
    {
        public ulong Id { get; set; }
        public ulong SampleId { get; set; }
        public string BaleLotNumber { get; set; }
        public int TruckNumber { get; set; }
        public int BaleCount { get; set; }
        public string VarietyCode { get; set; }
        public decimal ProbeTempMin { get; set; }
        public decimal ProbeTempMax { get; set; }
        public decimal ProbeMoistureMin { get; set; }
        public decimal ProbeMoistureMax { get; set; }
        public string Mrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Samples Sample { get; set; }
    }
}
