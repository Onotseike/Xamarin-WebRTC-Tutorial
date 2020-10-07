// onotseike@hotmail.comPaula Aliu
using System;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformFactory : INativeFactory
    {
        public Core.RTCCertificate CreateCertificate(EncryptionKeyType encryptionKeyType, long expiries)
        {
            return RTCCertificate.GenerateCertificateWithParams(new NSDictionary<NSString, NSObject>(new[] { "expires".ToPlatformNative(), "name".ToPlatformNative() }, new NSObject[] { new NSNumber(expiries), encryptionKeyType.ToStringNative() })).ToNativePort();
        }

        public IPeerConnectionFactory CreatePeerConnectionFactory() => new PlatformPeerConnectionFactory();

        public void ShutDownInternalTracer() => RTCTracing.RTCShutdownInternalTracer();

        public bool StartInternalCapture(string filePath) => RTCTracing.RTCStartInternalCapture(filePath);

        public void StopInternalTracingCapture() => RTCTracing.RTCStopInternalCapture();
    }
}
