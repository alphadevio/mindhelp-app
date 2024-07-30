using System.ComponentModel;

namespace AlexPacientes.Models.AppModels
{
    public class MyDoctorAppointmentTimeModel : NewApiModels.Responses.AvailabilityTimeSlotModel, INotifyPropertyChanged
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

        public MyDoctorAppointmentTimeModel(NewApiModels.Responses.AvailabilityTimeSlotModel model)
        {
            ID = model.ID;
            Day = model.Day;
            StartAt = model.StartAt;
            EndAt = model.EndAt;
            Available = model.Available;
            minutes = model.minutes;
            slot_type = model.slot_type;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
