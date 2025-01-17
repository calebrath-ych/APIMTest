using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Data.X3.Views
{
    public class LotAnalytic
    {
        public string Lot { get; set; }
        public string Variety { get; set; }
        public string VarietyId { get; set; }
        public string OilByDist { get; set; }
        public string OilBPinene { get; set; }
        public string OilMyrcene { get; set; }
        public string OilLinalool { get; set; }
        public string OilCaryophyllene { get; set; }
        public string OilFarnesene { get; set; }
        public string OilHumulene { get; set; }
        public string OilGeraniol { get; set; }
        public string UvAlpha { get; set; }
        public string UvBeta { get; set; }
        public string Hsi { get; set; }
        public string MoistMin { get; set; }
        public string MoistMax { get; set; }
        public string TempMin { get; set; }
        public string TempMax { get; set; }
    }
}
