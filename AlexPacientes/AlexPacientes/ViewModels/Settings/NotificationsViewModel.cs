using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class NotificationsViewModel : ViewModelBase
    {
        public ObservableCollection<Models.SQLite.NotificationData> _notifications;
        public ObservableCollection<Models.SQLite.NotificationData> Notifications
        {
            get { return _notifications; }
            set { _notifications = value;
                OnPropertyChanged();
            }
        }

        public NotificationsViewModel(Page context) : base(context)
        {
            _ = GetNotifications();
        }

        private async Task GetNotifications()
        {
            IsBusy = true;
            try
            {
                var result = await Utilities.NotificationsRepository.GetNotifications();
                Notifications = new ObservableCollection<Models.SQLite.NotificationData>(result);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
