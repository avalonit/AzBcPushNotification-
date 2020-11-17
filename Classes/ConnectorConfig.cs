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
                SendGridApiKey = config["SendGridApiKey"];
                SendGridSender = config["SendGridSender"];

                BCTenant = config["BCTenant"];
                BCCompanyID = config["BCCompanyID"];
                BCApiVersion = config["BCApiVersion"];
                BCApiPrefix = config["BCApiPrefix"];
                BCAuthInfo = config["BCAuthInfo"];

                if (!string.IsNullOrEmpty(config["DefaultBadge"]))
                    DefaultBadge = Convert.ToInt32(config["DefaultBadge"]);
                if (!string.IsNullOrEmpty(config["DefaultTag"]))
                    DefaultTag = config["DefaultTag"];

                if (!string.IsNullOrEmpty(config["SendApple"]))
                    SendApple = Convert.ToBoolean(config["SendApple"]);
                else
                    SendApple = true;

                if (!string.IsNullOrEmpty(config["SendAndroid"]))
                    SendAndroid = Convert.ToBoolean(config["SendAndroid"]);
                else
                    SendAndroid = true;

                if (!string.IsNullOrEmpty(config["SendMail"]))
                    SendMail = Convert.ToBoolean(config["SendMail"]);
                else
                    SendMail = true;


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
            if (String.IsNullOrEmpty(DefaultTag))
                DefaultTag = "044";
        }

        public string ConnectionString;
        public string NotificationHubName;
        public string DefaultMessage;
        public int DefaultBadge;
        public string DefaultTag;
        public bool SendApple;
        public bool SendAndroid;
        public bool SendMail;

        public string SendGridApiKey;
        public string SendGridSender;


        public string BCTenant;
        public string BCCompanyID;
        public string BCApiVersion;
        public string BCApiPrefix;
        public string BCAuthInfo;

    }
}
