// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IDtmfSender : INativeObject
    {
        bool CanInsertDtmf { get; }

        bool InsertDtmf(string tones, double duration, double interToneGap);

        string RemainingTones { get; }

        double Duration { get; }

        double InterToneGap { get; }
    }
}
