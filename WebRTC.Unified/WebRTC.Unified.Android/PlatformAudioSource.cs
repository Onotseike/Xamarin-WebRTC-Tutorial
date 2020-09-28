// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformAudioSource : PlatformMediaSource, IAudioSource
    {
        private readonly AudioSource _audioSource;
        private double _volume;

        public PlatformAudioSource(AudioSource audioSource) : base(audioSource) => _audioSource = audioSource;

        public double Volume { get => 0f; set => _volume = value; }
    }
}
