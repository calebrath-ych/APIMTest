using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class FinancialRecords
    {
        public uint Id { get; set; }
        public uint FinancialUploadId { get; set; }
        public string GrowerId { get; set; }
        public string Variety { get; set; }
        public decimal LbsDelivered { get; set; }
        public decimal LbsUnsettled { get; set; }
        public decimal LbsSettled { get; set; }
        public decimal AvgReturn { get; set; }
        public decimal RevenueEarned { get; set; }
        public decimal PaymentsReceived { get; set; }
        public decimal MonthlyAvgReturn { get; set; }
        public decimal MonthlyLbsSettled { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
