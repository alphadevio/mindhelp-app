using CoreData;
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using UserNotifications;

namespace AlexPacientes.iOS.Services
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound);
            Services.PushNotificationService.SendNotification(notification.Request.Content.UserInfo, UIKit.UIApplicationState.Active);
        }
    }
}