// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public delegate void SdpCompletionHandler(SessionDescription sdp, Exception error);

    public interface ISdpObserver : INativeObject
    {
        void OnCreateSuccess(SessionDescription sdp);
        void OnSetSuccess();
        void OnCreateFailure(string error);
        void OnSetFailure(string error);
    }
}
