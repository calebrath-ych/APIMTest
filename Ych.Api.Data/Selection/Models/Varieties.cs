using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Varieties
    {
        public Varieties()
        {
            Samples = new HashSet<Samples>();
            SamplesArchive = new HashSet<SamplesArchive>();
            Selections = new HashSet<Selections>();
            Sensory = new HashSet<Sensory>();
        }

        public uint Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Selectable { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Samples> Samples { get; set; }
        public virtual ICollection<SamplesArchive> SamplesArchive { get; set; }
        public virtual ICollection<Selections> Selections { get; set; }
        public virtual ICollection<Sensory> Sensory { get; set; }
    }
}
