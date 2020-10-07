// onotseike@hotmail.comPaula Aliu
using System;
using System.Diagnostics;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformFileVideoCapturer : NativePlatformBase, IFileVideoCapturer
    {
        private readonly RTCFileVideoCapturer _fileVideoCapturer;
        private readonly string _file;

        public PlatformFileVideoCapturer(IVideoSource videoSource)
        {
            _fileVideoCapturer = new RTCFileVideoCapturer(videoSource.ToPlatformNative<IRTCVideoCapturerDelegate>());
            NativeObject = _fileVideoCapturer;
        }

        public PlatformFileVideoCapturer(IVideoSource videoSource, string file) : this(videoSource) => _file = file;

        public bool IsScreencast => false;
        public virtual void StartCapture() => StartCapturingFromFileNamed(_file);

        public virtual void StartCapture(int videoWidth, int videoHeight, int fps) => StartCapture();

        public virtual void StartCapturingFromFileNamed(string file)
        {
            _fileVideoCapturer.StartCapturingFromFileNamed(file, (err) => Debug.WriteLine($"FileVideoCapturerNative failed:{err}"));
        }


        public virtual void StopCapture() => _fileVideoCapturer.StopCapture();
    }
}
