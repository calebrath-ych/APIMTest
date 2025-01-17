using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Data.X3.Views
{
    public class VarietyAnalytic
    {
        public string Grower { get; set; }
        public string Variety { get; set; }
        public string VarietyId { get; set; }
        public string AvgUvAlpha { get; set; }
        public string AvgUvBeta { get; set; }
        public string AvgHsi { get; set; }
        public string AvgOil { get; set; }
        public string AvgTemp { get; set; }
        public string AvgMois { get; set; }
        public string LotCount { get; set; }
        public string YchAvgUvAlpha { get; set; }
        public string YchAvgUvBeta { get; set; }
        public string YchAvgHsi { get; set; }
        public string YchAvgOil { get; set; }
        public string YchAvgTemp { get; set; }
        public string YchAvgMois { get; set; }
    }
}