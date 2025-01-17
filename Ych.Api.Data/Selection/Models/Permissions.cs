using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            ModelHasPermissions = new HashSet<ModelHasPermissions>();
            RoleHasPermissions = new HashSet<RoleHasPermissions>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public string GuardName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ModelHasPermissions> ModelHasPermissions { get; set; }
        public virtual ICollection<RoleHasPermissions> RoleHasPermissions { get; set; }
    }
}
