using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ych.Api
{
    /// <summary>
    /// ApiException to be thrown from request validation code. Allows specifying details about 
    /// the validation failure back in the error response.
    /// </summary>
    public class ApiValidationException : ApiException
    {
        /// <summary>
        /// Used to structure and serialize validation failures as user data in the error response.
        /// </summary>
        private class ValidationFailure
        {
            public string Parameter { get; set; }
            public object Value { get; set; }
            public string Message { get; set; }

            public ValidationFailure(string parameter, object value, string message)
            {
                Parameter = parameter;
                Value = value;
                Message = message;
            }
        }

        public ApiValidationException(string parameter, object value, string message) : this((parameter, value, message))
        {
        }

        public ApiValidationException(params (string parameter, object value, string message)[] failedParameters) :
            base("One or more parameters are invalid, see data for additional information.")
        {
            ErrorCode = ApiErrorCode.InvalidParameter_0x7100;
            ResponseCode = ApiResponseCodes.InvalidParameter;
            UserData = failedParameters.Select(s => new ValidationFailure(s.parameter, s.value, s.message));
        }
    }
}
