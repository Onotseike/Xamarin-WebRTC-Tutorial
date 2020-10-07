// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpReceiver : NativePlatformBase, IRtpReceiver
    {
        private RtpReceiver _rtpReceiver;

        public PlatformRtpReceiver(RtpReceiver rtpReceiver) : base(rtpReceiver) => _rtpReceiver = rtpReceiver;

        #region IRtpReceiver Implements

        public string Id => _rtpReceiver.Id();

        public IMediaStreamTrack Track => new PlatformMediaStreamTrack(_rtpReceiver.Track());
        public IRtpParameters Parameters => new PlatformRtpParameters(_rtpReceiver.Parameters);


        public override void Dispose()
        {
            _rtpReceiver?.Dispose();
            base.Dispose();
        }

        #endregion


    }
}