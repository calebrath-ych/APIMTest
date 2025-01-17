using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Interactions
    {
        public ulong Id { get; set; }
        public ulong CustomerId { get; set; }
        public ulong CreatedBy { get; set; }
        public ulong TypeId { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime InteractionAt { get; set; }

        [JsonIgnore]
        public virtual Users CreatedByNavigation { get; set; }
        [JsonIgnore]
        public virtual Customers Customer { get; set; }
        [JsonIgnore]
        public virtual InteractionTypes Type { get; set; }
    }
}
