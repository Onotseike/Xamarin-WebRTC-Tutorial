using Xamarin.Forms;

namespace WebRTC.DemoApp
{
    public class CallPage : ContentPage
    {
        public string RoomId { get; set; }
        public bool IsInitator { get; set; }

        public CallPage(string roomId, bool initiator)
        {
            RoomId = roomId;
            IsInitator = initiator;
        }
    }
}