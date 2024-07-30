using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexPacientes.Droid.Services;
using AlexPacientes.Services;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotification))]
namespace AlexPacientes.Droid.Services
{
    public class PushNotification : IPushNotification
    {
        public void Push(string title, string message)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                if (ContextCompat.CheckSelfPermission(Xamarin.Essentials.Platform.AppContext, 
                    Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(Xamarin.Essentials.Platform.CurrentActivity, new string[] { Manifest.Permission.PostNotifications }, 101);
                }
            }

            int notificationID = 4;
            string channelID = "1";
            string channelName = "MindhelpC";

            // Get the notification manager service
            NotificationManager manager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Channel notification for Android O and above
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel channel = new NotificationChannel(channelID, channelName, NotificationImportance.High);
                manager.CreateNotificationChannel(channel);
            }

            // Action that we can make when the app get clicked (probably it could be a parameter action) and prepare the task to do
            Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
            var stackBuilder = Android.App.TaskStackBuilder.Create(Android.App.Application.Context);
            stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(intent);
            PendingIntent pending = null;

            if(Build.VERSION.SdkInt >= BuildVersionCodes.S)
                pending = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent| PendingIntentFlags.Immutable);
            else
                pending = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            // Set the notification builder parameters
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Android.App.Application.Context, channelID)
                .SetAutoCancel(true)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentIntent(pending);

            // Build the notification and display it (if we can)
            manager.Notify(notificationID, builder.Build());
        }


    }
}