// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IMediaStream : INativeObject
    {
        IAudioTrack[] AudioTracks { get; }
        IVideoTrack[] VideoTracks { get; }

        string StreamId { get; }

        void AddAudioTrack(IAudioTrack audioTrack);
        void AddVideoTrack(IVideoTrack videoTrack);
        void RemoveAudioTrack(IAudioTrack audioTrack);
        void RemoveVideoTrack(IVideoTrack videoTrack);

    }
}
