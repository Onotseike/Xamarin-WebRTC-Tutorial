// onotseike@hotmail.comPaula Aliu
using Web.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IPeerConnectionDelegate : INativeObject
    {
        void DidChangeSignalingState(IPeerConnection peerConnection, SignalingState stateChanged);

        void DidAddStream(IPeerConnection peerConnection, IMediaStream stream);

        void DidRemoveStream(IPeerConnection peerConnection, IMediaStream stream);

        void PeerConnectionShouldNegotiate(IPeerConnection peerConnection);

        void DidChangeIceConnectionState(IPeerConnection peerConnection, IceConnectionState newState);

        void DidChangeIceGatheringState(IPeerConnection peerConnection, IceGatheringState newState);

        void DidGenerateIceCandidate(IPeerConnection peerConnection, IceCandidate candidate);

        void DidRemoveIceCandidates(IPeerConnection peerConnection, IceCandidate[] candidates);

        void DidOpenDataChannel(IPeerConnection peerConnection, IDataChannel dataChannel);

        void DidChangeStandardizedIceConnectionState(IPeerConnection peerConnection, IceConnectionState newState);

        void DidChangeConnectionState(IPeerConnection peerConnection, PeerConnectionState newState);

        void DidStartReceivingOnTransceiver(IPeerConnection peerConnection, IRtpTransceiver transceiver);

        void DidAddReceiver(IPeerConnection peerConnection, IRtpReceiver rtpReceiver, IMediaStream[] mediaStreams);

        void DidRemoveReceiver(IPeerConnection peerConnection, IRtpReceiver rtpReceiver);

        void DidChangeLocalCandidate(IPeerConnection peerConnection, IceCandidate local, IceCandidate remote, int lastDataReceivedMs, string reason);

    }
}
