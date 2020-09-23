// onotseike@hotmail.comPaula Aliu
using System;
using System.Linq;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformPeerConnection : Core.NativePlatformBase, IPeerConnection
    {

        private readonly PeerConnection _peerConnection;
        public PlatformPeerConnection(PeerConnection peerConnection, Core.RTCConfiguration configuration, PlatformPeerConnectionFactory platformPeerConnectionFactory) : base(peerConnection)
        {
            _peerConnection = peerConnection;
            Configuration = configuration;
            PeerConnectionFactory = platformPeerConnectionFactory;
        }



        public IMediaStream[] LocalStreams { get; }

        public Core.SessionDescription LocalDescription => _peerConnection.LocalDescription.ToNativePort();

        public Core.SessionDescription RemoteDescription => _peerConnection.RemoteDescription.ToNativePort();

        public SignalingState SignalingState => _peerConnection.InvokeSignalingState().ToNativePort();

        public IceConnectionState IceConnectionState => _peerConnection.InvokeIceConnectionState().ToNativePort();

        public PeerConnectionState ConnectionState => _peerConnection.ConnectionState().ToNativePort();

        public IceGatheringState IceGatheringState => _peerConnection.InvokeIceGatheringState().ToNativePort();

        public Core.RTCConfiguration Configuration { get; }

        public IRtpSender[] Senders => _peerConnection.Senders.Select(sender => new PlatformRtpSender(sender)).Cast<IRtpSender>().ToArray();

        public IRtpReceiver[] Receivers => _peerConnection.Receivers.Select(sender => new PlatformRtpReceiver(sender)).Cast<IRtpReceiver>().ToArray();

        public IRtpTransceiver[] Transceivers
        {
            get
            {
                if (Configuration.SdpSemantics != SdpSemantics.UnifiedPlan) throw new InvalidOperationException(
                        "GETTRANSCEIVERS is only supported with Unified Plan SdpSemantics.");
                return _peerConnection.Transceivers.Select(transceiver => new PlatformRtpTransceiver(transceiver)).Cast<IRtpTransceiver>()
                    .ToArray();
            }
        }

        public PlatformPeerConnectionFactory PeerConnectionFactory { get; }

        public void AddIceCandidate(Core.IceCandidate iceCandidate) => _peerConnection.AddIceCandidate(iceCandidate.ToPlatformNative());

        public void AddStream(IMediaStream mediaStream) => _peerConnection.AddStream(mediaStream.ToPlatformNative<MediaStream>());

        public IRtpSender AddTrack(IMediaStreamTrack mediaStreamTrack, string[] streamIds) => new PlatformRtpSender(_peerConnection.AddTrack(mediaStreamTrack.ToPlatformNative<MediaStreamTrack>(), streamIds));

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType) => throw new System.NotImplementedException();

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType, IRtpTransceiverInit transceiverInit)
        {
            throw new System.NotImplementedException();
        }

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack)
        {
            throw new System.NotImplementedException();
        }

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack, IRtpTransceiverInit transceiverInit)
        {
            throw new System.NotImplementedException();
        }

        public void AnswerForConstraints(Core.MediaConstraints mediaConstraints, Core.Interfaces.ISdpObserver sdpObserver)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void OfferForConstraints(Core.MediaConstraints mediaConstraints, Core.Interfaces.ISdpObserver sdpObserver)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveIceCandidates(Core.IceCandidate[] iceCandidates)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveStream(IMediaStream mediaStream)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveTrack(IRtpSender rtpSender)
        {
            throw new System.NotImplementedException();
        }

        public bool SetBweMinBitrateBps(int minBitrateBps, int currentBitrateBps, int maxBitrateBps)
        {
            throw new System.NotImplementedException();
        }

        public bool SetConfiguration(Core.RTCConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }

        public void SetLocalDescription(Core.SessionDescription sessionDescription, Core.Interfaces.ISdpObserver sdpObserver)
        {
            throw new System.NotImplementedException();
        }

        public void SetRemoteDescription(Core.SessionDescription sessionDescription, Core.Interfaces.ISdpObserver sdpObserver)
        {
            throw new System.NotImplementedException();
        }

        public bool StartRtcEventLogWithFilePath(string filePath, long maxSizeInBytes)
        {
            throw new System.NotImplementedException();
        }

        public void StopRtcEventLog()
        {
            throw new System.NotImplementedException();
        }
    }
}