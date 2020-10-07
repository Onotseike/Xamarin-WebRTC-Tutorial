// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.Unified.iOS.Enums
{
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
        IPodTouch1G,
        IPodTouch2G,
        IPodTouch3G,
        IPodTouch4G,
        IPodTouch5G,
        IPodTouch6G,
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
        Simulatori386,
        Simulatorx86_64,
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



}
