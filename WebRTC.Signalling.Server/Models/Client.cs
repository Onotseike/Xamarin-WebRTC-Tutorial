using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using WebRTC.Signalling.Server.Models.Complex;

namespace WebRTC.Signalling.Server.Models
{
    public class Client
    {
        #region Properties

        [JsonProperty("ClientId")]
        public string ClientId { get; set; }

        [JsonProperty("InRoom")]
        public bool InRoom { get; set; }

        [JsonProperty("IsInitiator")]
        public bool IsInitiator { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Messages")]
        public List<SignalMessage> Messages { get; set; }

        [JsonProperty("IceCandidate")]
        public List<IceCandidate> Candidate { get; set; }

        [JsonProperty("Offer")]
        public SessionDescription SessionDescriptionOffer { get; set; }

        [JsonProperty("Answer")]
        public SessionDescription SessionDescriptionAnswer { get; set; }

        #endregion

        public Client(string _clientId, string _username = "New Client")
        {
            ClientId = _clientId;
            Username = _username;
            Messages = new List<SignalMessage>();
            Candidate = new List<IceCandidate>();
        }


        #region Function(s)

        public void AddMessage(SignalMessage _signalMessage) => Messages?.Add(_signalMessage);
        public void AddMessages(List<SignalMessage> _signalMessages) => Messages?.AddRange(_signalMessages);

        public void ClearMessage(SignalMessage _signalMessage) => Messages?.RemoveAll(message => message.MesssageId == _signalMessage.MesssageId);
        public void ClearAllMessages() => Messages?.Clear();

        #endregion
    }
}
