using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class FilesComments
    {
        public uint Id { get; set; }
        public string Body { get; set; }
        public uint FileId { get; set; }
        public uint AuthorId { get; set; }
        public uint? DeletedById { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users Author { get; set; }
        public virtual Files File { get; set; }
    }
}
