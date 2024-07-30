using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Booking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingView : BaseContentPage
    {
        public BookingView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Booking.BookingViewModel(this);
        }
    }
}