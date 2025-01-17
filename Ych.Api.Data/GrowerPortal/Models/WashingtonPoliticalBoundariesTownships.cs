using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class WashingtonPoliticalBoundariesTownships
    {
        public ulong Id { get; set; }
        public string ObjectId { get; set; }
        public string LegalDescLabelNm { get; set; }
        public string LegalDescNm { get; set; }
        public string PlsTwpNo { get; set; }
        public string PlsTwpFractCd { get; set; }
        public string PlsTwpDirCd { get; set; }
        public string PlsRngNo { get; set; }
        public string PlsRngFractCd { get; set; }
        public string PlsRngDirCd { get; set; }
        public string LegalDescSystId { get; set; }
        public string LegalDescTypeCd { get; set; }
        public string LegalDescDupStatusFlg { get; set; }
        public string LegalDescEstDt { get; set; }
        public string PlsMeridianCd { get; set; }
        public string AquaticLandFlg { get; set; }
        public string EditDate { get; set; }
        public string EditStatus { get; set; }
        public string EditWho { get; set; }
        public string ShapeArea { get; set; }
        public string ShapeLength { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
