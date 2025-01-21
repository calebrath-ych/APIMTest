using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Lims.Models
{
    public partial class SampleCodeLists
    {
        public ulong Id { get; set; }
        public string SampleCodes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
