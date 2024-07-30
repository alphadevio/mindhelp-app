using AlexPacientes.Models.ApiModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public Command ChangePasswordCommand { get; set; }

        public ChangePasswordViewModel(Page context) : base(context)
        {
            ChangePasswordCommand = new Command(async () => await OnChangePassword());
        }

        private bool ValidatePassword(string password)
        {
            if (password == null)
                return false;
            if (password.Length < 5 && password.Length > 1)
                return false;
            return true;
        }

        private async Task OnChangePassword()
        {
            if(!ValidatePassword(Password))
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.EnterPassword, Labels.Labels.OK);
                return;
            }
            if (!ValidatePassword(NewPassword))
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.ErrorPasswordLenght, Labels.Labels.OK);
                return;
            }
            if (!ValidatePassword(ConfirmPassword))
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.ErrorPasswordLenght, Labels.Labels.OK);
                return;
            }
            if(ConfirmPassword != NewPassword)
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.ErrorMustBeSamePassword, Labels.Labels.OK);
                return;
            }

            IsBusy = true;
            try
            {
                var request = new ChangePasswordRequest { ID = GlobalConfig.Identity.ID, OldPassword = Password, NewPassword = NewPassword };
                var rest = new RestClient();
                var response = await rest.PostAsync<object>(ApiSettings.Methods.UPDATE_PASSWORD, request, GlobalConfig.Identity.Enckey);
                if (response != null)
                {
                    await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.Done, Labels.Labels.OK);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayError(Labels.Labels.GenericErrorMessage);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
