using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Statistics;
using Ych.Api.Logging;
using Ych.Logging;
using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Ych.Configuration;
using Ych;
using Azure.Core;

namespace YchApiFunctions
{
    /// <summary>
    /// Base class for all API functions to allow consistent request/response/error handling.
    /// </summary>
    public abstract class ApiFunction
    {
        protected virtual string OperationName => GetType().Name;
        protected ILogWriter Log => log;
        protected string RequestId { get; private set; } = Guid.NewGuid().ToString();

        private string logSource => GetType().Name;
        private ILogWriter log;
        private IApiStatisticsService statistics;
        private HttpRequest request;

        protected ApiFunction(ILogWriter log, IApiStatisticsService statistics)
        {
            this.log = log;
            this.statistics = statistics;

            if (log is LogWriterBase)
            {
                (log as LogWriterBase).DefaultTraceId = RequestId;
            }
        }

        /// <summary>
        /// Wraps function execution with error handling and standardized response formatting.
        /// </summary>
        /// <param name="req">Function request.</param>
        /// <param name="action">Callback to implement function logic within.</param>
        /// <returns></returns>
        protected async Task<IActionResult> ProcessRequest(HttpRequest req, Func<Task<IActionResult>> action)
        {
            try
            {
                request = req;

                await LogRequest(req);
                
                statistics.IncrementRequestStatistics(OperationName, GetApiPath(req));

                return await action();
            }
            catch (ApiException validationEx)
            {
                statistics.IncrementValidationStatistics(OperationName, GetApiPath(req));

                // Expected failure, do not log as unexpected, but worth noting
                log.Write(new ApiLogEntry(GetType().Name, validationEx, LogSeverities.Notice));

                return FailureResponse(null, validationEx);
            }
            catch (Exception ex)
            {
                // All other unexpected errors here, log as appropriate 
                log.Write(new ApiLogEntry(GetType().Name, ex));

                statistics.IncrementErrorStatistics(OperationName, GetApiPath(req));

                return FailureResponse($"Unexpected error while processing request.", ex);
            }
            finally
            {
                (log as LogWriterBase)?.Flush();
            }
        }

        /// <summary>
        /// Responds with a standardized error response. This will be automatically handled by throwing an exception from within ProcessRequest.
        /// </summary>
        /// <param name="message">Custom consumer message for the response object.</param>
        /// <param name="ex">Exception object to base the error from. Use an ApiException derived error to customise response and error codes.</param>
        /// <returns></returns>
        protected IActionResult FailureResponse(string message = null, Exception ex = null)
        {
            ApiErrorResponse apiError = ApiErrorResponse.FromException(ex, message);

            JsonResult response = new JsonResult(apiError, JsonConvert.DefaultSettings())
            {
                StatusCode = apiError.ResponseStatusCode,
            };

            LogResponse(new
            {
                response.StatusCode,
                response.ContentType,
                Content = response.Value
            });

            return response;
        }

        /// <summary>
        /// Responds with a 200 response containing the JSON serialized payload provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult SuccessResponse<T>(T result, int statusCode = 200, JsonSerializerSettings serializerSettings = null)
        {
            JsonResult response = new JsonResult(new
            {
                message = "success",
                data = result
            }, serializerSettings ?? JsonConvert.DefaultSettings())
            {
                StatusCode = statusCode
            };

            LogResponse(new
            {
                response.StatusCode,
                response.ContentType,
                Content = response.Value
            });

            return response;
        }

        protected IActionResult RawResponse(string result, int statusCode = 200, ContentType? contentType = null)
        {
            ContentResult response = new ContentResult
            {
                Content = result,
                StatusCode = statusCode,
                ContentType = (contentType ?? ContentType.ApplicationJson).ToString()
            };

            LogResponse(new
            {
                response.StatusCode,
                response.ContentType,
                response.Content
            });

            return response;
        }

        /// <summary>
        /// Responds with a 200 response containing the GeoJSON serialized payload provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult GeojsonResponse<T>(T result, int statusCode = 200, JsonSerializerSettings serializerSettings = null)
        {
            JsonResult response = new JsonResult(new
            {
                type = "FeatureCollection",
                features = result
            }, serializerSettings ?? JsonConvert.DefaultSettings())
            {
                StatusCode = statusCode
            };

            LogResponse(new
            {
                response.StatusCode,
                response.ContentType,
                Content = response.Value
            });

            return response;
        }

        protected string GetRequiredString(HttpRequest req, string name)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new ApiValidationException(name, value, "This parameter is required.");
            }

            return value;
        }

        protected int GetRequiredInt(HttpRequest req, string name)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new ApiValidationException(name, value, "This parameter is required.");
            }

            if (!int.TryParse(value, out int result))
            {
                throw new ApiValidationException(name, value, "This parameter must be a valid int32.");
            }

            return result;
        }

        protected int? GetInt(HttpRequest req, string name)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out int result))
            {
                return null;
            }

            return result;
        }

        protected bool GetRequiredBool(HttpRequest req, string name)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new ApiValidationException(name, value, "This parameter is required.");
            }

            if (!bool.TryParse(value, out bool result))
            {
                throw new ApiValidationException(name, value, "This parameter must be a valid boolean.");
            }

            return result;
        }

        protected bool GetBool(HttpRequest req, string name, bool defaultValue)
        {
            return GetBool(req, name) ?? defaultValue;
        }

        protected bool? GetBool(HttpRequest req, string name)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out bool result))
            {
                return null;
            }

            return result;
        }

        protected T GetRequiredEnum<T>(HttpRequest req, string name) where T : struct
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new ApiValidationException(name, value, "This parameter is required.");
            }

            if (!Enum.TryParse<T>(value, out T result))
            {
                throw new ApiValidationException(name, value, $"This parameter must be a valid {typeof(T).Name}.");
            }

            return result;
        }

        protected T? GetEnum<T>(HttpRequest req, string name) where T : struct
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (!Enum.TryParse<T>(value, out T result))
            {
                // Should we translate invalid values to null, or throw here?
                return null;
                //throw new ApiValidationException(name, value, $"This parameter must be empty or a valid {typeof(T).Name}.");
            }

            return result;
        }

        protected T[] GetEnumArray<T>(HttpRequest req, string name) where T : struct
        {
            List<T> enums = new List<T>();

            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            if (!string.IsNullOrEmpty(value))
            {
                string[] tokens = value.Split(",", StringSplitOptions.RemoveEmptyEntries);

                foreach (string token in tokens)
                {
                    if (Enum.TryParse(token, out T result))
                    {
                        enums.Add(result);
                    }
                }
            }

            return enums.ToArray();
        }

        protected string GetString(HttpRequest req, string name, string defaultValue = null)
        {
            string value = req.HasFormContentType ? req.Form[name] : req.Query[name];

            return value ?? defaultValue;
        }

        protected string GetClientIpAddress(HttpRequest req)
        {
            StringValues xForwardedFor;
            req.Headers.TryGetValue("X-Forwarded-For", out xForwardedFor);
            string ipAddress = xForwardedFor.FirstOrDefault();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                // We are receiving IP's in a list, probably including Azure resources in-between the client and function app
                // Some of these IP's have a port specified, which may change between calls, so we need to only grab the first
                // IP without the port as the source IP address. example: 176.222.66.242:60830,176.222.66.242, 13.77.182.191:13184
                ipAddress = ipAddress.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                ipAddress = ipAddress.Split(':', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                ipAddress = ipAddress.Trim();
            }

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = req.HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            return ipAddress;
        }

        protected virtual string AdditionalInfo(HttpRequest req)
        {
            using (StreamReader reader = new StreamReader(req.Body))
            {
                return JsonConvert.SerializeObject(new
                {
                    req.Method,
                    req.Protocol,
                    req.Path,
                    req.Headers,
                    req.QueryString,
                    Body = reader.ReadToEnd()
                });
            }
        }

        protected virtual async Task<string> AdditionalInfoAsync(HttpRequest req)
        {
            using (StreamReader reader = new StreamReader(req.Body))
            {
                return JsonConvert.SerializeObject(new
                {
                    req.Method,
                    req.Protocol,
                    req.Path,
                    req.Headers,
                    req.QueryString,
                    Body = await reader.ReadToEndAsync()
                });
            }
        }

        /// <summary>
        /// Gets the full path to the current request, which when coming from APIM will be included in a header.
        /// For local testing, this will simply be the request path.
        /// </summary>
        private string GetApiPath(HttpRequest req)
        {
            if (req.Headers.ContainsKey("ApimPath"))
            {
                // Allow API's to manually override the path
                return req.Headers["ApimPath"].ToString();
            }
            else if (req.Headers.ContainsKey("X-Original-URL"))
            {
                // Default to X-Original-URL which should be the APIM request path
                return req.Headers["X-Original-URL"].ToString();
            }
            else
            {
                // If neither header exists this is not run through APIM and is probably local dev, return original request path.
                return req.Path;
            }
        }

        private Task LogRequest(HttpRequest req)
        {
            try
            {
                return log.ConditionalWriteAsync(LogSeverities.Debug, async () =>
                {
                    string url = req.GetDisplayUrl();
                    string query = req.QueryString.ToString();
                    string ipAddress = GetClientIpAddress(req);

                    List<(string, object)> additionalProps = new List<(string, object)>(new (string, object)[]
                    {
                        ("Url", url),
                        ("Route", req.Path.Value),
                        ("Method", req.Method),
                        ("Function", GetType().FullName),
                        ("IP", ipAddress)
                    });

                    if (!string.IsNullOrEmpty(query))
                    {
                        additionalProps.Add(("Query", query));
                    }

                    if (req.Method == HttpMethods.Post && EnvironmentVariableProvider.Instance.GetValue(Config.Settings.Api().LogRequestBody(), true))
                    {
                        try
                        {
                            int thresh = EnvironmentVariableProvider.Instance.GetValue(Config.Settings.Api().RequestBufferThreshold(), 0);
                            long limit = EnvironmentVariableProvider.Instance.GetValue(Config.Settings.Api().RequestBufferLimit(), 0);

                            if (thresh > 0 && limit > 0)
                            {
                                if (req.ContentLength > limit)
                                {
                                    log.Warning(logSource, $"Request body exceeds threshold of {limit} bytes, logging will be truncated to {limit} bytes.");
                                }
                                log.Warning(logSource, $"Request body exceeds threshold of {thresh} bytes, logging will be truncated to {limit} bytes.");
                                req.EnableBuffering(thresh, limit);
                            }
                            else if (limit > 0)
                            {
                                req.EnableBuffering(limit);
                            }
                            else if (thresh > 0)
                            {
                                req.EnableBuffering(thresh);
                            }
                            else
                            {
                                req.EnableBuffering();
                            }

                            // Don't dispose of this reader, it will close the stream and we still need it for the request
                            StreamReader reader = new StreamReader(req.Body);
                            string body = await reader.ReadToEndAsync();

                            if (!string.IsNullOrEmpty(body))
                            {
                                additionalProps.Add(("Body", body));
                            }

                            if (req.Body.CanSeek)
                            {
                                try
                                {
                                    req.Body.Position = 0;
                                }
                                catch (Exception ex)
                                {
                                    log.Error(logSource, ex, "Failed to reset body stream position.");
                                }
                            }

                            reader = null;
                        }
                        catch (Exception ex)
                        {
                            log.Error(logSource, ex, "Failed to log request body.");
                        }
                    }

                    if (EnvironmentVariableProvider.Instance.GetValue(Config.Settings.Api().LogRequestHeaders(), true))
                    {
                        additionalProps.Add(("Headers", new Dictionary<string, string>(
                            req.Headers.Select(s => new KeyValuePair<string, string>(s.Key, s.Value.ToString())))));
                    }

                    ApiLogEntry logEntry = new ApiLogEntry(logSource, LogSeverities.Debug,
                        $"Received {req.Method} {url} from {ipAddress}")
                    {
                        AdditionalProperties = additionalProps
                    };

                    return logEntry;
                });
            }
            catch (Exception ex)
            {
                try
                {
                    log.Error(logSource, ex, "Failed to log request.");
                }
                catch { } // Don't let logging failures take down API

                return Task.CompletedTask;
            }
        }

        private Task LogResponse(object response)
        {
            try
            {
                if (EnvironmentVariableProvider.Instance.GetValue(Config.Settings.Api().LogResponses(), true))
                {
                    return log.ConditionalWriteAsync(LogSeverities.Debug, () =>
                    {
                        string url = request.GetDisplayUrl();
                        string query = request.QueryString.ToString();
                        string ipAddress = GetClientIpAddress(request);

                        List<(string, object)> additionalProps = new List<(string, object)>(new (string, object)[]
                        {
                            ("Url", url),
                            ("Route", request.Path.Value),
                            ("Method", request.Method),
                            ("Function", GetType().FullName),
                            ("IP", ipAddress),
                            ("Response", response)
                        });

                        ApiLogEntry logEntry = new ApiLogEntry(logSource, LogSeverities.Debug,
                            $"Response to {request.Method} {url} from {ipAddress}")
                        {
                            AdditionalProperties = additionalProps
                        };

                        return Task.FromResult<ILogEntry>(logEntry);
                    });
                }
            }
            catch (Exception ex)
            {
                try
                {
                    log.Error(logSource, ex, "Failed to log response.");
                }
                catch { } // Don't let logging failures take down API
            }

            return Task.CompletedTask;
        }

        protected async Task<T> LogErrors<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                log.Write(new ApiLogEntry()
                {
                    Source = GetType().Name,
                    Severity = LogSeverities.Error,
                    Exception = ex,
                    Message = ex.Message,
                    AdditionalInfo = ex.StackTrace,
                });

                return default(T);
            }
        }
    }
}
