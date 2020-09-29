// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;
using System.Drawing.Text;

using Newtonsoft.Json;

using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core
{
    public class RTCCertificate
    {
        #region Properties

        [JsonProperty("private_key")]
        public string PrivateKey { get; }

        [JsonProperty("certificate")]
        public string Certificate { get; }

        private const long DefaultExpiry = long.MaxValue;


        #endregion

        #region Constructor(s)

        public RTCCertificate(string privateKey, string certificate)
        {
            PrivateKey = privateKey;
            Certificate = certificate;
        }

        #endregion

        #region CertificateGenerator Method(s)

        public static RTCCertificate GenerateCertificateWithParams(Dictionary<string, string> parameters)
        {
            EncryptionKeyType encryptionKeyType = (EncryptionKeyType)Enum.Parse(typeof(EncryptionKeyType), parameters["private_key"]);
            long expiry = long.Parse(parameters["certificate"]);
            return (parameters.ContainsKey("private_key") && parameters.ContainsKey("certificate")) ? NativeFactory.CreateCertificate(encryptionKeyType, expiry) : null;
        }

        #endregion
    }
}
