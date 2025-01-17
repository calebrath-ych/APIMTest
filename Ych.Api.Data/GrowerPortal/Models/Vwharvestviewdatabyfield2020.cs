using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Vwharvestviewdatabyfield2020
    {
        public string GrowerName { get; set; }
        public string GrowerId { get; set; }
        public string FieldName { get; set; }
        public double Acres { get; set; }
        public string Lots { get; set; }
        public long LotCount { get; set; }
        public DateTime? HarvestStartAt { get; set; }
        public DateTime? HarvestEndAt { get; set; }
        public long MaleHopPlantCount { get; set; }
    }
}
