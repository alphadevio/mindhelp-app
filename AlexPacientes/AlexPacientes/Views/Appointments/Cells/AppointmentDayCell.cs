using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Appointments.Cells
{
    public class AppointmentDayCell : Frame
    {
        public bool IsSelected { get { return (bool)GetValue(IsSelectedProperty); } set { SetValue(IsSelectedProperty, value); } }
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected), typeof(bool), typeof(AppointmentDayCell), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as AppointmentDayCell;
                if (sender == null) return;
                sender.BorderColor = (bool)n ? Colors.PrimaryColor : Colors.Divider;
            });

        public AppointmentDayCell()
        {
            var day = new LabelDarkPrimary()
            { 
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var appointments = new LabelDarkSecondary()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var container = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Spacing = Layouts.Margin / 2,
                Children = { day, appointments }
            };

            day.SetBinding(Label.TextProperty, "Day", 
                converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.NameDayMonth, InputType = Converters.DatetimeToHumanTextInputType.UnixTimeMillisecond });
            appointments.SetBinding(Label.TextProperty, "TotalTimeSlots", stringFormat: Labels.Labels.AppoimentsAvailableFormat);
            this.SetBinding(IsSelectedProperty, "IsSelected");

            MinimumWidthRequest = 165;
            Padding = Layouts.Margin;
            BackgroundColor = Colors.PrimaryBackgroundColor;
            BorderColor = IsSelected ? Colors.PrimaryColor : Colors.Divider;
            CornerRadius = Layouts.FrameCornerRadius/2;
            HasShadow = false;
            Content = container;
        }
    }
}
