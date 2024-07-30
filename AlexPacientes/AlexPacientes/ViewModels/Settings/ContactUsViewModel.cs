using AlexPacientes.Models.ApiModels.Requests;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class ContactUsViewModel:ViewModelBase
    {
        private ContactUsModel _contactData;
        public ContactUsModel ContactData
        {
            get { return _contactData; }
            set { _contactData = value;
                OnPropertyChanged();
            }
        }
        public Command ContactCommand { get; set; }
        public ContactUsViewModel(Page context):base(context)
        {
            ContactData = new ContactUsModel();
            ContactCommand = new Command(async () => await OnContact());
        }

        async System.Threading.Tasks.Task OnContact()
        {
            if(!ContactData.Validate())
            {
                await Alert.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.AllFieldsRequired, Labels.Labels.OK);
                return;
            }

            IsBusy = true;
            try
            {
                var sendContactRequest = new Models.NewApiModels.SendContactFormRequest()
                {
                    email = ContactData.Email,
                    name = ContactData.Name,
                    phone = ContactData.Phone,
                    description = ContactData.Description
                };
                var response = await new ApiRepository().SendContactForm(sendContactRequest);
                if (response.HasValidStatus())
                {
                    await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.QuestionSended, Labels.Labels.OK);
                    await App.ReturnToTabbedPage();
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
