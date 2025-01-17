using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class WorkOrders
    {
        public WorkOrders()
        {
            SelectionsWorkorders = new HashSet<SelectionsWorkorders>();
        }

        public uint Id { get; set; }
        public uint CreatedById { get; set; }
        public string WorkOrderCode { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CreatedBy { get; set; }
        public virtual ICollection<SelectionsWorkorders> SelectionsWorkorders { get; set; }
    }
}
