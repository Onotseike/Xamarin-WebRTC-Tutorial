// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.DemoApp.iOS.Views;

namespace WebRTC.DemoApp.iOS.Interfaces
{
    public interface IVideoCallViewDelegate
    {
        void DidSwitchCamera(VideoCallView view);
        void DidChangeRoute(VideoCallView view);
        void DidHangUp(VideoCallView view);
        void DidEnableStats(VideoCallView view);
    }
}
