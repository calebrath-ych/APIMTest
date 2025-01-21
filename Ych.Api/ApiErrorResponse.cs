using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ych.Configuration;

namespace Ych.Api
{
    /// <summary>
    /// Payload returned for any failed API request. This class is automatically used by built in error handling in the ApiFunction
    /// base class and should not need to be created manually.
    /// </summary>
    public class ApiErrorResponse
    {
        [JsonProperty("code")]
        public string ErrorCode => errorCode.Code;
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
        [JsonIgnore]
        public int ResponseStatusCode => responseCode.StatusCode;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Exceptions { get; set; }

        private ApiErrorCode errorCode;
        private ApiResponseCodes responseCode;

        private ApiErrorResponse(ApiErrorCode errorCode, ApiResponseCodes responseCode, string message = null)
        {
            this.errorCode = errorCode;
            this.responseCode = responseCode;
            Message = message ?? errorCode.DefaultMessage;
        }

        /// <summary>
        /// Translates known exception types into appropriate responses. 
        /// Extend this method to add additional common exception handling for non-API exceptions, such as database exceptions.
        /// Unknown or unhandled exception types will result
        /// in a generic server error response. 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiErrorResponse FromException(Exception ex, string message = null)
        {
            // TODO: Inject settings from ApiFunction, would require all entry-point functions to pass this to base class
            ApiErrorResponse response = null;

            // TODO: SqlServer specific exceptions

            // RetryLimitExceededException is thrown by EF when an operation failed after the configured retry limit. The underlying failure
            // should be found in the InnerException, whether it was connection failure, query failure, etc.
            if (ex is RetryLimitExceededException)
            {
                if (ex.InnerException is MySqlException)
                {
                    response = FromMySqlException(ex.InnerException as MySqlException, message);
                }
                else if (ex.InnerException is SqlException)
                {
                    response = FromSqlServerException(ex.InnerException as SqlException, message);
                }
                else
                {
                    response = new ApiErrorResponse(ApiErrorCode.DatabaseConnection_0x7102, ApiResponseCodes.DatabaseQueryError, message);
                }
            }
            else if (ex is MySqlException)
            {
                response = FromMySqlException(ex.InnerException as MySqlException, message);
            }
            else if (ex is SqlException)
            {
                response = FromSqlServerException(ex as SqlException, message);
            }

            if (response == null)
            {
                // If exception is not a handled exception above, check for ApiException type or use default values
                ApiErrorCode errorCode = (ex as ApiException)?.ErrorCode ?? ApiErrorCode.InternalError_0x7105;
                ApiResponseCodes responseCode = (ex as ApiException)?.ResponseCode ?? ApiResponseCodes.InternalError;
                // Only use exception message if it comes from an ApiException (user facing)
                message = message ?? (ex as ApiException)?.Message;

                response = new ApiErrorResponse(errorCode, responseCode, message);
            }

            bool includeExceptionDetails = false;

#if DEBUG
            includeExceptionDetails = true;
#endif

            if (includeExceptionDetails || (EnvironmentVariableProvider.Instance.TryGetValue(Config.Settings.Api().IncludeExceptionDetails(), out includeExceptionDetails) && includeExceptionDetails))
            {
                response.Data = (ex as ApiException)?.UserData;

                // If configured, send full exception details in response to client
                response.Exceptions = new List<object>();
                Exception current = ex;
                int depth = 0;

                while (current != null && depth < 20)
                {
                    // Include all inner exceptions in the response
                    response.Exceptions.Add(new
                    {
                        current.Message,
                        current.StackTrace
                    });

                    current = current.InnerException;
                    depth++;
                }
            }

            return response;
        }

        private static ApiErrorResponse FromMySqlException(MySqlException ex, string message)
        {
            // MySql error codes located on MySqlErrorCode
            // MySqlErrorCode.UnableToConnectToHost

            if (ex == null)
            {
                return null;
            }
            else if (ex.Number >= 1040 && ex.Number <= 1046)
            {
                return new ApiErrorResponse(ApiErrorCode.DatabaseConnection_0x7102, ApiResponseCodes.DatabaseConnectionError, message);
            }
            else
            {
                return new ApiErrorResponse(ApiErrorCode.DatabaseQuery_0x7103, ApiResponseCodes.DatabaseQueryError, message);
            }
        }

        private static ApiErrorResponse FromSqlServerException(SqlException ex, string message)
        {
            //https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15
            // 53 == Network path not found

            int[] sqlServerConnectionErrors = new int[] { -2, -1, 2, 53, 4060 };

            if (sqlServerConnectionErrors.Contains(ex.Number))
            {
                return new ApiErrorResponse(ApiErrorCode.DatabaseConnection_0x7102, ApiResponseCodes.DatabaseConnectionError, message);
            }
            else
            {
                return new ApiErrorResponse(ApiErrorCode.DatabaseQuery_0x7103, ApiResponseCodes.DatabaseQueryError, message);
            }
        }
    }
}
