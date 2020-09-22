// onotseike@hotmail.comPaula Aliu
using System;

using Newtonsoft.Json;

namespace WebRTC.Unified.Core
{
    public class CryptoOptions
    {
        #region Properties

        [JsonProperty("srtpEnableGcmCryptoSuites")]
        public bool SrtpEnableGcmCryptoSuites { get; set; }

        // @property (assign, nonatomic) BOOL srtpEnableAes128Sha1_32CryptoCipher;
        [JsonProperty("srtpEnableAes128Sha1_32CryptoCipher")]
        public bool SrtpEnableAes128Sha1_32CryptoCipher { get; set; }

        // @property (assign, nonatomic) BOOL srtpEnableEncryptedRtpHeaderExtensions;
        [JsonProperty("srtpEnableEncryptedRtpHeaderExtensions")]
        public bool SrtpEnableEncryptedRtpHeaderExtensions { get; set; }

        // @property (assign, nonatomic) BOOL sframeRequireFrameEncryption;
        [JsonProperty("sframeRequireFrameEncryption")]
        public bool SframeRequireFrameEncryption { get; set; }

        #endregion

        #region Constructor(s)

        public CryptoOptions(bool srtpEnableGcmCryptoSuites, bool srtpEnableAes128Sha1_32CryptoCipher, bool srtpEnableEncryptedRtpHeaderExtensions, bool sframeRequireFrameEncryption)
        {
            SrtpEnableGcmCryptoSuites = srtpEnableGcmCryptoSuites;
            SrtpEnableAes128Sha1_32CryptoCipher = srtpEnableAes128Sha1_32CryptoCipher;
            SrtpEnableEncryptedRtpHeaderExtensions = srtpEnableEncryptedRtpHeaderExtensions;
            SframeRequireFrameEncryption = sframeRequireFrameEncryption;
        }

        #endregion

    }
}
