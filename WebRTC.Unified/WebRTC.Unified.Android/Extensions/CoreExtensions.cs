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
using System.Collections;

namespace WebRTC.Unified.Extensions
{
    internal static class DataChannelConfigurationExtension
    {
        public static DataChannel.Init ToPlatformNative(this Core.DataChannel.DataChannelConfiguration nativePort)
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

        public static Core.DataChannel.DataChannelConfiguration ToNativePort(this DataChannel.Init platformNative)
        {
            return new Core.DataChannel.DataChannelConfiguration
            {
                ChannelId = platformNative.Id,
                IsNegotiated = platformNative.Negotiated,
                IsOrdered = platformNative.Ordered,
                Protocol = platformNative.Protocol,
                MaxRetransmits = platformNative.MaxRetransmits,
                MaxRetransmitTimeMs = platformNative.MaxRetransmitTimeMs,
                //MaxPacketLifeTime = platformNative.
            };
        }

    }

    internal static class IceCandidateExtension
    {
        public static IceCandidate ToPlatformNative(this Core.IceCandidate nativePort) => new IceCandidate(nativePort.SdpMid, nativePort.SdpMLineIndex, nativePort.Sdp);

        public static Core.IceCandidate ToNativePort(this IceCandidate platformNative) => new Core.IceCandidate(platformNative.SdpMid, platformNative.SdpMLineIndex, platformNative.Sdp);

        public static IEnumerable<IceCandidate> ToPlatformNative(this IEnumerable<Core.IceCandidate> nativePort) => nativePort.Select(ToPlatformNative);

        public static IEnumerable<Core.IceCandidate> ToNativePort(this IEnumerable<IceCandidate> platformNative) => platformNative.Select(ToNativePort);
    }

    internal static class IceServerExtension
    {

        public static IceServer ToPlatformNative(this Core.IceServer nativePort)
        {
            var iceServer = IceServer.InvokeBuilder(nativePort.UrlStrings).SetTlsCertPolicy(nativePort.TlsCertPolicy.ToPlatformNative()).SetHostname(nativePort.Hostname).SetPassword(nativePort.Credential).SetTlsAlpnProtocols(nativePort.TlsAlpnProtocols).SetTlsEllipticCurves(nativePort.TlsEllipticCurves);
            return iceServer.CreateIceServer();
        }

        public static Core.IceServer ToNativePort(this IceServer platformNative)
        {
            var urlStrings = platformNative.Urls.ToStringArray();
            var ellipticCurves = platformNative.TlsEllipticCurves.ToStringArray();
            var alpnProtocols = platformNative.TlsAlpnProtocols.ToStringArray();
            foreach (var url in platformNative.Urls) urlStrings.Append(url);
            return new Core.IceServer(urlStrings: urlStrings, username: platformNative.Username, credential: platformNative.Password, policy: platformNative.TlsCertPolicy.ToNativePort(), hostname: platformNative.Hostname, tlsEllipticCurves: ellipticCurves, tlsAlpnProtocols: alpnProtocols);
        }

        public static IEnumerable<IceServer> ToPlatformNative(this IEnumerable<Core.IceServer> nativePort) => nativePort.Select(ToPlatformNative);

        public static IEnumerable<Core.IceServer> ToNativePort(this IEnumerable<IceServer> platformNative) => platformNative.Select(ToNativePort);

        #region Helper Methods
        private static string[] ToStringArray(this IList iListObject)
        {
            var arrayObject = new string[iListObject.Count];
            foreach (var item in iListObject) arrayObject.Append(item);
            return arrayObject;
        }
        #endregion

    }

    internal static class MediaConstraintsExtension
    {
        public static MediaConstraints ToPlatformNative(this Core.MediaConstraints nativePort) => new MediaConstraints
        {
            Mandatory = nativePort.Mandatory.Select(p => new MediaConstraints.KeyValuePair(p.Key, p.Value)).ToList(),
            Optional = nativePort.Optional.Select(p => new MediaConstraints.KeyValuePair(p.Key, p.Value)).ToList()
        };

        public static Core.MediaConstraints ToNativePort(this MediaConstraints platformNative) => new Core.MediaConstraints
        {
            Mandatory = platformNative.Mandatory.ToDictionary(),
            Optional = platformNative.Optional.ToDictionary()
        };

        #region Helper Methods

        private static Dictionary<string, string> ToDictionary(this IList iListObject)
        {
            var dict = new Dictionary<string, string>();
            foreach (MediaConstraints.KeyValuePair item in iListObject) dict.TryAdd(item.Key, item.Value);
            return dict;
        }

        #endregion
    }

    internal static class MediaStreamTrackExtensions
    {
        public static MediaStreamTrack ToPlatformNative(this Core.Interfaces.IMediaStreamTrack nativePort) => nativePort.ToPlatformNative<MediaStreamTrack>();

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
        public static RTCConfiguration ToPlatformNative(this Core.RTCConfiguration nativePort) => new RTCConfiguration(nativePort.IceServers.ToPlatformNative().ToList())
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
            Certificate = nativePort.Certificate?.ToPlatformNative(),
            IceBackupCandidatePairPingInterval = nativePort.IceBackupCandidatePairPingInterval,
            AllowCodecSwitching = (Java.Lang.Boolean)nativePort.AllowCodecSwitching,
            EnableDscp = nativePort.EnableDscp,
            IceServers = (IList)nativePort.IceServers.ToPlatformNative(),
            CombinedAudioVideoBwe = (Java.Lang.Boolean)nativePort.CombinedAudioVideoBwe,
            EnableCpuOveruseDetection = nativePort.EnableCpuOverUseDetection,
            EnableRtpDataChannel = nativePort.EnableRtpDataChannel,
            IceCheckIntervalStrongConnectivityMs = nativePort.IceCheckIntervalStrongConnectivityMs.HasValue ? new Integer(nativePort.IceCheckIntervalStrongConnectivityMs.Value) : null,
            IceCheckIntervalWeakConnectivityMs = nativePort.IceCheckIntervalWeakConnectivityMs.HasValue ? new Integer(nativePort.IceCheckIntervalWeakConnectivityMs.Value) : null

        };

        public static Core.RTCConfiguration ToNativePort(this RTCConfiguration platformNative) => new Core.RTCConfiguration
        {
            IceTransportPolicy = platformNative.IceTransportsType.ToNativePort(),
            BundlePolicy = platformNative.BundlePolicy.ToNativePort(),
            RtcpMuxPolicy = platformNative.RtcpMuxPolicy.ToNativePort(),
            TcpCandidatePolicy = platformNative.TcpCandidatePolicy.ToNativePort(),
            CandidateNetworkPolicy = platformNative.CandidateNetworkPolicy.ToNativePort(),
            AudioJitterBufferFastAccelerate = platformNative.AudioJitterBufferFastAccelerate,
            AudioJitterBufferMaxPackets = platformNative.AudioJitterBufferMaxPackets,
            IceConnectionReceivingTimeout = platformNative.IceConnectionReceivingTimeout,
            KeyType = platformNative.KeyType.ToNativePort(),
            ContinualGatheringPolicy = platformNative.ContinualGatheringPolicy.ToNativePort(),
            IceCandidatePoolSize = platformNative.IceCandidatePoolSize,
            ShouldPruneTurnPorts = platformNative.PruneTurnPorts,
            ShouldPresumeWritableWhenFullyRelayed = platformNative.PresumeWritableWhenFullyRelayed,
            IceCheckMinInterval = platformNative.IceCheckMinInterval.IntValue(),
            DisableIPV6OnWiFi = platformNative.DisableIPv6OnWifi,
            MaxIPv6Networks = platformNative.MaxIPv6Networks,
            DisableIPV6 = platformNative.DisableIpv6,
            SdpSemantics = platformNative.SdpSemantics.ToNativePort(),
            ActiveResetSrtpParams = platformNative.ActiveResetSrtpParams,
            UseMediaTransport = platformNative.UseMediaTransport,
            UseMediaTransportForDataChannels = platformNative.UseMediaTransportForDataChannels,
            EnableDtlsSrtp = platformNative.EnableDtlsSrtp.BooleanValue(),
            Certificate = platformNative.Certificate.ToNativePort(),
            IceBackupCandidatePairPingInterval = platformNative.IceBackupCandidatePairPingInterval
        };
    }

    internal static class SessionDescriptionExtension
    {
        public static SessionDescription ToPlatformNative(this Core.SessionDescription nativePort) => new SessionDescription(nativePort.Type.ToPlatformNative(), nativePort.Sdp);

        public static Core.SessionDescription ToNativePort(this SessionDescription platformNative) => new Core.SessionDescription(platformNative.SdpType.ToNativePort(), platformNative.Description);
    }
}
