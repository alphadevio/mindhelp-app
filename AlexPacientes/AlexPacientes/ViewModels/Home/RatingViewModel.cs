using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Home
{
    public class RatingViewModel : ViewModelBase
    {
        private ObservableCollection<ReviewModel> _ratings;
        public ObservableCollection<ReviewModel> Ratings 
        {
            get { return _ratings; }
            set { _ratings = value;
                OnPropertyChanged();
            }
        }

        private int _doctorID;

        public RatingViewModel(Page context, int doctorID) : base(context)
        {
            _doctorID = doctorID;
            Ratings = new ObservableCollection<ReviewModel>();

            GetData();
        }

        private async void GetData()
        {
            IsBusy = true;
            try
            {
                var response = await new ApiRepository().Reviews(_doctorID);
                if (response.HasValidStatus())
                {
                    Ratings = new ObservableCollection<ReviewModel>(response.Data.Items);
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
