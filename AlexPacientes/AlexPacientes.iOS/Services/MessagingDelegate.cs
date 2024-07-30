using Firebase.CloudMessaging;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace AlexPacientes.iOS.Services
{
    public class FirebaseMessagingDelegate : MessagingDelegate
    {
        public override void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            //base.DidReceiveRegistrationToken(messaging, fcmToken);
            Settings.GlobalConfig.USER_PUSH_NOTIFICATION_TOKEN = fcmToken;
        }
    }
}