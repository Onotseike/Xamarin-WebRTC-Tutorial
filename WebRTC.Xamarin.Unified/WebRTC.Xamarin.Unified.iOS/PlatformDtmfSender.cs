// onotseike@hotmail.comPaula Aliu
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    internal class PlatformDtmfSender : NativePlatformBase, IDtmfSender
    {
        private RTCDtmfSender _dtmfSender;

        public PlatformDtmfSender(RTCDtmfSender dtmfSender) : base(nativeObject: dtmfSender) => _dtmfSender = dtmfSender;

        public bool CanInsertDtmf => _dtmfSender.CanInsertDtmf;

        public string RemainingTones => _dtmfSender.RemainingTones;

        public double Duration => _dtmfSender.Duration;

        public double InterToneGap => _dtmfSender.InterToneGap;

        public bool InsertDtmf(string tones, double duration, double interToneGap) => _dtmfSender.InsertDtmf(tones, duration, interToneGap);
    }
}