using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Territories
    {
        public Territories()
        {
            Customers = new HashSet<Customers>();
            TerritoryUser = new HashSet<TerritoryUser>();
        }

        public ulong Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<TerritoryUser> TerritoryUser { get; set; }
    }
}
