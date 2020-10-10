// onotseike@hotmail.comPaula Aliu
using System;
using System.Text;

using Newtonsoft.Json;

using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core
{
    public class IceServer
    {
        #region Properties

        [JsonProperty("urlStrings")]
        public string[] UrlStrings { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("credential")]
        public string Credential { get; set; }

        [JsonProperty("tlsCertPolicy")]
        public TlsCertPolicy TlsCertPolicy { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("tlsAlpnProtocols")]
        public string[] TlsAlpnProtocols { get; set; }

        [JsonProperty("tlsEllipticCurves")]
        public string[] TlsEllipticCurves { get; set; }

        #endregion

        #region Constructor(s)

        public IceServer()
        {

        }
        public IceServer(string uri, string username = "", string password = "", TlsCertPolicy tlsCertPolicy = TlsCertPolicy.Secure) : this(new[] { uri }, username, password, tlsCertPolicy)
        {
        }

        public IceServer(string[] urlStrings, string username = null, string credential = null)
        {
            UrlStrings = urlStrings;
            Username = username;
            Credential = credential;
        }

        public IceServer(string[] urlStrings, string username = null, string credential = null, TlsCertPolicy policy = TlsCertPolicy.Secure)
        {
            UrlStrings = urlStrings;
            Username = username;
            Credential = credential;
            TlsCertPolicy = policy;
        }

        public IceServer(string[] urlStrings, string username = null, string credential = null, TlsCertPolicy policy = TlsCertPolicy.Secure, string hostname = null)
        {
            UrlStrings = urlStrings;
            Username = username;
            Credential = credential;
            TlsCertPolicy = policy;
            Hostname = hostname;
        }

        public IceServer(string[] urlStrings, string[] tlsAlpnProtocols, string username = null, string credential = null, TlsCertPolicy policy = TlsCertPolicy.Secure, string hostname = null)
        {
            UrlStrings = urlStrings;
            TlsAlpnProtocols = tlsAlpnProtocols;
            Username = username;
            Credential = credential;
            TlsCertPolicy = policy;
            Hostname = hostname;

        }

        public IceServer(string[] urlStrings, string[] tlsAlpnProtocols, string username = null, string credential = null, TlsCertPolicy policy = TlsCertPolicy.Secure, string hostname = null, string[] tlsEllipticCurves = null)
        {
            UrlStrings = urlStrings;
            TlsAlpnProtocols = tlsAlpnProtocols;
            Username = username;
            Credential = credential;
            TlsCertPolicy = policy;
            Hostname = hostname;
            TlsEllipticCurves = tlsEllipticCurves;
        }
        #endregion

        #region Helper Method Overrides

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var url in UrlStrings)
            {
                sb.Append(url).Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append("] ");
            if (!string.IsNullOrEmpty(Username))
                sb.Append("[").Append(Username).Append(":").Append("] ");
            sb.Append("[").Append(TlsCertPolicy).Append("]");
            return sb.ToString();
        }

        #endregion
    }
}
