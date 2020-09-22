// onotseike@hotmail.comPaula Aliu


using IceCandidate = Org.Webrtc.IceCandidate;
using DataChannel = Org.Webrtc.DataChannel;
using IceServer = Org.Webrtc.PeerConnection.IceServer;
using System.Collections.Generic;
using System.Linq;
using Org.Webrtc;

namespace WebRTC.Unified.Extensions
{
    internal static class DataChannelConfigurationExtension
    {
        public static DataChannel.Init ToPlatformNative(this DataChannelConfiguration nativePort)
        {
            return new DataChannel.Init
            {
                Id = nativePort.ChannelId,
                Negotiated = nativePort.IsNegotiated,
                Ordered = nativePort.IsOrdered,
                Protocol = nativePort.Protocol,
                MaxRetransmits = nativePort.MaxRetransmits,
                MaxRetransmitTimeMs = nativePort.MaxRetransmitTimeMs
            };
        }

    }

    internal static class IceCandidateExtension
    {
        public static IceCandidate ToPlatformNative(this Core.IceCandidate nativePort) => new IceCandidate(nativePort.SdpMid, nativePort.SdpMLineIndex, nativePort.Sdp);

        public static Core.IceCandidate ToNativePort(this IceCandidate platformNative) => new Core.IceCandidate(platformNative.SdpMid, platformNative.SdpMLineIndex, platformNative.Sdp);
    }

    internal static class IceServerExtension
    {

        public static IceServer ToPlatformNative(this Core.IceServer nativePort)
        {
            var iceServer = IceServer.InvokeBuilder(nativePort.UrlStrings).SetTlsCertPolicy(nativePort.TlsCertPolicy.ToPlatformNative()).SetHostname(nativePort.Hostname).SetPassword(nativePort.Credential).SetTlsAlpnProtocols(nativePort.TlsAlpnProtocols).SetTlsEllipticCurves(nativePort.TlsEllipticCurves);
            return iceServer.CreateIceServer();
        }

        public static IEnumerable<IceServer> ToPlatformNative(this IEnumerable<Core.IceServer> nativePort) => nativePort.Select(ToPlatformNative);
    }

}
