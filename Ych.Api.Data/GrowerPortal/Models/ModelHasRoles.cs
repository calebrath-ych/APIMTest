using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class ModelHasRoles
    {
        public uint RoleId { get; set; }
        public uint ModelId { get; set; }
        public string ModelType { get; set; }

        public virtual Roles Role { get; set; }
    }
}
