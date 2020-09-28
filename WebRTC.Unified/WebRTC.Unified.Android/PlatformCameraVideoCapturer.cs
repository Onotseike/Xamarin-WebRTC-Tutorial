// onotseike@hotmail.comPaula Aliu


using Android.Content;

using Org.Webrtc;

namespace WebRTC.Unified.Android
{
    internal class PlatformCameraVideoCapturer : PlatformVideoCapturer, Core.Interfaces.ICameraVideoCapturer
    {
        private readonly ICameraVideoCapturer _cameraVideoCapturer;

        public PlatformCameraVideoCapturer(Context context, ICameraVideoCapturer cameraVideoCapturer, VideoSource videoSource, IEglBaseContext eglBaseContext) : base(context, videoSource, cameraVideoCapturer, eglBaseContext) => _cameraVideoCapturer = cameraVideoCapturer;

        #region ICameraVideoCapturer Implements

        public void SwitchCamera() => _cameraVideoCapturer.SwitchCamera(null);

        public void SwitchCamera(string cameraName) => _cameraVideoCapturer.SwitchCamera(null, cameraName);



        #endregion
    }
}
