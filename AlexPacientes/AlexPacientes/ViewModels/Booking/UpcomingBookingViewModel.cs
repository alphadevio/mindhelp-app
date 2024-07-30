using AlexPacientes.Converters;
using AlexPacientes.Models.ApiModels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Models.NewApiModels.Responses;
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
    public class UpcomingBookingViewModel : ViewModelBase
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
            set { _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public Command GoToAppointmentsCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public UpcomingBookingViewModel(Page context) : base(context)
        {
            IsRefreshing = false;
            Bookings = new ObservableCollection<BookingModel>();
            RefreshCommand = new Command(async () => await GetUpcommingAppointments());
            OnAuthenticateCommand = new Command(async() => await GetUpcommingAppointments());
            GoToAppointmentsCommand = new Command(() => App.ScrollToSpecificTabbedPage(0));
        }

        private async Task GetUpcommingAppointments()
        {
            try
            {
                Bookings.Clear();
                var upcomingAppointments = await new ApiRepository().GetFutureAppointments(GlobalConfig.Identity.ID);
                //var ids = upcomingAppointments.ToDictionary(x => x.DoctorUserId.Id, x => new Tuple<long, long>(x.DoctorUserId.Id,x.DoctorUserId.PhotoMediaId));
                List<Tuple<long, long>> ids = new List<Tuple<long, long>>();
                upcomingAppointments.ForEach(x =>
                {
                    ids.Add(new Tuple<long, long>(x.DoctorUserId.Id, x.DoctorUserId.PhotoMediaId));
                });
                var images = await GetProfileImages(ids);
                foreach (var upcomingAppointment in upcomingAppointments)
                {
                    var currentLocalTime = ((DateTime)new LongToDatetimeConverter().Convert(upcomingAppointment.StartAt, null, null, null)).ToLocalTime();
                    // Filter appointments already expired
                    if (currentLocalTime < DateTime.Now)
                        continue;
                    Bookings.Add(new BookingModel
                    {
                        Name = upcomingAppointment.DoctorUserId.FirstName + " " + upcomingAppointment.DoctorUserId.LastName,
                        Area = upcomingAppointment.DoctorUserId.UserDescriptionShort,
                        Date = currentLocalTime.AddHours(Utilities.Utilities.GetExtraSummerHoursOfDate(currentLocalTime, false)),
                        Status = Statues.ParseStatus(upcomingAppointment.Status),
                        Image = images[upcomingAppointment.DoctorUserId.Id].Source,
                        Reschedule=new Command(async () => await RescheduleAppointment(upcomingAppointment))
                    });
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            IsRefreshing = false;
        }

        long GetHoursFromMiliseconds(long miliseconds)
        {
            return (miliseconds / (long)1000) / (long)3600;
        }

        private async Task<Dictionary<long, ImageMediaSource>> GetProfileImages(List<Tuple<long, long>> ids)
        {
            var results = new List<Tuple<long, ImageMediaSource>>();
            var repository = new ApiRepository();
            foreach (var x in ids)
            {
                if (!results.Any(e=> e.Item1 == x.Item1))
                    results.Add(new Tuple<long, ImageMediaSource>(x.Item1, await repository.GetProfileImage(x.Item2)));
            }
            return results.ToDictionary(e=>e.Item1,e=>e.Item2);
        }
    
        private async Task RescheduleAppointment(Appointment appointmentData)
        {
            var page = new Views.Appointments.BookAppointmentView(appointmentData.DoctorUserId.Id, appointmentData.Id);
            await Navigation.PushAsync(page);
        }
    }
}
