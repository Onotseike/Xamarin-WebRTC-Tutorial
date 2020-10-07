// onotseike@hotmail.comPaula Aliu
using System.Linq;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformMediaSource : NativePlatformBase, IMediaSource
    {
        private readonly RTCMediaSource _mediaSource;
        public PlatformMediaSource(RTCMediaSource mediaSource) : base(mediaSource) => _mediaSource = mediaSource;

        public SourceState State => _mediaSource.State.ToNativePort();
    }
}