// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    public class PlatformRtpParameters : NativePlatformBase, IRtpParameters
    {
        private readonly RtpParameters _rtpParameters;

        public PlatformRtpParameters(RtpParameters rtpParameters) : base(rtpParameters) => _rtpParameters = rtpParameters;

        public string TransactionId { get { return _rtpParameters.TransactionId; } set { _rtpParameters.TransactionId = value; } }
    }
}
