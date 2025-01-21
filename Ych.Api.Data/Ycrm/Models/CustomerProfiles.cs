using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class CustomerProfiles
    {
        public ulong Id { get; set; }
        public ulong CustomerId { get; set; }
        public ulong CreatedBy { get; set; }
        public int? TaproomCount { get; set; }
        public int? LocationsCount { get; set; }
        public bool SelfDistribution { get; set; }
        public bool WholesaleDistribution { get; set; }
        public bool Draft { get; set; }
        public bool Cans { get; set; }
        public bool Bottles { get; set; }
        public bool Crowlers { get; set; }
        public bool Growlers { get; set; }
        public bool PilotSystem { get; set; }
        public bool SourProgram { get; set; }
        public bool BarrelAgeing { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
