// onotseike@hotmail.comPaula Aliu
using WebRTC.Unified.Enums;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IPeerConnectionDelegate : INativeObject
    {
        void OnSignalingChange(SignalingState signalingState);
        void OnIceConnectionChange(IceConnectionState iceConnectionState);
        void OnConnectionChange(PeerConnectionState newState);
        void OnIceGatheringChange(IceGatheringState iceGatheringState);
        void OnIceCandidate(IceCandidate iceCandidate);
        void OnIceCandidatesRemoved(IceCandidate[] iceCandidates);
        void OnAddStream(IMediaStream mediaStream);
        void OnRemoveStream(IMediaStream mediaStream);
        void OnDataChannel(IDataChannel dataChannel);
        void OnRenegotiationNeeded();
        void OnAddTrack(IRtpReceiver rtpReceiver, IMediaStream[] mediaStreams);
        void OnTrack(IRtpTransceiver transceiver);
    }
}
