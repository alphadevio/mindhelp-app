using System.ComponentModel;

namespace AlexPacientes.Models.AppModels
{
    public class MyDoctorAppointmentDayModel : NewApiModels.Responses.TimeSlotModel, INotifyPropertyChanged
    {
        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public int TotalTimeSlots => Availability.Count;

        public MyDoctorAppointmentDayModel(NewApiModels.Responses.TimeSlotModel model)
        {
            Availability = model.Availability;
            Day = model.Day;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
