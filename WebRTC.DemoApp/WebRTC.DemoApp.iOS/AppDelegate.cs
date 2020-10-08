using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using UIKit;

using WebRTC.DemoApp.iOS.Helpers;

namespace WebRTC.DemoApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static UIEdgeInsets SafeAreaInsets => UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            WebRTCPlatform.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        [Export("applicationWillTerminate:")]
        public void WillTerminate(UIApplication application)
        {
            WebRTCPlatform.Cleanup();
        }
    }
}
