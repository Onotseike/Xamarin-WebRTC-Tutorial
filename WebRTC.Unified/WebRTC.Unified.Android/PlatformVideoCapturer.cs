// onotseike@hotmail.comPaula Aliu
using Android.Content;

using Org.Webrtc;

using WebRTC.Unified.Core;

namespace WebRTC.Unified.Android
{
    internal class PlatformVideoCapturer : NativePlatformBase, Core.Interfaces.IVideoCapturer
    {
        private readonly IVideoCapturer _videoCapturer;
        private readonly SurfaceTextureHelper _surfaceTextureHelper;

        public PlatformVideoCapturer(Context context, VideoSource videoSource, IVideoCapturer videoCapturer, IEglBaseContext eglBaseContext) : base(videoCapturer)
        {
            _videoCapturer = videoCapturer;
            _surfaceTextureHelper = SurfaceTextureHelper.Create("VIDEO CAPTURE THREAD", eglBaseContext);
            _videoCapturer.Initialize(_surfaceTextureHelper, context, videoSource.CapturerObserver);
        }


        #region IVideoCapturer Implements

        public bool IsScreencast => _videoCapturer.IsScreencast;

        public void StartCapture() => _videoCapturer.StartCapture(0, 0, 0);

        public void StartCapture(int videoWidth, int videoHeight, int fps) => _videoCapturer.StartCapture(videoWidth, videoHeight, fps);
        public void StopCapture() => _videoCapturer.StopCapture();

        public override void Dispose()
        {
            _videoCapturer?.Dispose();
            base.Dispose();
        }

        #endregion

    }
}