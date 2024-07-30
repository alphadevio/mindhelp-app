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
    public partial class EditProfileView : BaseContentPage
    {
        public EditProfileView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Settings.EditProfileViewModel(this);
        }
    }
}