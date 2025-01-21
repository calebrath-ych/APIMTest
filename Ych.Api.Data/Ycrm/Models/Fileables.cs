using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Fileables
    {
        public ulong Id { get; set; }
        public ulong FileId { get; set; }
        public string FileableType { get; set; }
        public ulong FileableId { get; set; }

        public virtual Files File { get; set; }
    }
}
