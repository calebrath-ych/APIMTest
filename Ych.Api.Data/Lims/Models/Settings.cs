using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class Settings
    {
        public ulong Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
