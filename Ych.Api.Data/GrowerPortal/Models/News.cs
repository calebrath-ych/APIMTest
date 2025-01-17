using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class News
    {
        public uint Id { get; set; }
        public uint AuthorId { get; set; }
        public string UpdateAuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public string ImageLocation { get; set; }
        public string AttachmentLocation { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users Author { get; set; }
    }
}
