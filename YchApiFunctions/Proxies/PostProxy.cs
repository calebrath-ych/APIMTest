using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ych.Api;
using Ych.Api.Logging;
using Ych.Api.Statistics;
using Ych.Communication;
using Ych.Configuration;
using Ych.Logging;
using Ych;
using RestSharp;

namespace YchApiFunctions.Proxies
{
    public class PostProxy : ProxyFunction
    {
        public PostProxy(IProxyRoutingService service, ILogWriter log, IApiStatisticsService statistics) : base(service, log, statistics)
        {
        }

        [Function(nameof(PostProxy))]
        public virtual async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "proxy/post")] HttpRequest req)
        {
            return await ProcessRequest(req, async () =>
            {
                string system = GetSystemName(req);
                string destinationEndpoint = null;


                Log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"{GetType().Name} started collecting parameters."));

                List<(string key, object value)> forwardedParameters = new List<(string, object)>();

                if (req.ContentType.Equals(ContentType.Json.Value, StringComparison.InvariantCultureIgnoreCase) || req.ContentType.Equals(ContentType.Xml.Value, StringComparison.InvariantCulture))
                {
                    if (req.ContentType.Equals(ContentType.Json.Value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (StreamReader reader = new StreamReader(req.Body))
                        {
                            forwardedParameters.Add((RestApiClient.JsonBodyParameter, await reader.ReadToEndAsync()));
                        }
                    }
                    else if (req.ContentType.Equals(ContentType.Xml.Value, StringComparison.InvariantCulture))
                    {
                        using (StreamReader reader = new StreamReader(req.Body))
                        {
                            forwardedParameters.Add((RestApiClient.XmlBodyParameter, await reader.ReadToEndAsync()));
                        }
                    }

                    foreach (var parameter in req.Query)
                    {
                        if (parameter.Key.Equals(SystemParameter, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (!system.Equals(parameter.Value, StringComparison.InvariantCultureIgnoreCase))
                            {
                                Log.Error(GetType().Name, $"A {SystemParameter} parameter was provided that does not match the target system for this proxy and it will be ignored.");
                            }
                        }
                        else if (parameter.Key.Equals(EndpointParameter, StringComparison.InvariantCultureIgnoreCase))
                        {
                            destinationEndpoint = parameter.Value;
                        }
                        else
                        {
                            forwardedParameters.Add((parameter.Key, parameter.Value.ToString()));
                        }
                    }
                }
                else if (req.HasFormContentType)
                {
                    Log.Debug(GetType().Name, $"Reading request form data: {req.Path}");

                    var formData = await req.ReadFormAsync();

                    foreach (var parameter in formData)
                    {
                        try
                        {
                            if (parameter.Key.Equals(ContentType.Json.Value, StringComparison.InvariantCultureIgnoreCase) ||
                                parameter.Key.Equals(ContentType.Plain.Value, StringComparison.InvariantCultureIgnoreCase))
                            {
                                // If a POST contains a JSON body and form parameters, it will come across as multipart/form-data
                                // with the body keyed after it's content type.
                                forwardedParameters.Add((RestApiClient.JsonBodyParameter, parameter.Value));
                            }
                            else if (parameter.Key.Equals(SystemParameter, StringComparison.InvariantCultureIgnoreCase))
                            {
                                system = parameter.Value;
                            }
                            else if (parameter.Key.Equals(EndpointParameter, StringComparison.InvariantCultureIgnoreCase))
                            {
                                destinationEndpoint = parameter.Value;
                            }
                            else
                            {
                                forwardedParameters.Add((parameter.Key, parameter.Value.ToString()));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(GetType().Name, $"Error processing form parameter {parameter.Key}.", ex);
                        }
                    }

                    foreach (IFormFile file in req.Form.Files)
                    {
                        try
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);

                                forwardedParameters.Add((file.Name, new FileUpload(file.FileName, ms.ToArray(), file.ContentType)));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(GetType().Name, $"Error processing form file {file.Name}.", ex);
                        }
                    }
                }

                // TODO: Support query string on POST?

                string response = await ProxyService.RoutePostRequest(system, destinationEndpoint, forwardedParameters.ToArray());

                Log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"{GetType().Name} returning response."));

                return RawResponse(response);
            });
        }
    }
}