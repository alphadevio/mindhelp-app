using AlexPacientes.Extensions;
using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using AlexPacientes.Converters;

namespace AlexPacientes.ViewModels.Appoiments
{
    public class BookAppointmentViewModel : ViewModelBase
    {
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value;
                OnPropertyChanged();
                GetAppoiments();
            }
        }

        private MyDoctorAppointmentDayModel _selectedDay;
        public MyDoctorAppointmentDayModel SelectedDay
        {
            get { return _selectedDay; }
            set {
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
            set {
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
            set { _days = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MyDoctorAppointmentTimeModel> _times;
        public ObservableCollection<MyDoctorAppointmentTimeModel> Times
        {
            get { return _times; }
            set { _times = value;
                OnPropertyChanged();
            }
        }

        private bool _hasTimesAvailable;
        public bool HasTimesAvailable
        {
            get { return _hasTimesAvailable; }
            set { _hasTimesAvailable = value;
                OnPropertyChanged();
            }
        }

        public DateTime MinimumDate { get; set; } = DateTime.Now;
        public Command BookAppointmentCommand { get; set; }

        private DoctorModel _doctor;


        public BookAppointmentViewModel(Page context, DoctorModel doctor) : base(context)
        {
            _doctor = doctor;

            // Override back command
            BackCommand = new Command(() => App.ReturnToTabbedPage());
            BookAppointmentCommand = new Command(() => OnBookAppointment());
            SelectedDate = MinimumDate;
        }

        private async void GetAppoiments()
        {
            //if (SelectedDate == MinimumDate) return;
            IsBusy = true;
            try
            {
                var response = await new ApiRepository().TimeSlots(_doctor.ID, SelectedDate);
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

                    if(Days.Count > 0)
                    {
                        SelectedDay = Days[0];
                    }
                }
                else
                {
                    await DisplayError(response.Error.Errors[0].Message);
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
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnBookAppointment()
        {
            if(SelectedDay == null || SelectedTime == null)
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
    }
}
