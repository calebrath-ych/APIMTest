using System;
using System.Collections.Generic;
using System.Text;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data
{
    public abstract class ApiDataSource : DataSource
    {
        // TODO: This should be able to take advantage of ApiSettings.GetSystem<IDataSystem> to get its ConnectionString,
        // but that is defined in Ych.Api, which depends on this Ych.Api.Data. This dependency feels backwards,
        // but because our services are defined in Ych.Api it is a requirement.
        // Ideally, Ych.Api would be the base assembly, and services would move to a higher level assembly like
        // Ych.Api.Services. Then Data and Services could share a common base assembly with things like settings,
        // error handling and logging.
        protected override string ConnectionStringSetting => $"Api.{Name}.ConnectionString";

        protected ApiDataSource(ISettingsProvider settings) : base(settings)
        {
            
        }
    }
}
