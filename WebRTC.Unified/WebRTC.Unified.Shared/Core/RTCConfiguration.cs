// onotseike@hotmail.comPaula Aliu
using Newtonsoft.Json;

using Web.Unified.Enums;

using WebRTC.Unified.Core;

namespace Web.Unified.Core
{
    public class Configuration
    {
        #region Properties

        [JsonProperty("enableDscp")]
        public bool EnableDscp { get; set; }

        [JsonProperty("iceServers")]
        public IceServer IceServers { get; set; }

        [JsonProperty("certificate")]
        public RTCCertificate Certificate { get; set; }

        [JsonProperty("iceTransportPolicy")]
        public IceTransportPolicy IceTransportPolicy { get; set; }

        [JsonProperty("bundlePolicy")]
        public BundlePolicy BundlePolicy { get; set; }

        [JsonProperty("rtcpMuxPolicy")]
        public RtcpMuxPolicy pMuxPolicy { get; set; }

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
        public int IceCheckMinInterval { get; set; }

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

        #endregion
    }
}
