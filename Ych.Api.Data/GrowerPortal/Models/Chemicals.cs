using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Chemicals
    {
        public uint Id { get; set; }
        public string EpaRegNum { get; set; }
        public string AgrianEpaRegNum { get; set; }
        public string AgrianProductId { get; set; }
        public string CommonName { get; set; }
        public string TradeName { get; set; }
        public string ChemicalType { get; set; }
        public string LabelRate { get; set; }
        public string UOfM { get; set; }
        public string UOfMRate { get; set; }
        public string MaxAppsNum { get; set; }
        public string MaxRatePerSeason { get; set; }
        public string IntervalDaysLabel { get; set; }
        public string PhiDaysLabel { get; set; }
        public string ReentryHours { get; set; }
        public string OrganicCertificate { get; set; }
        public string SignalWord { get; set; }
        public string CutoffEu { get; set; }
        public string CutoffJp { get; set; }
        public bool? BannedEu { get; set; }
        public bool? BannedJp { get; set; }
        public bool? BannedKo { get; set; }
        public bool? BannedTw { get; set; }
        public string PhiEu { get; set; }
        public string PhiJp { get; set; }
        public string UsMrl { get; set; }
        public string CodexMrl { get; set; }
        public string EuMrl { get; set; }
        public string JpMrl { get; set; }
        public string CanadaMrl { get; set; }
        public string AustraliaMrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
