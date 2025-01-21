using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Addresses = new HashSet<Addresses>();
            Barrellages = new HashSet<Barrellages>();
            CustomerProfiles = new HashSet<CustomerProfiles>();
            CustomersGrades = new HashSet<CustomersGrades>();
            Interactions = new HashSet<Interactions>();
            LegalProceedings = new HashSet<LegalProceedings>();
            PaymentPlans = new HashSet<PaymentPlans>();
        }

        public ulong Id { get; set; }
        public string DoingBusinessAs { get; set; }
        public string LegalName { get; set; }
        public string Notes { get; set; }
        public string X3Id { get; set; }
        public string X3GroupId { get; set; }
        public ulong? TerritoryId { get; set; }
        public ulong? RegionalSalesManagerId { get; set; }
        public ulong? CustomerServiceSpecialistId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CustomerServiceSpecialist { get; set; }
        public virtual Users RegionalSalesManager { get; set; }
        public virtual Territories Territory { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<Barrellages> Barrellages { get; set; }
        public virtual ICollection<CustomerProfiles> CustomerProfiles { get; set; }
        public virtual ICollection<CustomersGrades> CustomersGrades { get; set; }
        public virtual ICollection<Interactions> Interactions { get; set; }
        public virtual ICollection<LegalProceedings> LegalProceedings { get; set; }
        public virtual ICollection<PaymentPlans> PaymentPlans { get; set; }
    }
}
