using AlexPacientes.Settings;
using AlexPacientes.Styles;
using Amazon;
//using Com.OneSignal;
using Foundation;
using ObjCRuntime;
using System.Linq;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace AlexPacientes.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, Firebase.CloudMessaging.IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            var logginConfig = AWSConfigs.LoggingConfig;
            logginConfig.LogMetrics = true;
            logginConfig.LogResponses = ResponseLoggingOption.Always;
            logginConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            logginConfig.LogTo = LoggingOptions.SystemDiagnostics;

            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            Firebase.Core.App.Configure();

            var x = (int)UIScreen.MainScreen.Bounds.Width;
            var y = (int)UIScreen.MainScreen.Bounds.Height;
            Layouts.DisplayYSizePX = x > y ? x : y;
            Layouts.DisplayXSizePX = x < y ? x : y;
            Layouts.DisplayScale = (float)UIScreen.MainScreen.Scale;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            //LoadApplication(new App());
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var splash = new SplashViewController();
            splash.Skipped += (s, e) =>
            {
                LoadApplication(new App());
                base.FinishedLaunching(app, options);
                Device.BeginInvokeOnMainThread(() => SetStatusBar(Colors.PrimaryColor));
            };
            Window.RootViewController = splash;
            Window.MakeKeyAndVisible();
            SetStatusBar(Colors.PrimaryColor);
            Services.PushNotificationService.RegisterToPushNotifications();
            //return base.FinishedLaunching(app, options);
            return true;
        }

        void SetStatusBar(Color requiredColor)
        {
            UIView statusBar;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                int tag = 4;
                UIWindow window = UIApplication.SharedApplication.Windows.LastOrDefault();
                statusBar = window.ViewWithTag(tag);
                if (statusBar == null)
                {
                    statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                    statusBar.Tag = tag;
                    statusBar.BackgroundColor = UIColor.Red; // Customize the view
                    window.AddSubview(statusBar);
                }
            }
            else
            {
                statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            }

            statusBar.BackgroundColor = requiredColor.ToUIColor();
            statusBar.TintColor = requiredColor.ToUIColor();
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired till the user taps on the notification launching the application.
            // TODO: Handle data of notification
            try
            {
                Services.PushNotificationService.SendNotification(userInfo, application.ApplicationState);
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message, "DidReceiveRemoteNotification");
            }
        }
    }
}
