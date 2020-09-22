// onotseike@hotmail.comPaula Aliu
using IceCandidate = Org.Webrtc.IceCandidate;
using DataChannel = Org.Webrtc.DataChannel;
using IceServer = Org.Webrtc.PeerConnection.IceServer;
using MediaConstraints = Org.Webrtc.MediaConstraints;
using MediaStreamTrack = Org.Webrtc.MediaStreamTrack;
using AudioTrack = Org.Webrtc.AudioTrack;
using VideoTrack = Org.Webrtc.VideoTrack;
using RtcCertificatePem = Org.Webrtc.RtcCertificatePem;
using RTCConfiguration = Org.Webrtc.PeerConnection.RTCConfiguration;
using SessionDescription = Org.Webrtc.SessionDescription;


using System.Collections.Generic;
using System.Linq;
using System;
using Java.Lang;

namespace WebRTC.Unified.Extensions
{
    internal static class DataChannelConfigurationExtension
    {
        public static DataChannel.Init ToPlatformNative(this Core.DataChannelConfiguration nativePort)
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

    internal static class MediaConstraintsExtension
    {
        public static MediaConstraints ToPlatformNative(this Core.MediaConstraints nativePort)
        {
            var optionals = nativePort.Optional.Select(p => new MediaConstraints.KeyValuePair(p.Key, p.Value)).ToList();
            var mandatory = nativePort.Mandatory.Select(p => new MediaConstraints.KeyValuePair(p.Key, p.Value)).ToList();
            return new MediaConstraints
            {
                Mandatory = mandatory,
                Optional = optionals
            };
        }
    }

    internal static class MediaStreamTrackExtensions
    {
        public static MediaStreamTrack ToPlatformNative(this Core.Interfaces.IMediaStreamTrack nativePort)
        {
            return nativePort.ToPlatformNative<MediaStreamTrack>();
        }

        public static Core.Interfaces.IMediaStreamTrack ToNativePort(this MediaStreamTrack platformNative)
        {
            switch (platformNative.Kind())
            {
                case Constants.WebRTCConstants.AudioTrackKind:
                    return new Android.PlatformAudioTrack((AudioTrack)platformNative);
                case Constants.WebRTCConstants.VideoTrackKind:
                    return new Android.PlatformVideoTrack((VideoTrack)platformNative);
                default:
                    throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
            }
        }
    }

    internal static class RTCCertificateExtension
    {
        public static RtcCertificatePem ToPlatformNative(this Core.RTCCertificate nativePort) =>
            new RtcCertificatePem(nativePort.PrivateKey, nativePort.Certificate);

        public static Core.RTCCertificate ToNativePort(this RtcCertificatePem nativePort) =>
            new Core.RTCCertificate(nativePort.PrivateKey, nativePort.Certificate);
    }

    internal static class RTCConfigurationExtensions
    {
        public static RTCConfiguration ToPlatformNative(this Core.RTCConfiguration nativePort)
        {
            return new RTCConfiguration(nativePort.IceServers.ToPlatformNative().ToList())
            {
                IceTransportsType = nativePort.IceTransportPolicy.ToPlatformNative(),
                BundlePolicy = nativePort.BundlePolicy.ToPlatformNative(),
                RtcpMuxPolicy = nativePort.RtcpMuxPolicy.ToPlatformNative(),
                TcpCandidatePolicy = nativePort.TcpCandidatePolicy.ToPlatformNative(),
                CandidateNetworkPolicy = nativePort.CandidateNetworkPolicy.ToPlatformNative(),
                AudioJitterBufferMaxPackets = nativePort.AudioJitterBufferMaxPackets,
                AudioJitterBufferFastAccelerate = nativePort.AudioJitterBufferFastAccelerate,
                IceConnectionReceivingTimeout = nativePort.IceConnectionReceivingTimeout,
                KeyType = nativePort.KeyType.ToPlatformNative(),
                ContinualGatheringPolicy = nativePort.ContinualGatheringPolicy.ToPlatformNative(),
                IceCandidatePoolSize = nativePort.IceCandidatePoolSize,
                PruneTurnPorts = nativePort.ShouldPruneTurnPorts,
                PresumeWritableWhenFullyRelayed = nativePort.ShouldPresumeWritableWhenFullyRelayed,
                IceCheckMinInterval = nativePort.IceCheckMinInterval.HasValue ? new Integer(nativePort.IceCheckMinInterval.Value) : null,
                DisableIPv6OnWifi = nativePort.DisableIPV6OnWiFi,
                MaxIPv6Networks = nativePort.MaxIPv6Networks,
                DisableIpv6 = nativePort.DisableIPV6,
                SdpSemantics = nativePort.SdpSemantics.ToPlatformNative(),
                ActiveResetSrtpParams = nativePort.ActiveResetSrtpParams,
                UseMediaTransport = nativePort.UseMediaTransport,
                UseMediaTransportForDataChannels = nativePort.UseMediaTransportForDataChannels,
                EnableDtlsSrtp = !nativePort.EnableDtlsSrtp ? new Java.Lang.Boolean(true) : null,
                Certificate = nativePort.Certificate?.ToPlatformNative()
            };
        }
    }

    internal static class SessionDescriptionExtension
    {
        public static SessionDescription ToPlatformNative(this Core.SessionDescription nativePort)
        {
            return new SessionDescription(nativePort.Type.ToPlatformNative(), nativePort.Sdp);
        }

        public static Core.SessionDescription ToNativePort(this SessionDescription platformNative)
        {
            return new Core.SessionDescription(platformNative.SdpType.ToNativePort(), platformNative.Description);
        }
    }
}
