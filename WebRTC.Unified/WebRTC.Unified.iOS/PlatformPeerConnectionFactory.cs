// onotseike@hotmail.comPaula Aliu
using System;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformPeerConnectionFactory : NativePlatformBase, IPeerConnectionFactory
    {
        private readonly RTCPeerConnectionFactory _peerConnectionFactory;

        public PlatformPeerConnectionFactory()
        {
            var decoderFactory = new RTCDefaultVideoDecoderFactory();
            var encoderFactory = new RTCDefaultVideoEncoderFactory();

            NativeObject = _peerConnectionFactory = new RTCPeerConnectionFactory(encoderFactory, decoderFactory);
        }

        public IVideoSource VideoSource => new PlatformVideoSource(_peerConnectionFactory.VideoSource);

        public IAudioSource AudioSourceWithConstraints(MediaConstraints mediaConstraints)
        {
            var audioSource = _peerConnectionFactory.AudioSourceWithConstraints(mediaConstraints.ToPlatformNative());
            return audioSource == null ? null : new PlatformAudioSource(audioSource);
        }

        public IAudioTrack AudioTrackWithSource(IAudioSource audioSource, string trackId)
        {
            var audioTrack = _peerConnectionFactory.AudioTrackWithSource(audioSource.ToPlatformNative<RTCAudioSource>(), trackId);
            return audioSource == null ? null : new PlatformAudioTrack(audioTrack);
        }

        public IMediaStream MediaStreamWithStreamId(string streamId) => new PlatformMediaStream(_peerConnectionFactory.MediaStreamWithStreamId(streamId));

        public IPeerConnection PeerConnectionWithConfiguration(Core.RTCConfiguration configuration, MediaConstraints constraints, IPeerConnectionDelegate peerConnectionDelegate)
        {
            var _configuration = configuration.ToPlatformNative();
            var _constraints = new RTCMediaConstraints(null,
                new NSDictionary<NSString, NSString>(new NSString("DtlsSrtpKeyAgreement"),
                    new NSString(configuration.EnableDtlsSrtp ? "false" : "true")));
            var _peerConnection = _peerConnectionFactory.PeerConnectionWithConfiguration(_configuration, _constraints, new PlatformPeerConnectionDelegate(peerConnectionDelegate));

            return _peerConnection == null ? null : new PlatformPeerConnection(_peerConnection, configuration, this);
        }

        public bool StartAecDumpWithFilePath(string filePath, long maxSizeInBytes) => _peerConnectionFactory.StartAecDumpWithFilePath(filePath, maxSizeInBytes);

        public void StopAecDump() => _peerConnectionFactory.StopAecDump();

        public IVideoTrack VideoTrackWithSource(IVideoSource videoSource, string trackId)
        {
            var videoTrack = _peerConnectionFactory.VideoTrackWithSource(videoSource.ToPlatformNative<RTCVideoSource>(), trackId);
            return videoTrack == null ? null : new PlatformVideoTrack(videoTrack);
        }
    }
}
