using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Bins
    {
        public Bins()
        {
            Samples = new HashSet<Samples>();
            SamplesArchive = new HashSet<SamplesArchive>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Samples> Samples { get; set; }
        public virtual ICollection<SamplesArchive> SamplesArchive { get; set; }
    }
}
