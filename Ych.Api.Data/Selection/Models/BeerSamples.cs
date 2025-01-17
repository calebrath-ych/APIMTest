using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class BeerSamples
    {
        public BeerSamples()
        {
            BeerSensory = new HashSet<BeerSensory>();
        }

        public ulong Id { get; set; }
        public string SampleCode { get; set; }
        public string BeerName { get; set; }
        public string BeerSource { get; set; }
        public string BeerStyle { get; set; }
        public string Notes { get; set; }
        public string NotesForDisplay { get; set; }
        public bool? InResultsSampleCode { get; set; }
        public bool InResultsBeerName { get; set; }
        public bool InResultsBeerSource { get; set; }
        public bool InResultsBeerStyle { get; set; }
        public bool? InResultsVarietyCode { get; set; }
        public bool? InResultsProductLineCode { get; set; }
        public bool? InResultsNotesForDisplay { get; set; }
        public bool DisplayBeerName { get; set; }
        public bool RevealOnSubmission { get; set; }
        public bool RevealOnResults { get; set; }
        public bool? InResultsIndividualStats { get; set; }
        public bool? InResultsGroupStats { get; set; }
        public DateTime? IntendedEvalDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<BeerSensory> BeerSensory { get; set; }
    }
}
