using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings.Plans
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlansView : BaseContentPage
    {
        public PlansView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Settings.PlansViewModel(this);
        }
    }
}