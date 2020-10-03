// onotseike@hotmail.comPaula Aliu
using System;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Enums;
using WebRTC.Unified.iOS.Extensions;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.iOS
{
    internal class PlatformDataChannel : NSObject, IDataChannel, IRTCDataChannelDelegate
    {
        private readonly RTCDataChannel _dataChannel;
        public PlatformDataChannel(RTCDataChannel dataChannel)
        {
            _dataChannel = dataChannel;
            _dataChannel.Delegate = this;
        }


        #region IDataChannel Implements

        public string Label => _dataChannel.Label;

        public int ChannelId => _dataChannel.ChannelId;

        public DataChannelState ReadyState => _dataChannel.ReadyState.ToNativePort();

        public long BufferedAmount => (long)_dataChannel.BufferedAmount;

        public event EventHandler OnStateChange;
        public event EventHandler<DataBuffer> OnMessage;
        public event EventHandler<long> OnBufferedAmountChange;

        public void Close() => _dataChannel.Close();

        protected override void Dispose(bool disposing)
        {
            if (disposing) _dataChannel.Delegate = null;
            base.Dispose(disposing);
        }

        public bool SendData(DataBuffer data) => _dataChannel.SendData(new RTCDataBuffer(NSData.FromArray(data.Data), data.IsBinary));

        #endregion

        #region IRTCDataChannelDelegate Implements

        public void DataChannelDidChangeState(RTCDataChannel dataChannel) => OnStateChange?.Invoke(this, EventArgs.Empty);

        public void DataChannel(RTCDataChannel dataChannel, RTCDataBuffer buffer) => OnMessage?.Invoke(this, new DataBuffer(buffer.Data.ToArray(), buffer.IsBinary));

        #endregion
    }
}
