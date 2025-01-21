using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class StorageConditions
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
