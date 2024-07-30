using AlexPacientes.Models.ApiModels.Requests;
using AlexPacientes.Models.AppModels.Chat;
using AlexPacientes.Services;
using AlexPacientes.Settings;
using AlexPacientes.Styles;
using AlexPacientes.Utilities;
using AlexPacientes.Views.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace AlexPacientes.ViewModels.Chat
{
    public class PendingChatsViewModel:ViewModelBase
    {

        private ObservableCollection<PendingChatModel> _pendingChats;
        public ObservableCollection<PendingChatModel> PendingChats
        {
            get { return _pendingChats; }
            set
            {
                _pendingChats = value;
                OnPropertyChanged();
            }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public Command PullToRefreshCommand { get; set; }
        public Command ItemSelectedCommand { get; set; }
        public PendingChatsViewModel(Page context):base(context)
        {
            RequireAuthentication = true;
            PendingChats = new ObservableCollection<PendingChatModel>();
            ItemSelectedCommand = new Command((item) => OnItemSelected(item as PendingChatModel));
            PullToRefreshCommand = new Command(async() => await GetPendingChats());
            OnAuthenticateCommand = new Command(async() => await GetPendingChats());
        }
        async void OnItemSelected(PendingChatModel item)
        {
            await Navigation.PushAsync(new ChatView(item));
        }
        async Task GetPendingChats()
        {
            IsBusy = true;
            try
            {
                var chats = await new ApiRepository().GetChatRooms(GlobalConfig.Identity.ID);
                PendingChats.Clear();
                foreach(var chatRoom in chats)
                {
                    PendingChats.Add(new PendingChatModel()
                    {
                        ChatDataContext = chatRoom,
                        UserImage = (await new ApiRepository().GetProfileImage((int)chatRoom.DoctorUserId.PhotoMediaId)).Source,
                        Name = chatRoom.DoctorUserId.FullName,
                        Subtitle = "Envia mensajes a tu terapeuta por este medio"
                    });
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }




    }
}
