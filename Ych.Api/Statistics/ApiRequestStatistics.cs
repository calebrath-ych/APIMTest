using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api.Statistics
{
    public class ApiRequestStatistics
    {
        public int Id { get; set; }
        public string Api { get; set; }
        public string Operation { get; set; }
        public long Requests { get; set; }
        public long ValidationFailures { get; set; }
        public long Errors { get; set; }
        public DateTime CaptureDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
