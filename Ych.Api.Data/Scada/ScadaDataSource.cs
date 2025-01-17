using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Pim.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Scada
{
    public partial class ScadaDataSource : ApiDataSource
    {
        protected override string Name => "Scada";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.SqlServer;

        public ScadaDataSource(ISettingsProvider settings) : base(settings)
        {
        }
    }
}
