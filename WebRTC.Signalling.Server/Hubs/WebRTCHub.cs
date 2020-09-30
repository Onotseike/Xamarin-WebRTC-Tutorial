using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

using WebRTC.Signalling.Server.Models;

namespace WebRTC.Signalling.Server.Hubs
{
    public class WebRTCHub : Hub<IHub>
    {
        #region Properties

        private List<Room> Rooms { get; set; }
        private List<Client> HubClients { get; set; }
        private List<CallOffer> CallOffers { get; set; }

        #endregion

        #region Constructor(s)

        public WebRTCHub()
        {
            Rooms = new List<Room>();
            HubClients = new List<Client>();
            CallOffers = new List<CallOffer>();
        }

        #endregion

        #region Hub Override Method(s)

        public override Task OnDisconnectedAsync(Exception exception)
        {
            HangUp();
            HubClients.RemoveAll(client => client.ClientId.ToString() == Context.ConnectionId);
            BroadcastHubClients();

            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Main Function(s)

        public void JoinHub(string _username)
        {
            if (!HubClients.Exists(client => client.ClientId.ToString() == Context.ConnectionId))
            {
                HubClients.Add(new Client(Context.ConnectionId, _username));
            }


            BroadcastHubClients();
        }

        public void CallHubClient(string _targetClientId)
        {
            var caller = HubClients.SingleOrDefault(client => client.ClientId.ToString() == Context.ConnectionId);
            var callee = HubClients.SingleOrDefault(client => client.ClientId.ToString() == _targetClientId);

            if (callee == null)
            {
                Clients.Caller.CallDeclined(_targetClientId, $"The use you called is not available.");
                return;
            }

            if (IsHubClientInRoom(callee.ClientId.ToString()) != null)
            {
                Clients.Caller.CallDeclined(_targetClientId, $"The Client with ClientId: {_targetClientId} is already in a call.", callee.Username);
                return;
            }

            Clients.Client(_targetClientId).IncomingCall(caller.ClientId.ToString());

            if (caller.InRoom && caller.IsInitiator)
            {
                UpdateCallOffer(callee, caller);
            }
            else
            {
                CallOffers.Add(new CallOffer
                {
                    Initiator = caller,
                    Participants = new List<Client> { callee }
                });
            }


        }

        public void AnwerCall(bool _acceptCall, string _targetClientId, int _maxOccupancy = 2)
        {
            var caller = HubClients.SingleOrDefault(client => client.ClientId.ToString() == Context.ConnectionId);
            var callee = HubClients.SingleOrDefault(client => client.ClientId.ToString() == _targetClientId);

            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (caller == null) return;

            if (callee == null)
            {
                Clients.Caller.CallEnded(_targetClientId, $"The Participant with Caller ClientId : {_targetClientId}, has left the call");
                return;
            }

            if (!_acceptCall)
            {
                Clients.Client(_targetClientId).CallDeclined(callee.ClientId.ToString(), $"Participant with ClientId: {_targetClientId} did not  accept your call.", callee.Username);
                return;
            }

            var callOfferCount = CallOffers.RemoveAll(offer => offer.Initiator.ClientId == caller.ClientId && offer.Participants.FirstOrDefault(client => client.ClientId == callee.ClientId) != null);
            if (callOfferCount < 1)
            {
                Clients.Caller.CallEnded(_targetClientId, $"Participant with Clientid: {_targetClientId} has already hung up.", callee.Username);
                return;
            }

            if (IsHubClientInRoom(callee.ClientId.ToString()) != null)
            {
                Clients.Caller.CallDeclined(_targetClientId, $"Participant with ClientId: {_targetClientId} is currently engaged in another call.", callee.Username);
                return;
            }

            CallOffers.RemoveAll(offer => offer.Initiator.ClientId == caller.ClientId);

            var newRoom = new Room(Guid.NewGuid().ToString(), _maxOccupancy);
            newRoom.AddClients(UpdateClientStates(caller, callee));
            Rooms.Add(newRoom);

            Clients.Client(_targetClientId).CallAccepted(caller);

            BroadcastHubClients();

        }

        public void HangUp()
        {
            var caller = HubClients.SingleOrDefault(client => client.ClientId.ToString() == Context.ConnectionId);
            if (caller == null) return;

            var room = IsHubClientInRoom(caller.ClientId.ToString());
            if (room != null)
            {
                foreach (var occupant in room.Occupants.Where(_occupant => _occupant.ClientId != caller.ClientId))
                {
                    Clients.Client(occupant.ClientId.ToString()).CallEnded(caller.ClientId.ToString(), $"Participant with ClientId: {caller.ClientId} has Hung up.", caller.Username);
                }
                room.Occupants.RemoveAll(occupant => occupant.ClientId == caller.ClientId);
                if (room.Occupants.Count < 2)
                {
                    Rooms.RemoveAll(_room => _room.RoomId == room.RoomId);
                }
            }

            CallOffers.RemoveAll(offer => offer.Initiator.ClientId == caller.ClientId || offer.Participants.Any(participant => participant.ClientId == caller.ClientId));
            BroadcastHubClients();

        }

        public void SendSignal(string _signal, string _targetClientId)
        {
            var caller = HubClients.SingleOrDefault(client => client.ClientId.ToString() == Context.ConnectionId);
            var callee = HubClients.SingleOrDefault(client => client.ClientId.ToString() == _targetClientId);

            if (caller == null || callee == null) return;

            var room = IsHubClientInRoom(_targetClientId);

            if (room != null && room.Occupants.Exists(_occupant => _occupant.ClientId == callee.ClientId))
            {
                Clients.Client(_targetClientId).ReceiveSignal(caller, _signal);
            }

        }

        #endregion

        #region Helper Function(s)

        private Room IsHubClientInRoom(string _clientId) => Rooms.Where(room => room.Occupants.Any(occupant => occupant.ClientId.ToString() == _clientId)).FirstOrDefault();

        private void BroadcastHubClients()
        {
            HubClients.ForEach(client => client.InRoom = (IsHubClientInRoom(client.ClientId.ToString()) != null));
            Clients.All.UpdateHubClientsList(HubClients);
        }

        private CallOffer GetCallOffer(string _callerId) => CallOffers.SingleOrDefault(offer => offer.Initiator.ClientId.ToString() == _callerId);

        private Tuple<bool, string> UpdateCallOffer(Client _participant, Client _initiator)
        {
            var callOffer = GetCallOffer(_initiator.ClientId.ToString());
            if (callOffer != null)
            {
                CallOffers.Remove(callOffer);
                callOffer.Participants.Add(_participant);
                CallOffers.Add(callOffer);
                return Tuple.Create(true, $"Participant with ClientId: {_participant.ClientId} has been added");
            }
            return Tuple.Create(false, $"Participant with ClientId : {_participant.ClientId} could not be added");
        }

        private Tuple<Client, Client> UpdateClientStates(Client _caller, Client _callee)
        {
            _caller.InRoom = true;
            _caller.IsInitiator = true;
            _callee.InRoom = true;
            _callee.IsInitiator = false;
            return Tuple.Create(_caller, _callee);
        }
        #endregion
    }
}
