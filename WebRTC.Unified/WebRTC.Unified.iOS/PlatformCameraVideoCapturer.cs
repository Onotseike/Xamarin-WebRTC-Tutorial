// onotseike@hotmail.comPaula Aliu
using System;
using System.Diagnostics;
using System.Linq;

using AVFoundation;

using CoreMedia;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    internal class PlatformCameraVideoCapturer : NativePlatformBase, ICameraVideoCapturer
    {
        private readonly RTCCameraVideoCapturer _cameraVideoCapturer;

        private bool _usingFrontCamera;
        private int _videoWidth;
        private int _videoHeight;
        private int _videoFps;

        private const float _frameRateLimit = 30.0f;

        public bool IsScreencast => throw new NotImplementedException();

        public PlatformCameraVideoCapturer(RTCCameraVideoCapturer cameraVideoCapturer, bool usingFrontCamera = true) : base(cameraVideoCapturer)
        {
            _cameraVideoCapturer = cameraVideoCapturer;
            _usingFrontCamera = usingFrontCamera;
        }

        public bool IsScreenCast => false;

        public void SwitchCamera()
        {
            _usingFrontCamera = !_usingFrontCamera;
            StartCapture(_videoWidth, _videoHeight, _videoFps);
        }

        public void StartCapture() => StartCapture(_videoWidth, _videoHeight, _videoFps);

        public void StartCapture(int videoWidth, int videoHeight, int fps)
        {
            _videoFps = fps;
            _videoHeight = videoHeight;
            _videoWidth = videoWidth;

            var position = _usingFrontCamera ? AVCaptureDevicePosition.Back : AVCaptureDevicePosition.Front;
            var device = GetDeviceByPosition(position);
            var format = GetFormatForDevice(device, videoWidth, videoHeight);

            if (format == null)
            {
                Debug.WriteLine($"PLATFORMCAMERAVIDEOCAPTURER: not valid format for {device}");
                format = RTCCameraVideoCapturer.SupportedFormatsForDevice(device).FirstOrDefault();
                if (format == null)
                {
                    Debug.WriteLine($"PLATFORMCAMERAVIDEOCAPTURER: no valid formats for device {0} and {device}");
                    return;
                }

                fps = GetFpsByFormat(format);
                _cameraVideoCapturer.StartCaptureWithDevice(device, format, fps);
            }
        }


        public void StopCapture() => _cameraVideoCapturer.StopCapture();

        #region Helper Method(s)

        private AVCaptureDevice GetDeviceByPosition(AVCaptureDevicePosition position)
        {
            var captureDevices = RTCCameraVideoCapturer.CaptureDevices;
            foreach (var device in captureDevices)
            {
                if (device.Position == position) return device;
            }
            return captureDevices.FirstOrDefault();
        }

        private AVCaptureDeviceFormat GetFormatForDevice(AVCaptureDevice device, int videoWidth, int videoHeight)
        {
            var formats = RTCCameraVideoCapturer.SupportedFormatsForDevice(device);
            AVCaptureDeviceFormat selectedFormat = null;
            var currentDiff = int.MaxValue;

            foreach (var format in formats)
            {
                if (!(format.FormatDescription is CMVideoFormatDescription videoFormatDescription)) continue;

                var dimension = videoFormatDescription.Dimensions;
                var pixelFormat = videoFormatDescription.MediaSubType;
                var diff = Math.Abs(videoWidth - dimension.Width) + Math.Abs(videoHeight - dimension.Height);

                if (diff < currentDiff)
                {
                    selectedFormat = format;
                    currentDiff = diff;
                }
                else if (diff == currentDiff && pixelFormat == _cameraVideoCapturer.PreferredOutputPixelFormat) selectedFormat = format;
            }

            return selectedFormat;
        }

        private int GetFpsByFormat(AVCaptureDeviceFormat format)
        {
            var maxSupportedFps = 0d;
            foreach (var fpsRange in format.VideoSupportedFrameRateRanges) maxSupportedFps = Math.Max(maxSupportedFps, fpsRange.MaxFrameRate);

            return (int)Math.Min(maxSupportedFps, _frameRateLimit);
        }

        #endregion
    }
}
