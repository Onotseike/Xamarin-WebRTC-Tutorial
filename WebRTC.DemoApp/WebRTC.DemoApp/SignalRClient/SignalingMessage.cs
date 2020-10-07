// onotseike@hotmail.comPaula Aliu
using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using WebRTC.DemoApp.SignalRClient.Abstractions;
using WebRTC.Unified.Core;
using WebRTC.Unified.Enums;

namespace WebRTC.DemoApp.SignalRClient
{

    #region SignalingMessageType

    public enum SignalingMessageType
    {
        Candidate,
        CandidateRemoval,
        Offer,
        Answer,
        PrAnswer,
        Bye,
    }

    #endregion

    #region Signaling Message Class(es)

    public class SignalingMessage
    {
        protected const string CandidateType = "candidate";
        protected const string CandidateRemovalType = "remove-candidates";
        protected const string OfferType = "offer";
        protected const string AnswerType = "answer";
        protected const string PrAnswerType = "pranswer";
        protected const string ByeType = "bye";

        public static SignalingMessage MessageFromJSONString(string json)
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            SignalingMessage message = new SignalingMessage();

            if (values.ContainsKey("type"))
            {
                var type = values["type"] ?? "";
                switch (type)
                {
                    case CandidateType:
                        int.TryParse(values["label"], out int label);
                        var candidate = new IceCandidate(values["candidate"], label, values["id"]);
                        message = new ICECandidateMessage(candidate);
                        break;
                    case CandidateRemovalType:

                        break;
                    case OfferType:
                        var description = new SessionDescription(SdpType.Offer, values["sdp"]);
                        message = new SessionDescriptionMessage(description);
                        break;
                    case AnswerType:
                        description = new SessionDescription(SdpType.Answer, values["sdp"]);
                        message = new SessionDescriptionMessage(description);
                        break;
                    case PrAnswerType:
                        description = new SessionDescription(SdpType.PrAnswer, values["sdp"]);
                        message = new SessionDescriptionMessage(description);
                        break;
                    case ByeType:
                        message = new ByeMessage();
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine($"ARDSignalingMessage unexpected type: {type}");
                        break;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"ARDSignalingMessage invalid json: {json}");
            }

            return message;
        }

        public static string CreateJson(IceCandidate iceCandidate) => new ICECandidateMessage(iceCandidate).JsonData;
        public static string CreateJson(SessionDescription sessionDescription) => new SessionDescriptionMessage(sessionDescription).JsonData;
        public static string CreateJson(IceCandidate[] removalCandidates) => new ICECandidateRemovalMessage(removalCandidates).JsonData;

        public static string CreateByeJson() => new ByeMessage().JsonData;

        public SignalingMessageType Type { get; set; }

        public virtual string JsonData { get; } = "{}";

        public override string ToString()
        {
            return JsonData;
        }

        public string ToStringPrettyPrinted()
        {
            var obj = new { type = Type.ToString(), message = JsonData.ToString() };
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }



        protected static string ToJsonCandidate(IceCandidate iceCandidate)
        {
            return JsonConvert.SerializeObject(new
            {
                label = iceCandidate.SdpMLineIndex,
                id = iceCandidate.SdpMid,
                candidate = iceCandidate.Sdp
            });
        }

        protected static string ToJsonCandidates(IEnumerable<IceCandidate> iceCandidates)
        {
            return JsonConvert.SerializeObject(iceCandidates.Select(ToJsonCandidate));
        }

        protected static string GetTypeString(SignalingMessageType type)
        {
            switch (type)
            {
                case SignalingMessageType.Candidate:
                    return CandidateType;
                case SignalingMessageType.CandidateRemoval:
                    return CandidateRemovalType;
                case SignalingMessageType.Offer:
                    return OfferType;
                case SignalingMessageType.PrAnswer:
                case SignalingMessageType.Answer:
                    return AnswerType;
                //return PrAnswerType;
                case SignalingMessageType.Bye:
                    return ByeType;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public class ICECandidateMessage : SignalingMessage
    {
        public ICECandidateMessage(IceCandidate candidate)
        {
            Type = SignalingMessageType.Candidate;
            Candidate = candidate;
        }

        public IceCandidate Candidate { get; set; }

        public override string JsonData => JsonConvert.SerializeObject(new
        {
            type = GetTypeString(Type),
            label = Candidate.SdpMLineIndex,
            id = Candidate.SdpMid,
            candidate = Candidate.Sdp
        });
    }

    public class ICECandidateRemovalMessage : SignalingMessage
    {
        public ICECandidateRemovalMessage(IceCandidate[] candidates)
        {
            Type = SignalingMessageType.CandidateRemoval;
            Candidates = candidates;
        }

        public IceCandidate[] Candidates { get; set; }

        public override string JsonData => JsonConvert.SerializeObject(new
        {
            type = GetTypeString(Type),
            candidates = ToJsonCandidates(Candidates)
        });
    }

    public class SessionDescriptionMessage : SignalingMessage
    {
        public SessionDescriptionMessage(SessionDescription description)
        {
            Description = description;
            switch (Description.Type)
            {
                case SdpType.Offer:
                    Type = SignalingMessageType.Offer;
                    break;
                case SdpType.PrAnswer:
                    Type = SignalingMessageType.PrAnswer;
                    break;
                case SdpType.Answer:
                    Type = SignalingMessageType.Answer;
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"ARDSessionDescriptionMessage unexpected type: {Type}");
                    break;
            }
        }

        public SessionDescription Description { get; set; }

        public override string JsonData => JsonConvert.SerializeObject(new
        {
            type = GetTypeString(Type),
            sdp = Description.Sdp
        });
    }

    public class ByeMessage : SignalingMessage
    {
        public ByeMessage()
        {
            Type = SignalingMessageType.Bye;
        }

        public override string JsonData => JsonConvert.SerializeObject(new { type = "bye" });
    }

    #endregion

    #region SignalingParameters

    public class SignalingParameters : ISignalingParameters
    {
        public IceServer[] IceServers { get; set; }
        public bool IsInitiator { get; set; }
        public string ClientId { get; set; }
        public string ClientUsername { get; set; }
        public SessionDescription OfferSdp { get; set; }
        public IceCandidate[] IceCandidates { get; set; }
    }

    #endregion

}
