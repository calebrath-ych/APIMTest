using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Selections
    {
        public Selections()
        {
            NotesSelections = new HashSet<NotesSelections>();
            SelectionsTransactions = new HashSet<SelectionsTransactions>();
            SelectionsWorkorders = new HashSet<SelectionsWorkorders>();
        }

        public uint Id { get; set; }
        public ushort? GroupId { get; set; }
        public uint UserId { get; set; }
        public string CustomerId { get; set; }
        public uint CreatedById { get; set; }
        public uint? VarietyId { get; set; }
        public string LotNumber { get; set; }
        public string Status { get; set; }
        public bool Selected { get; set; }
        public bool ValidationOverride { get; set; }
        public bool Rejected { get; set; }
        public uint CropYear { get; set; }
        public string PelletedLotNumber { get; set; }
        public string BlendCode { get; set; }
        public uint? BlendRatio { get; set; }
        public bool BlendFull { get; set; }
        public bool BlendSwing { get; set; }
        public bool SelectedWholeCone { get; set; }
        public bool SelectedT90 { get; set; }
        public bool SelectedCryo { get; set; }
        public string AllocatedLbs { get; set; }
        public decimal? ConeLbs { get; set; }
        public decimal? T90Lbs { get; set; }
        public decimal? CryoLbs { get; set; }
        public string Warehouse { get; set; }
        public decimal? AvgBaleWeight { get; set; }
        public string SalesOrder { get; set; }
        public string WorkOrder { get; set; }
        public string Notes { get; set; }
        public string AtcNotes { get; set; }
        public string AtcBrewerNotes { get; set; }
        public bool AtcIsPending { get; set; }
        public bool AtcIsSingleLot { get; set; }
        public bool AtcIsPelleted { get; set; }
        public bool AtcIsApproved { get; set; }
        public uint? AtcUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool AtcIsMpsComplete { get; set; }

        public virtual Users User { get; set; }
        public virtual Varieties Variety { get; set; }
        public virtual ICollection<NotesSelections> NotesSelections { get; set; }
        public virtual ICollection<SelectionsTransactions> SelectionsTransactions { get; set; }
        public virtual ICollection<SelectionsWorkorders> SelectionsWorkorders { get; set; }
    }
}
