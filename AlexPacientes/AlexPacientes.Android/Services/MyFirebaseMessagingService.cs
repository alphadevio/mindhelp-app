using Android.App;
using Android.Content;
using Firebase.Messaging;
using System.Collections.Generic;

namespace AlexPacientes.Droid.Services
{
    [Service(Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            // Save token
            AlexPacientes.Settings.GlobalConfig.USER_PUSH_NOTIFICATION_TOKEN = p0;
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            if (message.GetNotification() != null)
            {
                // Notification payload, not in use, wouldn't trigger this function when in background
                var notificationPayload = message.GetNotification();
                SendNotification(new Dictionary<string, string>() { { "message", notificationPayload.Body }, { "title", notificationPayload.Title } });
            }
            else
            {
                // Data payload, triggers this function in foreground or background
                SendNotification(message.Data);
            }
        }

        private void SendNotification(IDictionary<string, string> data)
        {
            var handler = Helpers.PushNotificationHandler.Instance;
            handler?.HandleNotificationReceived(new Helpers.Notification(data));
        }
    }
}