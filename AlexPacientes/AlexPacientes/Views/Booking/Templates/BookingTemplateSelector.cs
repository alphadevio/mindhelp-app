using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Templates
{
    public class BookingTemplateSelector : DataTemplateSelector
    {
        public static readonly DataTemplate CurrentBookingDataTemplate = new DataTemplate(typeof(CurrentBookingView));
        public static readonly DataTemplate UpcomingBookingDataTemplate = new DataTemplate(typeof(UpcomingBookingView));
        public static readonly DataTemplate PastBookingDataTemplate = new DataTemplate(typeof(PastBookingView));

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ViewModels.Booking.CurrentBookingViewModel)
                return CurrentBookingDataTemplate;
            else if (item is ViewModels.Booking.UpcomingBookingViewModel)
                return UpcomingBookingDataTemplate;
            else if (item is ViewModels.Booking.PastBookingViewModel)
                return PastBookingDataTemplate;
            else
                return null;
        }
    }
}
