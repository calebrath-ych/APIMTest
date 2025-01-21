using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class ActionEvents
    {
        public ulong Id { get; set; }
        public Guid BatchId { get; set; }
        public ulong UserId { get; set; }
        public string Name { get; set; }
        public string ActionableType { get; set; }
        public ulong ActionableId { get; set; }
        public string TargetType { get; set; }
        public ulong TargetId { get; set; }
        public string ModelType { get; set; }
        public ulong? ModelId { get; set; }
        public string Fields { get; set; }
        public string Status { get; set; }
        public string Exception { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Original { get; set; }
        public string Changes { get; set; }
    }
}
