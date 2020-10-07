// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformMediaSource : NativePlatformBase, IMediaSource
    {
        private readonly MediaSource _mediaSource;

        public SourceState State => _mediaSource.InvokeState().ToNativePort();

        public PlatformMediaSource(MediaSource mediaSource) : base(mediaSource) => _mediaSource = mediaSource;

    }
}