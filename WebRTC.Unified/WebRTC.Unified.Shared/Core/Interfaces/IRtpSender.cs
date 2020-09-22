// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IRtpSender : INativeObject
    {
        string SenderId { get; }

        IRtpParameters Parameters { get; set; }

        IMediaStreamTrack Track { get; set; }

        string[] StreamIds { get; set; }

        IDtmfSender DtmfSender { get; }
    }
}
