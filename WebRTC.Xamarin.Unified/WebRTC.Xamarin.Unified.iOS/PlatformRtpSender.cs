// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformRtpSender : NativePlatformBase, IRtpSender
    {
        private IRTCRtpSender _rtpSender;

        public PlatformRtpSender(IRTCRtpSender sender) => _rtpSender = sender;

        public string SenderId => _rtpSender.SenderId;

        public IRtpParameters Parameters { get => new PlatformRtpParameter(_rtpSender.Parameters); set => _rtpSender.Parameters = value.ToPlatformNative<RTCRtpParameters>(); }
        public IMediaStreamTrack Track { get => _rtpSender.Track.ToNativePort(); set => _rtpSender.Track = value.ToPlatformNative(); }
        public string[] StreamIds { get => _rtpSender.StreamIds; set => _rtpSender.StreamIds = value; }

        public IDtmfSender DtmfSender => new PlatformDtmfSender(_rtpSender.DtmfSender);

    }
}