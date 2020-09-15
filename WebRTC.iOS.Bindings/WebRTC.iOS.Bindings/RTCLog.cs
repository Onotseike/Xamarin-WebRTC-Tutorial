// onotseike@hotmail.comPaula Aliu
using System.Runtime.InteropServices;

using Foundation;

namespace WebRTC.iOS.Bindings
{
    public static class RTCLog
    {
        // extern void RTCLogEx (RTCLoggingSeverity severity, NSString *log_string) __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern void RTCLogEx(RTCLoggingSeverity severity, NSString log_string);

        public static void LogEx(RTCLoggingSeverity severity, NSString log_string) => RTCLogEx(severity, log_string);

        // extern void RTCSetMinDebugLogLevel (RTCLoggingSeverity severity) __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern void RTCSetMinDebugLogLevel(RTCLoggingSeverity severity);

        public static void SetMinDebugLogLevel(RTCLoggingSeverity severity) => RTCSetMinDebugLogLevel(severity);
    }
}
