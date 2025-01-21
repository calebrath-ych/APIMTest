using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ych.Api
{
    /// <summary>
    /// Exception type that allows services to communicate different response and error codes for a failed API request.
    /// Throwing this exception, or one derived from it, from business logic code will trigger global error handling and
    /// translate this exception into an appropriate ApiErrorResponse.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// The response code to return for this exception.
        /// </summary>
        public ApiResponseCodes ResponseCode { get; set; } = ApiResponseCodes.InternalError;
        /// <summary>
        /// The API error code to return for this exception.
        /// </summary>
        public ApiErrorCode ErrorCode { get; set; } = ApiErrorCode.InternalError_0x7105;
        /// <summary>
        /// Additional user data to attach to the error response.
        /// </summary>
        public object UserData { get; set; }

        public ApiException() : base() { }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, ApiErrorCode errorCode, ApiResponseCodes responseCode) : base(message) 
        {
            ErrorCode = errorCode;
            ResponseCode = responseCode;
        }

        public ApiException(string message, Exception innerException, ApiErrorCode errorCode, ApiResponseCodes responseCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
            ResponseCode = responseCode;
        }
    }
}
