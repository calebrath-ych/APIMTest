using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class Revisions
    {
        public uint Id { get; set; }
        public string RevisionableType { get; set; }
        public int RevisionableId { get; set; }
        public int? UserId { get; set; }
        public string Key { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
