using System;
using System.Runtime.InteropServices;

using CoreFoundation;

using WebRTC.DemoApp.SignalRClient.Abstractions;
using WebRTC.Unified.iOS;

namespace WebRTC.DemoApp.iOS.Helpers
{
    public static class WebRTCPlatform
    {
        public static void Init()
        {
            Platform.Initialize();
            //WebSocketConnectionFactory.Factory = () => new WebSocketConnection();
            ExecutorServiceFactory.MainExecutor = new MainExecutor();
        }


        public static void Cleanup()
        {
            Platform.Cleanup();
        }


        private class MainExecutor : IExecutor
        {
            public bool IsCurrentExecutor => DispatchQueue.MainQueue == DispatchQueue.CurrentQueue;

            public void Execute(Action action) => DispatchQueue.MainQueue.DispatchAsync(action);
        }

        private class ExecutorService : IExecutorService
        {
            [DllImport(ObjCRuntime.Constants.libcLibrary)]
            extern static IntPtr dispatch_release(IntPtr o);

            private readonly DispatchQueue _dispatchQueue;

            public ExecutorService(string tag)
            {
                _dispatchQueue = new DispatchQueue(tag, new DispatchQueue.Attributes
                {
                    QualityOfService = DispatchQualityOfService.Background,
                    Concurrent = false
                });
            }

            public bool IsCurrentExecutor => _dispatchQueue == DispatchQueue.CurrentQueue;

            public void Execute(Action action) => _dispatchQueue.DispatchAsync(action);


            public void Release()
            {
                dispatch_release(_dispatchQueue.Handle);
            }
        }

        //private class WebSocketConnection : IWebSocketConnection
        //{
        //    private WebSocket _webSocket;


        //    public event EventHandler OnOpened;
        //    public event EventHandler<(int code, string reason)> OnClosed;
        //    public event EventHandler<Exception> OnError;
        //    public event EventHandler<string> OnMessage;

        //    public void Dispose()
        //    {
        //        if (_webSocket != null)
        //            UnWire(_webSocket);
        //        Close();
        //        _webSocket?.Dispose();
        //        _webSocket = null;
        //    }

        //    public void Open(string url, string protocol = null, string authToken = null)
        //    {
        //        if (_webSocket != null)
        //        {
        //            UnWire(_webSocket);
        //        }

        //        Close();

        //        var nsUrl = NSUrl.FromString(url);
        //        _webSocket = !string.IsNullOrEmpty(protocol)
        //            ? new WebSocket(nsUrl, new NSObject[] { new NSString(protocol) })
        //            : new WebSocket(nsUrl);
        //        Wire(_webSocket);
        //        _webSocket.Open();
        //    }

        //    public void Close()
        //    {
        //        _webSocket?.Close();
        //    }

        //    public void Send(string message)
        //    {
        //        _webSocket.Send(new NSString(message, NSStringEncoding.UTF8));
        //    }

        //    private void Wire(WebSocket socket)
        //    {
        //        socket.WebSocketOpened += WebSocketDidOpen;
        //        socket.WebSocketFailed += WebSocketDidFailWithError;
        //        socket.WebSocketClosed += WebSocketDidClose;
        //        socket.ReceivedMessage += WebSocketDidReceiveMessage;
        //    }

        //    private void UnWire(WebSocket socket)
        //    {
        //        socket.WebSocketOpened -= WebSocketDidOpen;
        //        socket.WebSocketFailed -= WebSocketDidFailWithError;
        //        socket.WebSocketClosed -= WebSocketDidClose;
        //        socket.ReceivedMessage -= WebSocketDidReceiveMessage;
        //    }

        //    private void WebSocketDidOpen(object sender, EventArgs e)
        //    {
        //        OnOpened?.Invoke(this, EventArgs.Empty);
        //    }

        //    private void WebSocketDidReceiveMessage(object sender, WebSocketReceivedMessageEventArgs e)
        //    {
        //        OnMessage?.Invoke(this, e.Message?.ToString());
        //    }

        //    private void WebSocketDidFailWithError(object sender, WebSocketFailedEventArgs e)
        //    {
        //        OnError?.Invoke(this, new Exception(e.Error.LocalizedDescription));
        //    }

        //    private void WebSocketDidClose(object sender, WebSocketClosedEventArgs e)
        //    {
        //        OnClosed?.Invoke(this, ((int)e.Code, e.Reason));
        //    }
        //}
    }
}
