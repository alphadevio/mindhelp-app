using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoChatView : ContentPage
    {
        public VideoChatView(Models.ApiModels.UserDoctorModel doctor, Models.NewApiModels.Responses.Appointment appointmentModel)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Chat.VideoChatViewModel(this, hostView, clientView, doctor, appointmentModel);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as ViewModels.Chat.VideoChatViewModel)?.EndSession();
        }
    }
}