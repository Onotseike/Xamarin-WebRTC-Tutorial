// onotseike@hotmail.comPaula Aliu
using System;

using Web.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IMediaSource : INativeObject
    {
        SourceState State { get; }
    }
}
