using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Cells
{
    public class PastBookingCell : BookingCell
    {
        public PastBookingCell()
        {
            #region Button
            var button = new Button()
            {
                HeightRequest = 50,
                Text = Labels.Labels.BookAgain,
                Margin = new Thickness(0, Layouts.Margin)
            };

            button.Clicked += (s, e) => App.ScrollToSpecificTabbedPage(0);
            SetSubview(button);
            #endregion

            #region Bindings
            image.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, "Image");
            name.SetBinding(Label.TextProperty, "Name");
            area.SetBinding(Label.TextProperty, "Area");
            status.SetBinding(Label.TextProperty, "Status", converter: new BookingStatusConverter());
            date.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.MonthDay });
            time.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.Hour });
            button.SetBinding(Button.CommandProperty, "BookAgain");
            #endregion
        }
    }
}
