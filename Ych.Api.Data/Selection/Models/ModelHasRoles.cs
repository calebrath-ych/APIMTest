using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class ModelHasRoles
    {
        public uint RoleId { get; set; }
        public string ModelType { get; set; }
        public ulong ModelId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
