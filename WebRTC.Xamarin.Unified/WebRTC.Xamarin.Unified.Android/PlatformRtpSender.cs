// onotseike@hotmail.comPaula Aliu
using System.Linq;

using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpSender : NativePlatformBase, IRtpSender
    {
        private readonly RtpSender _rtpSender;

        public PlatformRtpSender(RtpSender rtpSender) : base(rtpSender) => _rtpSender = rtpSender;

        public string SenderId => _rtpSender.Id();

        public IRtpParameters Parameters { get => new PlatformRtpParameters(_rtpSender.Parameters); set => _rtpSender.SetParameters(value.ToPlatformNative<RtpParameters>()); }

        public IMediaStreamTrack Track { get => _rtpSender.Track()?.ToNativePort(); set => _rtpSender.SetTrack(value.ToPlatformNative<MediaStreamTrack>(), true); }
        public string[] StreamIds { get => _rtpSender.Streams.ToArray(); set => _rtpSender.Streams = value; }

        public IDtmfSender DtmfSender => new PlatformDtmfSender(_rtpSender.Dtmf());

        public override void Dispose()
        {
            _rtpSender?.Dispose();
            base.Dispose();
        }
    }
}