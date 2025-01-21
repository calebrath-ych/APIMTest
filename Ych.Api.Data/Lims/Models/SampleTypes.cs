using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class SampleTypes
    {
        public SampleTypes()
        {
            Samples = new HashSet<Samples>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public bool ExpectsUv { get; set; }
        public bool ExpectsOil { get; set; }
        public bool ExpectsOilComponents { get; set; }
        public bool ExpectsDryMatter { get; set; }
        public bool ExpectsHplc { get; set; }
        public bool ExpectsOvenMoisture { get; set; }
        public bool ExpectsLcv { get; set; }
        public bool ExpectsCumulativeUv { get; set; }
        public bool ExpectsCumulativeOil { get; set; }
        public bool ExpectsCumulativeOilComponents { get; set; }
        public bool ExpectsCumulativeOvenMoisture { get; set; }
        public bool ExpectsCumulativeDryMatter { get; set; }
        public bool ExpectsCumulativeHplc { get; set; }
        public bool ExpectsCumulativeLvc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Samples> Samples { get; set; }
    }
}
