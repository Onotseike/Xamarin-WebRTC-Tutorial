// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformMediaStreamTrack : NativePlatformBase, IMediaStreamTrack
    {

        private readonly RTCMediaStreamTrack _mediaStreamTrack;

        public PlatformMediaStreamTrack(RTCMediaStreamTrack mediaStreamTrack) : base(mediaStreamTrack) => _mediaStreamTrack = mediaStreamTrack;


        public string Kind => _mediaStreamTrack.Kind;

        public string TrackId => _mediaStreamTrack.TrackId;

        public bool IsEnabled { get => _mediaStreamTrack.IsEnabled; set => _mediaStreamTrack.IsEnabled = value; }

        public MediaStreamTrackState ReadyState => _mediaStreamTrack.ReadyState.ToNativePort();
    }
}