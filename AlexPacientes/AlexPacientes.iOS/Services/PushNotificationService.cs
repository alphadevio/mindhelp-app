using Firebase.CloudMessaging;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using UserNotifications;

namespace AlexPacientes.iOS.Services
{
    public class PushNotificationService
    {
        public static void RegisterToPushNotifications()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                    (granted, error) =>
                    {
                        if (granted)
                            UIApplication.SharedApplication.InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                    });
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

                // For iOS 10 data message (sent via FCM)
                Messaging.SharedInstance.Delegate = new FirebaseMessagingDelegate();
            }
            else
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
        }

        public static void SendNotification(NSDictionary userInfo, UIApplicationState state)
        {
            var aps = userInfo[new NSString("aps")] as NSDictionary;
            if (aps == null)
                return;

            var alert = aps[new NSString("alert")] as NSString;
            if (alert == null)
                return;

            var actionDataSer = aps[new NSString("actionData")] as NSString;
            var actionData = new Dictionary<string, string>();
            if (actionDataSer != null)
            {
                try
                {
                    actionData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(actionDataSer);
                }
                catch
                {
                    actionData.Add("actionData", actionDataSer);
                }
            }

            var handler = Helpers.PushNotificationHandler.Instance;
            handler?.HandleNotificationReceived(new Helpers.Notification(actionData));
        }
    }
}