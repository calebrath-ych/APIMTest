using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public class HarvestInformation
    {
        public string grown_by { get; set; }
        public string bale_moisture_low { get; set; }
        public string bale_moisture_high { get; set; }
        public string storage_conditions { get; set; }
        public string cooling_hours_before_baler { get; set; }
        public string cooling_hours_in_kiln { get; set; }
        public string drying_hours { get; set; }
        public string drying_temp_f { get; set; }
        public bool? humidified { get; set; }
        public string kiln_depth_in { get; set; }
        public string facility_name { get; set; }
        public string facility_other { get; set; }
        public string kiln_fuel_type { get; set; }
        public string picker_type { get; set; }
        public DateTime? harvest_start_at { get; set; }
        public DateTime? harvest_end_at { get; set; }
    }
}
