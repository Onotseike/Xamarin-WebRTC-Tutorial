// onotseike@hotmail.comPaula Aliu
using System.Linq;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.iOS
{
    internal class PlatformPeerConnection : NativePlatformBase, IPeerConnection
    {
        private RTCPeerConnection _peerConnection;


        public PlatformPeerConnection(RTCPeerConnection peerConnection, Core.RTCConfiguration configuration, IPeerConnectionFactory peerConnectionFactory)
        {
            _peerConnection = peerConnection;
            Configuration = configuration;
            PeerConnectionFactory = peerConnectionFactory;
        }

        public IPeerConnectionFactory PeerConnectionFactory { get; }

        public IMediaStream[] LocalStreams => _peerConnection.LocalStreams.Select(localStream => new PlatformMediaStream(localStream)).ToArray();

        public SessionDescription LocalDescription => _peerConnection.LocalDescription?.ToNativePort();

        public SessionDescription RemoteDescription => _peerConnection.RemoteDescription?.ToNativePort();

        public SignalingState SignalingState => _peerConnection.SignalingState.ToNativePort();

        public IceConnectionState IceConnectionState => _peerConnection.IceConnectionState.ToNativePort();

        public PeerConnectionState ConnectionState => _peerConnection.ConnectionState.ToNativePort();

        public IceGatheringState IceGatheringState => _peerConnection.IceGatheringState.ToNativePort();

        public Core.RTCConfiguration Configuration { get; private set; }

        public IRtpSender[] Senders => _peerConnection.Senders.Select(sender => new PlatformRtpSender(sender)).ToArray();

        public IRtpReceiver[] Receivers => _peerConnection.Receivers.Select(receiver => new PlatformRtpReceiver(receiver)).ToArray();

        public IRtpTransceiver[] Transceivers => _peerConnection.Transceivers.Select(transceiver => new PlatformTransceiver(transceiver)).ToArray();

        public void AddIceCandidate(IceCandidate iceCandidate) => _peerConnection.AddIceCandidate(iceCandidate.ToPlatformNative());

        public void AddStream(IMediaStream mediaStream) => _peerConnection.AddStream(mediaStream.ToPlatformNative<RTCMediaStream>());

        public IRtpSender AddTrack(IMediaStreamTrack mediaStreamTrack, string[] streamIds) => new PlatformRtpSender(_peerConnection.AddTrack(mediaStreamTrack.ToPlatformNative<RTCMediaStreamTrack>(), streamIds));

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType) => new PlatformTransceiver(_peerConnection.AddTransceiverOfType(rtpMediaType.ToPlatformNative()));

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType, IRtpTransceiverInit transceiverInit) => new PlatformTransceiver(_peerConnection.AddTransceiverOfType(rtpMediaType.ToPlatformNative(), transceiverInit.ToPlatformNative<RTCRtpTransceiverInit>()));

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack) => new PlatformTransceiver(_peerConnection.AddTransceiverWithTrack(mediaStreamTrack.ToPlatformNative<RTCMediaStreamTrack>()));

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack, IRtpTransceiverInit transceiverInit) => new PlatformTransceiver(_peerConnection.AddTransceiverWithTrack(mediaStreamTrack.ToPlatformNative<RTCMediaStreamTrack>(), transceiverInit.ToPlatformNative<RTCRtpTransceiverInit>()));

        public void AnswerForConstraints(MediaConstraints mediaConstraints, ISdpObserver sdpObserver)
        {
            var sdpCallbackHelper = new SdpCallbackHelper(sdpObserver);
            _peerConnection.AnswerForConstraints(mediaConstraints.ToPlatformNative(), sdpCallbackHelper.CreateSdp);
        }

        public void Close() => _peerConnection?.Close();

        public void OfferForConstraints(MediaConstraints mediaConstraints, ISdpObserver sdpObserver)
        {
            var sdpCallbackHelper = new SdpCallbackHelper(sdpObserver);
            _peerConnection.OfferForConstraints(mediaConstraints.ToPlatformNative(), sdpCallbackHelper.CreateSdp);
        }

        public void RemoveIceCandidates(IceCandidate[] iceCandidates) => _peerConnection.RemoveIceCandidates(iceCandidates.ToPlatformNative().ToArray());

        public void RemoveStream(IMediaStream mediaStream) => _peerConnection.RemoveStream(mediaStream.ToPlatformNative<RTCMediaStream>());

        public bool RemoveTrack(IRtpSender rtpSender) => _peerConnection.RemoveTrack(rtpSender.ToPlatformNative<RTCRtpSender>());

        public bool SetBweMinBitrateBps(int minBitrateBps, int currentBitrateBps, int maxBitrateBps) => _peerConnection.SetBweMinBitrateBps(new NSNumber(minBitrateBps), new NSNumber(currentBitrateBps), new NSNumber(maxBitrateBps));

        public bool SetConfiguration(Core.RTCConfiguration configuration) => _peerConnection.SetConfiguration(configuration.ToPlatformNative());

        public void SetLocalDescription(SessionDescription sessionDescription, ISdpObserver sdpObserver)
        {
            var sdpCallbackHelper = new SdpCallbackHelper(sdpObserver);
            _peerConnection.SetLocalDescription(sessionDescription.ToPlatformNative(), sdpCallbackHelper.SetSdp);
        }

        public void SetRemoteDescription(SessionDescription sessionDescription, ISdpObserver sdpObserver)
        {
            var sdpCallbackHelper = new SdpCallbackHelper(sdpObserver);
            _peerConnection.SetRemoteDescription(sessionDescription.ToPlatformNative(), sdpCallbackHelper.SetSdp);
        }

        public bool StartRtcEventLogWithFilePath(string filePath, long maxSizeInBytes) => _peerConnection.StartRtcEventLogWithFilePath(filePath, maxSizeInBytes);

        public void StopRtcEventLog() => _peerConnection.StopRtcEventLog();

        public IDataChannel CreateDataChannel(string label, DataChannelConfiguration dataChannelConfiguration)
        {
            var dataChannel = _peerConnection.DataChannelForLabel(label, dataChannelConfiguration.ToPlatformNative());
            return dataChannel == null ? null : new PlatformDataChannel(dataChannel);
        }

        private class SdpCallbackHelper
        {
            private readonly ISdpObserver _observer;

            public SdpCallbackHelper(ISdpObserver observer)
            {
                _observer = observer;
            }

            public void SetSdp(NSError error)
            {
                if (error != null)
                    _observer?.OnSetFailure(error.LocalizedDescription);
                else
                    _observer?.OnSetSuccess();
            }

            public void CreateSdp(RTCSessionDescription sdp, NSError error)
            {

                if (error != null)
                {
                    _observer?.OnCreateFailure(error.LocalizedDescription);
                }
                else
                {
                    _observer?.OnCreateSuccess(sdp.ToNativePort());
                }
            }
        }
    }
}