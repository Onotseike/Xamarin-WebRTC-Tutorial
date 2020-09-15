// onotseike@hotmail.comPaula Aliu
using System.Runtime.InteropServices;

using Foundation;

namespace WebRTC.iOS.Bindings
{
    public static class RTCFileNames
    {
        // extern NSString * RTCFileName (const char *filePath) __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern unsafe NSString RTCFileName(sbyte* filePath);

        public static unsafe NSString FileName(sbyte* filePath) => RTCFileName(filePath);
    }
}
