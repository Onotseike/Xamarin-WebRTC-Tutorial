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


    public enum VideoCodecMode : ulong
    {
        RealtimeVideo,
        Screensharing
    }


    public enum H264PacketizationMode : ulong
    {
        Nolongerleaved = 0,
        SingleNalUnit
    }


    public enum H264Profile : ulong
    {
        ConstrainedBaseline,
        Baseline,
        Main,
        ConstrainedHigh,
        High
    }


    public enum H264Level : ulong
    {
        H264Level1_b = 0,
        H264Level1 = 10,
        H264Level1_1 = 11,
        H264Level1_2 = 12,
        H264Level1_3 = 13,
        H264Level2 = 20,
        H264Level2_1 = 21,
        H264Level2_2 = 22,
        H264Level3 = 30,
        H264Level3_1 = 31,
        H264Level3_2 = 32,
        H264Level4 = 40,
        H264Level4_1 = 41,
        H264Level4_2 = 42,
        H264Level5 = 50,
        H264Level5_1 = 51,
        H264Level5_2 = 52
    }


    public enum DispatcherQueueType : long
    {
        Main,
        CaptureSession,
        AudioSession,
        NetworkMonitor
    }


    public enum DeviceType : long
    {
        Unknown,
        IPhone1G,
        IPhone3G,
        IPhone3GS,
        IPhone4,
        IPhone4Verizon,
        IPhone4S,
        IPhone5GSM,
        IPhone5GSM_CDMA,
        IPhone5CGSM,
        IPhone5CGSM_CDMA,
        IPhone5SGSM,
        IPhone5SGSM_CDMA,
        IPhone6Plus,
        IPhone6,
        IPhone6S,
        IPhone6SPlus,
        IPhone7,
        IPhone7Plus,
        IPhoneSE,
        IPhone8,
        IPhone8Plus,
        IPhoneX,
        IPhoneXS,
        IPhoneXSMax,
        IPhoneXR,
        IPhone11,
        IPhone11Pro,
        IPhone11ProMax,
        IPodTouch1G,
        IPodTouch2G,
        IPodTouch3G,
        IPodTouch4G,
        IPodTouch5G,
        IPodTouch6G,
        IPodTouch7G,
        IPad,
        IPad2Wifi,
        IPad2GSM,
        IPad2CDMA,
        IPad2Wifi2,
        IPadMiniWifi,
        IPadMiniGSM,
        IPadMiniGSM_CDMA,
        IPad3Wifi,
        IPad3GSM_CDMA,
        IPad3GSM,
        IPad4Wifi,
        IPad4GSM,
        IPad4GSM_CDMA,
        IPad5,
        IPad6,
        IPadAirWifi,
        IPadAirCellular,
        IPadAirWifiCellular,
        IPadAir2,
        IPadMini2GWifi,
        IPadMini2GCellular,
        IPadMini2GWifiCellular,
        IPadMini3,
        IPadMini4,
        IPadPro9Inch,
        IPadPro12Inch,
        IPadPro12Inch2,
        IPadPro10Inch,
        IPad7Gen10Inch,
        IPadPro3Gen11Inch,
        IPadPro3Gen12Inch,
        IPadMini5Gen,
        IPadAir3Gen,
        Simulatori386,
        Simulatorx86_64
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
