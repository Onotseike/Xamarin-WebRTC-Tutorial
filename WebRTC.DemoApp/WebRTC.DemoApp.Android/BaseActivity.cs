// onotseike@hotmail.comPaula Aliu

using Android.OS;
using Android.Views;

using Xamarin.Forms.Platform.Android;

namespace WebRTC.DemoApp.Droid
{

    public abstract class BaseActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.NoTitle);

            Window.AddFlags(WindowManagerFlags.Fullscreen | WindowManagerFlags.KeepScreenOn | WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.TurnScreenOn);
            Window.DecorView.SystemUiVisibility = GetSystemUiVisibility();
        }

        #region HelperFunctions

        private StatusBarVisibility GetSystemUiVisibility()
        {
            var flags = SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                flags = SystemUiFlags.ImmersiveSticky;
            return (StatusBarVisibility)flags;
        }

        #endregion
    }
}
