// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Core
{
    public class PeerConnectionFactory : IPeerConnectionFactory
    {
        #region Properties & Variables

        private readonly IPeerConnectionFactory _peerConnectionFactory;

        #endregion

        #region Constructor(s)
        public PeerConnectionFactory()
        {
            _peerConnectionFactory = NativeFactory.CreatePeerConnectionFactory();
        }

        public PeerConnectionFactory(IVideoEncoderFactory encoderFactory = null, IVideoDecoderFactory decoderFactory = null)
        {
            _peerConnectionFactory = NativeFactory.CreatePeerConnectionFactory();
        }
        #endregion

        #region Additional Method(s)

        public static void StopInternalTracingCapture() => NativeFactory.StopInternalTracingCapture();
        public static void ShutdownInternalTracer() => NativeFactory.ShutDownInternalTracer();

        #endregion

        #region Interface Implementation 

        public IVideoSource VideoSource => _peerConnectionFactory.VideoSource;

        public object NativeObject => _peerConnectionFactory;

        public IAudioSource AudioSourceWithConstraints(MediaConstraints mediaConstraints) => _peerConnectionFactory.AudioSourceWithConstraints(mediaConstraints);

        public IAudioTrack AudioTrackWithSource(IAudioSource audioSource, string trackId) => _peerConnectionFactory.AudioTrackWithSource(audioSource, trackId);

        //public IAudioTrack AudioTrackWithTrackId(string trackId) => _peerConnectionFactory.AudioTrackWithTrackId(trackId);

        public void Dispose() => _peerConnectionFactory.Dispose();

        public IMediaStream MediaStreamWithStreamId(string streamId) => _peerConnectionFactory.MediaStreamWithStreamId(streamId);

        public IPeerConnection PeerConnectionWithConfiguration(RTCConfiguration configuration, MediaConstraints constraints, IPeerConnectionDelegate peerConnectionDelegate) => _peerConnectionFactory.PeerConnectionWithConfiguration(configuration, constraints, peerConnectionDelegate);

        // public void SetOptions(IPeerConnectionFactoryOptions options) => _peerConnectionFactory.SetOptions(options);

        public bool StartAecDumpWithFilePath(string filePath, long maxSizeInBytes) => _peerConnectionFactory.StartAecDumpWithFilePath(filePath, maxSizeInBytes);

        public void StopAecDump() => _peerConnectionFactory.StopAecDump();

        public IVideoTrack VideoTrackWithSource(IVideoSource videoSource, string trackId) => _peerConnectionFactory.VideoTrackWithSource(videoSource, trackId);

        public ICameraVideoCapturer CreateCameraCapturer(IVideoSource videoSource, bool frontCamera) => _peerConnectionFactory.CreateCameraCapturer(videoSource, frontCamera);

        public IFileVideoCapturer CreateFileCapturer(IVideoSource videoSource, string file) => _peerConnectionFactory.CreateFileCapturer(videoSource, file);
        #endregion
    }
}
