using System;
using System.Collections.Generic;

namespace WebRTC.Signalling.Server.Models
{
    public class Client
    {
        #region Properties

        public Guid ClientId { get; set; }
        public bool InRoom { get; set; }
        public bool IsInitiator { get; set; }
        public string Username { get; set; }
        public List<SignalMessage> Messages { get; set; }

        #endregion

        public Client(string _clientId, string _username = "New Client")
        {
            ClientId = new Guid(_clientId);
            Username = _username;
            Messages = new List<SignalMessage>();
        }


        #region Function(s)

        public void AddMessage(SignalMessage _signalMessage) => Messages?.Add(_signalMessage);
        public void AddMessages(List<SignalMessage> _signalMessages) => Messages?.AddRange(_signalMessages);

        public void ClearMessage(SignalMessage _signalMessage) => Messages?.RemoveAll(message => message.MesssageId == _signalMessage.MesssageId);
        public void ClearAllMessages() => Messages?.Clear();

        #endregion
    }
}
