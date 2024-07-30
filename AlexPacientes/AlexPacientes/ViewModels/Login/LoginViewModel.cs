using AlexPacientes.Helpers;
using AlexPacientes.Models.ApiModels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Command LoginCommand { get; set; }
        public Command ForgotPasswordCommand { get; set; }
        public Command SignUpCommand { get; set; }
       
        public LoginViewModel(Page context) : base(context)
        {
            LoginCommand = new Command(async () => await OnLogin());
            ForgotPasswordCommand = new Command(async () => await Navigation.PushAsync(new Views.Login.ForgotPasswordView()));
            SignUpCommand = new Command(async () => await Navigation.PushAsync(new Views.Login.SignUpView()));

#if DEBUG
            //Username = "victorpurata@mindhelp.mx";
            //Password = "123456";
            //Username = "aperezsandid+1@gmail.com";
            //Password = "12345678";
            Username = "yasido2@hotmail.com";
            Password = "12345";
            //Username = "carlos.luevanocx@gmail.com";
            //Password = "123456";
            //Username = "prueba_borrado@yopmail.com";
            //Password = "qvOxjo";
#endif
        }

        async Task OnLogin()
        {
            Identity identity = new Identity()
            {
                Email = Username,
                Password = Password
            };

            IsBusy = true;
            var result = await App.Authenticate(identity);
            if (result.Item1)
                await MainNavigation.PopAsync();
            else
                await DisplayError(result.Item2);
            IsBusy = false;
        }
    }
}
