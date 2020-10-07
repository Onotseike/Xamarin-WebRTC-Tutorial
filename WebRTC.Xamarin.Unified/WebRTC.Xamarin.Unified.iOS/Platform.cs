// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;
using System.Linq;

using WebRTC.iOS.Bindings;

namespace WebRTC.Unified.iOS
{
    public static class Platform
    {
        public static void Initialize(IDictionary<string, string> trialsFields = null, bool enableInternalTracer = true)
        {
            if (trialsFields?.Any() ?? false)
            {
                RTCFieldTrials.InitFieldTrialDictionary(trialsFields);
            }

            if (enableInternalTracer)
            {
                //RTCTracing.RTCStartInternalCapture("log.cs");
            }

            RTCSSLAdapter.RTCInitializeSSL();
            Core.NativeFactory.Initalize(new PlatformFactory());
        }

        public static void Cleanup()
        {
            RTCTracing.RTCShutdownInternalTracer();
            RTCSSLAdapter.RTCCleanupSSL();
        }
    }
}
