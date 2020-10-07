// onotseike@hotmail.comPaula Aliu
using System;
using System.Linq;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformPeerConnectionDelegate : Java.Lang.Object, PeerConnection.IObserver
    {
        private IPeerConnectionDelegate _peerConnectionDelegate;

        public PlatformPeerConnectionDelegate(IPeerConnectionDelegate peerConnectionDelegate)
        {
            _peerConnectionDelegate = peerConnectionDelegate;
        }

        #region Helper Methods

        private IMediaStream[] ConvertToPlatformNative(MediaStream[] mediaStreams) => Array.ConvertAll<MediaStream, IMediaStream>(mediaStreams, mediaStream => new PlatformMediaStream(mediaStream));


        #endregion

        public void OnAddStream(MediaStream mediaStream) => _peerConnectionDelegate.OnAddStream(new PlatformMediaStream(mediaStream));

        public void OnAddTrack(RtpReceiver rtpReceiver, MediaStream[] mediaStreams) => _peerConnectionDelegate.OnAddTrack(new PlatformRtpReceiver(rtpReceiver), ConvertToPlatformNative(mediaStreams));


        public void OnDataChannel(DataChannel dataChannel) => _peerConnectionDelegate.OnDataChannel(new PlatformDataChannel(dataChannel));

        public void OnIceCandidate(IceCandidate iceCandidate) => _peerConnectionDelegate.OnIceCandidate(iceCandidate.ToNativePort());

        public void OnIceCandidatesRemoved(IceCandidate[] iceCandidates) => _peerConnectionDelegate.OnIceCandidatesRemoved(iceCandidates.ToNativePort().ToArray());

        public void OnIceConnectionChange(PeerConnection.IceConnectionState iceConnectionState) => _peerConnectionDelegate.OnIceConnectionChange(iceConnectionState.ToNativePort());

        public void OnIceConnectionReceivingChange(bool isReceivingChange)
        {
            // _peerConnectionDelegate.onice
            //throw new System.NotImplementedException();
        }

        public void OnIceGatheringChange(PeerConnection.IceGatheringState iceGatheringState) => _peerConnectionDelegate.OnIceGatheringChange(iceGatheringState.ToNativePort());

        public void OnRemoveStream(MediaStream mediaStream) => _peerConnectionDelegate.OnRemoveStream(new PlatformMediaStream(mediaStream));

        public void OnRenegotiationNeeded() => _peerConnectionDelegate.OnRenegotiationNeeded();

        public void OnSignalingChange(PeerConnection.SignalingState signalingState) => _peerConnectionDelegate.OnSignalingChange(signalingState.ToNativePort());
    }
}