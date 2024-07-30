using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using FFImageLoading.Forms;
using AlexPacientes.Styles;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using AlexPacientes.Views.Home.Popup;
using System.Windows.Input;

namespace AlexPacientes.ViewModels.Home
{
    public class DoctorsViewModel : ViewModelBase
    {
        private ObservableCollection<DoctorModel> _doctors;
        public ObservableCollection<DoctorModel> Doctors
        {
            get { return _doctors; }
            set { _doctors = value;
                OnPropertyChanged();
            }
        }

        private int _doctorsCount;
        public int DoctorsCount
        {
            get { return _doctorsCount; }
            set { _doctorsCount = value;
                OnPropertyChanged();
            }
        }

        private View _buttonsView;
        public View ButtonsView
        {
            get { return _buttonsView; }
            set { _buttonsView = value;
                OnPropertyChanged();
            }
        }

#region Filters
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged();    
            }
        }

        private bool _male;
        public bool Male
        {
            get { return _male; }
            set
            {
                _male = value;
                OnPropertyChanged();
            }
        }

        private bool _female;
        public bool Female
        {
            get { return _female; }
            set
            {
                _female = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value;
                OnPropertyChanged();
            }
        }


        private TimeSpan _startHour;
        public TimeSpan StartHour
        {
            get { return _startHour; }
            set { _startHour = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _endHout;
        public TimeSpan EndHour
        {
            get { return _endHout; }
            set { _endHout = value;
                OnPropertyChanged();
            }
        }
#endregion

        private int _categoryID;
        public Command SelectedCommand { get; set; }

        public ICommand SearchWithFiltersCommand { get; set; }
        public DoctorsViewModel(Page context, int categoryID) : base(context)
        {
            _categoryID = categoryID;
            Doctors = new ObservableCollection<DoctorModel>();
            SelectedCommand = new Command(async (param) => await Navigation.PushAsync(new Views.Home.DoctorView((DoctorModel)param, categoryID)));
            SearchWithFiltersCommand = new Command(async (param) => await OnSearchWithFilters());
            _=GetData();
            ButtonsView = CreateButtonsView();
            SelectedDate = DateTime.Now;
            EndHour = new DateTime(1, 1, 1, 23, 59, 59).TimeOfDay;
            StartHour = new DateTime(1, 1, 1, 0, 0, 0).TimeOfDay;
        }

        private async Task GetData()
        {
            IsBusy = true;
            try
            {
                //var response = await new ApiRepository().Doctors(_categoryID);
                var response = await new ApiRepository().DoctorsByCategoryAdvancedFilters(_categoryID);
                if (response.HasValidStatus())
                {
                    // Get doctors
                    response.Data.Items.ForEach(d => d.DoctorSelectedCommand = SelectedCommand);
                    Doctors = new ObservableCollection<DoctorModel>(response.Data.Items);                    
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

            DoctorsCount = Doctors.Count;
        }
   
        private View CreateButtonsView()
        {
            var imageSize = 30;
            var refreshIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize,
                HeightRequest = imageSize,
                Aspect = Aspect.AspectFill,
                Source = Icons.Icon_Refresh
            };

            var tgrRefresh = new TapGestureRecognizer();
            tgrRefresh.Tapped+= async (s, e) => await GetData();
            refreshIcon.GestureRecognizers.Add(tgrRefresh);

            var filtersIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize,
                HeightRequest = imageSize,
                Aspect = Aspect.AspectFill,
                Source = Icons.Icon_Filters
            };

            var tgrFilters = new TapGestureRecognizer();
            tgrFilters.Tapped+=async (s, e) => await DisplayFilters();
            filtersIcon.GestureRecognizers.Add(tgrFilters);

            return new StackLayout()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Spacing = Layouts.Margin/2,
                Children = { refreshIcon, filtersIcon }
            };
        }

        private async Task DisplayFilters()
        {
            var filtersPage = new FiltersPopupView();
            filtersPage.BindingContext = this;
            await Navigation.PushPopupAsync(filtersPage);
        }

        private async Task OnSearchWithFilters()
        {
            string genderFilter = string.Empty;
            if (Male & Female)
                genderFilter = string.Empty;
            else if (Male)
                genderFilter = "male";
            else if(Female)
                genderFilter = "female";

            IsBusy = true;
            try
            {
                //var response = await new ApiRepository().Doctors(_categoryID);
                var response = await new ApiRepository().DoctorsByCategoryAdvancedFilters(_categoryID, SelectedDate, SelectedDate, StartHour, EndHour, Name, genderFilter, 0);
                if (response.HasValidStatus())
                {
                    // Get doctors
                    response.Data.Items.ForEach(d => d.DoctorSelectedCommand = SelectedCommand);
                    Doctors = new ObservableCollection<DoctorModel>(response.Data.Items);
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

            DoctorsCount = Doctors.Count;

            await Navigation.PopAllPopupAsync();
        }
    }
}
