﻿// onotseike@hotmail.comPaula Aliu

using Web.Unified.Enums;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.Core.Interfaces
{
    public interface IDataChannel : INativeObject
    {
        string Label { get; }

        bool IsReliable { get; }

        bool IsOrdered { get; }

        int MaxRetransmitTime { get; }

        int MaxPacketLifeTime { get; }

        int MaxRetransmits { get; }

        string Protocol { get; }

        bool IsNegotiated { get; }

        int StreamId { get; }

        int ChannelId { get; }

        DataChannelState ReadyState { get; }

        long BufferedAmount { get; }

        void Close();

        bool SendData(DataBuffer data);
    }
}
