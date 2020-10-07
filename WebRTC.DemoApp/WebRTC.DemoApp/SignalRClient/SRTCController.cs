// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.DemoApp.SignalRClient.Abstractions;

namespace WebRTC.DemoApp.SignalRClient
{
    public class SRTCController : RTCControllerBase<RoomConnectionParameters, SignalingParameters>
    {
        #region Properties & Variables

        #endregion

        #region Constructor(s)

        public SRTCController(IRTCEngineEvents events, ILogger logger = null) : base(events, logger)
        {
        }

        #endregion

        #region Helper Function(s)

        #endregion

        #region RTCControllerBase Implementations

        protected override bool IsInitiator => SignalingParameters.IsInitiator;

        protected override IRTCClient<RoomConnectionParameters> CreateClient() => new SRTCClient(this);

        protected override PeerConnectionParameters CreatePeerConnectionParameters(SignalingParameters signalingParameters) => new PeerConnectionParameters(signalingParameters.IceServers) { VideoCallEnabled = true };

        protected override void OnChannelConnectedInternal(SignalingParameters signalingParameters)
        {
            if (signalingParameters.IsInitiator)
            {
                PeerConnectionClient.CreateOffer();
            }
            else
            {
                if (signalingParameters.OfferSdp != null)
                {
                    PeerConnectionClient.SetRemoteDescription(signalingParameters.OfferSdp);
                    PeerConnectionClient.CreateAnswer();
                }

                if (signalingParameters.IceCandidates != null)
                {
                    foreach (var _iceCandidate in signalingParameters.IceCandidates)
                    {
                        PeerConnectionClient.AddRemoteIceCandidate(_iceCandidate);
                    }
                }
            }
        }

        #endregion
    }
}
