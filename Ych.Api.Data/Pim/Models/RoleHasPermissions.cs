using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Pim.Models
{
    public partial class RoleHasPermissions
    {
        public ulong PermissionId { get; set; }
        public ulong RoleId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Roles Role { get; set; }
    }
}
