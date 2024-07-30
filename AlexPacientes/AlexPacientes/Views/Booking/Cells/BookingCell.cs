using AlexPacientes.Controls;
using AlexPacientes.Labels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Cells
{
    public class BookingCell : ViewCell
    {
        protected Grid grid;
        protected CachedImage image;
        protected LabelDarkPrimary name;
        protected LabelDarkPrimary area;
        protected LabelDarkPrimary status;
        protected LabelDarkPrimary date;
        protected LabelDarkPrimary time;
        protected BoxView statusBackground;
        protected Divider divider;
        

        public BookingCell()
        {
            #region Views
            image = new CachedImage()
            {
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,
                Aspect = Aspect.AspectFill,
                Transformations = { new FFImageLoading.Transformations.CircleTransformation() }
            };
            name = new LabelDarkPrimary()
            {
                FontSize = Fonts.LargeSize,
                MaxLines=1,
                LineBreakMode=LineBreakMode.TailTruncation
            };
            area = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Start,
                TextColor = Colors.PrimaryColor
            };
            status = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.PrimaryColor,
                Margin = new Thickness(8, 2)
            };
            statusBackground = new BoxView()
            {
                HeightRequest = status.FontSize + 6,
                CornerRadius = Device.iOS==Device.RuntimePlatform? 10: 20,
                Color = Color.FromHex("1E24AB73"),
                IsVisible = false
            };

            var consultation = new LabelDarkSecondary()
            {
                Text = Labels.Labels.ConsultationDateTime,
                Margin = new Thickness(0, Layouts.Margin, 0, Layouts.Margin / 2)
            };
            var iconDate = new SvgIconButton()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = Layouts.IconSizeCommon,
                HeightRequest = Layouts.IconSizeCommon,
                Source = Icons.Book,
                ColorToOverride = "#000000",
                OverrideColor = Colors.PrimaryColor
            };
            var iconTime = new SvgIconButton()
            {
                WidthRequest = Layouts.IconSizeCommon,
                HeightRequest = Layouts.IconSizeCommon,
                Source = Icons.Clock,
                ColorToOverride = "#000000",
                OverrideColor = Colors.PrimaryColor
            };
            date = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            time = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            var dividerDateTime = new DividerHorizontal()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 1)
            };
            var borderFrame = new BorderFrame()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(2, 0),
                Content = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { iconDate, date, dividerDateTime, iconTime, time }
                }
            };
            divider = new Divider()
            {
                Margin = 0
            };
            #endregion

            #region Grid
            grid = new Grid()
            {
                ColumnSpacing = Layouts.Margin / 2,
                RowSpacing = 2,
                Margin = Layouts.Margin,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };
            grid.Children.Add(image, 0, 1, 0, 2);
            grid.Children.Add(name, 1, 0);
            grid.Children.Add(area, 1, 1);
            grid.Children.Add(status, 2, 0);
            grid.Children.Add(statusBackground, 2, 0);
            grid.Children.Add(consultation, 0, 3, 2, 3);
            grid.Children.Add(borderFrame, 0, 3, 3, 4);
            grid.Children.Add(divider, 0, 3, 5, 6);
            grid.RaiseChild(status);
            #endregion

            #region Events
            status.BindingContextChanged += StatusLabelBindingContextChanged;
            #endregion

            View = grid;
        }

        public void SetSubview(View subview)
        {
            grid.Children.Add(subview, 0, 3, 4, 5);
        }

        private void StatusLabelBindingContextChanged(object sender, EventArgs e)
        {
            statusBackground.IsVisible = !string.IsNullOrEmpty((sender as Label).Text);
            var context = BindingContext as BookingModel;
            if (context == null) return;
            if (context.Status == BookingStatus.Expired)
            {
                statusBackground.Color = Colors.Danger;
                status.TextColor = Colors.Critical;
            }
        }
    }
}

public class BookingStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var status = (BookingStatus)value;
        if (status == BookingStatus.Requested)
            return Labels.Requested;
        else if (status == BookingStatus.Accepted)
            return Labels.AppointmentScheduled;
        else if (status == BookingStatus.PaymentRejected)
            return Labels.PaymentRejected;
        else if (status == BookingStatus.Completed)
            return Labels.AppointmentFinished;
        else if (status == BookingStatus.Rejected||status==BookingStatus.Canceled)
            return Labels.AppointmentCanceled;
        else if (status == BookingStatus.Expired)
            return Labels.Expired;
        else if (status == BookingStatus.PartnerConnected)
            return Labels.PartnerConnected;
        else
            return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}