// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformAudioTrack : PlatformMediaStreamTrack, IAudioTrack
    {
        private readonly AudioTrack _audioTrack;

        public double Volume { get => 0f; set => _audioTrack.SetVolume(value); }

        public PlatformAudioTrack(AudioTrack audioTrack) : base(audioTrack) => _audioTrack = audioTrack;
    }
}
