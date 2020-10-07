// onotseike@hotmail.comPaula Aliu
using System;

using Microsoft.AspNetCore.SignalR.Client;

using Newtonsoft.Json.Linq;

using WebRTC.DemoApp.SignalRClient.Abstractions;
using WebRTC.DemoApp.SignalRClient.Responses;
using WebRTC.Unified.Core;
using WebRTC.Unified.Enums;

namespace WebRTC.DemoApp.SignalRClient
{
    public class SRTCClient : IRTCClient<RoomConnectionParameters>
    {
        #region MessageType Enum

        private enum MessageType
        {
            Message,
            Leave
        }

        #endregion

        #region Properties & Variables

        private const string TAG = nameof(SRTCClient);

        private readonly ISignalingEvents<SignalingParameters> signalingEvents;
        private readonly IExecutorService executor;
        private readonly ILogger logger;

        private RoomConnectionParameters roomConnectionParameters;
        private bool isInitiator;
        private string messageUrl;


        #endregion

        #region Constructor(s)

        public SRTCClient(ISignalingEvents<SignalingParameters> _signalingEvents, ILogger _logger = null)
        {
            signalingEvents = _signalingEvents;
            executor = ExecutorServiceFactory.CreateExecutorService(nameof(SRTCClient));
            logger = _logger ?? new ConsoleLogger();

            State = ConnectionState.New;

            App.Instance = this;
        }

        #endregion

        #region Helper Function(s)

        private async void ConnectToHubRoom()
        {
            try
            {
                State = ConnectionState.New;
                //TODO: Call SignalR Hub Connect To Room; This returns The relevant Room Parameters and potential errors if available.
                //await App.HubConnection.StartAsync();
                var str = await App.HubConnection.InvokeAsync<string>("GetRoomParametersAsync", roomConnectionParameters.RoomId, roomConnectionParameters.IsInitator, "https://global.xirsys.net/_turn/DemoWebRTC");
                var roomSObject = JObject.Parse(str);
                var roomParams = roomSObject.ToObject<RoomParameterResponse>();
                var _roomSignalingParameters = new SignalingParameters
                {
                    IsInitiator = roomParams.IsInitiator,
                    ClientId = roomParams.ClientId,
                    IceServers = roomParams.IceServers,
                    IceCandidates = roomParams.IceCandidates,
                    OfferSdp = roomParams.OfferSdp

                };
                executor.Execute(() =>
                {
                    SignalingParametersReady(_roomSignalingParameters);
                });
            }
            catch (Exception ex)
            {
                ReportError($"ERROR {ex.Message}");
                return;
            }

        }

        private void SignalingParametersReady(SignalingParameters _roomSignalingParameters)
        {
            logger.Debug(TAG, "Room connection completed.");

            if (roomConnectionParameters.IsLoopback &&
                (!_roomSignalingParameters.IsInitiator || _roomSignalingParameters.OfferSdp != null))
            {
                ReportError("Loopback room is busy.");
                return;
            }

            if (!roomConnectionParameters.IsLoopback && !_roomSignalingParameters.IsInitiator &&
                _roomSignalingParameters.OfferSdp == null)
            {
                logger.Warning(TAG, "No offer SDP in room response.");
            }

            isInitiator = _roomSignalingParameters.IsInitiator;
            signalingEvents.OnChannelConnected(_roomSignalingParameters);
        }

        private void DisconnectFromHubRoom()
        {
            logger.Debug(TAG, "Disconnect. Room state: " + State);
            if (App.HubConnection.State == HubConnectionState.Connected)
            {
                logger.Debug(TAG, $"Leaving Room with RoomID: {roomConnectionParameters.RoomId}");
                //TODO: Call SignalR Leave Room Function
            }
            executor.Release();
        }

        private void ReportError(string _errorMessage)
        {
            logger.Error(TAG, _errorMessage);
            executor.Execute(() =>
            {
                if (State == ConnectionState.Error)
                    return;
                State = ConnectionState.Error;
                signalingEvents.OnChannelError(_errorMessage);
            });
        }

        private void SendPostMessage(MessageType _messageType, string _url, string _message)
        {
            var logInfo = _url;

            if (_message != null)
                logInfo += $". Message: {_message}";

            logger.Debug(TAG, $"C->GAE: {logInfo}");


        }



        #endregion

        #region IRTCClient Implementations

        public ConnectionState State { get; private set; }

        public void Connect(RoomConnectionParameters _connectionParameters)
        {
            roomConnectionParameters = _connectionParameters;
            executor.Execute(ConnectToHubRoom);
        }

        public void Disconnect() => executor.Execute(DisconnectFromHubRoom);

        public void SendOfferSdp(SessionDescription _sessionDescriotion)
        {
            executor.Execute(async () =>
            {
                if (App.HubConnection.State != HubConnectionState.Connected)//(State != ConnectionState.Connected)
                {
                    ReportError("Sending offer SDP in non connected state.");
                    return;
                }

                var json = SignalingMessage.CreateJson(_sessionDescriotion);

                //SendPostMessage(MessageType.Message, messageUrl, json);
                //TODO : SignalR SDPOffer Method
                var isOfferSent = await App.HubConnection.InvokeAsync<string>("SendOfferMessage", json);
                //TODO : Here, the you call a method in the SignalR Hub passing in a List of clients you want to call. 

                if (roomConnectionParameters.IsLoopback)
                {
                    // In loopback mode rename this offer to answer and route it back.
                    var sdpAnswer = new SessionDescription(SdpType.Answer, _sessionDescriotion.Sdp);
                    signalingEvents.OnRemoteDescription(sdpAnswer);
                }
            });
        }

        public void SendLocalIceCandidate(IceCandidate _candidate)
        {
            executor.Execute(async () =>
            {
                var json = SignalingMessage.CreateJson(_candidate);
                if (isInitiator)
                {
                    if (App.HubConnection.State != HubConnectionState.Connected)//(State != ConnectionState.Connected)
                    {
                        ReportError("Sending ICE candidate in non connected state.");
                        return;
                    }

                    //SendPostMessage(MessageType.Message, _messageUrl, json);
                    //TODO: SignalR Send LocalIceCandidate as  Initiator
                    var isIceSent = await App.HubConnection.InvokeAsync<string>("SendLocalIceCandidate", App.HubConnection.ConnectionId, json);
                    logger.Info(TAG, $"{isIceSent}");
                }
                else
                {
                    //_wsClient.Send(json);
                    //TODO: SignalR Send LocalIceCandidate as  Participant
                    var isIceSent = await App.HubConnection.InvokeAsync<string>("SendLocalIceCandidate", App.HubConnection.ConnectionId, json);
                    logger.Info(TAG, $"{isIceSent}");
                }
            });
        }

        public void SendLocalIceCandidateRemovals(IceCandidate[] _candidates)
        {
            executor.Execute(async () =>
            {
                var json = SignalingMessage.CreateJson(_candidates);

                if (isInitiator)
                {
                    if (App.HubConnection.State != HubConnectionState.Connected)//(State != ConnectionState.Connected)
                    {
                        ReportError("Sending ICE candidate removals in non connected state.");
                        return;
                    }

                    //SendPostMessage(MessageType.Message, _messageUrl, json);
                    //TODO: SignalR Send message to Remove Ice Candidates as Initiator
                    var isIceRemoved = await App.HubConnection.InvokeAsync<string>("RemoveIceCandidates", json);
                    logger.Info(TAG, $"{isIceRemoved}");
                    if (roomConnectionParameters.IsLoopback)
                    {
                        signalingEvents.OnRemoteIceCandidatesRemoved(_candidates);
                    }
                }
                else
                {
                    //_wsClient.Send(json);
                    //TODO: SignalR Send message to Remove Ice Candidates as Participant
                    var isIceRemoved = await App.HubConnection.InvokeAsync<string>("RemoveIceCandidates", json);
                    logger.Info(TAG, $"{isIceRemoved}");
                    if (roomConnectionParameters.IsLoopback)
                    {
                        signalingEvents.OnRemoteIceCandidatesRemoved(_candidates);
                    }
                }
            });
        }

        public void SendAnswerSdp(SessionDescription _sessionDescription)
        {
            executor.Execute(async () =>
            {
                if (roomConnectionParameters.IsLoopback)
                {
                    logger.Error(TAG, "Sending answer in loopback mode.");
                    return;
                }

                var json = SignalingMessage.CreateJson(_sessionDescription);


                //_wsClient.Send(json);
                //TODO: SignalR SSPAnswer Method
                var isAnswerSent = await App.HubConnection.InvokeAsync<string>("SendAnswerMessage", json);
            });
        }

        #endregion

        #region SignalR Client Function(s)

        public void OnWebSocketClose() => signalingEvents.OnChannelClose();

        public void OnWebSocketMessage(string _message)
        {
            // Check HubConnection is Registered Connected
            if (App.HubConnection.State != HubConnectionState.Connected)
            {
                logger.Error(TAG, $"The SignalR HubConnection is in a Non-Connected State");
                return;
            }

            var _signalingMessage = SignalingMessage.MessageFromJSONString(_message);

            switch (_signalingMessage.Type)
            {
                case SignalingMessageType.Candidate:
                    var candidate = (ICECandidateMessage)_signalingMessage;
                    signalingEvents.OnRemoteIceCandidate(candidate.Candidate);
                    break;
                case SignalingMessageType.CandidateRemoval:
                    var candidates = (ICECandidateRemovalMessage)_signalingMessage;
                    signalingEvents.OnRemoteIceCandidatesRemoved(candidates.Candidates);
                    break;
                case SignalingMessageType.Offer:
                    if (!isInitiator)
                    {
                        var sdp = (SessionDescriptionMessage)_signalingMessage;
                        signalingEvents.OnRemoteDescription(sdp.Description);
                    }
                    else
                    {
                        ReportError($"Received offer for call receiver : {_message}");
                    }
                    break;
                case SignalingMessageType.Answer:
                    if (isInitiator)
                    {
                        var sdp = (SessionDescriptionMessage)_signalingMessage;
                        signalingEvents.OnRemoteDescription(sdp.Description);
                    }
                    else
                    {
                        ReportError($"Received answer for call initiator: {_message}");
                    }
                    break;
                case SignalingMessageType.Bye:
                    signalingEvents.OnChannelClose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnWebSocketError(string _errorDescription) => ReportError($"SignalR WebSocket Error: {_errorDescription}");

        #endregion

    }
}
