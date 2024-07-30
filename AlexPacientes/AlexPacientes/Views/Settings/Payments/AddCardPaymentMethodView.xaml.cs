using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings.Payments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCardPaymentMethodView : BaseContentPage
    {
        List<int> years = new List<int>();
        List<int> months = new List<int>();
        public AddCardPaymentMethodView()
        {
            InitializeComponent();
            initialize();
        }

        void initialize()
        {
            int currentYear = DateTime.Now.Year;
            for (int x = 0; x < 12; x++)//to include the next year car
            {
                years.Add(currentYear + x);
                months.Add(x + 1);
            }
            YearPicker.ItemsSource = years;
            MonthPicker.ItemsSource = months;
        }
    }
}