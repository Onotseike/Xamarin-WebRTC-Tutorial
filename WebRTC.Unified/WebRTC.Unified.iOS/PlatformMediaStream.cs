// onotseike@hotmail.comPaula Aliu
using System.Linq;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformMediaStream : NativePlatformBase, IMediaStream
    {
        private readonly RTCMediaStream _mediaStream;

        public PlatformMediaStream(RTCMediaStream mediaStream) : base(mediaStream) => _mediaStream = mediaStream;

        public IAudioTrack[] AudioTracks => _mediaStream.AudioTracks.Select(audioTrack => audioTrack.ToNativePort()).Cast<IAudioTrack>().ToArray();

        public IVideoTrack[] VideoTracks => _mediaStream.VideoTracks.Select(VideoTracks => VideoTracks.ToNativePort()).Cast<IVideoTrack>().ToArray();

        public string StreamId => _mediaStream.StreamId;

        public void AddAudioTrack(IAudioTrack audioTrack) => _mediaStream.AddAudioTrack(audioTrack.ToPlatformNative<RTCAudioTrack>());

        public void AddVideoTrack(IVideoTrack videoTrack) => _mediaStream.AddVideoTrack(videoTrack.ToPlatformNative<RTCVideoTrack>());

        public void RemoveAudioTrack(IAudioTrack audioTrack) => _mediaStream.RemoveAudioTrack(audioTrack.ToPlatformNative<RTCAudioTrack>());

        public void RemoveVideoTrack(IVideoTrack videoTrack) => _mediaStream.RemoveVideoTrack(videoTrack.ToPlatformNative<RTCVideoTrack>());
    }
}
