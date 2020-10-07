// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Extensions
{
    /// <summary>
    /// This extension converts a .NET object to its WebRTC Native counterpart.
    /// </summary>
    public static class NativeExtension
    {
        public static T ToPlatformNative<T>(this INativeObject nativePort) => (T)nativePort.NativeObject;
    }
}
