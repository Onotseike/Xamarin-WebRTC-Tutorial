// onotseike@hotmail.comPaula Aliu
using System;

using WebRTC.Unified.Core.Interfaces;

namespace WebRTC.DemoApp.SignalRClient.Abstractions
{
    public class RTCEventLog
    {
        #region Enum RTCEventLogState

        private enum RTCEventLogState
        {
            Inactive,
            Started,
            Stopped
        }

        #endregion

        private const string TAG = nameof(RTCEventLog);

        private static int OutputFileMaxBytes = 10_000_000;

        private readonly IPeerConnection _peerConnection;
        private readonly ILogger _logger;
        private readonly string _file;

        private RTCEventLogState _state;


        public RTCEventLog(IPeerConnection peerConnection, string file, ILogger logger = null)
        {
            _peerConnection = peerConnection ?? throw new ArgumentNullException(nameof(peerConnection), "The peer connection is null.");
            _logger = logger ?? new ConsoleLogger();
            _file = file;
        }

        public void Start()
        {
            if (_state == RTCEventLogState.Started)
            {
                _logger.Debug(TAG, "RtcEventLog has already started.");
                return;
            }

            var success = _peerConnection.StartRtcEventLogWithFilePath(_file, OutputFileMaxBytes);
            if (!success)
            {
                _logger.Error(TAG, "Failed to start RTC event log.");
                return;
            }

            _state = RTCEventLogState.Started;
            _logger.Debug(TAG, "RtcEventLog started.");
        }

        public void Stop()
        {
            if (_state != RTCEventLogState.Started)
            {
                _logger.Error(TAG, "RtcEventLog was not started.");
                return;
            }
            _peerConnection.StopRtcEventLog();
            _state = RTCEventLogState.Stopped;
            _logger.Debug(TAG, "RtcEventLog stopped.");
        }
    }
}