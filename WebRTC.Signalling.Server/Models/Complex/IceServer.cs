using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WebRTC.Signalling.Server.Models.Complex
{
    public enum TlsCertPolicy : ulong
    {
        Secure,
        InsecureNoCheck
    }

    public class IceServer
    {
        public IceServer(string uri, string username = "", string password = "", TlsCertPolicy tlsCertPolicy = TlsCertPolicy.Secure) : this(new[] { uri }, username, password, tlsCertPolicy)
        {
        }

        public IceServer(string[] urls, string username, string password,
            TlsCertPolicy tlsCertPolicy = TlsCertPolicy.Secure)
        {
            Urls = urls;
            Username = username;
            Password = password;
            TlsCertPolicy = tlsCertPolicy;
        }

        [JsonProperty("urls")] public string[] Urls { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("tlsCertPolicy")] public TlsCertPolicy TlsCertPolicy { get; }

    }
}
