// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformVideoSource : IVideoSource
    {
        private VideoSource videoSource;

        public PlatformVideoSource(VideoSource videoSource)
        {
            this.videoSource = videoSource;
        }
    }
}