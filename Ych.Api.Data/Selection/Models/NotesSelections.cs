using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class NotesSelections
    {
        public uint NoteId { get; set; }
        public uint SelectionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual SelectionNotes Note { get; set; }
        public virtual Selections Selection { get; set; }
    }
}
