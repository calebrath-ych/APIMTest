using System;
using System.Collections.Generic;
using System.Text;
using Ych.Logging;

namespace Ych.Api.Logging
{
    /// <summary>
    /// A custom log entry to add API specific properties to be logged.
    /// </summary>
    public class ApiLogEntry : LogEntry
    {
        public ApiLogEntry() 
        {
        }

        public ApiLogEntry(string source, LogSeverities severity, string message)
        {
            Source = source;
            Severity = severity;
            Message = message;
        }

        public ApiLogEntry(string source, Exception ex, LogSeverities severity = LogSeverities.Error)
        {
            Source = source;
            Severity = severity;
            Exception = ex;
        }

        public override string ToString()
        {
            return $"{CreatedAt} [{Source}] {Message}";
        }
    }
}
