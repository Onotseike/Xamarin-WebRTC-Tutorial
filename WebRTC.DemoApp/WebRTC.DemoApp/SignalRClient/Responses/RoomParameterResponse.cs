// onotseike@hotmail.comPaula Aliu
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WebRTC.Unified.Core;
using WebRTC.Unified.Enums;

namespace WebRTC.DemoApp.SignalRClient.Responses
{
    public class RoomParameterResponse
    {
        [JsonProperty("errorMessages")] public IList<string> ErrorMessages { get; set; }

        [JsonProperty("warningMessages")] public IList<string> WarningMessages { get; set; }

        [JsonProperty("isLoopBack")] public bool IsLoopBack { get; set; }

        [JsonProperty("pcConstraints")] public PCConstraint PcConstraint { get; set; }

        [JsonProperty("offerOptions")] public JObject OfferOptions { get; set; }

        [JsonProperty("mediaConstraints")] public MediaConstraints MediaConstraints { get; set; }

        [JsonProperty("iceServerUrl")] public string IceServerUrl { get; set; }

        [JsonProperty("iceServerTransports")] public string IceServerTransports { get; set; }

        [JsonProperty("roomId")] public string RoomId { get; set; }

        [JsonProperty("clientId")] public string ClientId { get; set; }

        [JsonProperty("roomUrl")] public string RoomUrl { get; set; }

        [JsonProperty("isInitiator")] public bool IsInitiator { get; set; }

        [JsonProperty("IceServers")] public IceServer[] IceServers { get; set; }

        [JsonProperty("offerSdp")] public SessionDescription OfferSdp { get; set; }
        [JsonProperty("iceCandidates")] public IceCandidate[] IceCandidates { get; set; }
    }


    public class PCConstraint
    {
        [JsonProperty("optional")] public List<JObject> Optional { get; set; }
    }

    public class MediaConstraints
    {
        [JsonProperty("audio")] public bool Audio { get; set; }

        [JsonProperty("video")] public bool Video { get; set; }
    }

    public class FormatedIceServers
    {
        [JsonProperty("Urls")] public string[] Urls { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("tlsCertPolicy")] public TlsCertPolicy TlsCertPolicy { get; set; }
    }
}
