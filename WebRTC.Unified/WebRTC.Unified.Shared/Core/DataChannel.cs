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
}
