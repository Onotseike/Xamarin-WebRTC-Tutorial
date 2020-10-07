// onotseike@hotmail.comPaula Aliu
using System.Linq;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformPeerConnectionDelegate : NSObject, IRTCPeerConnectionDelegate
    {
        private readonly IPeerConnectionDelegate _peerConnectionDelegate;

        public PlatformPeerConnectionDelegate(IPeerConnectionDelegate peerConnectionDelegate) => _peerConnectionDelegate = peerConnectionDelegate;

        public void DidAddStream(RTCPeerConnection peerConnection, RTCMediaStream stream) => _peerConnectionDelegate?.OnAddStream(new PlatformMediaStream(stream));

        public void DidChangeIceConnectionState(RTCPeerConnection peerConnection, RTCIceConnectionState newState) => _peerConnectionDelegate?.OnIceConnectionChange(newState.ToNativePort());

        public void DidChangeIceGatheringState(RTCPeerConnection peerConnection, RTCIceGatheringState newState) => _peerConnectionDelegate?.OnIceGatheringChange(newState.ToNativePort());

        public void DidChangeSignalingState(RTCPeerConnection peerConnection, RTCSignalingState stateChanged) => _peerConnectionDelegate?.OnSignalingChange(stateChanged.ToNativePort());

        public void DidGenerateIceCandidate(RTCPeerConnection peerConnection, RTCIceCandidate candidate) => _peerConnectionDelegate?.OnIceCandidate(candidate.ToNativePort());

        public void DidOpenDataChannel(RTCPeerConnection peerConnection, RTCDataChannel dataChannel) => _peerConnectionDelegate?.OnDataChannel(new PlatformDataChannel(dataChannel));

        public void DidRemoveIceCandidates(RTCPeerConnection peerConnection, RTCIceCandidate[] candidates) => _peerConnectionDelegate?.OnIceCandidatesRemoved(candidates.ToNativePort().ToArray());

        public void DidRemoveStream(RTCPeerConnection peerConnection, RTCMediaStream stream) => _peerConnectionDelegate?.OnRemoveStream(new PlatformMediaStream(stream));

        public void PeerConnectionShouldNegotiate(RTCPeerConnection peerConnection) => _peerConnectionDelegate?.OnRenegotiationNeeded();
    }
}