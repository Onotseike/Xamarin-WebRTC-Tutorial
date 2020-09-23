// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Enums;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Core
{
    /// <summary>
    /// A static class used to setup the initial processes in accordance to the WebRTC guidlines.
    /// </summary>
    public static class NativeFactory
    {
        private static INativeFactory _nativeFactory;
        public static void Initalize(INativeFactory nativeFactory) => _nativeFactory = nativeFactory;

        internal static RTCCertificate CreateCertificate(EncryptionKeyType encryptionKeyType, long expires)
        {
            IsNativeFactoryInitialized();
            return _nativeFactory?.CreateCertificate(encryptionKeyType, expires);
        }

        internal static IPeerConnectionFactory CreatePeerConnectionFactory()
        {
            IsNativeFactoryInitialized();
            return _nativeFactory?.CreatePeerConnectionFactory();
        }

        internal static void StopInternalTracingCapture()
        {
            IsNativeFactoryInitialized();
            _nativeFactory.StopInternalTracingCapture();
        }
        internal static void ShutDownInternalTracer()
        {
            IsNativeFactoryInitialized();
            _nativeFactory.ShutDownInternalTracer();
        }
        //internal static void SetupInternalTracer()
        //{
        //    IsNativeFactoryInitialized();
        //    _nativeFactory.SetupInternalTracer();
        //}
        internal static bool StartInternalCapture(string filePath)
        {
            IsNativeFactoryInitialized();
            return _nativeFactory.StartInternalCapture(filePath);
        }


        private static void IsNativeFactoryInitialized()
        {
            if (_nativeFactory == null) throw new Exception($"NativeFactory Initialize method was not called.");
        }
    }
}
