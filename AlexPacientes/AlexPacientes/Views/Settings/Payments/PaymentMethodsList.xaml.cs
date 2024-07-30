using AlexPacientes.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Settings.Payments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentMethodsList : BaseContentPage
    {
        public PaymentMethodsList()
        {
            InitializeComponent();
            BindingContext = new PaymentMethodsViewModel(this);
        }


    }
}