using AlexPacientes.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : BaseContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(this);
        }
    }
}