// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

namespace WebRTC.Unified.Android
{
    internal class PlatformDtmfSender : Java.Lang.Object, Core.Interfaces.IDtmfSender
    {
        private readonly DtmfSender _dtmfSender;
        public PlatformDtmfSender(DtmfSender dtmfSender) => _dtmfSender = dtmfSender;

        public bool CanInsertDtmf => _dtmfSender.CanInsertDtmf();

        public string RemainingTones => _dtmfSender.Tones();

        public double Duration => _dtmfSender.Duration();

        public double InterToneGap => _dtmfSender.InterToneGap();

        protected override void Dispose(bool disposing)
        {
            if (disposing) _dtmfSender.Dispose();
            base.Dispose(disposing);
        }

        public bool InsertDtmf(string tones, double duration, double interToneGap) => _dtmfSender.InsertDtmf(tones, (int)duration, (int)interToneGap);
    }
}
