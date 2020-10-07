// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformBuiltinAudioDecoderFactory : NativePlatformBase, IBuiltinAudioDecoderFactory
    {
        private BuiltinAudioDecoderFactoryFactory _builtInAudioDecoderFactory;
        public PlatformBuiltinAudioDecoderFactory(BuiltinAudioDecoderFactoryFactory builtinAudioDecoderFactory) : base(builtinAudioDecoderFactory) => _builtInAudioDecoderFactory = builtinAudioDecoderFactory;

        public long CreateNativeAudioDecoderFactory() => _builtInAudioDecoderFactory.CreateNativeAudioDecoderFactory();
    }
}
