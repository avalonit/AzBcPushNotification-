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
            // Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
            var hubConfig = new ConnectorConfig(config);

            // Compose message
            string message = req.Query["message"];
            if (String.IsNullOrEmpty(message))
                message = hubConfig.DefaultMessage;
            // If you want to implement tag ..
            string tag = req.Query["tag"];
            if (String.IsNullOrEmpty(tag))
                tag = hubConfig.DefaultTag;

            if (hubConfig.SendApple)
            {
                // Create class for AzureNotification Hub (APPLE)
                var appleAps = new AppleBaseAps()
                {
                    InAppMessage = message,
                    Aps = new AppleAps()
                    { Badge = hubConfig.DefaultBadge, Sound = "default", Alert = message }
                };

                // Dispatch push message (APPLE)
                NotificationHubClient hub =
                    NotificationHubClient.CreateClientFromConnectionString(
                        hubConfig.ConnectionString,
                        hubConfig.NotificationHubName);
                hub.SendAppleNativeNotificationAsync(JsonConvert.SerializeObject(appleAps), tag).Wait();
            }

            if (hubConfig.SendAndroid)
            {
                // Create class for AzureNotification Hub (GOOGLE FIREBASE FCM)
                var firebaseAps = new FirebaseBaseAps()
                {
                    data = new FirebaseAps()
                    { Message = message }
                };

                // Dispatch push message (GOOGLE FIREBASE FCM)
                hub.SendFcmNativeNotificationAsync(JsonConvert.SerializeObject(firebaseAps)).Wait();
            }

            return new StatusCodeResult(200);
        }
    }
}
