using Newtonsoft.Json;

namespace com.businesscentral
{

    public partial class FirebaseBaseAps
    {
        [JsonProperty("data")]
        public FirebaseAps data { get; set; }

    }

    public partial class FirebaseAps
    {
        [JsonProperty("message")]
        public string Message { get; set; }

    }
}
