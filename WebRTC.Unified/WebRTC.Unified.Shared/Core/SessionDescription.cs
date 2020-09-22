// onotseike@hotmail.comPaula Aliu
using System;

using Newtonsoft.Json;

using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core
{
    public class SessionDescription
    {
        public SessionDescription(SdpType type, string sdp)
        {
            Sdp = sdp;
            Type = type;
        }


        [JsonProperty("type")]
        public SdpType Type { get; }

        [JsonProperty("sdp")]
        public string Sdp { get; }

        public static SdpType GetSdpTypeFromString(string sdp)
        {
            switch (sdp)
            {
                case "offer":
                    return SdpType.Offer;
                case "answer":
                    return SdpType.Answer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sdp));
            }
        }
    }
}
