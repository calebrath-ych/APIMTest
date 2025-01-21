using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Ych.Api.Logging;
using Ych.Communication;
using Ych.Configuration;
using Ych.Logging;
using System.Runtime.CompilerServices;
using System.Net.Security;

namespace Ych.Api
{
    public interface IProxyRoutingService
    {
        Task<string> RoutePostRequest(string targetSystem, string destinationEndpoint, params (string key, object value)[] parameters);
        Task<string> RouteGetRequest(string targetSystem, string destinationEndpoint, params (string key, object value)[] parameters);
    }

    public class ProxyRoutingService : IProxyRoutingService
    {
        private ISettingsProvider settings;
        private ILogWriter log;

        public ProxyRoutingService(ISettingsProvider settings, ILogWriter log)
        {
            this.settings = settings;
            this.log = log;
        }

        public async Task<string> RoutePostRequest(string targetSystem, string destinationEndpoint, params (string key, object value)[] parameters)
        {
            RestClient restClient = CreateProxyClient(targetSystem, destinationEndpoint);
            RestRequest request = new RestRequest(destinationEndpoint, Method.Post);

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"RoutePostRequest adding request parameters."));

            foreach (var parameter in parameters)
            {
                if (parameter.key == RestApiClient.JsonBodyParameter)
                {
                    request.AddStringBody(parameter.value.ToString(), DataFormat.Json);
                }
                else if (parameter.key == RestApiClient.XmlBodyParameter)
                {
                    request.AddStringBody(parameter.value.ToString(), DataFormat.Xml);
                }
                else if (parameter.value is FileUpload)
                {
                    FileUpload file = parameter.value as FileUpload;
                    request.AddFile(parameter.key, file.FileBytes, file.FileName, file.ContentType);
                }
                else
                {
                    request.AddParameter(parameter.key, parameter.value?.ToString() ?? "");
                }
            }

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"RoutePostRequest sending request to {targetSystem}:{destinationEndpoint}."));

            RestResponse response = await restClient.ExecuteAsync(request).ConfigureAwait(false);

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"RoutePostRequest received response with status {response.StatusCode}."));

            return HandleProxyResponse(response, targetSystem, destinationEndpoint, parameters).Content;
        }

        public async Task<string> RouteGetRequest(string targetSystem, string destinationEndpoint, params (string key, object value)[] parameters)
        {
            RestClient restClient = CreateProxyClient(targetSystem, destinationEndpoint);
            RestRequest request = new RestRequest(destinationEndpoint, Method.Get);

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.key, parameter.value?.ToString());
            }

            RestResponse response = await restClient.ExecuteAsync(request).ConfigureAwait(false);

            return HandleProxyResponse(response, targetSystem, destinationEndpoint, parameters).Content;
        }

        private RestClient CreateProxyClient(string targetSystem, string destinationEndpoint, [CallerMemberName] string methodName = null)
        {
            IProxySystem system = ApiSettings.GetSystem<IProxySystem>(targetSystem);

            if (system == null)
            {
                throw new ApiException($"{targetSystem} does not support proxy routing.", ApiErrorCode.InternalError_0x7105, ApiResponseCodes.InternalError);
            }

            string targetUrl = settings[system.BaseUrl()];

            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug, $"{methodName} creating RestClient for {targetUrl}/{destinationEndpoint}."));

            RemoteCertificateValidationCallback remoteCertificateValidationCallback = null;

            if (settings.GetValue(Config.Settings.Api().IgnoreProxyCertificates(), false))
            {
                remoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };
            }

            RestClient restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri(targetUrl),
                RemoteCertificateValidationCallback = remoteCertificateValidationCallback
            });

            // Default to BasicAuth since most proxy systems use it so we don't need to configure them
            ProxyAuthenticationModes authMode = settings.GetValue(system.AuthMode(), ProxyAuthenticationModes.BasicAuth);

            if (authMode == ProxyAuthenticationModes.BasicAuth)
            {
                string authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(settings[system.AuthCredentials()]));

                if (string.IsNullOrEmpty(authToken))
                {
                    log.Error(nameof(ProxyRoutingService), $"System {system.Name} is configured for {nameof(ProxyAuthenticationModes)}.{authMode} but has no {system.AuthCredentials()} AppSetting and authentication may not function.");
                }

                restClient.AddDefaultHeader("Authorization", $"Basic {authToken}");
            }
            else if (authMode == ProxyAuthenticationModes.Apim)
            {
                string authToken = settings[system.AuthCredentials()];

                if (string.IsNullOrEmpty(authToken))
                {
                    log.Error(nameof(ProxyRoutingService), $"System {system.Name} is configured for {nameof(ProxyAuthenticationModes)}.{authMode} but has no {system.AuthCredentials()} AppSetting and authentication may not function.");
                }

                // This header can be configured in Azure, should it be configurable here or assume it will be the default?
                restClient.AddDefaultHeader("Ocp-Apim-Subscription-Key", authToken);
            }
            else if (authMode == ProxyAuthenticationModes.None)
            {
                // No authentication
            }
            else
            {
                log.Error(nameof(ProxyRoutingService), $"{nameof(ProxyAuthenticationModes)}.{authMode} is not implemented.");
            }

            return restClient;
        }

        private RestResponse HandleProxyResponse(RestResponse response, string targetSystem, string destinationEndpoint, params (string key, object value)[] parameters)
        {
            if (!response.IsSuccessful)
            {
                if ((int)response.StatusCode == 422)
                {
                    // Some sort of Error Handling here. 
                    var responseContent = JObject.Parse(response.Content);

                    List<(string, object, string)> failures = new List<(string, object, string)>();
                    foreach (JProperty error in responseContent["errors"])
                    {
                        var parameter = parameters.Where(x => x.key == error.Name);

                        failures.Add((error.Name, (parameter.Count() > 0 ? parameter.First().value : null),
                            string.Join(" ", error.Value.ToObject<string[]>())));
                    }
                    throw new ApiValidationException(failures.ToArray());
                }
                else
                {
                    throw new ApiException(
                        $"Received error code {(int)response.StatusCode} from {targetSystem} endpoint {destinationEndpoint}",
                        ApiErrorCode.InternalError_0x7105, ApiResponseCodes.InternalError)
                    {
                        UserData = response.Content
                    };
                }
            }

            return response;
        }
    }
}