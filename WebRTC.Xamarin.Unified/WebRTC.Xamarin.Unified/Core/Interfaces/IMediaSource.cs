// onotseike@hotmail.comPaula Aliu
using WebRTC.Unified.Enums;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IMediaSource : INativeObject
    {
        SourceState State { get; }
    }
}
