// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IRtpReceiver : INativeObject
    {
        string Id { get; }

        IMediaStreamTrack Track { get; }

        //IRtpParameters Parameters { get; }
    }
}
