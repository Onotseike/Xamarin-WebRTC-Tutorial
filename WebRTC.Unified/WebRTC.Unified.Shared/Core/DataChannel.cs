// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Enums;

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

        public class DataChannelConfiguration
        {
            public bool IsOrdered { get; set; } = true;

            public int MaxRetransmitTimeMs { get; set; } = -1;

            public int MaxPacketLifeTime { get; set; } = -1;

            public int MaxRetransmits { get; set; } = -1;

            public bool IsNegotiated { get; set; }

            public int StreamId { get; set; } = -1;

            public int ChannelId { get; set; } = -1;

            public string Protocol { get; set; } = "";
        }

        public interface IDataChannel : IDisposable
        {
            string Label { get; }

            //int StreamId { get; }

            int ChannelId { get; }

            DataChannelState ReadyState { get; }

            long BufferedAmount { get; }

            void Close();

            bool SendData(DataBuffer data);

            event EventHandler OnStateChange;
            event EventHandler<DataBuffer> OnMessage;
            event EventHandler<long> OnBufferedAmountChange;
        }
    }


}
