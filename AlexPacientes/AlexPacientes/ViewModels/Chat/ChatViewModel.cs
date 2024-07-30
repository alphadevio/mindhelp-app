using AlexPacientes.Models.AppModels.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using AlexPacientes.Models.AppModels.Camera;
using System.Threading.Tasks;
using AlexPacientes.Views.Notes;
using AlexPacientes.Models.AppModels.PushNotifications;
using AlexPacientes.Services;

namespace AlexPacientes.ViewModels.Chat
{
    public class ChatViewModel:ViewModelBase
    {
        private ObservableCollection<Models.NewApiModels.Responses.MessageModel> _messages;
        public ObservableCollection<Models.NewApiModels.Responses.MessageModel> Messages
        {
            get { return _messages; }
            set { _messages = value;
                OnPropertyChanged();
            }
        }

        private string doctorToken = string.Empty;
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value;
                OnPropertyChanged();
            }
        }


        public Command DisplayActionSheetCommand { get; set; }
        public Command SendMessageCommand { get; set; }
        Models.AppModels.Chat.PendingChatModel _chatContext;
        public ChatViewModel(Models.AppModels.Chat.PendingChatModel chatContext, Page context):base(context)
        {
            _chatContext = chatContext;
            var timeConverter = new Converters.LongToDatetimeConverter();
            IsActive = ((DateTime)timeConverter.Convert(chatContext.ChatDataContext.DueAt, null, null, null) > DateTime.Now);
            OnPropertyChanged("IsActive");
            Messages = new ObservableCollection<Models.NewApiModels.Responses.MessageModel>();
            DisplayActionSheetCommand = new Command(() => OnDisplayActionSheet());
            SendMessageCommand = new Command(async () => await OnSendMessage());
            MessagingCenter.Subscribe<OneSignalPushNotificationHandler, Models.SQLite.MessageData>(this,
                ApiSettings.MessagingCenterMessageSubcriptions.RefreshChat, (sender, args) => {
                    try {
                        if (args.chatRoomID != _chatContext.ChatDataContext.Id) return;
                        AddMessage(
                           new Models.NewApiModels.Responses.MessageModel()
                           {
                               IsPasient = args.messageOwnerID == GlobalConfig.Identity.ID,
                               Message = args.message,
                               SentByUserId=new Models.NewApiModels.Responses.SentByUserId()
                               {
                                   Id= args.messageOwnerID
                               }
                           });

                    }
                    catch { }
                });
            LoadMessages();
        }

        async void OnDisplayActionSheet()
        {
            var result = await Alert.DisplayActionSheet(Labels.Labels.SelectOneAction, Labels.Labels.Cancel, null, Labels.Labels.Camera, Labels.Labels.PhotoAndLibrary, Labels.Labels.Notes);
            if (result == null)
                return;
            if (result == Labels.Labels.Camera)
            {
                CamaraHandler camera = new CamaraHandler();
                var photoResult = await camera.TakePhotoAsynk();
            }
            else if (result == Labels.Labels.PhotoAndLibrary)
            {
                CamaraHandler camera = new CamaraHandler();
                var photoResult = await camera.PickPhotoAsync();
            }
            else if (result == Labels.Labels.Notes)
            {
                await Navigation.PushAsync(new NotesListView());
            }
        }

        public async Task OnSendMessage()
        {
            if (!IsActive)
                return;
            if (string.IsNullOrWhiteSpace(Message)) return;
            var copy = Message;
            Message = string.Empty;
            OnPropertyChanged("Message");
            var result=await new ApiRepository().PostChatMessage(GlobalConfig.Identity.ID, (int)_chatContext.ChatDataContext.Id, copy);
        }

        void AddMessage(Models.NewApiModels.Responses.MessageModel m)
        {
            if (Messages.Count == 0 || Messages.Last().IsPasient != m.IsPasient)
                m.NeedDisplayPicture = false;//make this logic if want to add a user image in the chat
            else
                m.NeedDisplayPicture = false;
            m.IsPasient = m.SentByUserId.Id == GlobalConfig.Identity.ID;
            Messages.Add(m);
            OnPropertyChanged("Messages");
        }

        async void LoadMessages()
        {
            try
            {
                var messages = await new ApiRepository().GetChatMessagesHistory(GlobalConfig.Identity.ID, (int)_chatContext.ChatDataContext.Id);
                messages.Reverse();
                messages.ForEach(e => AddMessage(e));
            }
            catch { }
        }
    }
}
