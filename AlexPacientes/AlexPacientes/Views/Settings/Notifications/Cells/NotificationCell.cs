using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Settings.Notifications.Cells
{
    public class NotificationCell : ViewCell
    {
        public NotificationCell()
        {
            var title = new LabelDarkPrimary() 
            {
                Margin = new Thickness(0, 2, 0, 0)
            };
            var date = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.TextDarkSecondary,
            };
            var divider = new Divider()
            {
                Margin = new Thickness(0, Layouts.Margin / 2, 0, 0)
            };

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Margin = new Thickness(Layouts.Margin, Layouts.Margin / 2),
                Children = { date, title, divider }
            };

            title.SetBinding(Label.TextProperty, "Title");
            date.SetBinding(Label.TextProperty, "CreatedAt", converter: new Converters.ValueConverter<string>(x => ((DateTime)x).ToString("dd MMMM yyyy   HH:mm")));

            View = stack;
        }
    }
}
