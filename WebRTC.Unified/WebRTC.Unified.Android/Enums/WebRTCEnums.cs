// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.Android.Enums
{
    public enum AudioDevice
    {
        SpeakerPhone,
        WiredHeadset,
        Earpiece,
        Bluetooth,
        None
    }

    public enum AudioManagerState
    {
        Uninitialized,
        PreInitialized,
        Running
    }

    public enum VideoCodecStatus
    {
        Error,
        ErrParmater,
        ErrRequestSli,
        ErrSize,
        FallbackSoftware,
        LevelExceeded,
        Memory,
        NoOutput,
        Ok,
        RequestSli,
        TargetBitrateOvershoot,
        Timeout,
        Uninitialized
    }

    public enum AdapterType
    {
        AdapterTypeAny,
        Cellular,
        Cellular2g,
        Cellular3g,
        Cellular4g,
        Cellular5g,
        Ethernet,
        Loopback,
        Unknown,
        Vpn,
        Wifi
    }

    public enum PortPrunePolicy
    {
        KeepReadyFirst,
        NoPrune,
        PruneBasedOnPriority
    }

    public enum ConnectionType
    {
        Connection2g,
        Connection3g,
        Connection4g,
        Connection5g,
        ConnectionBluetooth,
        ConnectionEthernet,
        ConnectionNone,
        ConnectionUnknown,
        ConnectionUnknownCellular,
        ConnectionVpn,
        ConnectionWifi
    }

    public enum TextureBufferType
    {
        Oes,
        Rgb
    }

    public enum AudioRecordStartErrorCode
    {
        AudioRecordStartException,
        AudioRecordStartStateMismatch
    }

    public enum AudioTrackStartErrorCode
    {
        AudioTrackStartException,
        AudioTrackStartStateMismatch
    }
}
