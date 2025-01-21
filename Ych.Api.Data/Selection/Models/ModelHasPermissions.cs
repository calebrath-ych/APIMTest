using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class ModelHasPermissions
    {
        public uint PermissionId { get; set; }
        public string ModelType { get; set; }
        public ulong ModelId { get; set; }

        public virtual Permissions Permission { get; set; }
    }
}
