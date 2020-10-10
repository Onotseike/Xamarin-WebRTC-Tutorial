// onotseike@hotmail.comPaula Aliu
using System;

using Newtonsoft.Json;

namespace WebRTC.Unified.Core
{
    public class IceCandidate
    {
        [JsonProperty("sdpMid")]
        public string SdpMid { get; }

        [JsonProperty("sdpMLineIndex")]
        public int SdpMLineIndex { get; }

        [JsonProperty("sdp")]
        public string Sdp { get; }

        [JsonProperty("serverUrl")]
        public string ServerUrl { get; }

        public IceCandidate(string sdp, int sdpMLineIndex, string sdpMid)
        {
            Sdp = sdp;
            SdpMid = sdpMid;
            SdpMLineIndex = sdpMLineIndex;
        }

        public IceCandidate()
        {

        }
    }
}
