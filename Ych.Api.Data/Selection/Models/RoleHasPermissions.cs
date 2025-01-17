using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class RoleHasPermissions
    {
        public uint PermissionId { get; set; }
        public uint RoleId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Roles Role { get; set; }
    }
}
