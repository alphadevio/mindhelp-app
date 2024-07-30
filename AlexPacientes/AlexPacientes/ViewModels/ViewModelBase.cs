using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public INavigation MainNavigation { get { return App.Current.MainPage.Navigation; } }
        public Page context { get; set; }
        public Page Alert { get; set; }
        public Command BackCommand { get; set; }
        public Command OnAuthenticateCommand { get; set; }

        public ViewModelBase(Page context)
        {
            this.Navigation = context.Navigation;
            this.context = context;
            this.Alert = context;
            BackCommand = new Command(async() => {
                await Navigation.PopAsync();
            });
            DisplayLoginPageCommand = new Command(async () => await MainNavigation.PushAsync(new Views.Login.LoginView()));
            MessagingCenter.Subscribe<Application, bool>(this, "ActiveSession", async (sender, status) =>
            {
                RequireAuthentication = !status;
                OnPropertyChanged("RequireAuthentication");

                if (!status)
                    try
                    {
                        await Navigation.PopToRootAsync();
                        await MainNavigation.PopToRootAsync();
                    }
                    catch { }
                else if (OnAuthenticateCommand != null)
                    OnAuthenticateCommand.Execute(status);
            });
        }

        private bool _requireAuthentication;
        public bool RequireAuthentication
        {
            get { return _requireAuthentication; }
            set
            {
                _requireAuthentication = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        private Command _displayLoginPageCommand;
        public Command DisplayLoginPageCommand
        {
            get { return _displayLoginPageCommand; }
            set { _displayLoginPageCommand = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Device.BeginInvokeOnMainThread(() =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        public async Task DisplayError(string message)
        {
            await DisplayError(Labels.Labels.GenericErrorTittle, message);
        }

        public async Task DisplayError(string title, string message)
        {
            await Alert.DisplayAlert(title, message, Labels.Labels.OK);
        }
    
    }
}
