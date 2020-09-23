// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpTransceiver
    {
        private RtpTransceiver transceiver;

        public PlatformRtpTransceiver(RtpTransceiver transceiver)
        {
            this.transceiver = transceiver;
        }
    }
}