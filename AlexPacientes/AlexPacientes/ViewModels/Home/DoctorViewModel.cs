using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using AlexPacientes.Converters;
using Xamarin.Plugin.Calendar.Models;
using System.Windows.Input;
using AlexPacientes.Extensions;

namespace AlexPacientes.ViewModels.Home
{
    public class DoctorViewModel : ViewModelBase
    {
        private DoctorModel _doctor;
        public DoctorModel Doctor
        {
            get { return _doctor; }
            set { _doctor = value;
                OnPropertyChanged();
            }
        }

        private bool _hasResume;
        public bool HasResume
        {
            get { return _hasResume; }
            set { _hasResume = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReviewModel> _ratings;
        public ObservableCollection<ReviewModel> Ratings
        {
            get { return _ratings; }
            set { _ratings = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                GetAppoiments();
            }
        }

        private ObservableCollection<MyDoctorAppointmentTimeModel> _times;
        public ObservableCollection<MyDoctorAppointmentTimeModel> Times
        {
            get { return _times; }
            set
            {
                _times = value;
                OnPropertyChanged();
            }
        }

        private MyDoctorAppointmentDayModel _selectedDay;
        public MyDoctorAppointmentDayModel SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if (_selectedDay != null)
                    _selectedDay.IsSelected = false;
                _selectedDay = value;
                _selectedDay.IsSelected = true;
                OnPropertyChanged();
                GetTimes();
            }
        }

        private MyDoctorAppointmentTimeModel _selectedTime;
        public MyDoctorAppointmentTimeModel SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != null)
                    _selectedTime.IsSelected = false;
                _selectedTime = value;
                if (_selectedTime != null)
                    _selectedTime.IsSelected = true;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MyDoctorAppointmentDayModel> _days;
        public ObservableCollection<MyDoctorAppointmentDayModel> Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }

        private bool _hasTimesAvailable;
        public bool HasTimesAvailable
        {
            get { return _hasTimesAvailable; }
            set
            {
                _hasTimesAvailable = value;
                OnPropertyChanged();
            }
        }

        private EventCollection _monthEvents;
        public EventCollection MonthEvents
        {
            get { return _monthEvents; }
            set { _monthEvents = value;
                OnPropertyChanged();
            }
        }

        private int currentMonth;
        public int CurrentMonth
        {
            get { return currentMonth; }
            set { currentMonth = value;
                if(CurrentYear>0&& value > 0)
                    SelectedDate = new DateTime(CurrentYear, value, 1);
                OnPropertyChanged();
            }
        }

        private int _currentYear;

        public int CurrentYear
        {
            get { return _currentYear; }
            set { _currentYear = value;
                if(value>0 && CurrentMonth > 0)
                    SelectedDate = new DateTime(value, CurrentMonth, 1);
                OnPropertyChanged();
            }
        }

        private string availableDatesTitle;
        public string AvailableDatesTitle
        {
            get { return availableDatesTitle; }
            set { availableDatesTitle = value;
                OnPropertyChanged();
            }
        }



        #region Filters
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private bool _male;
        public bool Male
        {
            get { return _male; }
            set
            {
                _male = value;
                OnPropertyChanged();
            }
        }

        private bool _female;
        public bool Female
        {
            get { return _female; }
            set
            {
                _female = value;
                OnPropertyChanged();
            }
        }


        private TimeSpan _startHour;
        public TimeSpan StartHour
        {
            get { return _startHour; }
            set
            {
                _startHour = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _endHout;
        public TimeSpan EndHour
        {
            get { return _endHout; }
            set
            {
                _endHout = value;
                OnPropertyChanged();
            }
        }

        int _categoryID = 0;
        #endregion



        public Command FeedbacksCommand { get; set; }
        public Command BookAppointmentCommand { get; set; }
        public Command OpenResumeCommand { get; set; }

        public DoctorViewModel(Page context, DoctorModel doctor, int categoryId = 0) : base(context)
        {
            Doctor = doctor;
            HasResume = !string.IsNullOrEmpty(doctor.ResumeMedia?.Url ?? "");
            Ratings = new ObservableCollection<ReviewModel>();

            SelectedDate = DateTime.Now;
            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;
            FeedbacksCommand = new Command(async (param) => await Navigation.PushAsync(new Views.Home.RatingView(Doctor.ID)));
            BookAppointmentCommand = new Command(async () => await OnBookAppointment());
            OpenResumeCommand = new Command(async () => await App.NavigateFromTabbedPage(new Views.Home.ResumeView(Doctor.ResumeMedia?.Url)));
            MonthEvents = new EventCollection();
            SelectedDate = DateTime.Now;
            EndHour = new DateTime(1, 1, 1, 23, 59, 59).TimeOfDay;
            StartHour = new DateTime(1, 1, 1, 0, 0, 0).TimeOfDay;
            this._categoryID = categoryId;
            GetData();
        }

        private async void GetData()
        {
            IsBusy = true;
            await GetFeedBacks();
            IsBusy = false;
        }

        private async Task GetFeedBacks()
        {
            try
            {
                var response = await new ApiRepository().Reviews(Doctor.ID);
                if (response.HasValidStatus())
                {
                    // Take the first 5 reviews
                    Ratings = new ObservableCollection<ReviewModel>(response.Data.Items.Take(5));
                }
            }
            catch { }
        }

        private async Task OnBookAppointment()
        {
            if (SelectedDay == null || SelectedTime == null)
            {
                await Alert.DisplayAlert(Labels.Labels.Oops, Labels.Labels.SelectDateTime, Labels.Labels.OK);
                return;
            }

            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(SelectedDay.Day).DateTime;
            DateTime datetimeStart = date.ChangeTime(SelectedTime.StartAt, date.Minute, date.Second, date.Millisecond);
            datetimeStart = datetimeStart.AddMinutes(SelectedTime.minutes);
            DateTime datetimeEnd = date.ChangeTime(SelectedTime.EndAt, date.Minute, date.Second, date.Millisecond);
            datetimeEnd = datetimeEnd.AddMinutes(SelectedTime.minutes);
            await Navigation.PushAsync(new Views.Appointments.AppointmentDetailView(_doctor, datetimeStart, datetimeEnd));
        }

        private async void GetAppoiments()
        {
            IsBusy = true;
            try
            {
                SelectedDate.AddHours(-SelectedDate.Hour);
                var sdate = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
                var edate = new DateTime(SelectedDate.Year, SelectedDate.Month, DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month));
                var response = await new ApiRepository().TimeSlots(_doctor.ID, SelectedDate);
                var monthlyTimeSlot = await new ApiRepository().DoctorsByCategoryAdvancedFilters(_categoryID, sdate, edate, StartHour, EndHour, Name, null, _doctor.ID);
                int totalDates = 0;
                if (monthlyTimeSlot != null && monthlyTimeSlot.Data != null & monthlyTimeSlot.Data.Items != null && monthlyTimeSlot.Data.Items.Count > 0)
                {
                    var timeSlots = monthlyTimeSlot.Data.Items.FirstOrDefault().TimeSlots;

                    MonthEvents.Clear();
                    timeSlots?.ForEach(e =>
                    {
                        if(e.start_at_miliseconds_utc!=null&& e.start_at_miliseconds_utc != 0)
                        {
                            var currentDatetime = (new DateTime(1970, 1, 1)).AddMilliseconds(long.Parse(e.start_at_miliseconds_utc.ToString()));
                            if(currentDatetime > DateTime.Now){
                                System.Collections.ICollection t;
                                totalDates++;
                                if (!MonthEvents.TryGetValue(currentDatetime, out t))
                                    MonthEvents.Add(currentDatetime, new DayEventCollection<EventModel>(Color.Purple, Color.Blue));
                            }
                        }
                    });
                }

                AvailableDatesTitle = string.Format("En {0} {1} Citas Disponibles", SelectedDate.ToString("MMMM"), totalDates);

                if (response.HasValidStatus())
                {
                    // Decrypt content
                    Days = new ObservableCollection<MyDoctorAppointmentDayModel>();
                    var converter = new LongToDatetimeConverter();
                    response.Data.Items.ForEach(e => {
                        var date = (DateTime)converter.Convert(e.Day, typeof(DateTime), null, System.Globalization.CultureInfo.InvariantCulture);
                        if (date > DateTime.Now.AddDays(-1))
                            Days.Add(new MyDoctorAppointmentDayModel(e));
                    });

                    if (Days.Count > 0)
                    {
                        var day = Days.Where(d => (new DateTime(1970, 1, 1)).AddMilliseconds(d.Day).Day == SelectedDate.Day).FirstOrDefault();
                        if (day != null)
                            SelectedDay = day;
                        else
                            SelectedDay = new MyDoctorAppointmentDayModel(new TimeSlotModel());
                    }
                }
                else
                {
                    //await DisplayError(response.Error.Errors[0].Message);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                //await DisplayError(Labels.Labels.GenericErrorMessage);
                //Cambiar seleccionar fechas del appointment a directamente completar el appointment!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            finally
            {
                IsBusy = false;
            }
        }


        private async void GetTimes()
        {
            IsBusy = true;
            try
            {
                Times = new ObservableCollection<MyDoctorAppointmentTimeModel>();
                SelectedDay.Availability.ForEach(e => Times.Add(new MyDoctorAppointmentTimeModel(e)));
                SelectedTime = null;
                HasTimesAvailable = Times.Count != 0;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                //await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
