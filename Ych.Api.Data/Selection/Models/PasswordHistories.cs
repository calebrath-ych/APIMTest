using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class PasswordHistories
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users User { get; set; }
    }
}
