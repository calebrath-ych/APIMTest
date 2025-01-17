using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api
{
    /// <summary>
    /// API error codes and descriptions for different conditions. Becomes the code property of an ApiErrorResponse object.
    /// Only use the static pre-defined error codes to ensure responses remain consistent.
    /// </summary>
    public class ApiErrorCode
    {
        public static readonly ApiErrorCode InvalidParameter_0x7100 = new ApiErrorCode("0x7100", "Invalid parameter.");
        public static readonly ApiErrorCode InvalidToken_0x7101 = new ApiErrorCode("0x7101", "Invalid token.");
        public static readonly ApiErrorCode DatabaseConnection_0x7102 = new ApiErrorCode("0x7102", "Database connection failed.");
        public static readonly ApiErrorCode DatabaseQuery_0x7103 = new ApiErrorCode("0x7103", "Database query failed.");
        public static readonly ApiErrorCode X3Exception_0x7104 = new ApiErrorCode("0x7104", "X3 operation failed.");
        public static readonly ApiErrorCode InternalError_0x7105 = new ApiErrorCode("0x7105", "Unexpected internal error.");
        public static readonly ApiErrorCode ResourceNotFound_0x7106 = new ApiErrorCode("0x7106", "Could not find expected resource");

        /// <summary>
        /// The custom code that represents this error.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Default user message for this code. Can be overridden when throwing an ApiException.
        /// </summary>
        public string DefaultMessage { get; set; }

        private ApiErrorCode(string code, string defaultMessage)
        {
            Code = code;
            DefaultMessage = defaultMessage;
        }

        public override string ToString() => Code;
    }
}
