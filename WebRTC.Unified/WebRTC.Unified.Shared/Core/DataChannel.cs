// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Core
{
    public class DataChannel
    {
        public class DataBuffer
        {
            public DataBuffer(byte[] data, bool isBinary)
            {
                Data = data;
                IsBinary = isBinary;
            }

            public bool IsBinary { get; }
            public byte[] Data { get; }
        }
    }

    public interface DataChannelConfiguration
    {
        public bool IsOrdered { get; set; }

        public int MaxRetransmitTimeMs { get; set; }

        public int MaxPacketLifeTime { get; set; }

        public int MaxRetransmits { get; set; }

        public bool IsNegotiated { get; set; }

        public int StreamId { get; set; }

        public int ChannelId { get; set; }

        public string Protocol { get; set; }
    }
}
