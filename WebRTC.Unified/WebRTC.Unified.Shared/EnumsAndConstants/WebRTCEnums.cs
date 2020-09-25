// onotseike@hotmail.comPaula Aliu
namespace WebRTC.Unified.Enums
{

    public enum VideoRotation : long
    {
        VideoRotation_0 = 0,
        VideoRotation_90 = 90,
        VideoRotation_180 = 180,
        VideoRotation_270 = 270
    }


    public enum FrameType : ulong
    {
        EmptyFrame = 0,
        AudioFrameSpeech = 1,
        AudioFrameCN = 2,
        VideoFrameKey = 3,
        VideoFrameDelta = 4
    }


    public enum VideoContentType : ulong
    {
        Unspecified,
        Screenshare
    }


    public enum LoggingSeverity : long
    {
        Verbose,
        Info,
        Warning,
        Error,
        None
    }

    public enum FileLoggerRotationType : ulong
    {
        Call,
        App,
    }

    public enum VideoCodecMode : ulong
    {
        RealtimeVideo,
        Screensharing
    }



    public enum SourceState : long
    {
        Initializing,
        Live,
        Ended,
        Muted
    }


    public enum MediaStreamTrackState : long
    {
        Live,
        Ended
    }


    public enum IceTransportPolicy : long
    {
        None,
        Relay,
        NoHost,
        All
    }


    public enum BundlePolicy : long
    {
        Balanced,
        MaxCompat,
        MaxBundle
    }


    public enum RtcpMuxPolicy : long
    {
        Negotiate,
        Require
    }


    public enum TcpCandidatePolicy : long
    {
        Enabled,
        Disabled
    }


    public enum CandidateNetworkPolicy : long
    {
        All,
        LowCost
    }


    public enum ContinualGatheringPolicy : long
    {
        Once,
        Continually
    }


    public enum EncryptionKeyType : long
    {
        Rsa,
        Ecdsa
    }


    public enum SdpSemantics : long
    {
        PlanB,
        UnifiedPlan
    }


    public enum DataChannelState : long
    {
        Connecting,
        Open,
        Closing,
        Closed
    }


    public enum TlsCertPolicy : ulong
    {
        Secure,
        InsecureNoCheck
    }


    public enum SignalingState : long
    {
        Stable,
        HaveLocalOffer,
        HaveLocalPrAnswer,
        HaveRemoteOffer,
        HaveRemotePrAnswer,
        Closed
    }


    public enum IceConnectionState : long
    {
        New,
        Checking,
        Connected,
        Completed,
        Failed,
        Disconnected,
        Closed,
        Count
    }


    public enum PeerConnectionState : long
    {
        New,
        Connecting,
        Connected,
        Disconnected,
        Failed,
        Closed
    }


    public enum IceGatheringState : long
    {
        New,
        Gathering,
        Complete
    }


    public enum StatsOutputLevel : long
    {
        Standard,
        Debug
    }


    public enum Priority : long
    {
        VeryLow,
        Low,
        Medium,
        High
    }


    public enum DegradationPreference : long
    {
        Disabled,
        MaintainFramerate,
        MaintainResolution,
        Balanced
    }


    public enum RtpMediaType : long
    {
        Audio,
        Video,
        Data
    }


    public enum RtpTransceiverDirection : long
    {
        SendRecv,
        SendOnly,
        RecvOnly,
        Inactive,
        Stopped
    }


    public enum SdpType : long
    {
        Offer,
        PrAnswer,
        Answer
    }


    public enum FileLoggerSeverity : ulong
    {
        Verbose,
        Info,
        Warning,
        Error
    }

    public enum ScalingType
    {
        AspectFit,
        AspectFill,
        AspectBalanced
    }


}
