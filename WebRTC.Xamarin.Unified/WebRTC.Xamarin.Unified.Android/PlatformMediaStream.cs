// onotseike@hotmail.comPaula Aliu


using System;
using System.Collections;

using Org.Webrtc;

using WebRTC.Unified.Core;
using WebRTC.Unified.Core.Interfaces;
using WebRTC.Unified.Extensions;

namespace WebRTC.Unified.Android
{
    internal class PlatformMediaStream : NativePlatformBase, IMediaStream
    {
        private readonly MediaStream _mediaStream;
        public PlatformMediaStream(MediaStream mediaStream) : base(mediaStream) => _mediaStream = mediaStream;

        public IAudioTrack[] AudioTracks => GetAudioTracks();

        public IVideoTrack[] VideoTracks => GetVideoTracks();

        public string StreamId => _mediaStream.Id;

        public void AddAudioTrack(IAudioTrack audioTrack) => _mediaStream.AddTrack(audioTrack.ToPlatformNative<AudioTrack>());

        public void AddVideoTrack(IVideoTrack videoTrack) => _mediaStream.AddTrack(videoTrack.ToPlatformNative<VideoTrack>());

        public void RemoveAudioTrack(IAudioTrack audioTrack) => _mediaStream.RemoveTrack(audioTrack.ToPlatformNative<AudioTrack>());

        public void RemoveVideoTrack(IVideoTrack videoTrack) => _mediaStream.RemoveTrack(videoTrack.ToPlatformNative<VideoTrack>());

        public void AddPreservedTrack(IVideoTrack videoTrack) => _mediaStream.AddPreservedTrack(videoTrack.ToPlatformNative<VideoTrack>());

        #region Helper Methods

        private IAudioTrack[] GetAudioTracks()
        {
            var audioTracks = new AudioTrack[_mediaStream.AudioTracks.Count];
            _mediaStream.AudioTracks.CopyTo(audioTracks, 0);
            return Array.ConvertAll<AudioTrack, IAudioTrack>(audioTracks, track => new PlatformAudioTrack(track));
        }

        private IVideoTrack[] GetVideoTracks()
        {
            var videoTracks = new VideoTrack[_mediaStream.VideoTracks.Count];
            _mediaStream.VideoTracks.CopyTo(videoTracks, 0);
            return Array.ConvertAll<VideoTrack, IVideoTrack>(videoTracks, track => new PlatformVideoTrack(track));
        }
        #endregion

    }
}
