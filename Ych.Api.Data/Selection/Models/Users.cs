using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class Users
    {
        public Users()
        {
            BeerSensory = new HashSet<BeerSensory>();
            PasswordHistories = new HashSet<PasswordHistories>();
            ProductionSensory = new HashSet<ProductionSensory>();
            SelectionNotes = new HashSet<SelectionNotes>();
            SelectionTransactions = new HashSet<SelectionTransactions>();
            Selections = new HashSet<Selections>();
            Sensory = new HashSet<Sensory>();
            SocialAccounts = new HashSet<SocialAccounts>();
            WorkOrders = new HashSet<WorkOrders>();
        }

        public uint Id { get; set; }
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public string CustomerDisplay { get; set; }
        public bool SensoryValidated { get; set; }
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

        public virtual ICollection<BeerSensory> BeerSensory { get; set; }
        public virtual ICollection<PasswordHistories> PasswordHistories { get; set; }
        public virtual ICollection<ProductionSensory> ProductionSensory { get; set; }
        public virtual ICollection<SelectionNotes> SelectionNotes { get; set; }
        public virtual ICollection<SelectionTransactions> SelectionTransactions { get; set; }
        public virtual ICollection<Selections> Selections { get; set; }
        public virtual ICollection<Sensory> Sensory { get; set; }
        public virtual ICollection<SocialAccounts> SocialAccounts { get; set; }
        public virtual ICollection<WorkOrders> WorkOrders { get; set; }
    }
}
