// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core.Interfaces
{
    public interface IVideoCapturer : INativeObject
    {
        bool IsScreencast { get; }

        void StartCapture();

        void StartCapture(int videoWidth, int videoHeight, int fps);
        void StopCapture();
    }
}
