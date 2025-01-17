using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class Taggables
    {
        public uint TagId { get; set; }
        public string TaggableType { get; set; }
        public ulong TaggableId { get; set; }

        public virtual Tags Tag { get; set; }
    }
}
