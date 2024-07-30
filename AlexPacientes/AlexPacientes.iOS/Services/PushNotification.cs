using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexPacientes.iOS.Services;
using AlexPacientes.Services;
using Foundation;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotification))]
namespace AlexPacientes.iOS.Services
{
    public class PushNotification:IPushNotification
    {
        public void Push(string title, string message)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                var content = new UNMutableNotificationContent();
                content.Title = title;
                content.Body = message;
                //content.UserInfo = NSDictionary.FromObjectAndKey(new NSString("local"), new NSString(NotificationLocalKey));
                var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(2, false);
                var request = UNNotificationRequest.FromIdentifier("generalNotifications", content, trigger);
                UNUserNotificationCenter.Current.AddNotificationRequest(request, null);
            }
            else
            {
                UILocalNotification notification = new UILocalNotification();
                notification.AlertAction = title;
                notification.AlertBody = message;
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            }
        }
    }
}