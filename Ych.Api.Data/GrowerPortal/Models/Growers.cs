using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Growers
    {
        public uint Id { get; set; }
        public Guid Uuid { get; set; }
        public string GrowerId { get; set; }
        public string AgrianGrowerId { get; set; }
        public string AgrianGrowerUuid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string FarmSatelliteImage { get; set; }
    }
}
