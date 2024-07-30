using AlexPacientes.Models.ApiModels;
using AlexPacientes.Services;
using AlexPacientes.Settings;
using AlexPacientes.Styles;
using AlexPacientes.Utilities;
using AlexPacientes.Views.Chat.Controls;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace AlexPacientes.ViewModels.Chat
{
    public class VideoChatViewModel : ViewModelBase
    {
        private ImageSource _callStatusIcon;
        public ImageSource CallStatusIcon
        {
            get { return _callStatusIcon; }
            set { _callStatusIcon = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _cameraStatusIcon;
        public ImageSource CameraStatusIcon
        {
            get { return _cameraStatusIcon; }
            set { _cameraStatusIcon = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _audioStatusIcon;
        public ImageSource AudioStatusIcon
        {
            get { return _audioStatusIcon; }
            set { _audioStatusIcon = value;
                OnPropertyChanged();
            }
        }

        private UserDoctorModel _doctor;
        public UserDoctorModel Doctor
        {
            get { return _doctor; }
            set { _doctor = value;
                OnPropertyChanged();
            }
        }

        private bool _shouldDisplayInstructions;
        public bool ShouldDisplayInstructions
        {
            get { return _shouldDisplayInstructions; }
            set { _shouldDisplayInstructions = value;
                OnPropertyChanged();
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value;
                CallStatusIcon = value ? Icons.CallRed : Icons.CallGreen;
                OnPropertyChanged();
            }
        }

        private bool _isMicrophoneMuted;
        public bool IsMicrophoneMuted
        {
            get { return _isMicrophoneMuted; }
            set { _isMicrophoneMuted = value;
                AudioStatusIcon = value ? Icons.Muted : Icons.Voice;
                OnPropertyChanged();
            }
        }

        private bool _isVideoOff;
        public bool IsVideoOff
        {
            get { return _isVideoOff; }
            set { _isVideoOff = value;
                CameraStatusIcon = value ? Icons.CameraRed : Icons.Camera;
                OnPropertyChanged();
            }
        }

        public Command ConnectToSessionCommand { get; set; }
        public Command MuteMicrophoneCommand { get; set; }
        public Command DisableVideoCommand { get; set; }
        public Command SwapCameraCommand { get; set; }

        private Models.NewApiModels.Responses.Appointment _appointmentModel;
        private Models.NewApiModels.Responses.Session _sessionData;
        private IOpenTokService _openTokService;

        private RatingSessionView _ratingSessionView;
        private BookSessionView _bookSessionView;

        public VideoChatViewModel(Page context, Controls.StreamView host, Controls.StreamView client, UserDoctorModel doctor, Models.NewApiModels.Responses.Appointment appointmentModel) : base(context)
        {
            ShouldDisplayInstructions = true;
            IsConnected = false;
            IsMicrophoneMuted = false;
            IsVideoOff = false;
            Doctor = doctor;
            _appointmentModel = appointmentModel;
            ConnectToSessionCommand = new Command(async() => await OnConnectToSession());
            MuteMicrophoneCommand = new Command(() => OnMicrophoneMuted());
            DisableVideoCommand = new Command(() => OnVideoOff());
            SwapCameraCommand = new Command(() => _openTokService.SwapCamera());
            BackCommand = new Command(async () => await App.ReturnToTabbedPage());

            _ratingSessionView = new RatingSessionView()
            {
                SubmitReviewCommand = new Command(async (param) => await RateSession((List<Tuple<string, object>>)param))
            };
            _bookSessionView = new BookSessionView()
            {
                //BookCommand = new Command(async () => await BookSession())
            };

            _openTokService = DependencyService.Get<IOpenTokService>();
            host.NativeViewSet += (s, e) => _openTokService.SetHostContainer(host);
            client.NativeViewSet += (s, e) => _openTokService.SetClientContainer(client);
            _openTokService.OnPartnerConnectedCommand = new Command((param) => ShouldDisplayInstructions = false);
            Device.BeginInvokeOnMainThread(async () =>
            {
                _sessionData = await GetAppointmentData((int)_appointmentModel.PatientUserId.Id, (int)appointmentModel.Id);
                await OnConnectToSession();
            });
        }

        private async Task<bool> CheckPermissions()
        {
            // Check permission camera
            var status = await CheckAndRequestPermission(new Permissions.Camera());
            if(status != PermissionStatus.Granted)
            {
                return false;
            }

            // Check permission microphone
            status = await CheckAndRequestPermission(new Permissions.Microphone());
            if (status != PermissionStatus.Granted)
            {
                return false;
            }

            return true;
        }

        private async Task<PermissionStatus> CheckAndRequestPermission<T>(T permission) where T : Permissions.BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }

        private async Task OnConnectToSession()
        {
            if(!await CheckPermissions())
            {
                return;
            }

            if (IsConnected)
            {
                _openTokService.EndSession();
                IsConnected = false;
                IsMicrophoneMuted = false;
                IsVideoOff = false;

                await _ratingSessionView.Show();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    if (_sessionData == null) return;
                    _openTokService.InitNewSession(ApiSettings.TOKBOX_API_KEY, _sessionData.ConferenceSessionToken, _sessionData.Token);
                    IsConnected = true;
                });
            }
        }

        public async Task<Models.NewApiModels.Responses.Session> GetAppointmentData(int userID, int appointmentID)
        {
            try
            {
                return await new ApiRepository().GetSessionAppointmentData(userID, appointmentID);
            }
            catch { }
            return new Models.NewApiModels.Responses.Session();
        }

        private void OnMicrophoneMuted()
        {
            IsMicrophoneMuted = !IsMicrophoneMuted;
            _openTokService.PublishAudio(!IsMicrophoneMuted);
        }

        private void OnVideoOff()
        {
            IsVideoOff = !IsVideoOff;
            _openTokService.PublishVideo(!IsVideoOff);
        }

        private async Task RateSession(List<Tuple<string, object>> value)
        {
            try
            {
                var response = await new ApiRepository().RateSession((int)_appointmentModel.PatientUserId.Id, (double)value[0].Item2, (string)value[1].Item2);
                if (response)
                {
                    await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.SessionFinished, Labels.Labels.OK);
                    await Navigation.PopAllPopupAsync();
                    await App.ReturnToTabbedPage();
                    await new ApiRepository().FinishAppointment(GlobalConfig.Identity.ID, (int)_appointmentModel.Id);
                }
                else
                {
                    await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.SessionFinished, Labels.Labels.OK);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
        }

        public void EndSession()
        {
            if(_openTokService != null)
                _openTokService.EndSession();
        }

    }
}
