// onotseike@hotmail.comPaula Aliu
using Android.Content;

using Org.Webrtc;

namespace WebRTC.Unified.Android
{
    internal class PlatformFileVideoCapturer : PlatformVideoCapturer, Core.Interfaces.IFileVideoCapturer
    {
        private readonly FileVideoCapturer _fileVideoCapturer;
        public PlatformFileVideoCapturer(Context context, FileVideoCapturer fileVideoCapturer, VideoSource videoSource, IEglBaseContext eglBaseContext) : base(context, videoSource, fileVideoCapturer, eglBaseContext) => _fileVideoCapturer = fileVideoCapturer;
    }
}
