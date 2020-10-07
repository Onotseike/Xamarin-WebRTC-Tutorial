// onotseike@hotmail.comPaula Aliu
using System;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Enums;
using WebRTC.Unified.iOS.Enums;

namespace WebRTC.Unified.iOS.Extensions
{
    internal static class EnumExtensions
    {

        #region ToPlatformNative

        public static RTCSourceState ToPlatformNative(this SourceState nativePort) => (RTCSourceState)nativePort;

        public static RTCMediaStreamTrackState ToPlatformNative(this MediaStreamTrackState nativePort) => (RTCMediaStreamTrackState)nativePort;

        public static RTCIceTransportPolicy ToPlatformNative(this IceTransportPolicy nativePort) => (RTCIceTransportPolicy)nativePort;

        public static RTCBundlePolicy ToPlatformNative(this BundlePolicy nativePort) => (RTCBundlePolicy)nativePort;

        public static RTCRtcpMuxPolicy ToPlatformNative(this RtcpMuxPolicy nativePort) => (RTCRtcpMuxPolicy)nativePort;

        public static RTCTcpCandidatePolicy ToPlatformNative(this TcpCandidatePolicy nativePort) => (RTCTcpCandidatePolicy)nativePort;

        public static RTCCandidateNetworkPolicy ToPlatformNative(this CandidateNetworkPolicy nativePort) => (RTCCandidateNetworkPolicy)nativePort;

        public static RTCContinualGatheringPolicy ToPlatformNative(this ContinualGatheringPolicy nativePort) => (RTCContinualGatheringPolicy)nativePort;

        public static RTCEncryptionKeyType ToPlatformNative(this EncryptionKeyType nativePort) => (RTCEncryptionKeyType)nativePort;

        public static RTCSdpSemantics ToPlatformNative(this SdpSemantics nativePort) => (RTCSdpSemantics)nativePort;

        public static RTCDataChannelState ToPlatformNative(this DataChannelState nativePort) => (RTCDataChannelState)nativePort;

        public static RTCTlsCertPolicy ToPlatformNative(this TlsCertPolicy nativePort) => (RTCTlsCertPolicy)nativePort;

        public static RTCSignalingState ToPlatformNative(this SignalingState nativePort) => (RTCSignalingState)nativePort;

        public static RTCIceConnectionState ToPlatformNative(this IceConnectionState nativePort) => (RTCIceConnectionState)nativePort;

        public static RTCPeerConnectionState ToPlatformNative(this PeerConnectionState nativePort) => (RTCPeerConnectionState)nativePort;

        public static RTCIceGatheringState ToPlatformNative(this IceGatheringState nativePort) => (RTCIceGatheringState)nativePort;

        public static RTCStatsOutputLevel ToPlatformNative(this StatsOutputLevel nativePort) => (RTCStatsOutputLevel)nativePort;

        public static RTCRtpMediaType ToPlatformNative(this RtpMediaType nativePort) => (RTCRtpMediaType)nativePort;

        public static RTCRtpTransceiverDirection ToPlatformNative(this RtpTransceiverDirection nativePort) => (RTCRtpTransceiverDirection)nativePort;

        public static RTCSdpType ToPlatformNative(this SdpType nativePort) => (RTCSdpType)nativePort;

        public static RTCFileLoggerSeverity ToPlatformNative(this FileLoggerSeverity nativePort) => (RTCFileLoggerSeverity)nativePort;

        public static RTCDeviceType ToPlatformNative(this DeviceType nativePort) => (RTCDeviceType)nativePort;

        public static RTCH264Level ToPlatformNative(this H264Level nativePort) => (RTCH264Level)nativePort;

        public static RTCDispatcherQueueType ToPlatformNative(this DispatcherQueueType nativePort) => (RTCDispatcherQueueType)nativePort;

        public static RTCFrameType ToPlatformNative(this FrameType nativePort) => (RTCFrameType)nativePort;

        public static RTCVideoRotation ToPlatformNative(this VideoRotation nativePort) => (RTCVideoRotation)nativePort;

        public static RTCVideoContentType ToPlatformNative(this VideoContentType nativePort) => (RTCVideoContentType)nativePort;

        public static RTCVideoCodecMode ToPlatformNative(this VideoCodecMode nativePort) => (RTCVideoCodecMode)nativePort;

        public static RTCDegradationPreference ToPlatformNative(this DegradationPreference nativePort) => (RTCDegradationPreference)nativePort;

        public static RTCFileLoggerRotationType ToPlatformNative(this FileLoggerRotationType nativePort) => (RTCFileLoggerRotationType)nativePort;

        public static RTCH264PacketizationMode ToPlatformNative(this H264PacketizationMode nativePort) => (RTCH264PacketizationMode)nativePort;

        public static RTCH264Profile ToPlatformNative(this H264Profile nativePort) => (RTCH264Profile)nativePort;

        public static RTCPriority ToPlatformNative(this Priority nativePort) => (RTCPriority)nativePort;

        public static NSString ToStringNative(this EncryptionKeyType self)
        {
            switch (self)
            {
                case EncryptionKeyType.Rsa:
                    return new NSString("RSASSA-PKCS1-v1_5");
                case EncryptionKeyType.Ecdsa:
                    return new NSString("ECDSA");
                default:
                    throw new ArgumentOutOfRangeException(nameof(self), self, null);
            }
        }

        #endregion


        #region ToNativePort

        public static SourceState ToNativePort(this RTCSourceState platformNative) => (SourceState)platformNative;

        public static MediaStreamTrackState ToNativePort(this RTCMediaStreamTrackState platformNative) => (MediaStreamTrackState)platformNative;

        public static IceTransportPolicy ToNativePort(this RTCIceTransportPolicy platformNative) => (IceTransportPolicy)platformNative;

        public static BundlePolicy ToNativePort(this RTCBundlePolicy platformNative) => (BundlePolicy)platformNative;

        public static RtcpMuxPolicy ToNativePort(this RTCRtcpMuxPolicy platformNative) => (RtcpMuxPolicy)platformNative;

        public static TcpCandidatePolicy ToNativePort(this RTCTcpCandidatePolicy platformNative) => (TcpCandidatePolicy)platformNative;

        public static CandidateNetworkPolicy ToNativePort(this RTCCandidateNetworkPolicy platformNative) => (CandidateNetworkPolicy)platformNative;

        public static ContinualGatheringPolicy ToNativePort(this RTCContinualGatheringPolicy platformNative) => (ContinualGatheringPolicy)platformNative;

        public static EncryptionKeyType ToNativePort(this RTCEncryptionKeyType platformNative) => (EncryptionKeyType)platformNative;

        public static SdpSemantics ToNativePort(this RTCSdpSemantics platformNative) => (SdpSemantics)platformNative;

        public static DataChannelState ToNativePort(this RTCDataChannelState platformNative) => (DataChannelState)platformNative;

        public static TlsCertPolicy ToNativePort(this RTCTlsCertPolicy platformNative) => (TlsCertPolicy)platformNative;

        public static SignalingState ToNativePort(this RTCSignalingState platformNative) => (SignalingState)platformNative;

        public static IceConnectionState ToNativePort(this RTCIceConnectionState platformNative) => (IceConnectionState)platformNative;

        public static PeerConnectionState ToNativePort(this RTCPeerConnectionState platformNative) => (PeerConnectionState)platformNative;

        public static IceGatheringState ToNativePort(this RTCIceGatheringState platformNative) => (IceGatheringState)platformNative;

        public static StatsOutputLevel ToNativePort(this RTCStatsOutputLevel platformNative) => (StatsOutputLevel)platformNative;

        public static RtpMediaType ToNativePort(this RTCRtpMediaType platformNative) => (RtpMediaType)platformNative;

        public static RtpTransceiverDirection ToNativePort(this RTCRtpTransceiverDirection platformNative) => (RtpTransceiverDirection)platformNative;

        public static SdpType ToNativePort(this RTCSdpType platformNative) => (SdpType)platformNative;

        public static FileLoggerSeverity ToNativePort(this RTCFileLoggerSeverity platformNative) => (FileLoggerSeverity)platformNative;

        public static DeviceType ToNativePort(this RTCDeviceType platformNative) => (DeviceType)platformNative;

        public static H264Level ToNativePort(this RTCH264Level platformNative) => (H264Level)platformNative;

        public static DispatcherQueueType ToNativePort(this RTCDispatcherQueueType platformNative) => (DispatcherQueueType)platformNative;

        public static FrameType ToNativePort(this RTCFrameType platformNative) => (FrameType)platformNative;

        public static VideoRotation ToNativePort(this RTCVideoRotation platformNative) => (VideoRotation)platformNative;

        public static VideoContentType ToNativePort(this RTCVideoContentType platformNative) => (VideoContentType)platformNative;

        public static VideoCodecMode ToNativePort(this RTCVideoCodecMode platformNative) => (VideoCodecMode)platformNative;

        public static DegradationPreference ToNativePort(this RTCDegradationPreference platformNative) => (DegradationPreference)platformNative;

        public static FileLoggerRotationType ToNativePort(this RTCFileLoggerRotationType platformNative) => (FileLoggerRotationType)platformNative;

        public static H264PacketizationMode ToNativePort(this RTCH264PacketizationMode platformNative) => (H264PacketizationMode)platformNative;

        public static H264Profile ToNativePort(this RTCH264Profile platformNative) => (H264Profile)platformNative;

        public static Priority ToNativePort(this RTCPriority platformNative) => (Priority)platformNative;

        #endregion

    }
}
