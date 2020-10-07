// onotseike@hotmail.comPaula Aliu
using System.Collections.Generic;

namespace WebRTC.Unified.Core
{
    public class MediaConstraints
    {
        public Dictionary<string, string> Mandatory { get; set; }
        public Dictionary<string, string> Optional { get; set; }

        public MediaConstraints(Dictionary<string, string> mandatory = null, Dictionary<string, string> optional = null)
        {
            Mandatory = mandatory;
            Optional = optional;
        }
    }
}
