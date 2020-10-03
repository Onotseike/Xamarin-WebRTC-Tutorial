// onotseike@hotmail.comPaula Aliu
using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.iOS.Extensions;

namespace WebRTC.Unified.iOS
{
    internal class PlatformTransceiver : NativePlatformBase, IRtpTransceiver
    {
        private RTCRtpTransceiver _rtpTransceiver;

        public PlatformTransceiver(RTCRtpTransceiver transceiver) : base(transceiver) => _rtpTransceiver = transceiver;

        public RtpMediaType MediaType => _rtpTransceiver.MediaType.ToNativePort();

        public string Mid => _rtpTransceiver.Mid;

        public IRtpSender Sender => new PlatformRtpSender(_rtpTransceiver.Sender);

        public IRtpReceiver Receiver => new PlatformRtpReceiver(_rtpTransceiver.Receiver);

        public bool IsStopped => _rtpTransceiver.IsStopped;

        public RtpTransceiverDirection Direction => _rtpTransceiver.Direction.ToNativePort();

        public void SetDirection(RtpTransceiverDirection direction)
        {
            NSError error;
            _rtpTransceiver.SetDirection(direction.ToPlatformNative(), out error);
        }

        public void StopInternal() => _rtpTransceiver.StopInternal();
    }
}