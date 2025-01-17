using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Grades
    {
        public Grades()
        {
            CustomersGrades = new HashSet<CustomersGrades>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<CustomersGrades> CustomersGrades { get; set; }
    }
}
