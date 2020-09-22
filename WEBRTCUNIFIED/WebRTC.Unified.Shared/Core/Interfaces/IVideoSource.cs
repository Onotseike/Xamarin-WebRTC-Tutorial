// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IVideoSource : IMediaSource
    {
        void AdaptOutputFormatToWidth(int width, int height, int fps);
    }
}
