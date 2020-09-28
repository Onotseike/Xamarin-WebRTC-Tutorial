// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformBuiltinAudioEncoderFactory : NativePlatformBase, IBuiltinAudioEncoderFactory
    {
        private BuiltinAudioEncoderFactoryFactory _builtinAudioEncoderFactory;

        public PlatformBuiltinAudioEncoderFactory(BuiltinAudioEncoderFactoryFactory builtinAudioEncoderFactory) : base(builtinAudioEncoderFactory) => _builtinAudioEncoderFactory = builtinAudioEncoderFactory;

        public long CreateNativeAudioEncoderFactory() => _builtinAudioEncoderFactory.CreateNativeAudioEncoderFactory();
    }
}
