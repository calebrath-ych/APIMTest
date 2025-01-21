using System;
using System.Collections.Generic;
using System.Text;

namespace Ych.Api
{
    /// <summary>
    /// HTTP response status codes for different error conditions.
    /// Only use the static pre-defined response codes to ensure responses remain consistent.
    /// </summary>
    public class ApiResponseCodes
    {
        /// <summary>
        /// A user provided parameter was invalid, or a required parameter was not provided.
        /// </summary>
        public static readonly ApiResponseCodes InvalidParameter = new ApiResponseCodes(400);
        /// <summary>
        /// A specified resource was not found and is required for the operation to be successful.
        /// </summary>
        public static readonly ApiResponseCodes ResourceNotFound = new ApiResponseCodes(404);
        public static readonly ApiResponseCodes DatabaseConnectionError = new ApiResponseCodes(503);
        public static readonly ApiResponseCodes DatabaseQueryError = new ApiResponseCodes(500);
        public static readonly ApiResponseCodes InternalError = new ApiResponseCodes(500);

        public int StatusCode { get; set; }

        private ApiResponseCodes(int responseStatusCode)
        {
            StatusCode = responseStatusCode;
        }
    }
}
