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
    public partial class AppointmentDetailView : BaseContentPage
    {
        public AppointmentDetailView(Models.NewApiModels.Responses.DoctorModel doctor, DateTime start, DateTime end)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Appoiments.AppointmentDetailViewModel(this, doctor, start, end);
        }
    }
}