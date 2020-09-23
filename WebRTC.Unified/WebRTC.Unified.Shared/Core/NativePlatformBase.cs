// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Core
{
    /// <summary>
    /// This class serves a base of all Native(Android and iOS) implementations of specific WebRTC interface implementation.
    /// </summary>
    public abstract class NativePlatformBase : INativeObject
    {
        protected NativePlatformBase(object nativeObject)
        {
            NativeObject = nativeObject;
        }

        public NativePlatformBase()
        {

        }

        public object NativeObject { get; protected set; }

        public virtual void Dispose()
        {
            if (NativeObject == null)
                throw new NullReferenceException($"NativeObject is NULL which is not allowed");
            if (NativeObject is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
