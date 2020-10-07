// onotseike@hotmail.comPaula Aliu
using WebRTC.Unified.Enums;

using static WebRTC.Unified.Core.DataChannel;

namespace WebRTC.Unified.Core.Interfaces
{
    public delegate void IStatisticsCompletionHandler(IStatisticsReport report);

    public interface IPeerConnection : INativeObject
    {
        IMediaStream[] LocalStreams { get; }

        SessionDescription LocalDescription { get; }
        SessionDescription RemoteDescription { get; }

        SignalingState SignalingState { get; }
        IceConnectionState IceConnectionState { get; }
        PeerConnectionState ConnectionState { get; }
        IceGatheringState IceGatheringState { get; }

        RTCConfiguration Configuration { get; }

        IRtpSender[] Senders { get; }
        IRtpReceiver[] Receivers { get; }
        IRtpTransceiver[] Transceivers { get; }


        bool SetConfiguration(RTCConfiguration configuration);
        void Close();


        void AddIceCandidate(IceCandidate iceCandidate);
        void RemoveIceCandidates(IceCandidate[] iceCandidates);

        void AddStream(IMediaStream mediaStream);
        void RemoveStream(IMediaStream mediaStream);

        IRtpSender AddTrack(IMediaStreamTrack mediaStreamTrack, string[] streamIds);
        bool RemoveTrack(IRtpSender rtpSender);

        IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack);
        IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack, IRtpTransceiverInit transceiverInit);
        IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType);
        IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType, IRtpTransceiverInit transceiverInit);

        void OfferForConstraints(MediaConstraints mediaConstraints, ISdpObserver sdpObserver);
        void AnswerForConstraints(MediaConstraints mediaConstraints, ISdpObserver sdpObserver);

        void SetLocalDescription(SessionDescription sessionDescription, ISdpObserver sdpObserver);
        void SetRemoteDescription(SessionDescription sessionDescription, ISdpObserver sdpObserver);

        bool SetBweMinBitrateBps(int minBitrateBps, int currentBitrateBps, int maxBitrateBps);
        bool StartRtcEventLogWithFilePath(string filePath, long maxSizeInBytes);

        void StopRtcEventLog();
    }

    public interface IPeerConnectionStats : IPeerConnection
    {
        void StatsForTrack(IMediaStreamTrack mediaStreamTrack, StatsOutputLevel statsOutputLevel);
        void StatisticsWithCompletionHandler(IStatisticsCompletionHandler completionHandler);
        void StatisticsForSender(IRtpSender sender, IStatisticsCompletionHandler completionHandler);
        void StatisticsForReceiver(IRtpReceiver receiver, IStatisticsCompletionHandler completionHandler);
    }

    public interface IPeerConnectionMedia : IPeerConnection
    {
        IRtpSender SenderWithKind(string kind, string streamId);
    }

    public interface IPeerConnectionDataChannel : IPeerConnection
    {
        IDataChannel DataChannelForLabel(string label, DataChannelConfiguration configuration);
    }
}
