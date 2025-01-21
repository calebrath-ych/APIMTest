using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Panels
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public int PanelistCount { get; set; }
        public int RoundCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
