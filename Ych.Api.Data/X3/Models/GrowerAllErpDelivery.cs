using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Data.X3.Views
{
    public class GrowerAllErpDelivery
    {
        public string DateReceived { get; set; }
        public string Lot { get; set; }
        public string Variety { get; set; }
        public int QtyBalesDlv { get; set; }
        public decimal QtylbsDlv { get; set; }
        public decimal BaleWeight { get; set; }
        public string LeafStem { get; set; }
        public string Seed { get; set; }
        public string Owner { get; set; }
        public string Alpha { get; set; }
        public string BaleType { get; set; }
        public decimal AlphaPounds { get; set; }
    }
}