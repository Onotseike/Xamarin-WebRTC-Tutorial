// onotseike@hotmail.comPaula Aliu


namespace WebRTC.Unified.Core.Interfaces
{
    public interface IPeerConnectionFactory : INativeObject
    {
        IAudioSource AudioSourceWithConstraints(MediaConstraints mediaConstraints);

        // IAudioTrack AudioTrackWithTrackId(string trackId);
        IAudioTrack AudioTrackWithSource(IAudioSource audioSource, string trackId);

        IVideoSource VideoSource { get; }

        IVideoTrack VideoTrackWithSource(IVideoSource videoSource, string trackId);

        IMediaStream MediaStreamWithStreamId(string streamId);

        IPeerConnection PeerConnectionWithConfiguration(RTCConfiguration configuration, MediaConstraints constraints, IPeerConnectionDelegate peerConnectionDelegate);

        //void SetOptions(IPeerConnectionFactoryOptions options);

        bool StartAecDumpWithFilePath(string filePath, long maxSizeInBytes);
        void StopAecDump();
    }

    public interface IPeerConnectionFactoryOptions : INativeObject
    {
        bool DisableEncrytion { get; set; }
        bool DisableNetworkMonitor { get; set; }
        bool IgnoreLoopbackNetworkAdapter { get; set; }
        bool IgnoreVPNNetworkAdapter { get; set; }
        bool IgnoreCellularNetworkAdapter { get; set; }
        bool IgnoreWiFiNetworkAdapter { get; set; }
        bool IgnoreEthernetNetworkAdapter { get; set; }
    }
}
