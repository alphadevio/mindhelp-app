using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Appointments.Cells
{
    public class AppointmentTimeCell : Frame
    {
        public bool IsSelected { get { return (bool)GetValue(IsSelectedProperty); } set { SetValue(IsSelectedProperty, value); } }
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected), typeof(bool), typeof(AppointmentTimeCell), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as AppointmentTimeCell;
                if (sender == null) return;
                sender.BackgroundColor = (bool)n ? Colors.PrimaryColor : Color.Transparent;
                sender._time.TextColor = (bool)n ? Color.White : Colors.TextDarkPrimary;
            });

        private LabelDarkPrimary _time;
        
        public AppointmentTimeCell()
        {
            _time = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = IsSelected ? Color.White : Colors.TextDarkPrimary,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment=TextAlignment.Center
            };
            _time.SetBinding(Label.TextProperty, "StartAtDateTime", converter: new AppointmentTimeConverter());
            this.SetBinding(IsSelectedProperty, "IsSelected");

            MinimumWidthRequest = 80;
            Padding = Layouts.Margin / 2;
            BackgroundColor = IsSelected ? Colors.PrimaryBackgroundColor : Color.Transparent;
            CornerRadius = Layouts.FrameCornerRadius/2;
            HasShadow = false;
            Content = _time;
        }

        private class AppointmentTimeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    var start = (DateTime)value;
                    //var time = new DateTime(2001, 1, 1, start, 0, 0, 0);
                    return start.ToString("hh:mm \n tt").Replace(" ","").Replace(".","").ToUpper();
                }
                catch
                {
                    return string.Empty;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
