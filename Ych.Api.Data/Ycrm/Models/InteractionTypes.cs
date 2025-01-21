using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class InteractionTypes
    {
        public InteractionTypes()
        {
            Interactions = new HashSet<Interactions>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Interactions> Interactions { get; set; }
    }
}
