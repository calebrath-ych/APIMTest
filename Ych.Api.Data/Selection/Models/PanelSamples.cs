using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class PanelSamples
    {
        public ulong Id { get; set; }
        public int PanelId { get; set; }
        public int PanelableId { get; set; }
        public string PanelableType { get; set; }
    }
}
