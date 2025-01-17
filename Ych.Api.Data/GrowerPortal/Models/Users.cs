using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Users
    {
        public Users()
        {
            AuditLogs = new HashSet<AuditLogs>();
            FilesComments = new HashSet<FilesComments>();
            News = new HashSet<News>();
            SocialAccounts = new HashSet<SocialAccounts>();
        }

        public uint Id { get; set; }
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarType { get; set; }
        public string AvatarLocation { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordChangedAt { get; set; }
        public byte Active { get; set; }
        public string ConfirmationCode { get; set; }
        public bool? Confirmed { get; set; }
        public string Timezone { get; set; }
        public string RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<AuditLogs> AuditLogs { get; set; }
        public virtual ICollection<FilesComments> FilesComments { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<SocialAccounts> SocialAccounts { get; set; }
    }
}
