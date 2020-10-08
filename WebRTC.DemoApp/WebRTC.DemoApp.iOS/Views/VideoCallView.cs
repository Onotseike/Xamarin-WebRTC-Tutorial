// onotseike@hotmail.comPaula Aliu
using System;

using AVFoundation;

using CoreGraphics;

using Foundation;

using UIKit;

using WebRTC.DemoApp.iOS.Interfaces;
using WebRTC.iOS.Bindings;

namespace WebRTC.DemoApp.iOS.Views
{
    public class VideoCallView : UIView, IRTCVideoViewDelegate
    {
        #region Properties & Variables

        const float kButtonPadding = 16;
        const float kButtonSize = 48;
        const float kLocalVideoViewSize = 120;
        const float kLocalVideoViewPadding = 8;
        const float kStatusBarHeight = 20;

        private readonly UIButton routeChangeBtn;
        private readonly UIButton cameraSwitchBtn;
        private readonly UIButton hangUpBtn;
        private readonly UIButton muteBtn;

        private CGSize remoteVideoSize;

        public UIView RemoteView { get; }
        public UIView LocalVideoView { get; }

        public StatsView StatsView { get; }

        public UILabel StatusLabel { get; }

        public IVideoCallViewDelegate Delegate { get; set; }

        public IRTCVideoRenderer RemoteVideoRender => RemoteView as IRTCVideoRenderer;
        public IRTCVideoRenderer LocalVideoRender => LocalVideoView as IRTCVideoRenderer;

        #endregion

        #region Constructor(s)

        public VideoCallView(CGRect _frame, bool _useCameraPreview) : base(_frame)
        {
            RemoteView = new RTCEAGLVideoView
            {
                Delegate = this
            };
            AddSubview(RemoteView);

            if (_useCameraPreview)
            {
                LocalVideoView = new RTCCameraPreviewView();
            }
            else
            {
                LocalVideoView = new RTCEAGLVideoView
                {
                    Delegate = this
                };
            }
            AddSubview(LocalVideoView);

            StatsView = new StatsView(_frame);
            StatsView.Hidden = true;
            AddSubview(StatsView);

            routeChangeBtn = new UIButton(UIButtonType.InfoDark);
            routeChangeBtn.TouchUpInside += OnRouteChanged;
            AddSubview(routeChangeBtn);

            cameraSwitchBtn = new UIButton(UIButtonType.DetailDisclosure);
            cameraSwitchBtn.TouchUpInside += OnCameraSwitched;
            AddSubview(cameraSwitchBtn);

            hangUpBtn = new UIButton(UIButtonType.Close);
            hangUpBtn.TouchUpInside += OnHangup;
            AddSubview(hangUpBtn);

            StatusLabel = new UILabel();
            StatusLabel.Font = UIFont.SystemFontOfSize(16);
            StatusLabel.TextColor = UIColor.White;
            AddSubview(StatusLabel);

            var tapRecognizer = new UITapGestureRecognizer(DidTripleTapped);
            tapRecognizer.NumberOfTapsRequired = 3;

            AddGestureRecognizer(tapRecognizer);


        }

        #endregion

        #region Button Event(s)

        private void OnCameraSwitched(object sender, EventArgs e)
        {
            Delegate?.DidSwitchCamera(this);
        }

        private void OnRouteChanged(object sender, EventArgs e)
        {
            Delegate?.DidChangeRoute(this);

        }

        private void OnHangup(object sender, EventArgs e)
        {
            Delegate?.DidHangUp(this);

        }

        private void DidTripleTapped()
        {
            Delegate?.DidEnableStats(this);
        }

        #endregion

        #region Overrides of IRTCVideoViewDelegate

        [Export("videoView:didChangeVideoSize:")]
        public void DidChangeVideoSize(IRTCVideoRenderer videoView, CGSize size)
        {
            if (videoView == RemoteVideoRender)
            {
                remoteVideoSize = size;
            }
            SetNeedsLayout();
        }

        #endregion

        #region Overrides of UIView

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var bounds = Bounds;

            if (remoteVideoSize.Width > 0 && remoteVideoSize.Height > 0)
            {
                // Aspect fill remote video into bounds.

                var remoteVideoFrame = bounds.WithAspectRatio(remoteVideoSize);
                nfloat scale = 1f;
                if (remoteVideoFrame.Size.Width > remoteVideoFrame.Size.Height)
                {
                    // Scale by height.
                    scale = bounds.Size.Height / remoteVideoFrame.Size.Height;
                }
                else
                {
                    // Scale by width.
                    scale = bounds.Size.Width / remoteVideoFrame.Size.Width;
                }

                remoteVideoFrame.Size = new CGSize(remoteVideoFrame.Size.Width * scale, remoteVideoFrame.Size.Height * scale);
                RemoteView.Frame = remoteVideoFrame;
                RemoteView.Center = new CGPoint(bounds.GetMidX(), bounds.GetMidY());
            }
            else
            {
                RemoteView.Frame = bounds;
            }
            // Aspect fit local video view into a square box.
            var localVideoFrame = new CGRect(0, 0, kLocalVideoViewSize, kLocalVideoViewSize);
            // Place the view in the bottom right.
            localVideoFrame.Location = new CGPoint(
                bounds.GetMaxX() - localVideoFrame.Size.Width - kLocalVideoViewPadding, bounds.GetMaxY() - localVideoFrame.Size.Height - kLocalVideoViewPadding - AppDelegate.SafeAreaInsets.Top);

            LocalVideoView.Frame = localVideoFrame;

            // Place stats at the top.
            var statsSize = StatsView.SizeThatFits(bounds.Size);
            StatsView.Frame = new CGRect(bounds.GetMinX(), bounds.GetMinY() + kStatusBarHeight + AppDelegate.SafeAreaInsets.Top, statsSize.Width, statsSize.Height);

            // Place hangup button in the bottom left.
            hangUpBtn.Frame = new CGRect(bounds.GetMinX() + kButtonPadding, bounds.GetMaxY() - kButtonPadding - kButtonSize - AppDelegate.SafeAreaInsets.Bottom, kButtonSize, kButtonSize);

            // Place button to the right of hangup button.
            var cameraSwitchFrame = hangUpBtn.Frame;
            cameraSwitchFrame.Location = new CGPoint(cameraSwitchFrame.GetMaxX() + kButtonPadding, cameraSwitchFrame.Location.Y);
            cameraSwitchBtn.Frame = cameraSwitchFrame;

            // Place route button to the right of camera button.
            var routeChangeFrame = cameraSwitchBtn.Frame;
            routeChangeFrame.Location = new CGPoint(routeChangeFrame.GetMaxX() + kButtonPadding, routeChangeFrame.Location.Y);
            routeChangeBtn.Frame = routeChangeFrame;

            StatusLabel.SizeToFit();
            StatusLabel.Center = new CGPoint(bounds.GetMidX(), bounds.GetMidY());
        }

        #endregion
    }

}
