using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

using Newtonsoft.Json.Linq;

using WebRTC.DemoApp.Interfaces;
using WebRTC.DemoApp.SignalRClient;
using WebRTC.DemoApp.SignalRClient.Abstractions;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebRTC.DemoApp
{
    public partial class App : Application
    {
        #region Properties & Variables

        private ILogger logger;

        public static HubConnection HubConnection { get; set; }

        protected ISdpAnswerRecieved SdpAnswerRecieved { get; set; }
        public static SRTCClient Instance { get; set; }

        #endregion

        public App()
        {
            SdpAnswerRecieved = DependencyService.Get<ISdpAnswerRecieved>();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                HubConnection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:5004/webrtchub").WithAutomaticReconnect().Build();
            }
            else
            {
                HubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5001/webrtchub").WithAutomaticReconnect().Build();
            }

            LoadAllClientSideFunctions();

            LoadHubOverrideFunctions();

            logger = new ConsoleLogger();

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            // Connect to SignalR Hub
            await HubConnection.StartAsync();
            await HubConnection.InvokeAsync("JoinHub", $"CLIENT_{new Random().Next(1, 20)}");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #region SignalR Hub Connection Methods


        private void LoadHubOverrideFunctions()
        {
            HubConnection.Closed += async (error) =>
            {
                await HubConnection.StartAsync();
            };
            HubConnection.Reconnecting += error =>
            {
                //Debug.Assert(connection.State == HubConnectionState.Reconnecting);
                logger.Debug("APP", "SignalR Hub is in RECONNECTING STATE");

                // Notify users the connection was lost and the client is reconnecting.
                // Start queuing or dropping messages.

                return Task.CompletedTask;
            };
        }

        private void LoadAllClientSideFunctions()
        {
            HubConnection.On<JArray>("UpdateHubClientsList", (_hubClientsList) =>
            {


            });

            HubConnection.On<string>("OnAnswerSDPRecieved", (_answerSdp) => { Instance.OnWebSocketMessage(_answerSdp); });

            HubConnection.On<string>("OnOfferSDPReceived", (_offerSdp) => { Instance.OnWebSocketMessage(_offerSdp); });

            HubConnection.On<string>("OnLocalICECandidateReceived", (_iceCandidate) => { Instance.OnWebSocketMessage(_iceCandidate); });

            HubConnection.On<string>("OnICECandidatesRemoved", (_candidates) => { Instance.OnWebSocketMessage(_candidates); });
        }
        #endregion
    }
}
