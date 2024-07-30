using AlexPacientes.Converters;
using AlexPacientes.Models.ApiModels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Styles;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Booking
{
    public class PastBookingViewModel : ViewModelBase
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

        public PastBookingViewModel(Page context) : base(context)
        {
            Bookings = new ObservableCollection<BookingModel>();
            OnAuthenticateCommand = new Command(async () => await GetPastAppointments());
            PullToRefreshCommand = new Command(async() => await GetPastAppointments());
        }

        private async Task GetPastAppointments()
        {
            try
            {
                Bookings.Clear();
                var pastAppointments = await new ApiRepository().GetPastAppointments(GlobalConfig.Identity.ID);
                List<Tuple<long, long>> ids = new List<Tuple<long, long>>();
                pastAppointments.ForEach(x =>
                {
                    ids.Add(new Tuple<long, long>(x.DoctorUserId.Id, x.DoctorUserId.PhotoMediaId));
                });
                var images = await GetProfileImages(ids);
                foreach (var upcomingAppointment in pastAppointments)
                {
                    //var currentLocalTime = ((DateTime)new LongToDatetimeConverter().Convert(upcomingAppointment.DoctorUserId.CreatedAt, null, null, null)).ToLocalTime();
                    var currentLocalTime = ((DateTime)new LongToDatetimeConverter().Convert(upcomingAppointment.StartAt, null, null, null)).ToLocalTime();
                    Bookings.Add(new BookingModel
                    {
                        Name = upcomingAppointment.DoctorUserId.FirstName + " " + upcomingAppointment.DoctorUserId.LastName,
                        BookAgain = new Command(() => {/*do something*/ }),
                        Area = upcomingAppointment.DoctorUserId.UserDescriptionShort,
                        Date = currentLocalTime.AddHours(Utilities.Utilities.GetExtraSummerHoursOfDate(currentLocalTime, false)),
                        Status = Statues.ParseStatus(upcomingAppointment.Status),
                        Image = images[upcomingAppointment.DoctorUserId.Id].Source
                    });
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            IsRefreshing = false;
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
    }
}
