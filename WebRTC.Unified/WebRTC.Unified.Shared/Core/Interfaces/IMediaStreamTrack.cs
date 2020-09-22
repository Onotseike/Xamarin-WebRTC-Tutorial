// onotseike@hotmail.comPaula Aliu
using System;

using Web.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IMediaStreamTrack : INativeObject
    {
        string Kind { get; }
        string TrackId { get; }
        bool IsEnabled { get; set; }
        MediaStreamTrackState ReadyState { get; }
    }
}
