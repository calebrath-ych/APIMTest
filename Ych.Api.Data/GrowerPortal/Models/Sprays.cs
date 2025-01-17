using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Sprays
    {
        public uint Id { get; set; }
        public string AgrianPurId { get; set; }
        public string GrowerId { get; set; }
        public uint? ChemicalId { get; set; }
        public long? SprayLicenseeId { get; set; }
        public uint? MeasurementUnitId { get; set; }
        public string MeasurementUnitOther { get; set; }
        public string CropYear { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string RatePerAcre { get; set; }
        public string ConcentrationApplied { get; set; }
        public string OtherEpaRegNum { get; set; }
        public string OtherTradeName { get; set; }
        public string OtherCommonName { get; set; }
        public string OtherChemicalTypeName { get; set; }
        public uint? OtherChemicalType { get; set; }
        public string Comments { get; set; }
        public long? SprayApplicationMethodId { get; set; }
        public string AgrianViewUrl { get; set; }
        public uint? CreatedById { get; set; }
        public uint? DeletedById { get; set; }
        public uint? CustomChemApprovedById { get; set; }
        public DateTime? CustomChemApproved { get; set; }
        public DateTime? AgrianReportedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TimeSpan? ApplicationTimeStart { get; set; }
        public TimeSpan? ApplicationTimeEnd { get; set; }
        public decimal? TemperatureRangeLow { get; set; }
        public decimal? TemperatureRangeHigh { get; set; }
        public string WindVector { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
