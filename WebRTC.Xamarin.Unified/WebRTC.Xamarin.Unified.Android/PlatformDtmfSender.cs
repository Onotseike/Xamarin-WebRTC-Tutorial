// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core;

namespace WebRTC.Unified.Android
{
    internal class PlatformDtmfSender : NativePlatformBase, Core.Interfaces.IDtmfSender
    {
        private readonly DtmfSender _dtmfSender;
        public PlatformDtmfSender(DtmfSender dtmfSender) : base(dtmfSender) => _dtmfSender = dtmfSender;

        public bool CanInsertDtmf => _dtmfSender.CanInsertDtmf();

        public string RemainingTones => _dtmfSender.Tones();

        public double Duration => _dtmfSender.Duration();

        public double InterToneGap => _dtmfSender.InterToneGap();

        public bool InsertDtmf(string tones, double duration, double interToneGap) => _dtmfSender.InsertDtmf(tones, (int)duration, (int)interToneGap);
    }
}
