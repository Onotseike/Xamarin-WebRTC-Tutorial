
using Org.Webrtc;

using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    public class PlatformSdpObserver : Java.Lang.Object, ISdpObserver
    {
        private readonly Core.Interfaces.ISdpObserver _sdpObserver;

        public PlatformSdpObserver(Core.Interfaces.ISdpObserver sdpObserver) => _sdpObserver = sdpObserver;


        #region ISdpObserver Implements

        public void OnCreateFailure(string errorMessage) => _sdpObserver.OnCreateFailure(errorMessage);

        public void OnCreateSuccess(SessionDescription sessionDescription) => _sdpObserver.OnCreateSuccess(sessionDescription.ToNativePort());

        public void OnSetFailure(string failureMessage) => _sdpObserver.OnSetFailure(failureMessage);

        public void OnSetSuccess() => _sdpObserver.OnSetSuccess();

        #endregion

    }
}
