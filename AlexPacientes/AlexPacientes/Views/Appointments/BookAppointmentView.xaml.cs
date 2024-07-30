using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Appointments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookAppointmentView : BaseContentPage
    {
        public BookAppointmentView(Models.NewApiModels.Responses.DoctorModel doctor)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Appoiments.BookAppointmentViewModel(this, doctor);
        }

        public BookAppointmentView(long doctorID, long appointmentID)
        {
            InitializeComponent();
            BindingContext=new ViewModels.Appoiments.RescheduleAppointmentViewModel(this, doctorID, appointmentID);
        }
    }
}