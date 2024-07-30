using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordView : BaseContentPage
    {
        public ChangePasswordView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Settings.ChangePasswordViewModel(this);
        }
    }
}