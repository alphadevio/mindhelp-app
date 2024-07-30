using AlexPacientes.ViewModels.Settings.DeleteAccount;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings.DeleteAccount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteAccountView : BaseContentPage
    {
        public DeleteAccountView()
        {
            InitializeComponent();
            BindingContext = new DeleteAccountViewModel(this);
        }
    }
}