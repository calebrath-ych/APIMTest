using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class WashingtonPoliticalBoundariesCounties
    {
        public ulong Id { get; set; }
        public string ObjectId { get; set; }
        public string JurisdictSystId { get; set; }
        public string JurisdictTypeCd { get; set; }
        public string JurisdictLabelNm { get; set; }
        public string JurisdictNm { get; set; }
        public string JurisdictDesgCd { get; set; }
        public string JurisdictFipsDesgCd { get; set; }
        public string EditDate { get; set; }
        public string EditStatus { get; set; }
        public string EditWho { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
