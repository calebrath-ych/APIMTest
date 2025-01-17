using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class SocialAccounts
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users User { get; set; }
    }
}
