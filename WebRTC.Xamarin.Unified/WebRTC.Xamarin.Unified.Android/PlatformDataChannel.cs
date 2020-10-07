// onotseike@hotmail.comPaula Aliu


using System;

using Android.OS;

using Java.Nio;

using Org.Webrtc;

using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.Android
{
    internal class PlatformDataChannel : Java.Lang.Object, IDataChannel, DataChannel.IObserver
    {
        private readonly DataChannel _dataChannel;

        private readonly Handler _handler = new Handler(Looper.MainLooper);

        public PlatformDataChannel(DataChannel dataChannel)
        {
            _dataChannel = dataChannel;
            _dataChannel.RegisterObserver(this);
        }


        #region IDataChannel Implements

        public string Label => _dataChannel.Label();

        public int ChannelId => _dataChannel.Id();

        public DataChannelState ReadyState => _dataChannel.InvokeState().ToNativePort();

        public long BufferedAmount => _dataChannel.BufferedAmount();

        public event EventHandler OnStateChange;
        public event EventHandler<DataBuffer> OnMessage;
        public event EventHandler<long> OnBufferedAmountChange;

        public void Close() => _dataChannel.Close();

        public bool SendData(DataBuffer data) => _dataChannel.Send(new DataChannel.Buffer(ByteBuffer.Wrap(data.Data), data.IsBinary));

        protected override void Dispose(bool disposing)
        {
            if (disposing) _dataChannel.UnregisterObserver();

            base.Dispose(disposing);
        }

        #endregion

        #region DataChannel.IObserver Implements.

        void DataChannel.IObserver.OnBufferedAmountChange(long previousAmount) => _handler.Post(() => { OnBufferedAmountChange?.Invoke(this, previousAmount); });

        void DataChannel.IObserver.OnMessage(DataChannel.Buffer buffer) => _handler.Post(() =>
                                                                         {
                                                                             var _buffer = new byte[buffer.Data.Remaining()];
                                                                             buffer.Data.Get(_buffer, 0, _buffer.Length);
                                                                             OnMessage?.Invoke(this, new DataBuffer(_buffer, buffer.Binary));
                                                                         });

        void DataChannel.IObserver.OnStateChange() => _handler.Post(() => OnStateChange?.Invoke(this, EventArgs.Empty));
        #endregion

    }
}
