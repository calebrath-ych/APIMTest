using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class ProductionSamples
    {
        public ProductionSamples()
        {
            ProductionSensory = new HashSet<ProductionSensory>();
        }

        public ulong Id { get; set; }
        public string LotNumber { get; set; }
        public string SampleCode { get; set; }
        public string VarietyCode { get; set; }
        public string ProductLineCode { get; set; }
        public string Notes { get; set; }
        public string NotesForDisplay { get; set; }
        public bool InResultsLotNumber { get; set; }
        public bool? InResultsSampleCode { get; set; }
        public bool? InResultsVarietyCode { get; set; }
        public bool? InResultsProductLineCode { get; set; }
        public bool? InResultsNotesForDisplay { get; set; }
        public bool DisplayLotNumber { get; set; }
        public bool RevealOnSubmission { get; set; }
        public bool RevealOnResults { get; set; }
        public bool? InResultsIndividualStats { get; set; }
        public bool? InResultsGroupStats { get; set; }
        public DateTime? IntendedEvalDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ProductionSensory> ProductionSensory { get; set; }
    }
}
