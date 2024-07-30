using AlexPacientes.Models.AppModels.Settings;
using AlexPacientes.Settings;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class SettingsViewModel:ViewModelBase
    {
        private ObservableCollection<SettingsModel> _settings;
        public ObservableCollection<SettingsModel> Settings
        {
            get { return _settings; }
            set { _settings = value;
                OnPropertyChanged();
            }
        }
        private SettingsIdentityModel _userData;
        public SettingsIdentityModel UserData
        {
            get { return _userData; }
            set { _userData = value;
                OnPropertyChanged();
            }
        }


        public Command SelectedSettingCommand { get; set; }
        public SettingsViewModel(Page context):base(context)
        {
            RequireAuthentication = true;
            UserData = new SettingsIdentityModel();
            Settings = new ObservableCollection<SettingsModel>();
            SelectedSettingCommand = new Command((param) => OnSelectedSetting(param as SettingsModel));
            SetSettings();
            SetUserData();

            MessagingCenter.Subscribe<Application, bool>(this, "ActiveSession", (sender, e) => SetUserData());
            MessagingCenter.Subscribe<EditProfileViewModel>(this, "RefreshIdentity", (sender) => SetUserData());
        }

        void SetUserData()
        {
            if (GlobalConfig.Identity != null)
            {
                UserData.Name = string.Format("{0} {1}", GlobalConfig.Identity.FirstName, GlobalConfig.Identity.LastName);
                UserData.Email = GlobalConfig.Identity.Email;
                UserData.Image = GlobalConfig.Identity.ProfileImage;
                OnPropertyChanged("UserData");
            }
        }

        void OnSelectedSetting(SettingsModel setting)
        {
            if (setting != null && setting.Action != null)
                setting.Action.Execute(null);
        }

        void SetSettings()
        {
            Settings.Add(new SettingsModel() { Title = Labels.Labels.EditProfile, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.EditProfileView())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.ChangePassword, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.ChangePasswordView())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.PaymentMethods, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.Payments.PaymentMethodsList())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.Plans, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.Plans.PlansView())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.Notifications, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.Notifications.NotificationsView())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.ContactUs, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.ContactUsView())) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.DeleteAccountLabel, Action = new Command(async () => await App.NavigateFromTabbedPage(new Views.Settings.DeleteAccount.DeleteAccountView())) });
            //Settings.Add(new SettingsModel() { Title = Labels.Labels.Notification, IsSwitch=true });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.FAQ,
                Action = new Command(() => {
                    Device.OpenUri(new Uri("https://usuario.mindhelp.mx/faq"));
                })
            });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.AboutMH, Action=new Command(()=> {
                Device.OpenUri(new Uri("https://mindhelp.mx/nostros"));
            }) });
            Settings.Add(new SettingsModel() { Title = Labels.Labels.Logout, Action = new Command(() => App.Logout()) });
        }
    }

    public class SettingsIdentityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
