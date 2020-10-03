// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    internal class PlatformRtpParameter : NativePlatformBase, IRtpParameters
    {
        private RTCRtpParameters _rtpParameters;

        public PlatformRtpParameter(RTCRtpParameters parameters) : base(parameters) => _rtpParameters = parameters;

        public string TransactionId => _rtpParameters.TransactionId;
    }
}