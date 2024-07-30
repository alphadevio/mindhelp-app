using AlexPacientes.Converters;
using AlexPacientes.Helpers;
using AlexPacientes.Models.ApiModels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Styles;
using AlexPacientes.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using System.Collections.Generic;

namespace AlexPacientes.ViewModels.Booking
{
    public class CurrentBookingViewModel : ViewModelBase
    {
        private ObservableCollection<BookingModel> _bookings;
        public ObservableCollection<BookingModel> Bookings
        {
            get { return _bookings; }
            set { _bookings = value;
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

        public CurrentBookingViewModel(Page context) : base(context)
        {
            GetData();
        }

        public Command OnAppearingCommand { get; set; }

        private void GetData()
        {
            Bookings = new ObservableCollection<BookingModel>();
            OnAuthenticateCommand=new Command(async() => await GetCurrentAppointments());
            PullToRefreshCommand = new Command(async () => await GetCurrentAppointments());
            MessagingCenter.Subscribe<Appoiments.AppointmentDetailViewModel>(this, ApiSettings.GenerigMessagingCenterMessagesSubscriptions.RefreshCurrentDates, async(caller) => await GetCurrentAppointments());
        }

        private async Task GetCurrentAppointments()
        {
            try
            {
                var currentAppointments = await new ApiRepository().GetCurrentAppointments(GlobalConfig.Identity.ID);
                currentAppointments.Reverse();
                List<Tuple<long, long>> ids = new List<Tuple<long, long>>();
                currentAppointments.ForEach(x =>
                {
                    ids.Add(new Tuple<long, long>(x.DoctorUserId.Id, x.DoctorUserId.PhotoMediaId));
                });
                var images = await GetProfileImages(ids);
                Bookings.Clear();
                foreach (var item in currentAppointments)
                {
                    if (Statues.ParseStatus(item.Status) != BookingStatus.Expired && Statues.ParseStatus(item.Status) != BookingStatus.Rejected)
                    {
                        var currentLocalTime = ((DateTime)new LongToDatetimeConverter().Convert(item.StartAt, null, null, null)).ToLocalTime();                        
                        Bookings.Add(new BookingModel
                        {
                            Name = item.DoctorUserId.FirstName + " " + item.DoctorUserId.LastName,
                            Area = "Terapeuta",
                            Date = currentLocalTime.AddHours(Utilities.Utilities.GetExtraSummerHoursOfDate(currentLocalTime, false)),
                            Status = Statues.ParseStatus(item.Status),
                            Image = images[item.DoctorUserId.Id].Source,
                            Appointment = item,
                            Total = item.FinalPrice,
                            Chat = new Command(async (param) =>
                            {
                                IsBusy = true;
                                try {
                                    var appointment = param as Models.NewApiModels.Responses.Appointment;
                                    if (appointment == null) return;

                                    //Force to create a chat session
                                    var _sessionData = await new ApiRepository().GetSessionAppointmentData((int)appointment.PatientUserId.Id, (int)appointment.Id);
                                    var chats = await new ApiRepository().GetChatRooms(GlobalConfig.Identity.ID);
                                    var filteredChat = (from chat in chats where chat.DoctorUserId.Id == _sessionData.DoctorUserId select chat).FirstOrDefault();
                                    var chatModel = new Models.AppModels.Chat.PendingChatModel()
                                    {
                                        ChatDataContext = filteredChat,
                                        UserImage = (await new ApiRepository().GetProfileImage((int)filteredChat.DoctorUserId.PhotoMediaId)).Source,
                                        Name = filteredChat.DoctorUserId.FullName,
                                        Subtitle = "Envia mensajes a tu terapeuta por este medio"
                                    };
                                    await App.ScrollToSpecificTabbedPage(2, new Views.Chat.ChatView(chatModel));
                                }
                                catch
                                {
                                    await DisplayError(Labels.Labels.GenericErrorMessage);
                                }
                                IsBusy = false;
                            }),
                            VideoCall = new Command(async (param) => await App.NavigateFromTabbedPage(new Views.Chat.VideoChatView(((AlexPacientes.Models.NewApiModels.Responses.Appointment)param).ConvertToUserDoctor(), item))),
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            IsRefreshing = false;
        }

        public async Task<Models.NewApiModels.AppointmentStatus.Item> GetAppointmentData(int appointmentID, TokboxTokenModel tok, UserDoctorModel Doctor)
        {
            try
            {
                var client = new RestClient();
                var status = await client.GetAsync<Models.NewApiModels.AppointmentStatus.GetAppointmentStatus>(string.Format(ApiSettings.Methods.GET_APPOINTMENT_BY_APPOINTMENT_ID, appointmentID));
                if (status != null && status.data != null && status.data.items.Count > 0 && status.data.items[0].appointmentId != 0)
                    return status.data.items[0];
                var appointmentData = new Models.NewApiModels.POSTAppointmentStatus.POSTAppointmentStatus()
                {
                    appointmentId = appointmentID,
                    doctorId = Doctor.ID,
                    session = tok.SessionID,
                    userId = GlobalConfig.Identity.ID,
                    user_token = tok.doctor_token,
                    doctor_token = tok.doctor_token
                };
                var postAppointmentResult = await client.PostAsync<Models.NewApiModels.AppointmentStatus.GetAppointmentStatus>(ApiSettings.Methods.POST_APPOINTMENT_SESSION_DATA, appointmentData);
                return postAppointmentResult.data.items[0];
            }
            catch { }
            return new Models.NewApiModels.AppointmentStatus.Item();
        }

        private async Task<Dictionary<long, Models.NewApiModels.Responses.ImageMediaSource>> GetProfileImages(List<Tuple<long, long>> ids)
        {
            var results = new List<Tuple<long, Models.NewApiModels.Responses.ImageMediaSource>>();
            var repository = new ApiRepository();
            foreach (var x in ids)
            {
                if (!results.Any(e => e.Item1 == x.Item1))
                    results.Add(new Tuple<long, Models.NewApiModels.Responses.ImageMediaSource>(x.Item1, await repository.GetProfileImage(x.Item2)));
            }
            return results.ToDictionary(e => e.Item1, e => e.Item2);
        }

        public async Task<bool> SendSessionMessage(string doctorToken, int appointmentID, int doctorID)
        {
            return await new ApiRepository().SendMessage(Labels.Labels.HereYouCanChat, doctorToken, appointmentID, doctorID);
        }
    }
}
