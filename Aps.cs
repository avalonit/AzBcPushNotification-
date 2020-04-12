using Newtonsoft.Json;

namespace com.businesscentral
{

    public partial class AppleAps
    {
        [JsonProperty("aps")]
        public Aps Aps { get; set; }

        [JsonProperty("inAppMessage")]
        public string InAppMessage { get; set; }
    }

    public partial class Aps
    {
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("badge")]
        public long Badge { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }


    }
}
