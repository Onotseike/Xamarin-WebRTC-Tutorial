// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IVideoFrame : INativeObject
    {
        int Width { get; }

        int Height { get; }

        VideoRotation Rotation { get; }

        long TimeStampNs { get; }

        int TimeStamp { get; set; }

        IVideoFrameBuffer Buffer { get; }


    }

    public interface IVideoFrameBuffer : INativeObject
    {
        int Width { get; }
        int Height { get; }
    }
}
