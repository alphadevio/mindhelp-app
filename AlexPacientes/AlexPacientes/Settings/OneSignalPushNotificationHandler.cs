using AlexPacientes.Models.AppModels.PushNotifications;
using AlexPacientes.Views.Navigation;
//using Com.OneSignal;
//using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace AlexPacientes.Settings
{
    public class OneSignalPushNotificationHandler
    {
        public static bool OpenChat = false;
        HttpClient pushClient = new HttpClient();
        public static NavigationTabbedPage BasePageReference { get; set; }

        public OneSignalPushNotificationHandler()
        {
            //OneSignal.Current.StartInit(GlobalConfig.APP_PUSH_NOTIFICATION_ID_CLIENT)
            //    .HandleNotificationReceived(HandleNotificationReceived)
            //    .InFocusDisplaying(OSInFocusDisplayOption.None)//evitamos que muestre la alerta en el telefono                
            //    .EndInit();
            //OneSignal.Current.SetLocationShared(true);//avoid to show the notifications in the app


            this.Init();
        }

        public async Task Init()
        {
            //OneSignal.Current.IdsAvailable((playerID, token) =>
            //{
            //    //App.Current.MainPage.DisplayAlert("asda", playerID, "asda");
            //    GlobalConfig.USER_PUSH_NOTIFICATION_ID = playerID;
            //    GlobalConfig.USER_PUSH_NOTIFICATION_TOKEN = token;//could be null;
            //});
        }
        /*
        public void HandleNotificationReceived(OSNotification notification)
        {
            notification.shown = false;
            try
            {
                object Action;
                notification.payload.additionalData.TryGetValue("notificationType", out Action);
                var actionResult = GetValueFromObject<string>(Action);
                switch(actionResult){
                    case ApiSettings.PUSH_NOTIFICATION_ACTIONS.CHAT_MESSAGE:
                        {
                            object Title, messageOwnerID, message, chatRoomID;
                            notification.payload.additionalData.TryGetValue("sent_by_user_id", out messageOwnerID);
                            notification.payload.additionalData.TryGetValue("message", out message);
                            notification.payload.additionalData.TryGetValue("title", out Title);
                            notification.payload.additionalData.TryGetValue("chat_room_id", out chatRoomID);
                            var messageModel = new Models.SQLite.MessageData()
                            {
                                message = GetValueFromObject<string>(message),
                                messageOwnerID = GetValueFromObject<int>(messageOwnerID),
                                chatRoomID = GetValueFromObject<int>(chatRoomID),
                            };
                            string t = GetValueFromObject<string>(Title);
                            var pushHandler = DependencyService.Get<Services.IPushNotification>();
                            if (messageModel.messageOwnerID != GlobalConfig.Identity?.ID)
                                pushHandler.Push(string.IsNullOrWhiteSpace(t) ? Labels.Labels.NewPushArrive : t, messageModel.message);
                            MessagingCenter.Send<OneSignalPushNotificationHandler, Models.SQLite.MessageData>(this, 
                                ApiSettings.MessagingCenterMessageSubcriptions.RefreshChat,
                                messageModel);
                        }
                        break;
                    default:
                        {
                            object title;
                            notification.payload.additionalData.TryGetValue("title", out title);
                            var pushHandler = DependencyService.Get<Services.IPushNotification>();
                            var titleStr = GetValueFromObject<string>(title);
                            pushHandler.Push(titleStr, " ");

                            // Save notification to local storage
                            var data = new Models.SQLite.NotificationData { Title = titleStr, CreatedAt = DateTime.Now };
                            _ = Utilities.NotificationsRepository.SaveNotification(data);
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                exc.ToString();
            }
        }

        public async Task<bool> PostNotification(RequestPushModel data, OnPostNotificationSuccess success = null, OnPostNotificationFailure failure = null, bool sendToSender=true)
        {
            try
            {
                if (data.include_player_ids == null)
                {
                    data.include_player_ids = new List<string>();
                    data.include_player_ids.Add(GlobalConfig.USER_PUSH_NOTIFICATION_ID);
                }
                var doctor = data.Clone();
                doctor.app_id = GlobalConfig.APP_PUSH_NOTIFICATION_ID_DOCTOR;
                doctor.include_player_ids.Remove(GlobalConfig.USER_PUSH_NOTIFICATION_ID);
                await PostNotificationMessage(doctor);
                if (sendToSender)
                {
                    var pacient = data.Clone();
                    pacient.app_id = GlobalConfig.APP_PUSH_NOTIFICATION_ID_CLIENT;
                    pacient.include_player_ids.Clear();
                    pacient.include_player_ids.Add(GlobalConfig.USER_PUSH_NOTIFICATION_ID);
                    await PostNotificationMessage(pacient);
                }
            }
            catch { }
            return false;
        }

        public async Task<bool> PostNotificationMessage(RequestPushModel data)
        {
            //data.subtitle = "Testing";
            data.contents.en = "en";
            var post = JsonConvert.SerializeObject(data);
            HttpContent contentPost = new StringContent(post, Encoding.UTF8, "application/json");
            var response = await pushClient.PostAsync(GlobalConfig.API_PUSH_ROUTE, contentPost);
            var res = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            return false;
        }
        */
        public T GetValueFromObject<T>(object item)
        {
            var stringItem = item.ToString().Replace("{", "").Replace("}", "");
            return (T)Convert.ChangeType(stringItem, typeof(T));
        }
    }
}
