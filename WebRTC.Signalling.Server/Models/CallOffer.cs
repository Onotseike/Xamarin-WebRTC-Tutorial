using System.Collections.Generic;

namespace WebRTC.Signalling.Server.Models
{
    public class CallOffer
    {
        #region Properties

        public Client Initiator { get; set; }
        public List<Client> Participants { get; set; } = new List<Client>();

        #endregion
    }
}
