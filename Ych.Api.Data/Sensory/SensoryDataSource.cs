using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ych.Api.Data.Selection.Models;
using Ych.Configuration;
using Ych.Data;

namespace Ych.Api.Data.Sensory
{
    public partial class SensoryDataSource : ApiDataSource
    {
        protected override string Name => "Sensory";
        protected override DataSourceTypes DataSourceType => DataSourceTypes.MySql;

        public SensoryDataSource(ISettingsProvider settings) : base(settings)
        {
        }
    }
}
