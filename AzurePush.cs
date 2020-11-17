using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace com.businesscentral
{
    public static class AzurePush
    {
        [FunctionName("AzurePush")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation(string.Format("start"));

            // Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
            var hubConfig = new ConnectorConfig(config);

            // Compose message
            var message = req.Query["message"].ToString();
            var user = req.Query["user"].ToString();

            log.LogInformation(string.Format("user: {0}", user));
            log.LogInformation(string.Format("message: {0}", message));

            if (string.IsNullOrEmpty(message))
                message = String.Format(hubConfig.DefaultMessage, DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            // If you want to implement tag ..
            var tag = req.Query["tag"].ToString();
            if (string.IsNullOrEmpty(tag))
            {
                if (string.IsNullOrEmpty(user))
                    tag = hubConfig.DefaultTag;
                else
                {
                    var bcConnector = new BusinessCentralConnector(hubConfig, log);
                    var result = await bcConnector.GetUser(user, "APP365TEUsers");
                    if (result != null && result.Value != null && result.Value.Count > 0)
                        tag = result.Value[0].UserCode;
                }
            }

            var hub =
                NotificationHubClient.CreateClientFromConnectionString(
                    hubConfig.ConnectionString,
                    hubConfig.NotificationHubName);

            if (hubConfig.SendApple)
            {
                log.LogInformation(string.Format("SendApple {0}", tag));
                // Create class for AzureNotification Hub (APPLE)
                var appleAps = new AppleBaseAps()
                {
                    InAppMessage = message,
                    Aps = new AppleAps()
                    { Badge = hubConfig.DefaultBadge, Sound = "default", Alert = message }
                };

                // Dispatch push message (APPLE)
                if (!String.IsNullOrEmpty(tag))
                    hub.SendAppleNativeNotificationAsync(JsonConvert.SerializeObject(appleAps), tag).Wait();
                else
                    hub.SendAppleNativeNotificationAsync(JsonConvert.SerializeObject(appleAps)).Wait();
            }

            if (hubConfig.SendAndroid)
            {
                log.LogInformation(string.Format("SendAndroid {0}", tag));
                // Create class for AzureNotification Hub (GOOGLE FIREBASE FCM)
                // Dispatch push message (GOOGLE FIREBASE FCM)
                var firebaseAps = new FirebaseBaseAps()
                {
                    data = new FirebaseAps() { Message = message }
                };

                if (!String.IsNullOrEmpty(tag))
                    hub.SendFcmNativeNotificationAsync(JsonConvert.SerializeObject(firebaseAps), tag).Wait();
                else
                    hub.SendFcmNativeNotificationAsync(JsonConvert.SerializeObject(firebaseAps)).Wait();

            }

            if (hubConfig.SendMail && !string.IsNullOrEmpty(user))
            {
                var composer = new MessageComposer(hubConfig, log);
                var messenger = new MessageConnector(hubConfig, log);

                // Message is composed
                var messageHtml = composer.DataBindEmail(message);
                log.LogInformation(String.Format("Send Email Message : {0}", messageHtml));

                // Message sent
                var messageEmail = messenger.SendMail(messageHtml, user);
                log.LogInformation(String.Format("Email Message result : {0}", messageEmail.Status));

            }

            return new StatusCodeResult(200);
        }
    }
}
