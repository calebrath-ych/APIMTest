using System;
using System.Collections.Generic;
using System.Text;
using Ych.Configuration;
using Ych.Logging;
using Ych.Logging.Datadog;

namespace Ych.Api.Logging
{
    public class ApiLogWriter : DatadogLogWriter
    {
        public ApiLogWriter(ISettingsProvider settings) : base(settings)
        {
        }
    }
}
