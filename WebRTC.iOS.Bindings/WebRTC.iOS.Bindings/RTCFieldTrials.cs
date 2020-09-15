// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Foundation;

namespace WebRTC.iOS.Bindings
{
    public static class RTCFieldTrials
    {
        // extern void RTCInitFieldTrialDictionary (NSDictionary<NSString *,NSString *> *fieldTrials) __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern void RTCInitFieldTrialDictionary(IntPtr intPtr);

        private static void InitFieldTrialDictionary(NSDictionary<NSString, NSString> fieldTrials) => RTCInitFieldTrialDictionary(fieldTrials.Handle);

        public static void InitFieldTrialDictionary(IDictionary<string, string> fieldTrials)
        {

            var keys = new NSString[fieldTrials.Keys.Count];
            var values = new NSString[fieldTrials.Values.Count];
            var i = 0;
            foreach (var pair in fieldTrials)
            {
                keys[i] = new NSString(pair.Key);
                values[i++] = new NSString(pair.Value);
            }


            InitFieldTrialDictionary(new NSDictionary<NSString, NSString>(keys, values));
        }
    }
}
