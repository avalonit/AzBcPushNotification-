
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace com.businesscentral
{

    public partial class MessageComposer
    {
        private ConnectorConfig config;
        private ILogger log;

        public MessageComposer(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;
        }
        

        public string DataBindEmail(string textMessage)
        {
            var message = new StringBuilder();

            message.Append(string.Format("Hi, "));
            message.Append(string.Format("<br>"));
            message.Append(string.Format("<br>"));
            message.Append(string.Format(textMessage));
            message.Append(string.Format("<br>"));
            message.Append(string.Format("<br>"));
            message.Append(string.Format("<a href='https://play.google.com/store/apps/details?id=it.varprime.travelexpense'>Download Google</a>"));
            message.Append(string.Format("<br>"));
            message.Append(string.Format("<a href='https://apps.apple.com/us/app/prime365-travel-expenses/id1475646873'>Download Apple</a>"));

            return message.ToString();
        }

    }
}
