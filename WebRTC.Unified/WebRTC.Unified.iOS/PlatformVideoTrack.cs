// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformVideoTrack : NativePlatformBase, IVideoTrack
    {
        private RTCVideoTrack _videoTrack;

        public PlatformVideoTrack(RTCVideoTrack videoTrack) => _videoTrack = videoTrack;

        public string Kind => _videoTrack.Kind;

        public string TrackId => _videoTrack.TrackId;

        public bool IsEnabled { get => _videoTrack.IsEnabled; set => _videoTrack.IsEnabled = value; }

        public MediaStreamTrackState ReadyState => _videoTrack.ReadyState.ToNativePort();

        public void AddRenderer(IVideoRenderer videoRenderer) => _videoTrack.AddRenderer(videoRenderer.ToPlatformNative<IRTCVideoRenderer>());

        public void RemoveVideoRenderer(IVideoRenderer videoRenderer) => _videoTrack.RemoveRenderer(videoRenderer.ToPlatformNative<IRTCVideoRenderer>());
    }
}