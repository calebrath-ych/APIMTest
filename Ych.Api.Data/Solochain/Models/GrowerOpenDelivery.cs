using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Data.Solochain.Views
{
    public class GrowerOpenDelivery
    {
        public DateTime DateReceived { get; set; }
        public string Lot { get; set; }
        public string Variety { get; set; }
        public decimal QtyBalesDlv { get; set; }
    }
}