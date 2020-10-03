// onotseike@hotmail.comPaula Aliu
using System;

using CoreGraphics;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.iOS
{
    public class PlatformVideoRenderer : NSObject, IRTCVideoRenderer, IVideoRenderer
    {
        private IRTCVideoRenderer _renderer;

        public object NativeObject => this;

        public IRTCVideoRenderer Renderer
        {
            get => _renderer;
            set
            {
                if (_renderer == this)
                    throw new InvalidOperationException("You can set renderer to self");
                _renderer = value;
            }
        }

        public void RenderFrame(RTCVideoFrame frame)
        {
            Renderer?.RenderFrame(frame);
            frame.Dispose();
        }

        public void SetSize(CGSize size) => Renderer?.SetSize(size);
    }
}
