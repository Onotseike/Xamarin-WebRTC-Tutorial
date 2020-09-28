// onotseike@hotmail.comPaula Aliu
using Android.Content;

using Org.Webrtc;

using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformFactory : Core.Interfaces.INativeFactory
    {
        private Context _context;

        public PlatformFactory(Context context) => _context = context;

        public Core.RTCCertificate CreateCertificate(EncryptionKeyType encryptionKeyType, long expiries) => RtcCertificatePem.GenerateCertificate(encryptionKeyType.ToPlatformNative(), expiries).ToNativePort();

        public Core.Interfaces.IPeerConnectionFactory CreatePeerConnectionFactory() => new PlatformPeerConnectionFactory(_context);

        public void ShutDownInternalTracer() => PeerConnectionFactory.ShutdownInternalTracer();

        public bool StartInternalCapture(string filePath) => PeerConnectionFactory.StartInternalTracingCapture(filePath);

        public void StopInternalTracingCapture() => PeerConnectionFactory.StopInternalTracingCapture();
    }
}
