// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;
using System.IO;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.DemoApp.SignalRClient.Abstractions
{
    #region IPeerConnectionEvents Interface

    public interface IPeerConnectionEvents
    {
        void OnPeerFactoryCreated(IPeerConnectionFactory factory);

        void OnPeerConnectionCreated(IPeerConnection peerConnection);

        /// <summary>
        /// Callback fired once DTLS connection is established (PeerConnectionState is CONNECTED).
        /// </summary>
        void OnConnected();

        /// <summary>
        ///  Callback fired once DTLS connection is disconnected (PeerConnectionState is DISCONNECTED).
        /// </summary>
        void OnDisconnected();

        /// <summary>
        /// Callback fired once local SDP is created and set.
        /// </summary>
        /// <param name="sdp"></param>
        void OnLocalDescription(SessionDescription sdp);

        /// <summary>
        /// Callback fired once local Ice candidate is generated.
        /// </summary>
        /// <param name="candidate"></param>
        void OnIceCandidate(IceCandidate candidate);

        /// <summary>
        /// Callback fired once local ICE candidates are removed.
        /// </summary>
        /// <param name="candidates"></param>
        void OnIceCandidateRemoved(IceCandidate[] candidates);

        /// <summary>
        /// Callback fired once connection is established (IceConnectionState is CONNECTED).
        /// </summary>
        void OnIceConnected();

        /// <summary>
        /// Callback fired once connection is disconnected (IceConnectionState is DISCONNECTED).
        /// </summary>
        void OnIceDisconnected();

        /// <summary>
        /// Callback fired once peer connection is closed.
        /// </summary>
        void OnPeerConnectionClosed();

        /// <summary>
        /// Callback fired once peer connection error happened.
        /// </summary>
        /// <param name="description"></param>
        void OnPeerConnectionError(string description);

        IVideoCapturer CreateVideoCapturer(IPeerConnectionFactory factory, IVideoSource videoSource);
    }

    #endregion

    #region PeerConnectionParameter Class

    public class PeerConnectionParameters
    {
        public PeerConnectionParameters(IceServer[] iceServers)
        {
            IceServers = iceServers;
        }

        public IceServer[] IceServers { get; }

        public bool IsScreencast { get; set; }

        public bool VideoCallEnabled { get; set; }
        public bool Loopback { get; set; }
        public bool Tracing { get; set; }
        public int VideoWidth { get; set; }
        public int VideoHeight { get; set; }
        public int VideoFps { get; set; }
        public bool NoAudioProcessing { get; set; }
        public bool AecDump { get; set; }
        public string AecDumpFile { get; set; }
        public bool EnableRtcEventLog { get; set; } = false;
        public string RtcEventLogDirectory { get; set; }
    }

    #endregion


    #region PeerConnectionClient Class

    /// <summary>
    /// Peer connection client implementation.
    /// All public methods are routed to local looper thread.
    /// All PeerConnectionEvents callbacks are invoked from the same looper thread.
    /// This class is a singleton.
    /// </summary>
    public class PeerConnectionClient
    {
        public const string VideoTrackId = "ARDAMSv0";
        public const string AudioTrackId = "ARDAMSa0";
        public const string VideoTrackType = "video";

        private const string TAG = nameof(PeerConnectionClient);

        private const string AudioEchoCancellationConstraint = "googEchoCancellation";
        private const string AudioAutoGainControlConstraint = "googAutoGainControl";
        private const string AudioHighPassFilterConstraint = "googHighpassFilter";
        private const string AudioNoiseSuppressionConstraint = "googNoiseSuppression";


        private const int HDVideoWidth = 1280;
        private const int HDVideoHeight = 720;

        private readonly SdpObserver _observer;

        private readonly IPeerConnectionDelegate _peerConnectionListener;

        private readonly PeerConnectionParameters _parameters;

        private readonly IPeerConnectionEvents _peerConnectionEvents;

        private readonly IExecutorService _executor;

        private readonly ILogger _logger;

        private IPeerConnectionFactory _factory;

        private IPeerConnection _peerConnection;
        private List<IceCandidate> _queuedRemoteCandidates;


        private bool _renderVideo = true;
        private bool _enableAudio = true;

        private IVideoRenderer _localRenderer;
        private IVideoRenderer _remoteRenderer;

        private IVideoCapturer _videoCapturer;
        private bool _videoCapturerStopped;

        private IAudioSource _audioSource;
        private IAudioTrack _localAudioTrack;

        private IVideoSource _videoSource;
        private IVideoTrack _localVideoTrack;

        private IRtpSender _localVideoSender;

        private IVideoTrack _remoteVideoTrack;


        private int _videoWidth;
        private int _videoHeight;
        private int _fps;

        private MediaConstraints _audioConstraints;
        private MediaConstraints _sdpMediaConstraints;

        private SessionDescription _localSdp;

        private RTCEventLog _rtcEventLog;
        private bool _isInitiator;

        private bool _isError;

        private bool IsVideoCallEnabled => _parameters.VideoCallEnabled;

        public PeerConnectionClient(PeerConnectionParameters parameters, IPeerConnectionEvents peerConnectionEvents,
            ILogger logger = null)
        {
            _parameters = parameters;
            _peerConnectionEvents = peerConnectionEvents;
            _executor = ExecutorServiceFactory.CreateExecutorService(TAG);

            _logger = logger ?? new ConsoleLogger();

            _observer = new SdpObserver(this);
            _peerConnectionListener = new PeerConnectionListener(this);
        }

        public void SetVideoEnabled(bool enable)
        {
            _executor.Execute(() =>
            {
                _renderVideo = enable;
                if (_localVideoTrack != null)
                    _localVideoTrack.IsEnabled = _renderVideo;
                if (_remoteVideoTrack != null)
                    _remoteVideoTrack.IsEnabled = _renderVideo;
            });
        }

        public void SetAudioEnabled(bool enable)
        {
            _executor.Execute(() =>
            {
                _enableAudio = enable;
                if (_localAudioTrack != null)
                    _localAudioTrack.IsEnabled = _enableAudio;
            });
        }

        public void Close()
        {
            _executor.Execute(CloseInternal);
        }


        /// <summary>
        /// This function should only be called once.
        /// </summary>
        public void CreatePeerConnectionFactory()
        {
            if (_factory != null)
            {
                throw new InvalidOperationException("PeerConnectionFactory has already been constructed");
            }

            _executor.Execute(() =>
            {
                _logger.Debug(TAG, "Starting PeerConnectionFactory creation");
                _factory = new PeerConnectionFactory();
                _logger.Debug(TAG, "PeerConnectionFactory created");
                _peerConnectionEvents.OnPeerFactoryCreated(_factory);
                _logger.Debug(TAG, "PeerConnectionFactory OnCreated");
            });
        }

        public void CreatePeerConnection(IVideoRenderer localRenderer, IVideoRenderer remoteRenderer)
        {
            _localRenderer = localRenderer;
            _remoteRenderer = remoteRenderer;

            _executor.Execute(() =>
            {
                try
                {
                    CreateMediaConstraintsInternal();
                    CreatePeerConnectionInternal();
                    MaybeCreateAndStartRtcEventLog();
                }
                catch (Exception ex)
                {
                    ReportError("Failed to create peer connection: " + ex.Message);
                    throw;
                }
                if (_peerConnection != null)
                    _peerConnectionEvents.OnPeerConnectionCreated(_peerConnection);
            });
        }

        public void CreateOffer()
        {
            _executor.Execute(() =>
            {
                if (_peerConnection == null || _isError)
                    return;
                _logger.Debug(TAG, "PC Create OFFER");
                _isInitiator = true;
                _peerConnection.OfferForConstraints(_sdpMediaConstraints, _observer);
            });
        }

        public void CreateAnswer()
        {
            _executor.Execute(() =>
            {
                if (_peerConnection == null || _isError)
                    return;
                _logger.Debug(TAG, "PC create ANSWER");
                _isInitiator = false;
                _peerConnection.AnswerForConstraints(_sdpMediaConstraints, _observer);
            });
        }

        public void AddRemoteIceCandidate(IceCandidate candidate)
        {
            _executor.Execute(() =>
            {
                if (_peerConnection == null || _isError)
                    return;
                if (_queuedRemoteCandidates != null)
                {
                    _queuedRemoteCandidates.Add(candidate);
                }
                else
                {
                    _peerConnection.AddIceCandidate(candidate);
                }
            });
        }


        public void RemoveRemoteIceCandidates(IceCandidate[] candidates)
        {
            _executor.Execute(() =>
            {
                if (_peerConnection == null || _isError)
                    return;
                // Drain the queued remote candidates if there is any so that
                // they are processed in the proper order.
                DrainCandidates();
                _peerConnection.RemoveIceCandidates(candidates);
            });
        }

        public void SetRemoteDescription(SessionDescription sdp)
        {
            _executor.Execute(() =>
            {
                if (_peerConnection == null || _isError)
                    return;
                _logger.Debug(TAG, "Set remote SDP.");
                _peerConnection.SetRemoteDescription(sdp, _observer);
            });
        }

        public void StopVideoSource()
        {
            _executor.Execute(() =>
            {
                if (_videoCapturer != null && !_videoCapturerStopped)
                {
                    _logger.Debug(TAG, "Stop video source.");
                    _videoCapturer.StopCapture();
                }

                _videoCapturerStopped = true;
            });
        }

        public void StartVideoSource()
        {
            _executor.Execute(() =>
            {
                if (_videoCapturer == null || _videoCapturerStopped)
                    return;
                _logger.Debug(TAG, "Restart video source.");
                _videoCapturer.StartCapture(_videoWidth, _videoHeight, _fps);
                _videoCapturerStopped = false;
            });
        }

        public void SwitchCamera()
        {
            _executor.Execute(() =>
            {
                if (_videoCapturer is ICameraVideoCapturer cameraVideoCapturer)
                {
                    if (!IsVideoCallEnabled || _isError)
                    {
                        _logger.Error(TAG, $"Failed to switch camera. Video: {IsVideoCallEnabled}. Error : {_isError}");
                        return;
                    }

                    _logger.Debug(TAG, "Switch camera");
                    cameraVideoCapturer.SwitchCamera();
                }
                else
                {
                    _logger.Debug(TAG, "Will not switch camera, video caputurer is not a camera");
                }
            });
        }

        public void ChangeCaptureFormat(int width, int height, int framerate)
        {
            _executor.Execute(() =>
            {
                if (!IsVideoCallEnabled || _isError || _videoCapturer == null)
                {
                    _logger.Error(TAG, $"Failed to change capture format. Video: {IsVideoCallEnabled}. Error : {_isError}");
                    return;
                }

                _logger.Debug(TAG, $"ChangeCaptureFormat: {width}x{height}@{framerate}");
                _videoSource.AdaptOutputFormatToWidth(width, height, framerate);
            });

        }

        private void CloseInternal()
        {
            if (_factory != null && _parameters.AecDump)
            {
                _factory.StopAecDump();
            }

            _logger.Debug(TAG, "Closing peer connection.");
            if (_rtcEventLog != null)
            {
                // RtcEventLog should stop before the peer connection is disposed.
                _rtcEventLog.Stop();
                _rtcEventLog = null;
            }

            _logger.Debug(TAG, "Closing audio source.");
            if (_audioSource != null)
            {
                _audioSource.Dispose();
                _audioSource = null;
            }

            _logger.Debug(TAG, "Stopping capturer.");
            if (_videoCapturer != null)
            {
                _videoCapturer.StopCapture();
                _videoCapturer.Dispose();
                _videoCapturerStopped = true;
                _videoCapturer = null;
            }

            _logger.Debug(TAG, "Closing video source.");
            if (_videoSource != null)
            {
                _videoSource.Dispose();
                _videoSource = null;
            }

            _localRenderer = null;
            _remoteRenderer = null;
            _logger.Debug(TAG, "Closing peer connection factory.");
            if (_factory != null)
            {
                _factory.Dispose();
                _factory = null;
            }

            _logger.Debug(TAG, "Closing peer connection done.");
            _peerConnectionEvents.OnPeerConnectionClosed();
            PeerConnectionFactory.StopInternalTracingCapture();
            PeerConnectionFactory.ShutdownInternalTracer();

            _executor.Release();
        }

        private void CreateMediaConstraintsInternal()
        {
            if (IsVideoCallEnabled)
            {
                _videoWidth = _parameters.VideoWidth;
                _videoHeight = _parameters.VideoHeight;
                _fps = _parameters.VideoFps;

                if (_videoWidth == 0 || _videoHeight == 0)
                {
                    _videoWidth = HDVideoWidth;
                    _videoHeight = HDVideoHeight;
                }

                if (_fps == 0)
                    _fps = 30;
                _logger.Debug(TAG, $"Capturing format: {_videoWidth}x{_videoHeight}@{_fps}");
            }

            _audioConstraints = new MediaConstraints();
            if (_parameters.NoAudioProcessing)
            {
                _logger.Debug(TAG, "Disabling audio processing");
                _audioConstraints.Mandatory.Add(AudioEchoCancellationConstraint, "false");
                _audioConstraints.Mandatory.Add(AudioAutoGainControlConstraint, "false");
                _audioConstraints.Mandatory.Add(AudioHighPassFilterConstraint, "false");
                _audioConstraints.Mandatory.Add(AudioNoiseSuppressionConstraint, "false");
            }

            _sdpMediaConstraints = new MediaConstraints();
            _sdpMediaConstraints.Mandatory.Add("OfferToReceiveAudio", "true");
            _sdpMediaConstraints.Mandatory.Add("OfferToReceiveVideo", _parameters.VideoCallEnabled ? "true" : "false");
        }

        private void CreatePeerConnectionInternal()
        {
            if (_factory == null || _isError)
            {
                _logger.Error(TAG, "Peerconnection factory is not created");
                return;
            }

            _logger.Debug(TAG, "Create peer connection.");
            _queuedRemoteCandidates = new List<IceCandidate>();

            var rtcConfig = new RTCConfiguration();

            // TCP candidates are only useful when connecting to a server that supports
            // ICE-TCP.
            rtcConfig.IceServers = _parameters.IceServers;
            rtcConfig.TcpCandidatePolicy = TcpCandidatePolicy.Disabled;
            rtcConfig.BundlePolicy = BundlePolicy.MaxBundle;
            rtcConfig.RtcpMuxPolicy = RtcpMuxPolicy.Require;
            rtcConfig.ContinualGatheringPolicy = ContinualGatheringPolicy.Continually;
            // Use ECDSA encryption.
            rtcConfig.KeyType = EncryptionKeyType.Ecdsa;
            // Enable DTLS for normal calls and disable for loopback calls.
            rtcConfig.EnableDtlsSrtp = _parameters.Loopback;
            rtcConfig.SdpSemantics = SdpSemantics.UnifiedPlan;

            _peerConnection = _factory.PeerConnectionWithConfiguration(rtcConfig, _sdpMediaConstraints, _peerConnectionListener);

            var mediaStreamLabels = new[] { "ARDAMS" };

            if (IsVideoCallEnabled)
            {
                _peerConnection.AddTrack(CreateVideoTrack(), mediaStreamLabels);

                // We can add the renderers right away because we don't need to wait for an
                // answer to get the remote track.
                _remoteVideoTrack = GetRemoteVideoTrack();
                _remoteVideoTrack.IsEnabled = _renderVideo;
                _remoteVideoTrack.AddRenderer(_remoteRenderer);
            }

            _peerConnection.AddTrack(CreateAudioTrack(), mediaStreamLabels);

            if (IsVideoCallEnabled)
            {
                FindVideoSender();
            }

            if (_parameters.AecDump)
            {
                var result = _factory.StartAecDumpWithFilePath(_parameters.AecDumpFile, -1);
                if (!result)
                {
                    _logger.Error(TAG, "Can not open aecdump file");
                }
            }

            _logger.Debug(TAG, "Peer connection created.");
        }

        private void MaybeCreateAndStartRtcEventLog()
        {
            if (_peerConnection == null)
                return;
            if (!_parameters.EnableRtcEventLog)
            {
                _logger.Debug(TAG, "RtcEventLog is disabled.");
                return;
            }

            var file = Path.Combine(_parameters.RtcEventLogDirectory, CreateRtcEventLogOutputFile());
            _rtcEventLog = new RTCEventLog(_peerConnection, file, _logger);
            _rtcEventLog.Start();
        }

        private string CreateRtcEventLogOutputFile()
        {
            var date = DateTime.Now;
            return $"event_log_{date:yyyyMMdd_hhmm_ss}";
        }

        private IAudioTrack CreateAudioTrack()
        {
            _audioSource = _factory.AudioSourceWithConstraints(_audioConstraints);
            _localAudioTrack = _factory.AudioTrackWithSource(_audioSource, AudioTrackId);
            _localAudioTrack.IsEnabled = _enableAudio;
            return _localAudioTrack;
        }

        private IVideoTrack CreateVideoTrack()
        {

            _videoSource = _factory.VideoSource; //CreateVideoSource(_parameters.IsScreencast);
            _videoCapturer = _peerConnectionEvents.CreateVideoCapturer(_factory, _videoSource);

            _videoCapturer.StartCapture(_videoWidth, _videoHeight, _fps);
            _localVideoTrack = _factory.VideoTrackWithSource(_videoSource, VideoTrackId);

            _localVideoTrack.IsEnabled = _renderVideo;
            _localVideoTrack.AddRenderer(_localRenderer);
            return _localVideoTrack;
        }

        private IVideoTrack GetRemoteVideoTrack()
        {
            foreach (var transceiver in _peerConnection.Transceivers)
            {
                var track = transceiver.Receiver.Track;
                if (track is IVideoTrack videoTrack)
                    return videoTrack;
            }

            return null;
        }

        private void FindVideoSender()
        {
            foreach (var sender in _peerConnection.Senders)
            {
                var track = sender.Track;
                if (track == null)
                    continue;

                if (VideoTrackType.Equals(track.Kind))
                {
                    _logger.Debug(TAG, "Found video sender.");
                    _localVideoSender = sender;
                }
            }
        }

        private void DrainCandidates()
        {
            if (_queuedRemoteCandidates == null)
                return;
            _logger.Debug(TAG, $"Add  {_queuedRemoteCandidates.Count} remote candidates");
            foreach (var candidate in _queuedRemoteCandidates)
            {
                _peerConnection.AddIceCandidate(candidate);
            }

            _queuedRemoteCandidates = null;
        }


        private void ReportError(string errorMessage)
        {
            _logger.Error(TAG, $"Peerconnection error: {errorMessage}");
            _executor.Execute(() =>
            {
                if (_isError)
                    return;
                _peerConnectionEvents.OnPeerConnectionError(errorMessage);
                _isError = true;
            });
        }

        private class PeerConnectionListener : IPeerConnectionDelegate
        {
            private readonly PeerConnectionClient _peerConnectionClient;
            private readonly ILogger _logger;
            private readonly IExecutor _executor;
            private readonly IPeerConnectionEvents _events;

            public object NativeObject => throw new NotImplementedException();

            public PeerConnectionListener(PeerConnectionClient peerConnectionClient)
            {
                _peerConnectionClient = peerConnectionClient;
                _executor = _peerConnectionClient._executor;
                _events = _peerConnectionClient._peerConnectionEvents;
                _logger = _peerConnectionClient._logger;
            }

            public void OnSignalingChange(SignalingState signalingState)
            {
                _logger.Debug(TAG, $"SignalingState: {signalingState}");
            }

            public void OnIceConnectionChange(IceConnectionState iceConnectionState)
            {
                _executor.Execute(() =>
                {
                    _logger.Debug(TAG, $"IceConnectionState: {iceConnectionState}");
                    switch (iceConnectionState)
                    {
                        case IceConnectionState.Connected:
                            _events.OnIceConnected();
                            break;
                        case IceConnectionState.Disconnected:
                            _events.OnIceDisconnected();
                            break;
                        case IceConnectionState.Failed:
                            _peerConnectionClient.ReportError("Ice connection failed.");
                            break;
                    }
                });
            }

            public void OnConnectionChange(PeerConnectionState newState)
            {
                _executor.Execute(() =>
                {
                    _logger.Debug(TAG, $"PeerConnectionState: {newState}");
                    switch (newState)
                    {
                        case PeerConnectionState.Connected:
                            _events.OnConnected();
                            break;
                        case PeerConnectionState.Disconnected:
                            _events.OnDisconnected();
                            break;
                        case PeerConnectionState.Failed:
                            _peerConnectionClient.ReportError("DTLS connection failed.");
                            break;
                    }
                });
            }

            public void OnIceGatheringChange(IceGatheringState iceGatheringState)
            {
                _logger.Debug(TAG, $"IceGatheringState: {iceGatheringState}");
            }

            public void OnIceCandidate(IceCandidate iceCandidate)
            {
                _executor.Execute(() => _events.OnIceCandidate(iceCandidate));
            }

            public void OnIceCandidatesRemoved(IceCandidate[] iceCandidates)
            {
                _executor.Execute(() => _events.OnIceCandidateRemoved(iceCandidates));
            }

            public void OnAddStream(IMediaStream mediaStream)
            {

            }

            public void OnRemoveStream(IMediaStream mediaStream)
            {
            }

            public void OnDataChannel(IDataChannel dataChannel)
            {
            }

            public void OnRenegotiationNeeded()
            {

            }

            public void OnAddTrack(IRtpReceiver rtpReceiver, IMediaStream[] mediaStreams)
            {
            }

            public void OnTrack(IRtpTransceiver transceiver)
            {
            }

            public void Dispose()
            {
                this?.Dispose();
            }
        }

        private class SdpObserver : ISdpObserver
        {
            private readonly PeerConnectionClient _peerConnectionClient;
            private readonly IPeerConnectionEvents _events;
            private readonly IExecutor _executor;
            private readonly ILogger _logger;

            private bool IsError => _peerConnectionClient._isError;
            private IPeerConnection PeerConnection => _peerConnectionClient._peerConnection;

            private SessionDescription LocalSdp
            {
                get => _peerConnectionClient._localSdp;
                set => _peerConnectionClient._localSdp = value;
            }

            private bool IsInitiator => _peerConnectionClient._isInitiator;

            public object NativeObject => throw new NotImplementedException();

            public SdpObserver(PeerConnectionClient peerConnectionClient)
            {
                _peerConnectionClient = peerConnectionClient;
                _executor = peerConnectionClient._executor;
                _logger = _peerConnectionClient._logger;
                _events = _peerConnectionClient._peerConnectionEvents;
            }


            public void OnCreateSuccess(SessionDescription sdp)
            {
                if (LocalSdp != null)
                {
                    ReportError("Multiple SDP create.");
                    return;
                }

                LocalSdp = sdp;
                _executor.Execute(() =>
                {
                    if (PeerConnection == null || IsError)
                        return;
                    _logger.Debug(TAG, $"Set local SDP from {sdp.Type}");
                    PeerConnection.SetLocalDescription(sdp, this);
                });
            }

            public void OnSetSuccess()
            {
                _executor.Execute(() =>
                {
                    if (PeerConnection == null || IsError)
                    {
                        return;
                    }

                    if (IsInitiator)
                    {
                        // For offering peer connection we first create offer and set
                        // local SDP, then after receiving answer set remote SDP.
                        if (PeerConnection.RemoteDescription == null)
                        {
                            // We've just set our local SDP so time to send it.
                            _logger.Debug(TAG, "Local SDP set succesfully");
                            _events.OnLocalDescription(LocalSdp);
                        }
                        else
                        {
                            // We've just set remote description, so drain remote
                            // and send local ICE candidates.
                            _peerConnectionClient.DrainCandidates();
                        }
                    }
                    else
                    {
                        // For answering peer connection we set remote SDP and then
                        // create answer and set local SDP.
                        if (PeerConnection.LocalDescription != null)
                        {
                            // We've just set our local SDP so time to send it, drain
                            // remote and send local ICE candidates.
                            _logger.Debug(TAG, "Local SDP set succesfully");
                            _events.OnLocalDescription(LocalSdp);
                            _peerConnectionClient.DrainCandidates();
                        }
                        else
                        {
                            // We've just set remote SDP - do nothing for now -
                            // answer will be created soon.
                            _logger.Debug(TAG, "Remote SDP set succesfully");
                        }
                    }
                });
            }

            public void OnCreateFailure(string error)
            {
                ReportError($"createSDP error: {error}");
            }

            public void OnSetFailure(string error)
            {
                ReportError($"setSDP error: {error}");
            }

            private void ReportError(string description)
            {
                _peerConnectionClient.ReportError(description);
            }

            public void Dispose()
            {
                this?.Dispose();
            }
        }
    }
    #endregion
}
