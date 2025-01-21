using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class HarvestDatasheets
    {
        public uint Id { get; set; }
        public string GrowerId { get; set; }
        public string LotNumber { get; set; }
        public string GrownBy { get; set; }
        public string CropYear { get; set; }
        public string BaleMoistureHigh { get; set; }
        public string BaleMoistureLow { get; set; }
        public string BaleStorageConditions { get; set; }
        public string CoolingHoursBeforeBaler { get; set; }
        public string CoolingHoursInKiln { get; set; }
        public string DryingHours { get; set; }
        public string DryingTempF { get; set; }
        public bool? Humidified { get; set; }
        public string KilnDepthIn { get; set; }
        public uint? FacilityId { get; set; }
        public string FacilityOther { get; set; }
        public uint KilnFuelId { get; set; }
        public uint PickerTypeId { get; set; }
        public string TotalBales { get; set; }
        public string VarietyId { get; set; }
        public string VarietyOther { get; set; }
        public string VarietyAgrian { get; set; }
        public string Comments { get; set; }
        public DateTime? HarvestStartAt { get; set; }
        public DateTime? HarvestEndAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
