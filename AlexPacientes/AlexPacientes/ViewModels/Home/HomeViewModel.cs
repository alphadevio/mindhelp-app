using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categories;
        public ObservableCollection<CategoryModel> Categories
        {
            get { return _categories; }
            set { _categories = value;
                OnPropertyChanged();
            }
        }

        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value;
                OnPropertyChanged();
                OnSelectedCategory(_selectedCategory);
            }
        }

        public HomeViewModel(Page context) : base(context)
        {
            GetData();
        }

        private async void GetData()
        {
            IsBusy = true;
            try
            {
                var response = await new ApiRepository().Categories();
                if (response.HasValidStatus())
                {
                    // Decrypt content
                    Categories = new ObservableCollection<CategoryModel>(response.Data.Items);
                }
                else
                {
                    await DisplayError(response.StatusText);
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

        private async void OnSelectedCategory(CategoryModel category)
        {
            if (category == null) return;
            SelectedCategory = null;
            await Navigation.PushAsync(new Views.Home.DoctorsView(category.ID));
        }
    }
}
