using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SelectionNotes
    {
        public SelectionNotes()
        {
            NotesSelections = new HashSet<NotesSelections>();
        }

        public uint Id { get; set; }
        public uint CreatedById { get; set; }
        public string Body { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Users CreatedBy { get; set; }
        public virtual ICollection<NotesSelections> NotesSelections { get; set; }
    }
}
