// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using WebRTC.iOS.Bindings;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.iOS.Extensions
{
    internal static class DataChannelConfigurationExtension
    {
        public static RTCDataChannelConfiguration ToPlatformNative(this Core.DataChannel.DataChannelConfiguration nativePort)
        {
            return new RTCDataChannelConfiguration
            {
                ChannelId = nativePort.ChannelId,
                StreamId = nativePort.StreamId,
                IsNegotiated = nativePort.IsNegotiated,
                IsOrdered = nativePort.IsOrdered,
                Protocol = nativePort.Protocol,
                MaxRetransmits = nativePort.MaxRetransmits,
                MaxRetransmitTimeMs = nativePort.MaxRetransmitTimeMs,
                MaxPacketLifeTime = nativePort.MaxPacketLifeTime
            };
        }

        public static Core.DataChannel.DataChannelConfiguration ToNativePort(this RTCDataChannelConfiguration platformNative)
        {
            return new Core.DataChannel.DataChannelConfiguration
            {
                ChannelId = platformNative.ChannelId,
                IsNegotiated = platformNative.IsNegotiated,
                IsOrdered = platformNative.IsOrdered,
                Protocol = platformNative.Protocol,
                MaxRetransmits = platformNative.MaxRetransmits,
                MaxRetransmitTimeMs = (int)platformNative.MaxRetransmitTimeMs,
                StreamId = platformNative.StreamId,
                MaxPacketLifeTime = platformNative.MaxPacketLifeTime,

            };
        }

    }

    internal static class DictionaryExtensions
    {
        public static NSDictionary<NSString, NSString> ToPlatformNative(this IDictionary<string, string> nativePort)
        {
            var keys = nativePort.Keys.Select(_key => _key.ToPlatformNative()).ToArray();
            var values = nativePort.Values.Select(_value => _value.ToPlatformNative()).ToArray();
            return new NSDictionary<NSString, NSString>(keys, values);
        }

        public static NSString ToPlatformNative(this string nativePort) => new NSString(nativePort);

        public static IDictionary<string, string> ToNativePort(this NSDictionary<NSString, NSString> platformNative)
        {
            var dict = new Dictionary<string, string>();
            foreach (var keyValuePair in platformNative)
            {
                dict.Add(keyValuePair.Key.ToString(), keyValuePair.Value.ToString());
            }
            return dict;
        }


    }

    internal static class IceCandidateExtension
    {
        public static RTCIceCandidate ToPlatformNative(this Core.IceCandidate nativePort) => new RTCIceCandidate(nativePort.Sdp, nativePort.SdpMLineIndex, nativePort.SdpMid);

        public static Core.IceCandidate ToNativePort(this RTCIceCandidate platformNative) => new Core.IceCandidate(platformNative.SdpMid, platformNative.SdpMLineIndex, platformNative.Sdp);

        public static IEnumerable<RTCIceCandidate> ToPlatformNative(this IEnumerable<Core.IceCandidate> nativePort) => nativePort.Select(ToPlatformNative);

        public static IEnumerable<Core.IceCandidate> ToNativePort(this IEnumerable<RTCIceCandidate> platformNative) => platformNative.Select(ToNativePort);
    }

    internal static class IceServerExtension
    {

        public static RTCIceServer ToPlatformNative(this Core.IceServer nativePort)
        {
            return new RTCIceServer(nativePort.UrlStrings, nativePort.Username, nativePort.Credential, nativePort.TlsCertPolicy.ToPlatformNative(), nativePort.Hostname, nativePort.TlsAlpnProtocols, nativePort.TlsEllipticCurves);
        }

        public static Core.IceServer ToNativePort(this RTCIceServer platformNative) => new Core.IceServer(urlStrings: platformNative.UrlStrings, username: platformNative.Username, credential: platformNative.Credential, policy: platformNative.TlsCertPolicy.ToNativePort(), hostname: platformNative.Hostname, tlsEllipticCurves: platformNative.TlsEllipticCurves, tlsAlpnProtocols: platformNative.TlsAlpnProtocols);

        public static IEnumerable<RTCIceServer> ToPlatformNative(this IEnumerable<Core.IceServer> nativePort) => nativePort.Select(ToPlatformNative);

        public static IEnumerable<Core.IceServer> ToNativePort(this IEnumerable<RTCIceServer> platformNative) => platformNative.Select(ToNativePort);
    }

    internal static class MediaConstraintsExtension
    {
        public static RTCMediaConstraints ToPlatformNative(this Core.MediaConstraints nativePort) => new RTCMediaConstraints(mandatory: nativePort.Mandatory.ToPlatformNative(), optional: nativePort.Optional.ToPlatformNative());

        public static Core.MediaConstraints ToNativePort(this RTCMediaConstraints platformNative) => platformNative.ToNativePort();

    }

    internal static class MediaStreamTrackExtensions
    {
        public static RTCMediaStreamTrack ToPlatformNative(this Core.Interfaces.IMediaStreamTrack nativePort) => nativePort.ToPlatformNative<RTCMediaStreamTrack>();

        public static Core.Interfaces.IMediaStreamTrack ToNativePort(this RTCMediaStreamTrack platformNative)
        {
            switch (platformNative.Kind)
            {
                case Constants.WebRTCConstants.AudioTrackKind:
                    return new iOS.PlatformAudioTrack((RTCAudioTrack)platformNative);
                case Constants.WebRTCConstants.VideoTrackKind:
                    return new iOS.PlatformVideoTrack((RTCVideoTrack)platformNative);
                default:
                    throw new ArgumentOutOfRangeException(nameof(platformNative), platformNative, null);
            }
        }
    }

    internal static class RTCCertificateExtension
    {
        public static RTCCertificate ToPlatformNative(this Core.RTCCertificate nativePort) =>
            new RTCCertificate(nativePort.PrivateKey, nativePort.Certificate);

        public static Core.RTCCertificate ToNativePort(this RTCCertificate nativePort) =>
            new Core.RTCCertificate(nativePort.Private_key, nativePort.Certificate);
    }

    internal static class RTCConfigurationExtensions
    {
        public static RTCConfiguration ToPlatformNative(this Core.RTCConfiguration nativePort) => new RTCConfiguration
        {
            IceTransportPolicy = nativePort.IceTransportPolicy.ToPlatformNative(),
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
            ShouldPruneTurnPorts = nativePort.ShouldPruneTurnPorts,
            ShouldPresumeWritableWhenFullyRelayed = nativePort.ShouldPresumeWritableWhenFullyRelayed,
            IceCheckMinInterval = nativePort.IceCheckMinInterval.HasValue ? new NSNumber(nativePort.IceCheckMinInterval.Value) : null,
            DisableIPV6OnWiFi = nativePort.DisableIPV6OnWiFi,
            MaxIPv6Networks = nativePort.MaxIPv6Networks,
            DisableIPV6 = nativePort.DisableIPV6,
            SdpSemantics = nativePort.SdpSemantics.ToPlatformNative(),
            ActiveResetSrtpParams = nativePort.ActiveResetSrtpParams,
            Certificate = nativePort.Certificate?.ToPlatformNative(),
            IceBackupCandidatePairPingInterval = nativePort.IceBackupCandidatePairPingInterval,
            AllowCodecSwitching = nativePort.AllowCodecSwitching,
            EnableDscp = nativePort.EnableDscp,
            IceServers = nativePort.IceServers.ToPlatformNative().ToArray(),
        };

        public static Core.RTCConfiguration ToNativePort(this RTCConfiguration platformNative) => new Core.RTCConfiguration
        {
            IceTransportPolicy = platformNative.IceTransportPolicy.ToNativePort(),
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
            ShouldPruneTurnPorts = platformNative.ShouldPruneTurnPorts,
            ShouldPresumeWritableWhenFullyRelayed = platformNative.ShouldPresumeWritableWhenFullyRelayed,
            IceCheckMinInterval = platformNative.IceCheckMinInterval.Int32Value,
            DisableIPV6OnWiFi = platformNative.DisableIPV6OnWiFi,
            MaxIPv6Networks = platformNative.MaxIPv6Networks,
            DisableIPV6 = platformNative.DisableIPV6,
            SdpSemantics = platformNative.SdpSemantics.ToNativePort(),
            ActiveResetSrtpParams = platformNative.ActiveResetSrtpParams,
            Certificate = platformNative.Certificate.ToNativePort(),
            IceBackupCandidatePairPingInterval = platformNative.IceBackupCandidatePairPingInterval
        };
    }

    internal static class SessionDescriptionExtension
    {
        public static RTCSessionDescription ToPlatformNative(this Core.SessionDescription nativePort) => new RTCSessionDescription(nativePort.Type.ToPlatformNative(), nativePort.Sdp);

        public static Core.SessionDescription ToNativePort(this RTCSessionDescription platformNative) => new Core.SessionDescription(platformNative.Type.ToNativePort(), platformNative.Sdp);
    }
}
