using Newtonsoft.Json;

namespace com.businesscentral
{

    public partial class AppleBaseAps
    {
        [JsonProperty("aps")]
        public AppleAps Aps { get; set; }

        [JsonProperty("inAppMessage")]
        public string InAppMessage { get; set; }
    }

    public partial class AppleAps
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("badge")]
        public long Badge { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }


    }
}
