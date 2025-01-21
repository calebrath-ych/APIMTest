using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class Samples
    {
        public Samples()
        {
            MetaHarvests = new HashSet<MetaHarvests>();
            MetaProduction = new HashSet<MetaProduction>();
            MetaTimeOfProcessings = new HashSet<MetaTimeOfProcessings>();
        }

        public ulong Id { get; set; }
        public ulong SampleTypeId { get; set; }
        public ulong? ParentId { get; set; }
        public string SampleCode { get; set; }
        public string Notes { get; set; }
        public int ProductTypeId { get; set; }
        public bool IsCumulative { get; set; }
        public DateTime? X3SyncedAt { get; set; }
        public DateTime? GpSyncedAt { get; set; }
        public DateTime? Completed { get; set; }
        public bool NeedsRerun { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual SampleTypes SampleType { get; set; }
        public virtual ICollection<MetaHarvests> MetaHarvests { get; set; }
        public virtual ICollection<MetaProduction> MetaProduction { get; set; }
        public virtual ICollection<MetaTimeOfProcessings> MetaTimeOfProcessings { get; set; }
    }
}
