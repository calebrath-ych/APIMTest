using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Ycrm.Models
{
    public partial class TerritoryUser
    {
        public ulong Id { get; set; }
        public ulong TerritoryId { get; set; }
        public ulong UserId { get; set; }

        public virtual Territories Territory { get; set; }
        public virtual Users User { get; set; }
    }
}
