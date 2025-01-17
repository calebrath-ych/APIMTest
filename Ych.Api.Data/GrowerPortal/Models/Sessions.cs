using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class Sessions
    {
        public string Id { get; set; }
        public uint? UserId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Payload { get; set; }
        public int LastActivity { get; set; }
    }
}
