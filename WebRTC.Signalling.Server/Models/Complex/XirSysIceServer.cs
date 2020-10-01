using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WebRTC.Signalling.Server.Models.Complex
{
    public class XirSysIceServer
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("credential")]
        public string Credential { get; set; }

        [JsonProperty("maxRateKbps")]
        public string MaxRateKbps { get; set; }
    }

    public class FormatedIceServers
    {
        [JsonProperty("Urls")] public string[] Urls { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("tlsCertPolicy")] public TlsCertPolicy TlsCertPolicy { get; set; }
    }
}
