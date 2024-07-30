using AlexPacientes.ViewModels.Settings;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsView : BaseContentPage
    {
        public ContactUsView()
        {
            InitializeComponent();
            BindingContext = new ContactUsViewModel(this);
        }
    }
}