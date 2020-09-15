// onotseike@hotmail.comPaula Aliu
using System;

using CoreVideo;

namespace WebRTC.iOS.Bindings
{
    public partial class RTCCVPixelBuffer
    {
        private static unsafe bool CropAndScaleToWrapper(CVPixelBuffer outputPixelBuffer, byte[] buffer, Func<CVPixelBuffer, IntPtr, bool> handler)
        {
            if (outputPixelBuffer == null)
                throw new ArgumentNullException(nameof(outputPixelBuffer));
            if (buffer != null && buffer.Length > 0)
                fixed (byte* ptr = &buffer[0])
                    return handler(outputPixelBuffer, new IntPtr(ptr));
            return handler(outputPixelBuffer, IntPtr.Zero);
        }


        public bool CropAndScaleTo(CVPixelBuffer outputPixelBuffer, byte[] buffer)
        {
            return CropAndScaleToWrapper(outputPixelBuffer, buffer, CropAndScaleTo);
        }

    }
}
