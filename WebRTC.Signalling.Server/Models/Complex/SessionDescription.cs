using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace WebRTC.Signalling.Server.Models.Complex
{
    public enum SdpType : long
    {
        [EnumMember(Value = "offer")]
        Offer,
        Answer,
        [EnumMember(Value = "answer")]
        PrAnswer
    }

    public class SessionDescription
    {
        [JsonProperty("type")]
        public SdpType Type { get; }

        [JsonProperty("sdp")]
        public string Sdp { get; }

        public SessionDescription(SdpType type, string sdp)
        {
            Sdp = sdp;
            Type = type;
        }
    }

}
