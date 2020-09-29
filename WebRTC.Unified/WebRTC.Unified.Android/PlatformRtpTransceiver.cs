// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpTransceiver : NativePlatformBase, IRtpTransceiver
    {
        private RtpTransceiver _transceiver;

        public PlatformRtpTransceiver(RtpTransceiver transceiver) : base(transceiver) => _transceiver = transceiver;


        #region IRtpTransceiver Implements

        public RtpMediaType MediaType => _transceiver.MediaType.ToNativePort();

        public string Mid => _transceiver.Mid;

        public IRtpSender Sender => (_transceiver.Sender != null) ? new PlatformRtpSender(_transceiver.Sender) : null;

        public IRtpReceiver Receiver => (_transceiver.Receiver != null) ? new PlatformRtpReceiver(_transceiver.Receiver) : null;

        public bool IsStopped => _transceiver.IsStopped;

        public RtpTransceiverDirection Direction => _transceiver.Direction.ToNativePort();

        public RtpTransceiverDirection CurrentDirection => _transceiver.CurrentDirection.ToNativePort();

        RtpTransceiverDirection IRtpTransceiver.Direction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void SetDirection(RtpTransceiverDirection direction) => _transceiver.Direction = direction.ToNativePlatform();

        public void StopInternal() => _transceiver.Stop();

        #endregion
    }
}