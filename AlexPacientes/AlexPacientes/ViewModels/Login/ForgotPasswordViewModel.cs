using AlexPacientes.Models.ApiModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Login
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        public string Email { get; set; }

        public Command SendCommand { get; set; }

        public ForgotPasswordViewModel(Page context) : base(context)
        {
            SendCommand = new Command(async () => await OnSendEmail());
        }

        private async Task OnSendEmail()
        {
            IsBusy = true;
            try
            {
                var recoverPassword = new Models.NewApiModels.RecoverPasswordRequest()
                {
                    email = Email
                };
                var response = await new ApiRepository().ForgotPassword(recoverPassword);
                if (response.HasValidStatus())
                {
                    await Alert.DisplayAlert(Labels.Labels.Done, string.Format(Labels.Labels.ForgotPasswordEmailFormat, Email), Labels.Labels.OK);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayError(response.Error.Errors[0].Message);
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
