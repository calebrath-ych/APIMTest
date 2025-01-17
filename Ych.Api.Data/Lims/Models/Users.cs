using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class Users
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
