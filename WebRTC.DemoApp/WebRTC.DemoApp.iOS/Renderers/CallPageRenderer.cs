// onotseike@hotmail.comPaula Aliu
using System;

using AVFoundation;

using CoreGraphics;

using Foundation;

using UIKit;

using WebRTC.DemoApp;
using WebRTC.DemoApp.iOS.Interfaces;
using WebRTC.DemoApp.iOS.Renderers;
using WebRTC.DemoApp.iOS.ViewControllers;

using WebRTC.DemoApp.iOS.Views;

using WebRTC.DemoApp.SignalRClient;
using WebRTC.DemoApp.SignalRClient.Abstractions;
using WebRTC.iOS.Bindings;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.iOS;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using RoomConnectionParameters = WebRTC.DemoApp.SignalRClient.RoomConnectionParameters;
using SignalingParameters = WebRTC.DemoApp.SignalRClient.SignalingParameters;

[assembly: ExportRenderer(typeof(CallPage), typeof(CallPageRenderer))]
namespace WebRTC.DemoApp.iOS.Renderers
{
    public class CallPageRenderer : PageRenderer, IVideoCallViewDelegate, IRTCEngineEvents, IRTCAudioSessionDelegate, ICallPageRenderer<RoomConnectionParameters, SignalingParameters, SRTCController>
    {
        #region Properties & Variables

        private string RoomId { get; set; }
        private bool IsInitator { get; set; }

        //private RTCController CallController { get; set; }
        private SRTCController CallController { get; set; }

        private VideoCallView videoCallView;
        private AVAudioSessionPortOverride portOverride;


        private PlatformVideoRenderer localVideoRenderer;
        private PlatformVideoRenderer remoteVideoRenderer;

        private FileCapturerController fileCapturerController;



        public bool isSimulator => ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR;


        #endregion

        #region Overrides from PageRenderer

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }


            var callPage = (CallPage)e.NewElement;
            RoomId = callPage.RoomId;
            IsInitator = callPage.IsInitator;

            videoCallView = new VideoCallView(CGRect.Empty, !isSimulator);
            videoCallView.Delegate = this;

            localVideoRenderer = new PlatformVideoRenderer();
            remoteVideoRenderer = new PlatformVideoRenderer();

            localVideoRenderer.Renderer = videoCallView.LocalVideoRender;
            remoteVideoRenderer.Renderer = videoCallView.RemoteVideoRender;

            if (NativeView != null)
            {

            }


        }

        public override void LoadView()
        {
            base.LoadView();
            View = videoCallView;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CallController = CreateController();
            Connect(CallController);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Implementation of ICallPageRenderer

        public void Disconnect()
        {
            CallController.Disconnect();
            DidFinish(this);
        }

        public void SwitchCamera()
        {
            CallController.SwitchCamera();
        }

        public void StartVideoCall(IVideoRenderer localRenderer, IVideoRenderer remoteRenderer)
        {
            CallController.StartVideoCall(localRenderer, remoteRenderer);
        }

        public SRTCController CreateController()
        {
            return new SRTCController(this);
        }

        public void Connect(SRTCController controller)
        {
            controller.Connect(new RoomConnectionParameters
            {
                RoomId = RoomId,
                IsInitator = IsInitator,
                IsLoopback = false
            });
        }


        #endregion

        #region Implementations of IRTCEngineEvents

        public IVideoCapturer CreateVideoCapturer(IPeerConnectionFactory factory, IVideoSource videoSource)
        {
            if (!isSimulator)
            {
                return factory.CreateCameraCapturer(videoSource, true);
            }
            fileCapturerController = new FileCapturerController(videoSource);
            return fileCapturerController;
        }

        #endregion

        #region Implementation of IVideoCallViewDelegate

        public void DidChangeRoute(VideoCallView view)
        {
            var @override = AVAudioSessionPortOverride.None;
            if (portOverride == AVAudioSessionPortOverride.None)
            {
                @override = AVAudioSessionPortOverride.Speaker;
            }

            RTCDispatcher.DispatchAsyncOnType(RTCDispatcherQueueType.AudioSession, () =>
            {
                var session = RTCAudioSession.SharedInstance();
                session.LockForConfiguration();
                session.OverrideOutputAudioPort(@override, out NSError error);

                if (error == null)
                {
                    portOverride = @override;
                }
                else
                {
                    Console.WriteLine("Error overriding output port:{0}", error.LocalizedDescription);
                }
                session.UnlockForConfiguration();
            });
        }

        public void DidEnableStats(VideoCallView view)
        {

        }

        public void DidHangUp(VideoCallView view)
        {
            Disconnect();
        }

        public void DidSwitchCamera(VideoCallView view)
        {
            if (fileCapturerController != null)
            {
                fileCapturerController.Toggle();
                return;
            }
            SwitchCamera();
        }

        public void OnDisconnect(DisconnectType disconnectType)
        {
            Disconnect();
        }

        public void OnError(string description)
        {
            var alertDialog = UIAlertController.Create("Error", description, UIAlertControllerStyle.Alert);
            alertDialog.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Destructive, (s) => Disconnect()));
            PresentViewController(alertDialog, true, null);
        }

        public void OnPeerFactoryCreated(IPeerConnectionFactory peerConnectionFactory)
        {

        }

        public async void ReadyToStart()
        {
            var permission = await Permissions.RequestAsync<Permissions.Camera>();
            if (permission == PermissionStatus.Granted)
            {
                StartVideoCallInternal(localVideoRenderer, remoteVideoRenderer);
            }
            else
            {

            }
        }

        [Export("audioSession:didDetectPlayoutGlitch:")]
        public void AudioSession(RTCAudioSession audioSession, long totalNumberOfGlitches)
        {
            Console.WriteLine("Audio session detected glitch, total:{0}", totalNumberOfGlitches);
        }

        #endregion

        #region Helper Function(s)

        public void DidFinish(CallPageRenderer _viewController)
        {
            if (!_viewController.IsBeingDismissed)
            {
                Console.WriteLine("Dismissing VC");
                _viewController.DismissViewController(true, OnDismissVideoController);
            }
            var session = RTCAudioSession.SharedInstance();
            session.IsAudioEnabled = false;
        }

        protected virtual void OnDismissVideoController()
        {

        }

        private void StartVideoCallInternal(IVideoRenderer localRenderer, IVideoRenderer remoteRenderer)
        {
            var session = RTCAudioSession.SharedInstance();
            session.UseManualAudio = true;
            session.IsAudioEnabled = false;

            StartVideoCall(localRenderer, remoteRenderer);
        }



        #endregion
    }


    public interface ICallPageRenderer<TConnectionParameters, TSignalParameters, TController>
        where TConnectionParameters : IConnectionParameters
        where TSignalParameters : ISignalingParameters
        where TController : RTCControllerBase<TConnectionParameters, TSignalParameters>
    {
        #region Method(s)

        TController CreateController();
        void Connect(TController _controller);

        void Disconnect();
        void SwitchCamera();

        void StartVideoCall(IVideoRenderer localRenderer, IVideoRenderer remoteRenderer);

        #endregion
    }
}
