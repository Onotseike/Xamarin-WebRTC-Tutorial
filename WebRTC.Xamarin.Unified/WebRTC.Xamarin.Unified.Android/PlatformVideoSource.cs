// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformVideoSource : NativePlatformBase, IVideoSource
    {
        private VideoSource _videoSource;

        public PlatformVideoSource(VideoSource videoSource) : base(videoSource) => _videoSource = videoSource;

        public SourceState State => _videoSource.InvokeState().ToNativePort();

        public void AdaptOutputFormatToWidth(int width, int height, int fps) => _videoSource.AdaptOutputFormat(width, height, fps);
    }
}