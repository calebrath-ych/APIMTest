using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Users
    {
        public Users()
        {
            Barrellages = new HashSet<Barrellages>();
            CustomerProfiles = new HashSet<CustomerProfiles>();
            CustomersCustomerServiceSpecialist = new HashSet<Customers>();
            CustomersGrades = new HashSet<CustomersGrades>();
            CustomersRegionalSalesManager = new HashSet<Customers>();
            Interactions = new HashSet<Interactions>();
            TerritoryUser = new HashSet<TerritoryUser>();
        }

        public ulong Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string X3Id { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string Password { get; set; }
        public string RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Barrellages> Barrellages { get; set; }
        public virtual ICollection<CustomerProfiles> CustomerProfiles { get; set; }
        public virtual ICollection<Customers> CustomersCustomerServiceSpecialist { get; set; }
        public virtual ICollection<CustomersGrades> CustomersGrades { get; set; }
        public virtual ICollection<Customers> CustomersRegionalSalesManager { get; set; }
        public virtual ICollection<Interactions> Interactions { get; set; }
        public virtual ICollection<TerritoryUser> TerritoryUser { get; set; }
    }
}
