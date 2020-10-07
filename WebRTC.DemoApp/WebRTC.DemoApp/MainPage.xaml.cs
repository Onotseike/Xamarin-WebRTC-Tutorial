using System;

using WebRTC.DemoApp.Helper;

using Xamarin.Forms;

namespace WebRTC.DemoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void StartCall_Clicked(object sender, EventArgs e)
        {
            var roomId = RoomIdEntry.Text ?? GenerateRoom.GenerateRoomName();
            RoomIdEntry.Text = roomId;

            await Navigation.PushAsync(new CallPage(roomId, true));
        }

        async void JoinCall_Clicked(object sender, EventArgs e)
        {
            var roomId = RoomIdEntry.Text;
            if (!string.IsNullOrEmpty(roomId) || !string.IsNullOrWhiteSpace(roomId))
            {
                await Navigation.PushAsync(new CallPage(roomId, false));
            }
        }
    }
}
