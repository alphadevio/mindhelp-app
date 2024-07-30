using AlexPacientes.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Helpers
{
    public class Notification
    {
        public Notification(IDictionary<string, string> data) 
        {
            Data = data;
        }

        public IDictionary<string, string> Data { get; private set; }
    }

    public class PushNotificationHandler
    {
        const string Tag = "PushNotificationHandler";

        static PushNotificationHandler _instance;
        public static PushNotificationHandler Instance
        {
            get => _instance ?? (_instance = new PushNotificationHandler());
        }

        public void HandleNotificationReceived(Notification notification)
        {
            try
            {
                notification.Data.TryGetValue("title", out string title);
                notification.Data.TryGetValue("message", out string message);
                var pushHandler = DependencyService.Get<Services.IPushNotification>();
                pushHandler.Push(title, message);
                //notification.Data.TryGetValue("notificationType", out string action);
                //switch (action)
                //{
                //    case ApiSettings.PUSH_NOTIFICATION_ACTIONS.CHAT_MESSAGE:
                //        {
                //            notification.Data.TryGetValue("sent_by_user_id", out string messageOwnerID);
                //            notification.Data.TryGetValue("message", out string message);
                //            notification.Data.TryGetValue("title", out string title);
                //            notification.Data.TryGetValue("chat_room_id", out string chatRoomID);
                //            var messageModel = new Models.SQLite.MessageData()
                //            {
                //                message = GetValueFromObject<string>(message),
                //                messageOwnerID = GetValueFromObject<int>(messageOwnerID),
                //                chatRoomID = GetValueFromObject<int>(chatRoomID),
                //            };
                //            var pushHandler = DependencyService.Get<Services.IPushNotification>();
                //            if (messageModel.messageOwnerID != GlobalConfig.Identity?.ID)
                //                pushHandler.Push(string.IsNullOrWhiteSpace(title) ? Labels.Labels.NewPushArrive : title, messageModel.message);
                //            MessagingCenter.Send<PushNotificationHandler, Models.SQLite.MessageData>(this,
                //                ApiSettings.MessagingCenterMessageSubcriptions.RefreshChat,
                //                messageModel);
                //        }
                //        break;
                //    default:
                //        {
                            
                //            notification.Data.TryGetValue("title", out string title);
                //            notification.Data.TryGetValue("message", out string message);
                //            var pushHandler = DependencyService.Get<Services.IPushNotification>();
                //            pushHandler.Push(title, message);

                //            // Save notification to local storage
                //            //var data = new Models.SQLite.NotificationData { Title = title, CreatedAt = DateTime.Now };
                //            //_ = Utilities.NotificationsRepository.SaveNotification(data);
                //        }
                //        break;
                //}
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message, Tag);
            }
        }

        #region Aux methods
        private T GetValueFromObject<T>(object item)
        {
            var stringItem = item.ToString().Replace("{", "").Replace("}", "");
            return (T)Convert.ChangeType(stringItem, typeof(T));
        }
        #endregion
    }
}
