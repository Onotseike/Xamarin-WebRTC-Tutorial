// onotseike@hotmail.comPaula Aliu

using System;
using System.Collections.Generic;
using System.Linq;

using Android.Widget;

using Org.Webrtc;

namespace WebRTC.DemoApp.Droid.Fragments
{
    public class CaptureQualityController : Java.Lang.Object, SeekBar.IOnSeekBarChangeListener
    {
        private List<CaptureFormat> _formats = new List<CaptureFormat>
        {
            new CaptureFormat(1280, 720, 0, 30000),
            new CaptureFormat(960, 540, 0, 30000),
            new CaptureFormat(640, 480, 0, 30000),
            new CaptureFormat(320, 240, 0, 30000),
        };

        private const int FrameRateThreshold = 15;
        private const double ExpConstant = 3.0;

        private readonly TextView _captureFormatText;
        private readonly CallFragment.IOnCallEvents _callEvents;

        private int _width;
        private int _height;
        private int _framerate;

        private double _targetBandwidth;

        private double TargetBandwidth
        {
            get => _targetBandwidth;
            set
            {
                _targetBandwidth = value;
                UpdateTargetBandwidth();
            }
        }

        public CaptureQualityController(TextView captureFormatText, CallFragment.IOnCallEvents callEvents)
        {
            _captureFormatText = captureFormatText;
            _callEvents = callEvents;
        }


        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            if (progress == 0)
            {
                _width = 0;
                _height = 0;
                _framerate = 09;
                _captureFormatText.SetText(Resource.String.muted);
                return;
            }

            var maxCaptureBandwidth = long.MinValue;

            foreach (var format in _formats)
            {
                maxCaptureBandwidth = Math.Max(maxCaptureBandwidth, (long)format.Width * format.Height * format.MaxFramerate);
            }

            var bandwidthFraction = progress / 100f;


            bandwidthFraction = (float)((Math.Exp(ExpConstant * bandwidthFraction) - 1) / (Math.Exp(ExpConstant) - 1));


            TargetBandwidth = bandwidthFraction * maxCaptureBandwidth;


            var bestFormat = _formats.Max();

            _width = bestFormat.Width;
            _height = bestFormat.Height;
            _framerate = CalculateFramerate(TargetBandwidth, bestFormat);
            var context = _captureFormatText.Context;
            _captureFormatText.Text =
                context.GetString(Resource.String.format_description, _width, _height, _framerate);
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
            _callEvents.OnCaptureFormatChange(_width, _height, _framerate);
        }

        private void UpdateTargetBandwidth()
        {
            foreach (var format in _formats)
            {
                format.TargetBandwidth = TargetBandwidth;
            }
        }

        private class CaptureFormat : IComparable<CaptureFormat>
        {
            public CaptureFormat(int width, int height, int minFramerate, int maxFramerate)
            {
                Format = new CameraEnumerationAndroid.CaptureFormat(width, height, minFramerate, maxFramerate);
            }

            public CameraEnumerationAndroid.CaptureFormat Format { get; }

            public int Width => Format.Width;
            public int Height => Format.Height;

            public int MaxFramerate => Format.Framerate.Max;

            public double TargetBandwidth { get; set; }

            public int CompareTo(CaptureFormat other)
            {
                var firstFps = CalculateFramerate(TargetBandwidth, this);
                var secondFps = CalculateFramerate(TargetBandwidth, other);
                if ((firstFps >= FrameRateThreshold && secondFps >= FrameRateThreshold)
                    || firstFps == secondFps)
                {
                    // Compare resolution.
                    return Width * Height - other.Width * other.Height;
                }

                // Compare fps.
                return firstFps - secondFps;
            }

            // Return the highest frame rate possible based on bandwidth and format.
        }

        private static int CalculateFramerate(double bandwidth, CaptureFormat format)
        {
            return (int)Math.Round(
                Math.Min(format.MaxFramerate, (int)Math.Round(bandwidth / (format.Width * format.Height)))
                / 1000.0);
        }
    }

}