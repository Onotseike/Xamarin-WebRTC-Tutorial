
using Newtonsoft.Json;

namespace WebRTC.Signalling.Server.Models.Complex
{
    public class IceCandidate
    {
        [JsonProperty("sdp")]
        public string Sdp { get; }

        [JsonProperty("sdpMid")]
        public string SdpMid { get; }

        [JsonProperty("sdpMLineIndex")]
        public int SdpMLineIndex { get; }

        public IceCandidate(string sdp, string sdpMid, int sdpMLineIndex)
        {
            Sdp = sdp;
            SdpMid = sdpMid;
            SdpMLineIndex = sdpMLineIndex;
        }
    }
}
