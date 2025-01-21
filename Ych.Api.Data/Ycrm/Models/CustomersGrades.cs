using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class CustomersGrades
    {
        public ulong Id { get; set; }
        public ulong CustomerId { get; set; }
        public ulong GradeId { get; set; }
        public ulong CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Grades Grade { get; set; }
    }
}
