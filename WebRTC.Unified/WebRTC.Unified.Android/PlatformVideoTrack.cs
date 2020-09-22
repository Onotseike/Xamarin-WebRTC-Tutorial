// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformVideoTrack : PlatformMediaStreamTrack, IVideoTrack
    {
        private readonly VideoTrack _videoTrack;

        public PlatformVideoTrack(VideoTrack videoTrack) : base(videoTrack)
        {
            _videoTrack = videoTrack;
        }

        //public IVideoSource Source => 

        public void AddRenderer(IVideoRenderer videoRenderer) => _videoTrack.AddSink(videoRenderer.ToPlatformNative<IVideoSink>());



        public void RemoveVideoRenderer(IVideoRenderer videoRenderer) => _videoTrack.RemoveSink(videoRenderer.ToPlatformNative<IVideoSink>());
    }
}
