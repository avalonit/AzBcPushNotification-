using System;
using Microsoft.Extensions.Configuration;

namespace com.businesscentral
{

    public partial class ConnectorConfig
    {
        public ConnectorConfig(IConfigurationRoot config)
        {
            if (config != null)
            {
                ConnectionString = config["ConnectionString"];
                NotificationHubName = config["NotificationHubName"];
                DefaultMessage = config["DefaultMessage"];
                if (!String.IsNullOrEmpty(config["DefaultBadge"]))
                    DefaultBadge = Convert.ToInt32(config["DefaultBadge"]);

            }
            // If you are customizing here it means you
            //  should give a look on how use
            //  azure configuration file
            if (String.IsNullOrEmpty(ConnectionString))
                ConnectionString = "Endpoint=sb://{your_hub_namespace}.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey={your_hub_sharedkey}";
            if (String.IsNullOrEmpty(NotificationHubName))
                NotificationHubName = "{your_hub_name}";
            if (String.IsNullOrEmpty(DefaultMessage))
                DefaultMessage = "Hello world";
            if (DefaultBadge == 0)
                DefaultBadge = 42;
        }

        public String ConnectionString;
        public String NotificationHubName;
        public String DefaultMessage;
        public int DefaultBadge;
    }
}
