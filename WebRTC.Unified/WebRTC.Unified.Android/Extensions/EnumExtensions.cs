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
using IceConnectionState = Org.Webrtc.PeerConnection.IceConnectionState;
using MediaStreamTrack = Org.Webrtc.MediaStreamTrack;
using IceGatheringState = Org.Webrtc.PeerConnection.IceGatheringState;
using SignalingState = Org.Webrtc.PeerConnection.SignalingState;
using DataChannel = Org.Webrtc.DataChannel;
using MediaSource = Org.Webrtc.MediaSource;
using ScalingType = Org.Webrtc.RendererCommon.ScalingType;
using FrameType = Org.Webrtc.EncodedImage.FrameType;
using VideoCodecStatus = Org.Webrtc.VideoCodecStatus;
using AdapterType = Org.Webrtc.PeerConnection.AdapterType;
using PortPrunePolicy = Org.Webrtc.PeerConnection.PortPrunePolicy;
using MediaType = Org.Webrtc.MediaStreamTrack.MediaType;
using ConnectionType = Org.Webrtc.NetworkMonitorAutoDetect.ConnectionType;
using TextureBufferType = Org.Webrtc.VideoFrame.TextureBufferType;
using AudioRecordStartErrorCode = Org.Webrtc.Voiceengine.WebRtcAudioRecord.AudioRecordStartErrorCode;
using AudioTrackStartErrorCode = Org.Webrtc.Voiceengine.WebRtcAudioTrack.AudioTrackStartErrorCode;
using RtpTransceiverDirection = Org.Webrtc.RtpTransceiver.RtpTransceiverDirection;


namespace WebRTC.Unified.Extensions
{
    internal static class EnumExtensions
    {

        #region ToPlatformNative

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
                case Enums.SdpType.PrAnswer:
                    return SessionDescription.Type.Pranswer;
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

        public static IceConnectionState ToPlatformNative(this Enums.IceConnectionState nativePort)
        {
            switch (nativePort)
            {
                case Enums.IceConnectionState.New:
                    return IceConnectionState.New;
                case Enums.IceConnectionState.Checking:
                    return IceConnectionState.Checking;
                case Enums.IceConnectionState.Connected:
                    return IceConnectionState.Connected;
                case Enums.IceConnectionState.Completed:
                    return IceConnectionState.Completed;
                case Enums.IceConnectionState.Failed:
                    return IceConnectionState.Failed;
                case Enums.IceConnectionState.Disconnected:
                    return IceConnectionState.Disconnected;
                case Enums.IceConnectionState.Closed:
                    return IceConnectionState.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null); ;
            }
        }

        public static IceGatheringState ToPlatformNative(this Enums.IceGatheringState nativePort)
        {
            switch (nativePort)
            {
                case Enums.IceGatheringState.New:
                    return IceGatheringState.New;
                case Enums.IceGatheringState.Gathering:
                    return IceGatheringState.Gathering;
                case Enums.IceGatheringState.Complete:
                    return IceGatheringState.Complete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null); ;
            }
        }

        public static SignalingState ToPlatformNative(this Enums.SignalingState nativePort)
        {
            switch (nativePort)
            {
                case Enums.SignalingState.Stable:
                    return SignalingState.Stable;
                case Enums.SignalingState.HaveLocalOffer:
                    return SignalingState.HaveLocalOffer;
                case Enums.SignalingState.HaveLocalPrAnswer:
                    return SignalingState.HaveLocalPranswer;
                case Enums.SignalingState.HaveRemoteOffer:
                    return SignalingState.HaveRemoteOffer;
                case Enums.SignalingState.HaveRemotePrAnswer:
                    return SignalingState.HaveRemotePranswer;
                case Enums.SignalingState.Closed:
                    return SignalingState.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static MediaStreamTrack.State ToPlatformNative(this Enums.MediaStreamTrackState nativePort)
        {
            switch (nativePort)
            {
                case Enums.MediaStreamTrackState.Live:
                    return MediaStreamTrack.State.Live;
                case Enums.MediaStreamTrackState.Ended:
                    return MediaStreamTrack.State.Ended;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static DataChannel.State ToPlatformNative(this Enums.DataChannelState nativePort)
        {
            switch (nativePort)
            {
                case Enums.DataChannelState.Connecting:
                    return DataChannel.State.Connecting;
                case Enums.DataChannelState.Open:
                    return DataChannel.State.Open;
                case Enums.DataChannelState.Closing:
                    return DataChannel.State.Closing;
                case Enums.DataChannelState.Closed:
                    return DataChannel.State.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static ScalingType ToPlatformNative(this Enums.ScalingType nativePort)
        {
            switch (nativePort)
            {
                case Enums.ScalingType.AspectFit:
                    return ScalingType.ScaleAspectFit;
                case Enums.ScalingType.AspectFill:
                    return ScalingType.ScaleAspectFill;
                case Enums.ScalingType.AspectBalanced:
                    return ScalingType.ScaleAspectBalanced;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static FrameType ToPlatformNative(this Enums.FrameType nativePort)
        {
            switch (nativePort)
            {
                case Enums.FrameType.EmptyFrame:
                    return FrameType.EmptyFrame;
                case Enums.FrameType.VideoFrameKey:
                    return FrameType.VideoFrameKey;
                case Enums.FrameType.VideoFrameDelta:
                    return FrameType.VideoFrameDelta;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static VideoCodecStatus ToPlatformNative(this Android.Enums.VideoCodecStatus nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.VideoCodecStatus.Error:
                    return VideoCodecStatus.Error;
                case Android.Enums.VideoCodecStatus.ErrParmater:
                    return VideoCodecStatus.ErrParameter;
                case Android.Enums.VideoCodecStatus.ErrRequestSli:
                    return VideoCodecStatus.ErrRequestSli;
                case Android.Enums.VideoCodecStatus.ErrSize:
                    return VideoCodecStatus.ErrSize;
                case Android.Enums.VideoCodecStatus.FallbackSoftware:
                    return VideoCodecStatus.FallbackSoftware;
                case Android.Enums.VideoCodecStatus.LevelExceeded:
                    return VideoCodecStatus.LevelExceeded;
                case Android.Enums.VideoCodecStatus.Memory:
                    return VideoCodecStatus.Memory;
                case Android.Enums.VideoCodecStatus.NoOutput:
                    return VideoCodecStatus.NoOutput;
                case Android.Enums.VideoCodecStatus.Ok:
                    return VideoCodecStatus.Ok;
                case Android.Enums.VideoCodecStatus.RequestSli:
                    return VideoCodecStatus.RequestSli;
                case Android.Enums.VideoCodecStatus.TargetBitrateOvershoot:
                    return VideoCodecStatus.TargetBitrateOvershoot;
                case Android.Enums.VideoCodecStatus.Timeout:
                    return VideoCodecStatus.Timeout;
                case Android.Enums.VideoCodecStatus.Uninitialized:
                    return VideoCodecStatus.Uninitialized;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static AdapterType ToPlatformNative(this Android.Enums.AdapterType nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.AdapterType.AdapterTypeAny:
                    return AdapterType.AdapterTypeAny;
                case Android.Enums.AdapterType.Cellular:
                    return AdapterType.Cellular;
                case Android.Enums.AdapterType.Cellular2g:
                    return AdapterType.Cellular2g;
                case Android.Enums.AdapterType.Cellular3g:
                    return AdapterType.Cellular3g;
                case Android.Enums.AdapterType.Cellular4g:
                    return AdapterType.Cellular4g;
                case Android.Enums.AdapterType.Cellular5g:
                    return AdapterType.Cellular5g;
                case Android.Enums.AdapterType.Ethernet:
                    return AdapterType.Ethernet;
                case Android.Enums.AdapterType.Loopback:
                    return AdapterType.Loopback;
                case Android.Enums.AdapterType.Unknown:
                    return AdapterType.Unknown;
                case Android.Enums.AdapterType.Vpn:
                    return AdapterType.Vpn;
                case Android.Enums.AdapterType.Wifi:
                    return AdapterType.Wifi;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }

        }

        public static PortPrunePolicy ToPlatformNative(this Android.Enums.PortPrunePolicy nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.PortPrunePolicy.KeepReadyFirst:
                    return PortPrunePolicy.KeepFirstReady;
                case Android.Enums.PortPrunePolicy.NoPrune:
                    return PortPrunePolicy.NoPrune;
                case Android.Enums.PortPrunePolicy.PruneBasedOnPriority:
                    return PortPrunePolicy.PruneBasedOnPriority;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static MediaType ToPlatformNative(this Enums.RtpMediaType nativePort)
        {
            switch (nativePort)
            {
                case Enums.RtpMediaType.Audio:
                    return MediaType.MediaTypeAudio;
                case Enums.RtpMediaType.Video:
                    return MediaType.MediaTypeVideo;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null); ;
            }
        }

        public static ConnectionType ToPlatformNative(this Android.Enums.ConnectionType nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.ConnectionType.Connection2g:
                    return ConnectionType.Connection2g;
                case Android.Enums.ConnectionType.Connection3g:
                    return ConnectionType.Connection3g;
                case Android.Enums.ConnectionType.Connection4g:
                    return ConnectionType.Connection4g;
                case Android.Enums.ConnectionType.Connection5g:
                    return ConnectionType.Connection5g;
                case Android.Enums.ConnectionType.ConnectionBluetooth:
                    return ConnectionType.ConnectionBluetooth;
                case Android.Enums.ConnectionType.ConnectionEthernet:
                    return ConnectionType.ConnectionEthernet;
                case Android.Enums.ConnectionType.ConnectionNone:
                    return ConnectionType.ConnectionNone;
                case Android.Enums.ConnectionType.ConnectionUnknown:
                    return ConnectionType.ConnectionUnknown;
                case Android.Enums.ConnectionType.ConnectionUnknownCellular:
                    return ConnectionType.ConnectionUnknownCellular;
                case Android.Enums.ConnectionType.ConnectionVpn:
                    return ConnectionType.ConnectionVpn;
                case Android.Enums.ConnectionType.ConnectionWifi:
                    return ConnectionType.ConnectionWifi;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static TextureBufferType ToPlatformNative(this Android.Enums.TextureBufferType nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.TextureBufferType.Oes:
                    return TextureBufferType.Oes;
                case Android.Enums.TextureBufferType.Rgb:
                    return TextureBufferType.Rgb;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static AudioRecordStartErrorCode ToPlatformNative(this Android.Enums.AudioRecordStartErrorCode nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.AudioRecordStartErrorCode.AudioRecordStartException:
                    return AudioRecordStartErrorCode.AudioRecordStartException;
                case Android.Enums.AudioRecordStartErrorCode.AudioRecordStartStateMismatch:
                    return AudioRecordStartErrorCode.AudioRecordStartStateMismatch;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static AudioTrackStartErrorCode ToNativePlatform(this Android.Enums.AudioTrackStartErrorCode nativePort)
        {
            switch (nativePort)
            {
                case Android.Enums.AudioTrackStartErrorCode.AudioTrackStartException:
                    return AudioTrackStartErrorCode.AudioTrackStartException;
                case Android.Enums.AudioTrackStartErrorCode.AudioTrackStartStateMismatch:
                    return AudioTrackStartErrorCode.AudioTrackStartStateMismatch;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        public static RtpTransceiverDirection ToNativePlatform(this Enums.RtpTransceiverDirection nativePort)
        {
            switch (nativePort)
            {
                case Enums.RtpTransceiverDirection.SendRecv:
                    return RtpTransceiverDirection.SendRecv;
                case Enums.RtpTransceiverDirection.SendOnly:
                    return RtpTransceiverDirection.SendOnly;
                case Enums.RtpTransceiverDirection.RecvOnly:
                    return RtpTransceiverDirection.RecvOnly;
                case Enums.RtpTransceiverDirection.Inactive:
                    return RtpTransceiverDirection.Inactive;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nativePort), nativePort, null);
            }
        }

        #endregion


        #region ToNativePort

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

        public static Enums.IceConnectionState ToNativePort(this IceConnectionState platformNative)
        {
            if (platformNative == IceConnectionState.Checking)
                return Enums.IceConnectionState.Checking;
            if (platformNative == IceConnectionState.Closed)
                return Enums.IceConnectionState.Closed;
            if (platformNative == IceConnectionState.Completed)
                return Enums.IceConnectionState.Completed;
            if (platformNative == IceConnectionState.Connected)
                return Enums.IceConnectionState.Connected;
            if (platformNative == IceConnectionState.Disconnected)
                return Enums.IceConnectionState.Disconnected;
            if (platformNative == IceConnectionState.Failed)
                return Enums.IceConnectionState.Failed;
            if (platformNative == IceConnectionState.New)
                return Enums.IceConnectionState.New;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.IceGatheringState ToNativePort(this IceGatheringState platformNative)
        {
            if (platformNative == IceGatheringState.Complete)
                return Enums.IceGatheringState.Complete;
            if (platformNative == IceGatheringState.Gathering)
                return Enums.IceGatheringState.Gathering;
            if (platformNative == IceGatheringState.New)
                return Enums.IceGatheringState.New;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.SignalingState ToNativePort(this SignalingState platformNative)
        {
            if (platformNative == SignalingState.Closed)
                return Enums.SignalingState.Closed;
            if (platformNative == SignalingState.Stable)
                return Enums.SignalingState.Stable;
            if (platformNative == SignalingState.HaveLocalOffer)
                return Enums.SignalingState.HaveLocalOffer;
            if (platformNative == SignalingState.HaveLocalPranswer)
                return Enums.SignalingState.HaveLocalPrAnswer;
            if (platformNative == SignalingState.HaveRemoteOffer)
                return Enums.SignalingState.HaveRemoteOffer;
            if (platformNative == SignalingState.HaveRemotePranswer)
                return Enums.SignalingState.HaveRemotePrAnswer;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.DataChannelState ToNativePort(this DataChannel.State platformNative)
        {
            if (platformNative == DataChannel.State.Closed)
                return Enums.DataChannelState.Closed;
            if (platformNative == DataChannel.State.Closing)
                return Enums.DataChannelState.Closing;
            if (platformNative == DataChannel.State.Connecting)
                return Enums.DataChannelState.Connecting;
            if (platformNative == DataChannel.State.Open)
                return Enums.DataChannelState.Open;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.PeerConnectionState ToNativePort(this PeerConnectionState platformNative)
        {
            if (platformNative == PeerConnectionState.Closed)
                return Enums.PeerConnectionState.Closed;
            if (platformNative == PeerConnectionState.Connected)
                return Enums.PeerConnectionState.Connected;
            if (platformNative == PeerConnectionState.Connecting)
                return Enums.PeerConnectionState.Connecting;
            if (platformNative == PeerConnectionState.Disconnected)
                return Enums.PeerConnectionState.Disconnected;
            if (platformNative == PeerConnectionState.Failed)
                return Enums.PeerConnectionState.Failed;
            if (platformNative == PeerConnectionState.New)
                return Enums.PeerConnectionState.New;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.SourceState ToNativePort(this MediaSource.State platformNative)
        {
            if (platformNative == MediaSource.State.Ended)
                return Enums.SourceState.Ended;
            if (platformNative == MediaSource.State.Initializing)
                return Enums.SourceState.Initializing;
            if (platformNative == MediaSource.State.Live)
                return Enums.SourceState.Live;
            if (platformNative == MediaSource.State.Muted)
                return Enums.SourceState.Muted;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.RtpMediaType ToNativePort(this MediaStreamTrack.MediaType platformNative)
        {
            if (platformNative == MediaStreamTrack.MediaType.MediaTypeAudio)
                return Enums.RtpMediaType.Audio;
            if (platformNative == MediaStreamTrack.MediaType.MediaTypeVideo)
                return Enums.RtpMediaType.Video;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }

        public static Enums.RtpTransceiverDirection ToNativePort(this RtpTransceiverDirection platformNative)
        {
            if (platformNative == RtpTransceiverDirection.Inactive)
                return Enums.RtpTransceiverDirection.Inactive;
            if (platformNative == RtpTransceiverDirection.RecvOnly)
                return Enums.RtpTransceiverDirection.RecvOnly;
            if (platformNative == RtpTransceiverDirection.SendOnly)
                return Enums.RtpTransceiverDirection.SendOnly;
            if (platformNative == RtpTransceiverDirection.SendRecv)
                return Enums.RtpTransceiverDirection.SendRecv;
            throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
        }


        #endregion

    }
}
