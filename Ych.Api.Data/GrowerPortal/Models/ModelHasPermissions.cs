using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class ModelHasPermissions
    {
        public uint PermissionId { get; set; }
        public uint ModelId { get; set; }
        public string ModelType { get; set; }

        public virtual Permissions Permission { get; set; }
    }
}
