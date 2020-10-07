// onotseike@hotmail.comPaula Aliu
using System;
using System.Linq;

using Android.OS;
using Android.Util;

using Java.IO;
using Java.Lang;

using Org.Webrtc;

using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Enums;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformPeerConnection : Core.NativePlatformBase, IPeerConnection
    {

        private readonly PeerConnection _peerConnection;
        public PlatformPeerConnection(PeerConnection peerConnection, Core.RTCConfiguration configuration, PlatformPeerConnectionFactory platformPeerConnectionFactory) : base(peerConnection)
        {
            _peerConnection = peerConnection;
            Configuration = configuration;
            PeerConnectionFactory = platformPeerConnectionFactory;
        }



        public IMediaStream[] LocalStreams { get; }

        public Core.SessionDescription LocalDescription => _peerConnection.LocalDescription.ToNativePort();

        public Core.SessionDescription RemoteDescription => _peerConnection.RemoteDescription.ToNativePort();

        public SignalingState SignalingState => _peerConnection.InvokeSignalingState().ToNativePort();

        public IceConnectionState IceConnectionState => _peerConnection.InvokeIceConnectionState().ToNativePort();

        public PeerConnectionState ConnectionState => _peerConnection.ConnectionState().ToNativePort();

        public IceGatheringState IceGatheringState => _peerConnection.InvokeIceGatheringState().ToNativePort();

        public Core.RTCConfiguration Configuration { get; }

        public IRtpSender[] Senders => _peerConnection.Senders.Select(sender => new PlatformRtpSender(sender)).Cast<IRtpSender>().ToArray();

        public IRtpReceiver[] Receivers => _peerConnection.Receivers.Select(sender => new PlatformRtpReceiver(sender)).Cast<IRtpReceiver>().ToArray();

        public IRtpTransceiver[] Transceivers
        {
            get
            {
                if (Configuration.SdpSemantics != SdpSemantics.UnifiedPlan) throw new InvalidOperationException(
                        "GET TRANSCEIVERS is only supported with Unified Plan SdpSemantics.");
                return _peerConnection.Transceivers.Select(transceiver => new PlatformRtpTransceiver(transceiver)).Cast<IRtpTransceiver>()
                    .ToArray();
            }
        }

        public PlatformPeerConnectionFactory PeerConnectionFactory { get; }

        public void AddIceCandidate(Core.IceCandidate iceCandidate) => _peerConnection.AddIceCandidate(iceCandidate.ToPlatformNative());

        public void AddStream(IMediaStream mediaStream) => _peerConnection.AddStream(mediaStream.ToPlatformNative<MediaStream>());

        public IRtpSender AddTrack(IMediaStreamTrack mediaStreamTrack, string[] streamIds) => new PlatformRtpSender(_peerConnection.AddTrack(mediaStreamTrack.ToPlatformNative<MediaStreamTrack>(), streamIds));

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType) => new PlatformRtpTransceiver(_peerConnection.AddTransceiver(rtpMediaType.ToPlatformNative()));

        public IRtpTransceiver AddTransceiverOfType(RtpMediaType rtpMediaType, IRtpTransceiverInit transceiverInit) => new PlatformRtpTransceiver(
            _peerConnection.AddTransceiver(rtpMediaType.ToPlatformNative(), transceiverInit.ToPlatformNative<RtpTransceiver.RtpTransceiverInit>()));

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack) => new PlatformRtpTransceiver(_peerConnection.AddTransceiver(mediaStreamTrack.ToPlatformNative<MediaStreamTrack>()));

        public IRtpTransceiver AddTransceiverWithTrack(IMediaStreamTrack mediaStreamTrack, IRtpTransceiverInit transceiverInit) => new PlatformRtpTransceiver(_peerConnection.AddTransceiver(mediaStreamTrack.ToPlatformNative<MediaStreamTrack>(), transceiverInit.ToPlatformNative<RtpTransceiver.RtpTransceiverInit>()));

        public void AnswerForConstraints(Core.MediaConstraints mediaConstraints, Core.Interfaces.ISdpObserver sdpObserver) => _peerConnection.CreateAnswer(new PlatformSdpObserver(sdpObserver), mediaConstraints.ToPlatformNative());

        public void Close() => _peerConnection.Close();

        public void OfferForConstraints(Core.MediaConstraints mediaConstraints, Core.Interfaces.ISdpObserver sdpObserver) => _peerConnection.CreateOffer(new PlatformSdpObserver(sdpObserver), mediaConstraints.ToPlatformNative());

        public void RemoveIceCandidates(Core.IceCandidate[] iceCandidates) => _peerConnection.RemoveIceCandidates(iceCandidates.ToPlatformNative().ToArray());

        public void RemoveStream(IMediaStream mediaStream) => _peerConnection.RemoveStream(mediaStream.ToPlatformNative<MediaStream>());

        public bool RemoveTrack(IRtpSender rtpSender) => _peerConnection.RemoveTrack(rtpSender.ToPlatformNative<RtpSender>());

        public bool SetBweMinBitrateBps(int minBitrateBps, int currentBitrateBps, int maxBitrateBps) => _peerConnection.SetBitrate(min: new Integer(minBitrateBps), current: new Integer(currentBitrateBps), max: new Integer(maxBitrateBps));

        public bool SetConfiguration(Core.RTCConfiguration configuration) => _peerConnection.SetConfiguration(config: configuration.ToPlatformNative());

        public void SetLocalDescription(Core.SessionDescription sessionDescription, Core.Interfaces.ISdpObserver sdpObserver) => _peerConnection.SetLocalDescription(new PlatformSdpObserver(sdpObserver), sessionDescription.ToPlatformNative());

        public void SetRemoteDescription(Core.SessionDescription sessionDescription, Core.Interfaces.ISdpObserver sdpObserver) => _peerConnection.SetRemoteDescription(new PlatformSdpObserver(sdpObserver), sessionDescription.ToPlatformNative());

        public bool StartRtcEventLogWithFilePath(string filePath, long maxSizeInBytes)
        {
            try
            {
                ParcelFileDescriptor rtcEventLog = ParcelFileDescriptor.Open(new File(filePath),
                    ParcelFileMode.Create | ParcelFileMode.Truncate | ParcelFileMode.ReadWrite);

                return _peerConnection.StartRtcEventLog(rtcEventLog.DetachFd(), (int)maxSizeInBytes);
            }
            catch (System.Exception exception)
            {
                Log.Error("PlatformPeerConnection", $"Could not Start RTCEvent logging {exception.Message}");
                return false;
            }
        }

        public void StopRtcEventLog() => _peerConnection.StopRtcEventLog();
    }
}