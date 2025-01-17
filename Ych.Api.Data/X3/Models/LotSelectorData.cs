using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ych.Api.Data.X3.Views
{
    public class LotSelectorData
    {
        public string LotNumber { get; set; }
        public string GrowerName { get; set; }
        public List<Dictionary<string, object>> ProductTypes { get; set; }
        public Dictionary<string, object> BrewingValues { get; set; }
        public Dictionary<string, object> CalculatedValues { get; set; }
        public Dictionary<string, object> HarvestSheetValues { get; set; }
    }
}