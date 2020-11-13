using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;



namespace com.businesscentral
{
    public class MessageConnector
    {
        private ConnectorConfig config;
        private ILogger log;
        public MessageConnector(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;
        }

        public async Task<Response> SendMail(string messageBody, string email)
        {
            var client = new SendGridClient(config.SendGridApiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(config.SendGridSender, "Prime 365 Travel Expenses"));

            var recipients = new List<EmailAddress>
                { new EmailAddress(email) };
            msg.AddTos(recipients);

            var ccn = new List<EmailAddress>
                { new EmailAddress(config.SendGridSender) };
            msg.AddBccs(ccn);

            msg.SetSubject("Prime 365 Travel Expenses");

            msg.AddContent(MimeType.Html, messageBody);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
