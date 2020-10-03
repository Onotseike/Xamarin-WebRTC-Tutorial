// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    internal class PlatformVideoSource : PlatformMediaSource, IVideoSource
    {
        private RTCVideoSource _videoSource;

        public PlatformVideoSource(RTCVideoSource videoSource) : base(videoSource) => _videoSource = videoSource;

        public void AdaptOutputFormatToWidth(int width, int height, int fps) => _videoSource.AdaptOutputFormatToWidth(width, height, fps);
    }
}