// onotseike@hotmail.comPaula Aliu
using System;

using Web.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface INativeFactory
    {
        IPeerConnectionFactory CreatePeerConnectionFactory();

        RTCCertificate CreateCertificate(EncryptionKeyType encryptionKeyType, long expiries);

        void StopInternalTracingCapture();
        void ShutDownInternalTracer();
        void SetupInternalTracer();
        bool StartInternalCapture(string filePath);

    }
}
