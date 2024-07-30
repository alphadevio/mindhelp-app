using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Login
{
    public class SignUpViewModel : ViewModelBase
    {
        public NewProfile Profile { get; set; }
        public Command SignUpCommand { get; set; }
        public Command SignInCommand { get; set; }

        public SignUpViewModel(Page context) : base(context)
        {
            Profile = new NewProfile();
            SignUpCommand = new Command(async () => await SignUp());
            SignInCommand = new Command(async () => await Navigation.PopAsync());
        }

        private async Task SignUp()
        {
            if (!Validate(Profile))
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.AllFieldsRequired, Labels.Labels.OK);
                return;
            }
            if (!ValidatePassword(Profile.Password))
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.ErrorPasswordLenght, Labels.Labels.OK);
                return;
            }

            IsBusy = true;
            try
            {
                var signupRequest = new Models.NewApiModels.SignupModelRequest()
                {
                    first_name = Profile.FirstName,
                    last_name = Profile.LastName,
                    email = Profile.Email.Replace(" ", ""),
                    password = Profile.Password
                };
                var response = await new ApiRepository().SignUp(signupRequest);
                if (response.HasValidStatus())
                {
                    // Save profile information
                    var profileData = new Models.NewApiModels.EditProfile()
                    {
                        first_name=Profile.FirstName,
                        last_name=Profile.LastName,
                        phone = Profile.PhoneNumber,
                        birth_at = Profile.Birthday.ToString(),
                        gender = Profile.Gender,
                        country = Profile.Country,
                        state = "",
                        city = "",
                        street_address = "",
                        zip = "",
                        country_code = Profile.CountryCode,
                        country_code_flag = "",
                        time_zone = Profile.TimeZone
                    };

                    await new ApiRepository().EditUser(profileData, response.Data.Items[0].ID);

                    await Alert.DisplayAlert(Labels.Labels.WelcomeBackMindHelp, Labels.Labels.SignUpSuccessful, Labels.Labels.OK);
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

        private bool Validate(NewProfile profile)
        {
            if (string.IsNullOrWhiteSpace(profile.FirstName)
                || string.IsNullOrWhiteSpace(profile.LastName)
                || string.IsNullOrWhiteSpace(profile.Email)
                || string.IsNullOrWhiteSpace(profile.Password)
                || string.IsNullOrWhiteSpace(profile.PhoneNumber)
                || string.IsNullOrWhiteSpace(profile.Country)
                || string.IsNullOrWhiteSpace(profile.Gender))
                return false;
            return true;
        }

        private bool ValidatePassword(string password)
        {
            if (password == null)
                return false;
            if (password.Length < 5 && password.Length > 1)
                return false;
            return true;
        }

        public class NewProfile
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string CountryCode { get; set; } = "+52";
            public string Country { get; set; }
            public string PhoneNumber { get; set; }
            public long Birthday => new DateTimeOffset(BirthdayDateTime).ToUnixTimeMilliseconds();
            public string Gender { get; set; }
            public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
            [Newtonsoft.Json.JsonIgnore]
            public DateTime BirthdayDateTime { get; set; }
        }
    }
}
