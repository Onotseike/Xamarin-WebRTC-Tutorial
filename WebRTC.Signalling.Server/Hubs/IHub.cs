using System.Collections.Generic;

using WebRTC.Signalling.Server.Models;

namespace WebRTC.Signalling.Server.Hubs
{
    public interface IHub
    {
        void IncomingCall(string callerId);

        void CallDeclined(string _clientId, string _message, string _username);
        void CallDeclined(string _clientId, string _message);

        void CallEnded(string _targetClientId, string _message, string _username);
        void CallEnded(string _targetClientId, string _message);

        void CallAccepted(Client caller);

        void ReceiveSignal(Client caller, string signal);

        void UpdateHubClientsList(List<Client> _clients);

    }
}
