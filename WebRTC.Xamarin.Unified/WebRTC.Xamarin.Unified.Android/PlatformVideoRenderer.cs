// onotseike@hotmail.comPaula Aliu
using System;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    public class PlatformVideoRenderer : Java.Lang.Object, Org.Webrtc.IVideoSink, IVideoRenderer
    {
        private Org.Webrtc.IVideoSink _videoSink;

        public Org.Webrtc.IVideoSink Renderer
        {
            get => _videoSink;
            set
            {
                if (_videoSink == this) throw new InvalidOperationException("You can set renderer to self");
                _videoSink = value;
            }
        }

        public object NativeObject => this;

        #region IVideoSink Implement

        public void OnFrame(VideoFrame videoFrame) => _videoSink?.OnFrame(videoFrame);
    }

    #endregion
}

