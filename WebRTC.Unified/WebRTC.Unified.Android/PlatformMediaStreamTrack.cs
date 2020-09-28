// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformMediaStreamTrack : NativePlatformBase, IMediaStreamTrack
    {
        private readonly MediaStreamTrack _mediaStreamTrack;
        public PlatformMediaStreamTrack(MediaStreamTrack mediaStreamTrack) : base(mediaStreamTrack)
        {
            _mediaStreamTrack = mediaStreamTrack;
        }

        public string Kind => _mediaStreamTrack.Kind();

        public string TrackId => _mediaStreamTrack.Id();

        public bool IsEnabled
        {
            get => _mediaStreamTrack.Enabled();
            set => _mediaStreamTrack.SetEnabled(value);
        }

        public MediaStreamTrackState ReadyState => _mediaStreamTrack.InvokeState().ToNativePort();

    }
}