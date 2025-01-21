using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class MetaTimeOfProcessings
    {
        public ulong Id { get; set; }
        public ulong SampleId { get; set; }
        public string BaleLotNumber { get; set; }
        public string ProductionLotNumber { get; set; }
        public string VarietyCode { get; set; }
        public string Mrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Samples Sample { get; set; }
    }
}
