using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace com.businesscentral
{
    public class BusinessCentralConnector
    {
        private ConnectorConfig config;
        private ILogger log;
        private string ApiEndPoint = string.Empty;
        private string AuthInfo = string.Empty;
        public BusinessCentralConnector(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;

            this.ApiEndPoint = string.Format("https://api.businesscentral.dynamics.com/{0}/{1}/api/{2}/{3}/companies({4})/",
                                    config.BCApiVersion, config.BCTenant, config.BCApiPrefix, config.BCApiVersion, config.BCCompanyID);

            this.AuthInfo = Convert.ToBase64String(Encoding.Default.GetBytes(config.BCAuthInfo));
        }
        public async Task<App365TEUsers> GetUser(string email, string entity)
        {
            App365TEUsers user = null;

            if (string.IsNullOrWhiteSpace(email))
                return null;

            var apiEndPoint = string.Format("{0}/{1}?$filter=tolower(UserUsername) eq tolower('{2}')", ApiEndPoint, entity, email);
            log.LogInformation("GetUser HTTP " + apiEndPoint);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.AuthInfo);
                var responseMessage = await httpClient.GetAsync(apiEndPoint);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = await responseMessage.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<App365TEUsers>(json);
                }
                else
                    log.LogError("GetUser HTTP error: " + responseMessage.StatusCode.ToString());
            }
            return user;
        }

    }

}
