using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace WebRTC.Signalling.Server.Models.Complex
{
    public class TURNClient
    {
        #region Properties & Variables

        private readonly Uri turnBaseUrl;
        private readonly string turnServerAuthorization;

        #endregion

        #region Constructor(s)

        public TURNClient(string _turnBaseUrl, string _authorization)
        {
            turnBaseUrl = new Uri(_turnBaseUrl);
            turnServerAuthorization = EncodingAuthString(_authorization);
        }

        #endregion

        #region Helper Function(s)

        public string EncodingAuthString(string toEncode)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
            string toReturn = Convert.ToBase64String(bytes);
            return toReturn;
        }

        #endregion

        #region Main Function(s)

        public async Task<IceServer[]> RequestServersAsync()
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = turnBaseUrl;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", turnServerAuthorization);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serverResponse = await _httpClient.PutAsync("", new StringContent("")).ConfigureAwait(false);
                serverResponse.EnsureSuccessStatusCode();

                var responseContent = await serverResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var responseObject = JObject.Parse(responseContent);

                responseObject.TryGetValue("v", out JToken iceServersJson);
                if (iceServersJson != null)
                {
                    var serversObject = iceServersJson["iceServers"];

                    if (serversObject != null)
                    {
                        var xirSysServers = serversObject.ToObject<XirSysIceServer[]>();

                        var groupedServers = xirSysServers.GroupBy(
                            server => server.Username,
                            server => (server.Credential, server.Url),
                            (username, cred_url) =>
                            new IceServer(urls: cred_url.Select(cu => cu.Url).ToArray(), username: username, password: cred_url.Select(cu => cu.Credential).Distinct().FirstOrDefault())
                        ).ToArray();

                        return groupedServers;
                    }
                    return null;
                }
                return null;
            }
        }

        #endregion
    }
}
