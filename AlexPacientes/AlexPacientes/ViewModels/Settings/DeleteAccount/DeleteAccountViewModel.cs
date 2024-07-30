using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings.DeleteAccount
{
    public class DeleteAccountViewModel:ViewModelBase
    {
        public ICommand DelteAccountCommand { get; set; }
        public DeleteAccountViewModel(Page content):base(content)
        {
            DelteAccountCommand= new Command(async () => await OnDeleteAccount());
        }

        private async Task OnDeleteAccount()
        {
            var response = await new ApiRepository().DeleteAccount(GlobalConfig.Identity.ID);
            if (response)
            {
                await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.AccountDeleted, Labels.Labels.Confirm);
                await App.Logout();
            }
            else
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.UnableToDeleteAccount, Labels.Labels.Confirm);
            }
        }
    }
}
