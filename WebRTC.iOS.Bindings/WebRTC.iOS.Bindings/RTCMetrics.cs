// onotseike@hotmail.comPaula Aliu

using System.Runtime.InteropServices;

using Foundation;

namespace WebRTC.iOS.Bindings
{
    public static class RTCMetrics
    {
        // extern void RTCEnableMetrics () __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern void RTCEnableMetrics();

        public static void EnableMetrics() => RTCEnableMetrics();

        // extern NSArray<RTCMetricsSampleInfo *> * RTCGetAndResetMetrics () __attribute__((visibility("default")));
        [DllImport("__Internal")]
        private static extern RTCMetricsSampleInfo[] RTCGetAndResetMetrics();

        //public static RTCMetricsSampleInfo[] GetAndResetMetrics()
        //{
        //    var metrics = RTCGetAndResetMetrics();
        //    var _metrics = new RTCMetricsSampleInfo[metrics.Length];
        //    for (int idx = 0; idx < metrics.Length; idx++)
        //    {
        //        _metrics[idx] = new RTCMetricsSampleInfo
        //        {
        //            Name = metrics[idx].Name,
        //            Min = metrics[idx].Min,
        //            Max = metrics[idx].Max,
        //            BucketCount = metrics[idx].BucketCount,
        //            Samples = metrics[idx].Samples
        //        };
        //    }
        //    return _metrics;
        //}

    }


    //public partial class RTCMetricsSampleInfo
    //{
    //    public string Name { get; set; }

    //    public int Min { get; set; }

    //    public int Max { get; set; }

    //    public int BucketCount { get; set; }

    //    public NSDictionary<NSNumber, NSNumber> Samples { get; set; }
    //}
}
