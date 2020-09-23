// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpReceiver : IRtpReceiver
    {
        private RtpReceiver rtpReceiver;

        public PlatformRtpReceiver(RtpReceiver rtpReceiver)
        {
            this.rtpReceiver = rtpReceiver;
        }
    }
}