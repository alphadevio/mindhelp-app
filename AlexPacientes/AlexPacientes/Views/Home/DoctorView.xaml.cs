using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorView : BaseContentPage
    {
        public DoctorView(Models.NewApiModels.Responses.DoctorModel doctor, int categoryId = 0)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Home.DoctorViewModel(this, doctor, categoryId);
        }
    }
}