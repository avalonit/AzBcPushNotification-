    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    
namespace com.businesscentral
{

    public partial class App365TEUsers
    {
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("value")]
        public List<App365TEUser> Value { get; set; }
    }

    public partial class App365TEUser
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("UserCode")]
        public string UserCode { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("UserBadgeurl")]
        public string UserBadgeurl { get; set; }

        [JsonProperty("UserUsername")]
        public string UserUsername { get; set; }

        [JsonProperty("UserPassword")]
        public string UserPassword { get; set; }

        [JsonProperty("UserEnabled")]
        public bool UserEnabled { get; set; }

        [JsonProperty("UserDefaultPricekm")]
        public double UserDefaultPricekm { get; set; }

        [JsonProperty("UserMainOffice")]
        public string UserMainOffice { get; set; }
    }
}
