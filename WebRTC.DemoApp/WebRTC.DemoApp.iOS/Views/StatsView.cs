// onotseike@hotmail.comPaula Aliu
using System;

using CoreGraphics;

using UIKit;

using WebRTC.iOS.Bindings;

namespace WebRTC.DemoApp.iOS.Views
{
    public class StatsView : UIView
    {
        private readonly UILabel _statsLabel;
        // private readonly ARDStatsBuilder _statsBuilder;

        public StatsView(CGRect frame) : base(frame)
        {
            _statsLabel = new UILabel();
            _statsLabel.Lines = 0;
            _statsLabel.AdjustsFontSizeToFitWidth = true;
            _statsLabel.MinimumScaleFactor = 0.6f;
            _statsLabel.TextColor = UIColor.Green;
            AddSubview(_statsLabel);
            BackgroundColor = UIColor.Black.ColorWithAlpha(0.6f);

            //  _statsBuilder = new ARDStatsBuilder();
        }

        public void SetStats(RTCLegacyStatsReport[] stats)
        {
            //foreach (var report in stats)
            //{
            //    _statsBuilder.ParseStatsReport(report);
            //}
            //_statsLabel.Text = _statsBuilder.Stats;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            _statsLabel.Frame = Bounds;
        }

        public override CGSize SizeThatFits(CGSize size)
        {
            return _statsLabel.SizeThatFits(size);
        }
    }

}
