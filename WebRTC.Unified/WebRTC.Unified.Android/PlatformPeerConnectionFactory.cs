// onotseike@hotmail.comPaula Aliu
using Android.Content;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{

    internal class PlatformPeerConnectionFactory : Core.NativePlatformBase, IPeerConnectionFactoryAndroid
    {
        private readonly Context _context;
        private readonly PeerConnectionFactory _peerConnectionfactory;

        public PlatformPeerConnectionFactory(Context context)
        {
            _context = context;
            EglBaseContext = EglBaseHelper.Create().EglBaseContext;
            _peerConnectionfactory = CreatePeerConnectionFactory(_context, EglBaseContext);
            NativeObject = _peerConnectionfactory;
        }

        #region Helper Methods

        private static PeerConnectionFactory CreatePeerConnectionFactory(Context context, IEglBaseContext eglBaseContext)
        {
            var adm = CreateJavaAudioDevice(context);

            var encoderFactory = new DefaultVideoEncoderFactory(eglBaseContext, true, true);
            var decoderFactory = new DefaultVideoDecoderFactory(eglBaseContext);
            var factory = PeerConnectionFactory.InvokeBuilder()
                .SetAudioDeviceModule(adm)
                .SetVideoEncoderFactory(encoderFactory)
                .SetVideoDecoderFactory(decoderFactory)
                .CreatePeerConnectionFactory();

            adm.Release();

            return factory;
        }

        #endregion

        public IEglBaseContext EglBaseContext { get; }

        public IVideoSource VideoSource => new PlatformVideoSource(_peerConnectionfactory.CreateVideoSource(true));
        public IVideoSource CreateVideoSource(bool isScreenCast) => new PlatformVideoSource(_peerConnectionfactory.CreateVideoSource(isScreenCast));

        public IAudioSource AudioSourceWithConstraints(Core.MediaConstraints mediaConstraints) => new PlatformAudioSource(_peerConnectionfactory.CreateAudioSource(mediaConstraints.ToPlatformNative()));

        public IAudioTrack AudioTrackWithSource(IAudioSource audioSource, string trackId) => new PlatformAudioTrack(_peerConnectionfactory.CreateAudioTrack(trackId, audioSource.ToPlatformNative<AudioSource>()));

        public IMediaStream MediaStreamWithStreamId(string streamId) => new PlatformMediaStream(_peerConnectionfactory.CreateLocalMediaStream(streamId));

        public IPeerConnection PeerConnectionWithConfiguration(Core.RTCConfiguration configuration, Core.MediaConstraints constraints, IPeerConnectionDelegate peerConnectionDelegate)
        {
            var _configuration = configuration.ToPlatformNative();
            var _constraints = constraints.ToPlatformNative();
            var _peerConnection = _peerConnectionfactory.CreatePeerConnection(_configuration, new PlatformPeerConnectionDelegate(peerConnectionDelegate));
            if (_peerConnection == null) return null;
            return new PlatformPeerConnection(_peerConnection, configuration, this);
        }

        public void SetOptions(IPeerConnectionFactoryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public bool StartAecDumpWithFilePath(string filePath, long maxSizeInBytes)
        {
            throw new System.NotImplementedException();
        }

        public void StopAecDump()
        {
            throw new System.NotImplementedException();
        }

        public IVideoTrack VideoTrackWithSource(IVideoSource videoSource, string trackId)
        {
            throw new System.NotImplementedException();
        }
    }

    internal interface IPeerConnectionFactoryAndroid : Core.Interfaces.IPeerConnectionFactory
    {
        IEglBaseContext EglBaseContext { get; }
    }
}