// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformRtpReceiver : NativePlatformBase, IRtpReceiver
    {
        private IRTCRtpReceiver _receiver;

        public PlatformRtpReceiver(IRTCRtpReceiver receiver) : base(receiver) => _receiver = receiver;

        public string Id => _receiver.ReceiverId;

        public IMediaStreamTrack Track => _receiver.Track.ToNativePort();

        //public IRtpParameters Parameters => _receiver
    }
}