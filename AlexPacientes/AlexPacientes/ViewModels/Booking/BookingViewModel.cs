using AlexPacientes.Models.ApiModels;
using AlexPacientes.Settings;
using AlexPacientes.Views.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Booking
{
    public class BookingViewModel : ViewModelBase
    {
        private List<string> _titles;
        public List<string> Titles
        {
            get { return _titles; }
            set { _titles = value;
                OnPropertyChanged();
            }
        }

        private List<ViewModelBase> _views;
        public List<ViewModelBase> Views
        {
            get { return _views; }
            set { _views = value;
                OnPropertyChanged();
            }
        }

        public BookingViewModel(Page context) : base(context)
        {
            RequireAuthentication = true;
            Titles = new List<string> { Labels.Labels.Current, Labels.Labels.Upcoming , Labels.Labels.Past };
            Views = new List<ViewModelBase>
            {
                new CurrentBookingViewModel(context),
                new UpcomingBookingViewModel(context),
                new PastBookingViewModel(context),
            };
        }
    }
}
