// onotseike@hotmail.comPaula Aliu
using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.Unified.Android
{
    internal class PlatformRtpSender : IRtpSender
    {
        private RtpSender sender;

        public PlatformRtpSender(RtpSender sender)
        {
            this.sender = sender;
        }

        public string SenderId => throw new System.NotImplementedException();

        public IRtpParameters Parameters { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IMediaStreamTrack Track { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string[] StreamIds { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IDtmfSender DtmfSender => throw new System.NotImplementedException();

        public object NativeObject => throw new System.NotImplementedException();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}