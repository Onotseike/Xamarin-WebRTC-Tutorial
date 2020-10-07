// onotseike@hotmail.comPaula Aliu
using Newtonsoft.Json;


using WebRTC.Unified.Core;
using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core
{
    public class RTCConfiguration
    {
        #region Properties

        [JsonProperty("enableDscp")]
        public bool EnableDscp { get; set; }

        [JsonProperty("iceServers")]
        public IceServer[] IceServers { get; set; }

        [JsonProperty("certificate")]
        public RTCCertificate Certificate { get; set; }

        [JsonProperty("iceTransportPolicy")]
        public IceTransportPolicy IceTransportPolicy { get; set; }

        [JsonProperty("bundlePolicy")]
        public BundlePolicy BundlePolicy { get; set; }

        [JsonProperty("rtcpMuxPolicy")]
        public RtcpMuxPolicy RtcpMuxPolicy { get; set; }

        [JsonProperty("tcpCandidatePolicy")]
        public TcpCandidatePolicy TcpCandidatePolicy { get; set; }

        [JsonProperty("candidateNetworkPolicy")]
        public CandidateNetworkPolicy CandidateNetworkPolicy { get; set; }

        [JsonProperty("continualGatheringPolicy")]
        public ContinualGatheringPolicy ContinualGatheringPolicy { get; set; }

        [JsonProperty("disableIPV6")]
        public bool DisableIPV6 { get; set; }

        [JsonProperty("disableIPV6OnWiFi")]
        public bool DisableIPV6OnWiFi { get; set; }

        [JsonProperty("maxIPv6Networks")]
        public int MaxIPv6Networks { get; set; }

        [JsonProperty("disableLinkLocalNetworks")]
        public bool DisableLinkLocalNetworks { get; set; }

        [JsonProperty("audioJitterBufferMaxPackets")]
        public int AudioJitterBufferMaxPackets { get; set; }

        [JsonProperty("audioJitterBufferFastAccelerate")]
        public bool AudioJitterBufferFastAccelerate { get; set; }

        [JsonProperty("iceConnectionReceivingTimeout")]
        public int IceConnectionReceivingTimeout { get; set; }

        [JsonProperty("iceBackupCandidatePairPingInterval")]
        public int IceBackupCandidatePairPingInterval { get; set; }

        [JsonProperty("keyType")]
        public EncryptionKeyType KeyType { get; set; }

        [JsonProperty("iceCandidatePoolSize")]
        public int IceCandidatePoolSize { get; set; }

        [JsonProperty("shouldPruneTurnPorts")]
        public bool ShouldPruneTurnPorts { get; set; }

        [JsonProperty("shouldPresumeWritableWhenFullyRelayed")]
        public bool ShouldPresumeWritableWhenFullyRelayed { get; set; }

        [JsonProperty("shouldSurfaceIceCandidatesOnIceTransportTypeChanged")]
        public bool ShouldSurfaceIceCandidatesOnIceTransportTypeChanged { get; set; }

        [JsonProperty("iceCheckMinInterval")]
        public int? IceCheckMinInterval { get; set; }

        [JsonProperty("sdpSemantics")]
        public SdpSemantics SdpSemantics { get; set; }

        [JsonProperty("activeResetSrtpParams")]
        public bool ActiveResetSrtpParams { get; set; }

        [JsonProperty("allowCodecSwitching")]
        public bool AllowCodecSwitching { get; set; }

        [JsonProperty("cryptoOptions")]
        CryptoOptions CryptoOptions { get; set; }

        [JsonProperty("rtcpAudioReportIntervalMs")]
        public int pAudioReportIntervalMs { get; set; }

        [JsonProperty("rtcpVideoReportIntervalMs")]
        public int pVideoReportIntervalMs { get; set; }

        [JsonProperty("useMediaTransport")]
        public bool UseMediaTransport { get; internal set; }

        [JsonProperty("useMediaTransportForDataChannels")]
        public bool UseMediaTransportForDataChannels { get; internal set; }

        [JsonProperty("enableDtlsSrtp")]
        public bool EnableDtlsSrtp { get; set; }

        [JsonProperty("combinedAudioVideoBwe")]
        public bool CombinedAudioVideoBwe { get; internal set; }

        [JsonProperty("enableCpuOveruseDetection")]
        public bool EnableCpuOverUseDetection { get; set; }

        [JsonProperty("enableRtpDataChannel")]
        public bool EnableRtpDataChannel { get; set; }

        [JsonProperty("iceCheckIntervalStrongConnectivityMs")]
        public int? IceCheckIntervalStrongConnectivityMs { get; set; }

        [JsonProperty("iceCheckIntervalWeakConnectivityMs")]
        public int? IceCheckIntervalWeakConnectivityMs { get; set; }

        // TODO: Create all properties in RTCConfig.
        #endregion
    }
}
