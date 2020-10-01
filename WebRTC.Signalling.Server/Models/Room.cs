using System;
using System.Collections.Generic;
using System.Linq;

namespace WebRTC.Signalling.Server.Models
{
    public class Room
    {
        #region Properties

        public Guid RoomId { get; set; }
        public List<Client> Occupants { get; set; }
        public bool IsVideo { get; set; }
        private int MaxOccupancy { get; set; }

        #endregion

        public Room(string _roomId, int _maxOccupancy)
        {
            RoomId = new Guid(_roomId);
            MaxOccupancy = _maxOccupancy;
            Occupants = new List<Client>();
            IsVideo = true;
        }

        #region Function(s)

        public Tuple<bool, string> AddClient(Client _client)
        {
            if (Occupants?.Count < MaxOccupancy)
            {
                Occupants?.Add(_client);
                return Tuple.Create(true, $"Client with ID : {_client.ClientId} was added to Room with RoomId : {RoomId}");
            }
            if ((bool)Occupants?.Any(occupant => occupant.IsInitiator) && _client.IsInitiator)
            {
                Occupants?.Add(_client);
                return Tuple.Create(true, $"Client with ID : {_client.ClientId} was added to Room with RoomId : {RoomId} as a participant.");
            }

            return Tuple.Create(false, $"The Room with ID : {RoomId} has reached MAXIMUM Occupancy of {MaxOccupancy}");
        }

        public Tuple<bool, string> AddClients(Tuple<Client, Client> _clients)
        {
            if (Occupants?.Count < MaxOccupancy && Occupants?.Count + 2 < MaxOccupancy)
            {
                Occupants?.AddRange(new List<Client> { _clients.Item1, _clients.Item2 });
                return Tuple.Create(true, $"Clients with IDs : {_clients.Item1.ClientId} and {_clients.Item2.ClientId} were added to Room with RoomId : {RoomId}");
            }
            if ((bool)Occupants?.Any(occupant => occupant.IsInitiator) && _clients.Item1.IsInitiator)
            {
                return Tuple.Create(false, $"Client with ID : {_clients.Item1.ClientId} can't be an Initiator to Room with RoomId : {RoomId}.");
            }

            return Tuple.Create(false, $"The Room with ID : {RoomId} has reached MAXIMUM Occupancy of {MaxOccupancy}");
        }

        public Tuple<bool, string> RemoveClient(Client _client) => (bool)Occupants?.Remove(_client) ? Tuple.Create(true,
                    $"The Client with ID : {_client.ClientId} was added as an occupant of the Room with RoomId : {RoomId}")
                : Tuple.Create(false, $"The Client with ID : {_client.ClientId} is not an Occupant of the Room with RoomId : {RoomId}");

        public Tuple<bool, string> IsClientAnOccupant(Client _client) => (bool)Occupants?.Any(occupant => occupant.ClientId == _client.ClientId) ? Tuple.Create(true, $"Client with ID : {_client.ClientId} is an Occupant of Room with RoomId : {RoomId}") : Tuple.Create(false, $"The Client with ID : {_client.ClientId} is not an Occupant of the Room with RoomId : {RoomId}");

        public Tuple<bool, string> UpdateClient(Client _client)
        {
            var isOccupant = IsClientAnOccupant(_client);
            if (isOccupant.Item1)
            {
                var removedCount = (int)Occupants?.RemoveAll(client => client.ClientId == _client.ClientId);
                if (removedCount > 0)
                {
                    Occupants.Add(_client);
                    return Tuple.Create(true, $"Client with  ID: {_client.ClientId} was updated");
                }
                return Tuple.Create(false, $"Client with  ID: {_client.ClientId} was not updated");
            }

            return isOccupant;
        }

        #endregion

        #region Exception(s)

        private Exception MaxOccupancyException(string _roomId, string _maxOccupancy) => new Exception($"The Room with ID : {_roomId} has reached MAXIMUM Occupancy of {_maxOccupancy}");

        #endregion
    }
}
