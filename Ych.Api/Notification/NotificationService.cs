using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Data.GrowerPortal;
using Ych.Api.Logging;
using Ych.Configuration;
using Ych.Data;
using Ych.Logging;
using Ych.Api.Data.Notification.Models;
using Newtonsoft.Json.Linq;
using Ych.Api.GrowerPortal;

namespace Ych.Api.Notification
{
    /// <summary>
    /// Contract for GrowerPortal Service. 
    /// </summary>
    public interface INotificationService
    {
        Task<string> PostGrowerPortalNotification(string notificationTypeId, string growerId, string recipientEmail,
            string sentDate, object data);

        Task<string> PostYcrmNotification(string notificationTypeId, int? customerId, string recipientEmail,
            string sentDate, string triggeredBy, object data);
    }

    public class NotificationService : INotificationService
    {
        private ISettingsProvider settings;
        private ILogWriter log;
        private IProxyRoutingService proxyRoutingService;

        private const string GrowerPortalSystemName = "GrowerPortal";
        private const string YcrmSystemName = "Ycrm";

        public NotificationService(ISettingsProvider settings, ILogWriter log,
            IProxyRoutingService proxyRoutingService, IGrowerPortalService growerPortalService)
        {
            this.settings = settings;
            this.log = log;
            this.proxyRoutingService = proxyRoutingService;
        }

        public async Task<string> PostYcrmNotification(string notificationTypeId, int? customerId,
            string recipientEmail, string sentDate, string triggeredBy, object data)
        {
            string postNotificationResponse = null;
            
            List<(string key, object value)> parameters = new List<(string key, object value)>
            {
                ("notification_type_id", notificationTypeId),
                ("customer_id", customerId),
                ("sent_date", sentDate),
            };

            if (recipientEmail != null)
            {
                parameters.Add(("recipient_email", recipientEmail));
            }

            if (sentDate != null)
            {
                parameters.Add(("sent_date", sentDate));
            }

            if (triggeredBy != null)
            {
                parameters.Add(("triggered_by", triggeredBy));
            }

            var context = JObject.Parse(data.ToString()).ToObject<Dictionary<string, string>>();

            foreach (var parameter in context)
            {
                parameters.Add((parameter.Key, parameter.Value));
            }

            // Post proxy to custom app
            string response =
                await proxyRoutingService.RoutePostRequest(YcrmSystemName, "notification",
                    parameters.ToArray());

            JObject jsonResponse = JObject.Parse(response);
            bool success = jsonResponse["success"] != null && jsonResponse["success"].ToObject<bool>();

            if (success && jsonResponse["data"] != null)
            {
                JToken notificationObject = jsonResponse["data"].First();

                Data.Notification.Models.Notification notification =
                    JsonConvert.DeserializeObject<Data.Notification.Models.Notification>(notificationObject.ToString());
                
                postNotificationResponse = await PostNotification(YcrmSystemName, notification, data);
            }
            else
            {
                throw new ApiException($"Notification creation failed with code {jsonResponse["code"]} ",
                    ApiErrorCode.InternalError_0x7105,
                    ApiResponseCodes.InternalError);
            }

            return postNotificationResponse;
        }

        public async Task<string> PostGrowerPortalNotification(string notificationTypeId, string growerId,
            string recipientEmail, string sentDate, object data)
        {
            List<(string key, object value)> parameters = new List<(string key, object value)>
            {
                ("notification_type_id", notificationTypeId),
                ("grower_id", growerId),
                ("sent_date", sentDate),
            };

            if (recipientEmail != null)
            {
                parameters.Add(("recipient_email", recipientEmail));
            }

            if (sentDate != null)
            {
                parameters.Add(("sent_date", sentDate));
            }

            var context = JObject.Parse(data.ToString()).ToObject<Dictionary<string, string>>();

            foreach (var parameter in context)
            {
                parameters.Add((parameter.Key, parameter.Value));
            }

            // Post proxy to custom app
            string response =
                await proxyRoutingService.RoutePostRequest(GrowerPortalSystemName, "notification",
                    parameters.ToArray());

            Data.Notification.Models.Notification notification =
                JsonConvert.DeserializeObject<Data.Notification.Models.Notification>(response);

            return await PostNotification(GrowerPortalSystemName, notification, data);
        }

        private async Task<string> PostNotification(string systemName, Data.Notification.Models.Notification notification,
            object data)
        {
            IOneSignalSystem system = ApiSettings.GetSystem<IOneSignalSystem>(systemName);

            if (system == null)
            {
                throw new ApiException($"{systemName} does not support OneSignal notifications.", ApiErrorCode.InternalError_0x7105, ApiResponseCodes.InternalError);
            }

            var baseUrl = settings[Config.Settings.Api().OneSignal().BaseUrl()];
            var oneSignalAuthCredentials = settings[system.OneSignalAuthCredentials()];
            RestClient restClient = new RestClient(baseUrl);
            restClient.AddDefaultHeader("Authorization", $"Basic {oneSignalAuthCredentials}");
            RestRequest request = new RestRequest((string)null, Method.Post);

            List<Recipient> usersPushList = notification.Recipients.Where(x => x.Push).ToList();

            string notificationResponse = "";
            if (usersPushList.Any())
            {
                string dataJson = "";

                if (data != null)
                {
                    dataJson = $@",""data"": {data}";
                }

                string json = $@"{{
                ""app_id"": ""{settings[system.OneSignalAppId()]}"",
                ""headings"": {{ ""en"": ""{notification.Subject}"" }},
                ""contents"": {{ ""en"": ""{notification.Message}"" }},
                ""channel_for_external_user_ids"": ""push"",
                ""include_external_user_ids"": {JsonConvert.SerializeObject(usersPushList.Select(x => x.ExternalId).ToList())}
                {dataJson}
            }}";

                request.AddStringBody(json, DataFormat.Json);

                RestResponse response = await restClient.ExecuteAsync(request).ConfigureAwait(false);
                notificationResponse = response.Content;
            }

            return notificationResponse;
        }
    }
}