using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class MetaProduction
    {
        public ulong Id { get; set; }
        public ulong SampleId { get; set; }
        public string ProductionLotNumber { get; set; }
        public int TestNumber { get; set; }
        public string VarietyCode { get; set; }
        public string Mrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Samples Sample { get; set; }
    }
}
