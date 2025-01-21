using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class SprayLicensees
    {
        public ulong Id { get; set; }
        public int GrowerId { get; set; }
        public string IndividualName { get; set; }
        public string LicenseNumber { get; set; }
        public string FirmName { get; set; }
        public string Telephone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
