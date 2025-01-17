using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SelectionsWorkorders
    {
        public uint Id { get; set; }
        public uint WorkorderId { get; set; }
        public uint SelectionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Selections Selection { get; set; }
        public virtual WorkOrders Workorder { get; set; }
    }
}
