using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AlexPacientes.Styles;
using Plugin.CurrentActivity;
using AlexPacientes.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Amazon;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace AlexPacientes.Droid
{
    [Activity(Label = "MindHelp", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            var logginConfig = AWSConfigs.LoggingConfig;
            logginConfig.LogMetrics = true;
            logginConfig.LogResponses = ResponseLoggingOption.Always;
            logginConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            logginConfig.LogTo = LoggingOptions.SystemDiagnostics;

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var x = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            var y = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            Layouts.DisplayYSizePX = x > y ? x : y;
            Layouts.DisplayXSizePX = x < y ? x : y;
            Layouts.DisplayScale = Resources.DisplayMetrics.Density;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            //OneSignal.Current.StartInit(GlobalConfig.APP_PUSH_NOTIFICATION_ID_CLIENT)
            //    .EndInit();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            ChangeStatusBarColor();
            RequestForPermissions();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void ChangeStatusBarColor()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    currentWindow.SetStatusBarColor(Colors.PrimaryColor.ToAndroid());
                });
            }
        }
        
        Window GetCurrentWindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;

            // clear FLAG_TRANSLUCENT_STATUS flag:
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }

        void RequestForPermissions()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                if (ContextCompat.CheckSelfPermission(this,
                    Manifest.Permission.PostNotifications) == Permission.Denied)
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.PostNotifications }, 101);
                }
            }
        }
    }
}