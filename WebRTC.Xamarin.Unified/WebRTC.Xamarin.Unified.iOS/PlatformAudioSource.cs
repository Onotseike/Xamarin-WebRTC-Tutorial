// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    internal class PlatformAudioSource : PlatformMediaSource, IAudioSource
    {
        private readonly RTCAudioSource _audioSource;
        public PlatformAudioSource(RTCAudioSource audioSource) : base(audioSource) => _audioSource = audioSource;


        public double Volume { get => _audioSource.Volume; set => _audioSource.Volume = value; }
    }
}
