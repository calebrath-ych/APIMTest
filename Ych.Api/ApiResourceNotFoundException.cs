using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ych.Api
{
    /// <summary>
    /// ApiException to be thrown from service methods. 
    /// </summary>
    public class ApiResourceNotFoundException : ApiException
    {
        /// <summary>
        /// Used to structure resource not found exceptions.
        /// </summary>
        private class ResourceNotFoundFailure
        {
            public string Message { get; set; }

            public ResourceNotFoundFailure(string message)
            {
                Message = message;
            }
        }

        public ApiResourceNotFoundException(params string[] messages) : 
            base("An expected resource was not found with the provided parameters.")
        {
            ErrorCode = ApiErrorCode.ResourceNotFound_0x7106;
            ResponseCode = ApiResponseCodes.ResourceNotFound;
            UserData = messages.Select(s => new ResourceNotFoundFailure(s));
        }
    }
}
