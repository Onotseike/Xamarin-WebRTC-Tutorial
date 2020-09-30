using System;

namespace WebRTC.Signalling.Server.Models
{
    public class SignalMessage
    {
        #region Properties
        
        public Guid MesssageId { get; set; }
        public string Data { get; set; }

        #endregion
    }
}
