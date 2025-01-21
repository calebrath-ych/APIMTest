using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Data.Notification.Models
{
    public class Notification
    {
        public ulong Id { get; set; }
        public ulong NotificationTypeId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public List<Recipient> Recipients {get; set;}

    }
    public class Recipient
    {
        public string ExternalId { get; set; }
        public bool Push { get; set; }
        public bool Email { get; set; }
    }
}
