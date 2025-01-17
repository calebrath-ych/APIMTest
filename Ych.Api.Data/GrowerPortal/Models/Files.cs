using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Files
    {
        public Files()
        {
            FilesComments = new HashSet<FilesComments>();
        }

        public uint Id { get; set; }
        public string GrowerId { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint CreatedById { get; set; }
        public string ReceivedById { get; set; }
        public DateTime? Received { get; set; }
        public uint CropYear { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool UploadedByAdmin { get; set; }

        public virtual ICollection<FilesComments> FilesComments { get; set; }
    }
}
