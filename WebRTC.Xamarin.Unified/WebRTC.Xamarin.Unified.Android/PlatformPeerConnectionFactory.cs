// onotseike@hotmail.comPaula Aliu
using System;

using Android.Content;
using Android.OS;
using Android.Util;

using Java.IO;

using Org.Webrtc;
using Org.Webrtc.Audio;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

using ICameraEnumerator = Org.Webrtc.ICameraEnumerator;

namespace WebRTC.Unified.Android
{

    internal class PlatformPeerConnectionFactory : Core.NativePlatformBase, IPeerConnectionFactoryAndroid
    {
        private const string TAG = nameof(PlatformPeerConnectionFactory);

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
            var audioDeviceModule = CreateAudioDeviceModule(context);

            var encoderFactory = new DefaultVideoEncoderFactory(eglBaseContext, true, true);
            var decoderFactory = new DefaultVideoDecoderFactory(eglBaseContext);
            var factory = PeerConnectionFactory.InvokeBuilder()
                .SetAudioDeviceModule(audioDeviceModule)
                .SetVideoEncoderFactory(encoderFactory)
                .SetVideoDecoderFactory(decoderFactory)
                .CreatePeerConnectionFactory();

            audioDeviceModule.Release();

            return factory;
        }

        private static IAudioDeviceModule CreateAudioDeviceModule(Context context)
        {
            var audioErrorCallbacks = new AudioErrorCallbacks();
            return JavaAudioDeviceModule.InvokeBuilder(context)
                .SetAudioRecordErrorCallback(audioErrorCallbacks)
                .SetAudioRecordStateCallback(audioErrorCallbacks)
                .SetAudioTrackErrorCallback(audioErrorCallbacks)
                .SetAudioTrackStateCallback(audioErrorCallbacks)
                .CreateAudioDeviceModule();
        }

        private Core.Interfaces.ICameraVideoCapturer CreateCameraVideoCapturer(VideoSource videoSource, bool frontCamera)
        {
            Org.Webrtc.ICameraVideoCapturer videoCapturer;

            videoCapturer = UseCamera2()
                ? CreateCameraCapturer(new Camera2Enumerator(_context), frontCamera)
                : CreateCameraCapturer(new Camera1Enumerator(false), frontCamera);

            if (videoCapturer == null)
                return null;


            return new PlatformCameraVideoCapturer(_context, videoCapturer, videoSource, EglBaseContext);
        }

        private Org.Webrtc.ICameraVideoCapturer CreateCameraCapturer(ICameraEnumerator cameraEnumerator,
            bool frontCamera)
        {
            var devicesNames = cameraEnumerator.GetDeviceNames();
            foreach (var devicesName in devicesNames)
            {
                if (cameraEnumerator.IsFrontFacing(devicesName) && frontCamera)
                {
                    var videoCapturer = cameraEnumerator.CreateCapturer(devicesName, null);
                    if (videoCapturer != null)
                        return videoCapturer;
                }
            }

            foreach (var devicesName in devicesNames)
            {
                var videoCapturer = cameraEnumerator.CreateCapturer(devicesName, null);
                if (videoCapturer != null)
                    return videoCapturer;
            }

            return null;
        }

        private bool UseCamera2() => Camera2Enumerator.IsSupported(_context);


        #endregion

        #region IPeerConnectionFactoryAndroid

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


        public bool StartAecDumpWithFilePath(string filePath, long maxSizeInBytes)
        {
            try
            {
                ParcelFileDescriptor aecDumpFileDescriptor = ParcelFileDescriptor.Open(new File(filePath),
                    ParcelFileMode.Create | ParcelFileMode.Truncate | ParcelFileMode.ReadWrite);

                return _peerConnectionfactory.StartAecDump(aecDumpFileDescriptor.DetachFd(), (int)maxSizeInBytes);
            }
            catch (Exception exception)
            {
                Log.Error(TAG, $"An Exception has occured :  {exception.Message}");
                return false;
            }
        }

        public void StopAecDump() => _peerConnectionfactory.StopAecDump();

        public IVideoTrack VideoTrackWithSource(IVideoSource videoSource, string trackId)
        {
            var videoTrack = _peerConnectionfactory.CreateVideoTrack(trackId, videoSource.ToPlatformNative<VideoSource>());
            if (videoTrack == null)
                return null;
            return new PlatformVideoTrack(videoTrack);
        }

        #endregion

        #region Additional Methods

        public Core.Interfaces.ICameraVideoCapturer CreateCameraCapturer(IVideoSource videoSource, bool frontCamera) =>
           CreateCameraVideoCapturer(videoSource.ToPlatformNative<VideoSource>(), frontCamera);

        public IFileVideoCapturer CreateFileCapturer(IVideoSource videoSource, string file)
        {
            var fileVideoCapturer = new FileVideoCapturer(file);
            return new PlatformFileVideoCapturer(_context, fileVideoCapturer, videoSource.ToPlatformNative<VideoSource>(),
                EglBaseContext);
        }

        #endregion


        private class AudioErrorCallbacks : Java.Lang.Object,
            JavaAudioDeviceModule.IAudioRecordErrorCallback,
            JavaAudioDeviceModule.IAudioRecordStateCallback,
            JavaAudioDeviceModule.IAudioTrackErrorCallback,
            JavaAudioDeviceModule.IAudioTrackStateCallback

        {
            public void OnWebRtcAudioRecordError(string errorCode)
            {
                Log.Error(TAG, $"OnWebRtcAudioRecordError: {errorCode}");
            }

            public void OnWebRtcAudioRecordInitError(string errorCode)
            {
                Log.Error(TAG, $"OnWebRtcAudioRecordInitError: {errorCode}");
            }

            public void OnWebRtcAudioRecordStartError(JavaAudioDeviceModule.AudioRecordStartErrorCode errorCode, string errorMessage)
            {
                Log.Error(TAG, $"OnWebRtcAudioRecordStartError: errorCode {errorCode} {errorMessage}");
            }

            public void OnWebRtcAudioTrackError(string errorCode)
            {
                Log.Error(TAG, $"OnWebRtcAudioTrackError: errorCode {errorCode}");
            }

            public void OnWebRtcAudioTrackInitError(string errorCode)
            {
                Log.Error(TAG, $"OnWebRtcAudioTrackInitError: errorCode {errorCode}");
            }

            public void OnWebRtcAudioTrackStartError(JavaAudioDeviceModule.AudioTrackStartErrorCode errorCode, string errorMessage)
            {
                Log.Error(TAG, $"OnWebRtcAudioTrackStartError: errorCode {errorCode} {errorMessage}");
            }

            public void OnWebRtcAudioRecordStart()
            {
                Log.Info(TAG, "Audio recording starts");
            }

            public void OnWebRtcAudioRecordStop()
            {
                Log.Info(TAG, "Audio recording stops");
            }

            public void OnWebRtcAudioTrackStart()
            {
                Log.Info(TAG, "Audio playout starts");
            }

            public void OnWebRtcAudioTrackStop()
            {
                Log.Info(TAG, "Audio playout stops");
            }
        }

    }

    public interface IPeerConnectionFactoryAndroid : IPeerConnectionFactory
    {
        IEglBaseContext EglBaseContext { get; }
    }
}