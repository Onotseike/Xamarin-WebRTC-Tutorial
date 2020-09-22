// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IVideoTrack : IMediaStreamTrack
    {
        // IVideoSource Source { get; }

        void AddRenderer(IVideoRenderer videoRenderer);
        void RemoveVideoRenderer(IVideoRenderer videoRenderer);
    }
}
