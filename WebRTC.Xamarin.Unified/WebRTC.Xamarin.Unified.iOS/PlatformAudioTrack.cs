// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;

namespace WebRTC.Unified.iOS
{
    internal class PlatformAudioTrack : PlatformMediaStreamTrack, IAudioTrack
    {
        private readonly RTCAudioTrack _audioTrack;
        public PlatformAudioTrack(RTCAudioTrack audioTrack) : base(audioTrack) => _audioTrack = audioTrack;

        public string Kind => throw new NotImplementedException();

        public string TrackId => throw new NotImplementedException();

        public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MediaStreamTrackState ReadyState => throw new NotImplementedException();

        public object NativeObject => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
