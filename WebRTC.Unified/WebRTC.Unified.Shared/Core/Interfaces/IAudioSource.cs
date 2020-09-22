// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IAudioSource : IMediaSource
    {
        double Volume { get; set; }
    }
}
