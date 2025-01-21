using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace YchApiFunctions
{
    public static class Extensions
    {
        /// <summary>
        /// Splits a comma delimited list of strings into an array, removing any whitespace and empty entries.
        /// </summary>
        public static string[] ToArray(this StringCollection stringCollection)
        {
            return stringCollection.ToString().Replace(" ", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
