using System;

using Newtonsoft.Json;

namespace WebRTC.Signalling.Server.Models
{
    public class SignalMessage
    {
        #region Properties

        [JsonProperty("MessageId")]
        public Guid MesssageId { get; set; }

        [JsonProperty("Data")]
        public string Data { get; set; }

        #endregion
    }
}
