// onotseike@hotmail.comPaula Aliu
using System;


using IceTransportsType = Org.Webrtc.PeerConnection.IceTransportsType;
using BundlePolicy = Org.Webrtc.PeerConnection.BundlePolicy;
using RtcpMuxPolicy = Org.Webrtc.PeerConnection.RtcpMuxPolicy;
using TcpCandidatePolicy = Org.Webrtc.PeerConnection.TcpCandidatePolicy;
using CandidateNetworkPolicy = Org.Webrtc.PeerConnection.CandidateNetworkPolicy;
using ContinualGatheringPolicy = Org.Webrtc.PeerConnection.ContinualGatheringPolicy;
using KeyType = Org.Webrtc.PeerConnection.KeyType;
using SdpSemantics = Org.Webrtc.PeerConnection.SdpSemantics;
using TlsCertPolicy = Org.Webrtc.PeerConnection.TlsCertPolicy;
using SessionDescription = Org.Webrtc.SessionDescription;
using PeerConnectionState = Org.Webrtc.PeerConnection.PeerConnectionState;
using Org.Webrtc;

namespace WebRTC.Unified.Extensions
{
    internal static class EnumExtensions
    {
        public static IceTransportsType ToPlatformNative(this Enums.IceTransportPolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.IceTransportPolicy.None:
                    return IceTransportsType.None;
                case Enums.IceTransportPolicy.Relay:
                    return IceTransportsType.Relay;
                case Enums.IceTransportPolicy.NoHost:
                    return IceTransportsType.Nohost;
                case Enums.IceTransportPolicy.All:
                    return IceTransportsType.All;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static BundlePolicy ToPlatformNative(this Enums.BundlePolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.BundlePolicy.Balanced:
                    return BundlePolicy.Balanced;
                case Enums.BundlePolicy.MaxCompat:
                    return BundlePolicy.Maxcompat;
                case Enums.BundlePolicy.MaxBundle:
                    return BundlePolicy.Maxbundle;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static RtcpMuxPolicy ToPlatformNative(this Enums.RtcpMuxPolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.RtcpMuxPolicy.Negotiate:
                    return RtcpMuxPolicy.Negotiate;
                case Enums.RtcpMuxPolicy.Require:
                    return RtcpMuxPolicy.Require;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static TcpCandidatePolicy ToPlatformNative(this Enums.TcpCandidatePolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.TcpCandidatePolicy.Enabled:
                    return TcpCandidatePolicy.Enabled;
                case Enums.TcpCandidatePolicy.Disabled:
                    return TcpCandidatePolicy.Disabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static CandidateNetworkPolicy ToPlatformNative(this Enums.CandidateNetworkPolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.CandidateNetworkPolicy.All:
                    return CandidateNetworkPolicy.All;
                case Enums.CandidateNetworkPolicy.LowCost:
                    return CandidateNetworkPolicy.LowCost;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static ContinualGatheringPolicy ToPlatformNative(this Enums.ContinualGatheringPolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.ContinualGatheringPolicy.Once:
                    return ContinualGatheringPolicy.GatherOnce;
                case Enums.ContinualGatheringPolicy.Continually:
                    return ContinualGatheringPolicy.GatherContinually;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static KeyType ToPlatformNative(this Enums.EncryptionKeyType nativePort)
        {
            switch (nativePort)
            {
                case Enums.EncryptionKeyType.Rsa:
                    return KeyType.Rsa;
                case Enums.EncryptionKeyType.Ecdsa:
                    return KeyType.Ecdsa;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static SdpSemantics ToPlatformNative(this Enums.SdpSemantics nativePort)
        {
            switch (nativePort)
            {
                case Enums.SdpSemantics.PlanB:
                    return SdpSemantics.PlanB;
                case Enums.SdpSemantics.UnifiedPlan:
                    return SdpSemantics.UnifiedPlan;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static TlsCertPolicy ToPlatformNative(this Enums.TlsCertPolicy nativePort)
        {
            switch (nativePort)
            {
                case Enums.TlsCertPolicy.Secure:
                    return TlsCertPolicy.TlsCertPolicySecure;
                case Enums.TlsCertPolicy.InsecureNoCheck:
                    return TlsCertPolicy.TlsCertPolicyInsecureNoCheck;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static SessionDescription.Type ToPlatformNative(this Enums.SdpType nativePort)
        {
            switch (nativePort)
            {
                case Enums.SdpType.Answer:
                    return SessionDescription.Type.Answer;
                case Enums.SdpType.Offer:
                    return SessionDescription.Type.Offer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static PeerConnectionState ToPlatformNative(this Enums.PeerConnectionState nativePort)
        {
            switch (nativePort)
            {
                case Enums.PeerConnectionState.New:
                    return PeerConnectionState.New;
                case Enums.PeerConnectionState.Connecting:
                    return PeerConnectionState.Connecting;
                case Enums.PeerConnectionState.Connected:
                    return PeerConnectionState.Connected;
                case Enums.PeerConnectionState.Disconnected:
                    return PeerConnectionState.Disconnected;
                case Enums.PeerConnectionState.Failed:
                    return PeerConnectionState.Failed;
                case Enums.PeerConnectionState.Closed:
                    return PeerConnectionState.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }




        public static Enums.SdpType ToNativePort(this SessionDescription.Type platformNative)
        {
            if (platformNative == SessionDescription.Type.Answer)
                return Enums.SdpType.Answer;
            if (platformNative == SessionDescription.Type.Offer)
                return Enums.SdpType.Offer;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.MediaStreamTrackState ToNativePort(this MediaStreamTrack.State platformNative)
        {
            if (platformNative == MediaStreamTrack.State.Live)
                return Enums.MediaStreamTrackState.Live;
            if (platformNative == MediaStreamTrack.State.Ended)
                return Enums.MediaStreamTrackState.Ended;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static IceConnectionState ToNativePort(this IceConnectionState nativePort)
        {
            if (nativePort == IceConnectionState.Checking)
                return IceConnectionState.Checking;
            if (nativePort == IceConnectionState.Closed)
                return IceConnectionState.Closed;
            if (nativePort == IceConnectionState.Completed)
                return IceConnectionState.Completed;
            if (nativePort == IceConnectionState.Connected)
                return IceConnectionState.Connected;
            if (nativePort == IceConnectionState.Disconnected)
                return IceConnectionState.Disconnected;
            if (nativePort == IceConnectionState.Failed)
                return IceConnectionState.Failed;
            if (nativePort == IceConnectionState.New)
                return IceConnectionState.New;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static IceGatheringState ToNativePort(this IceGatheringState nativePort)
        {
            if (nativePort == IceGatheringState.Complete)
                return IceGatheringState.Complete;
            if (nativePort == IceGatheringState.Gathering)
                return IceGatheringState.Gathering;
            if (nativePort == IceGatheringState.New)
                return IceGatheringState.New;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static SignalingState ToNativePort(this SignalingState nativePort)
        {
            if (nativePort == SignalingState.Closed)
                return SignalingState.Closed;
            if (nativePort == SignalingState.Stable)
                return SignalingState.Stable;
            if (nativePort == SignalingState.HaveLocalOffer)
                return SignalingState.HaveLocalOffer;
            if (nativePort == SignalingState.HaveLocalPranswer)
                return SignalingState.HaveLocalPrAnswer;
            if (nativePort == SignalingState.HaveRemoteOffer)
                return SignalingState.HaveRemoteOffer;
            if (nativePort == SignalingState.HaveRemotePranswer)
                return SignalingState.HaveRemotePrAnswer;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static DataChannelState ToNativePort(this DataChannel.State nativePort)
        {
            if (nativePort == DataChannel.State.Closed)
                return DataChannelState.Closed;
            if (nativePort == DataChannel.State.Closing)
                return DataChannelState.Closing;
            if (nativePort == DataChannel.State.Connecting)
                return DataChannelState.Connecting;
            if (nativePort == DataChannel.State.Open)
                return DataChannelState.Open;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static PeerConnectionState ToNativePort(this PeerConnectionState nativePort)
        {
            if (nativePort == PeerConnectionState.Closed)
                return PeerConnectionState.Closed;
            if (nativePort == PeerConnectionState.Connected)
                return PeerConnectionState.Connected;
            if (nativePort == PeerConnectionState.Connecting)
                return PeerConnectionState.Connecting;
            if (nativePort == PeerConnectionState.Disconnected)
                return PeerConnectionState.Disconnected;
            if (nativePort == PeerConnectionState.Failed)
                return PeerConnectionState.Failed;
            if (nativePort == PeerConnectionState.New)
                return PeerConnectionState.New;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static SourceState ToNativePort(this MediaSource.State nativePort)
        {
            if (nativePort == MediaSource.State.Ended)
                return SourceState.Ended;
            if (nativePort == MediaSource.State.Initializing)
                return SourceState.Initializing;
            if (nativePort == MediaSource.State.Live)
                return SourceState.Live;
            if (nativePort == MediaSource.State.Muted)
                return SourceState.Muted;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static RtpMediaType ToNativePort(this MediaStreamTrack.MediaType nativePort)
        {
            if (nativePort == MediaStreamTrack.MediaType.MediaTypeAudio)
                return RtpMediaType.Audio;
            if (nativePort == MediaStreamTrack.MediaType.MediaTypeVideo)
                return RtpMediaType.Video;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }

        public static RtpTransceiverDirection ToNativePort(this RtpTransceiver.RtpTransceiverDirection nativePort)
        {
            if (nativePort == RtpTransceiver.RtpTransceiverDirection.Inactive)
                return RtpTransceiverDirection.Inactive;
            if (nativePort == RtpTransceiver.RtpTransceiverDirection.RecvOnly)
                return RtpTransceiverDirection.RecvOnly;
            if (nativePort == RtpTransceiver.RtpTransceiverDirection.SendOnly)
                return RtpTransceiverDirection.SendOnly;
            if (nativePort == RtpTransceiver.RtpTransceiverDirection.SendRecv)
                return RtpTransceiverDirection.SendRecv;
            throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
        }
    }
}
}
