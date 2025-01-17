using System;
using System.Collections.Generic;
using System.Text;
using TimeZoneConverter;

namespace Ych.Api
{
    public static partial class Extensions
    {
        public static DateTime ToPst(this DateTime dateTime)
        {
            TimeZoneInfo pacificTimeZone = TZConvert.GetTimeZoneInfo("Pacific Standard Time");
            DateTime pacificTime = TimeZoneInfo.ConvertTime(dateTime, pacificTimeZone);

            return pacificTime;
        }
    }
}
