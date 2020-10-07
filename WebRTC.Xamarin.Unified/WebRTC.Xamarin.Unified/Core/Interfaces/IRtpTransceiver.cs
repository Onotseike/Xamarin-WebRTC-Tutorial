// onotseike@hotmail.comPaula Aliu
using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IRtpTransceiver : INativeObject
    {
        RtpMediaType MediaType { get; }

        string Mid { get; }

        IRtpSender Sender { get; }

        IRtpReceiver Receiver { get; }

        bool IsStopped { get; }

        RtpTransceiverDirection Direction { get; }
        //RtpTransceiverDirection CurrentDirection { get; }

        void StopInternal();

        void SetDirection(RtpTransceiverDirection direction);
    }

    public interface IRtpTransceiverInit : INativeObject
    {
        string[] StreamIds { get; set; }
        RtpTransceiverDirection Direction { get; set; }
        IRtpEncodingParameters[] SendEncodings { get; set; }
    }

    public interface IRtpEncodingParameters
    {
    }
}
