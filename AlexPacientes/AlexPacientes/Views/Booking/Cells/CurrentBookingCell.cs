using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Cells
{
    public class CurrentBookingCell : BookingCell
    {
        public CurrentBookingCell() : base()
        {
            #region Views
            var consultationIncludes = new LabelDarkSecondary()
            {
                Text = Labels.Labels.ConsultationIncludes
            };
            var include1 = new LabelDarkPrimary()
            {
                Text = Labels.Labels.HourVideoSession,
                TextColor = Colors.TextDarkSecondary,
                VerticalOptions = LayoutOptions.Center
            };
            var include2 = new LabelDarkPrimary()
            {
                Text = Labels.Labels.UnlimitedTextMessages,
                TextColor = Colors.TextDarkSecondary,
                VerticalOptions = LayoutOptions.Center
            };
            var iconInclude1 = new SvgIconButton()
            {
                Source = Icons.Book,
                WidthRequest = Layouts.IconSizeCommon,
                HeightRequest = Layouts.IconSizeCommon,
                ColorToOverride = "#000000",
                OverrideColor = Colors.PrimaryColor
            };
            var iconInclude2 = new SvgIconButton()
            {
                Source = Icons.Book,
                WidthRequest = Layouts.IconSizeCommon,
                HeightRequest = Layouts.IconSizeCommon,
                ColorToOverride = "#000000",
                OverrideColor = Colors.PrimaryColor
            };
            var grid = new Grid()
            {
                RowSpacing = Layouts.Margin,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };
            grid.Children.Add(iconInclude1, 0, 0);
            grid.Children.Add(iconInclude2, 0, 1);
            grid.Children.Add(include1, 1, 0);
            grid.Children.Add(include2, 1, 1);
            var borderFrame = new BorderFrame()
            {
                Content = grid,
                Margin = new Thickness(2, 0)
            };
            var total = new LabelDarkSecondary()
            {
                VerticalOptions = LayoutOptions.Center,
                Text = Labels.Labels.TotalPrice
            };
            var totalValue = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Colors.PrimaryColor,
                Margin = new Thickness(0, Layouts.Margin)
            };
            var subdivider = new Divider()
            {
                Margin = 0
            };
            var cancellation = new LabelDarkSecondary()
            {
                Text = Labels.Labels.CancellationOptions,
                Margin = new Thickness(0, Layouts.Margin, 0, Layouts.Margin / 2)
            };
            var cancellation1 = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Start,
                Text = Labels.Labels.FreeCancellation,
                Margin = new Thickness(Layouts.Margin, 0, 0, 0),
            };
            var cancellation2 = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(Layouts.Margin, 0, 0, 0)
            };
            var line = new Controls.CircleLine()
            {
                WidthRequest = 5,
                HeightRequest = 80,
                Color = Colors.PrimaryColor
            };
            var buttonChat = new Button()
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 50,
                Text = Labels.Labels.Chat,
                Margin = new Thickness(0, Layouts.Margin, 0, 0),
            };
            var buttonCall = new Button()
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 50,
                Text = Labels.Labels.VideoCall,
                Margin = new Thickness(0, Layouts.Margin, 0, 0),
            };
            #endregion

            #region Subview Grid
            var subviewGrid = new Grid()
            {
                Margin = new Thickness(0, Layouts.Margin),
                ColumnSpacing = Layouts.Margin,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };
            subviewGrid.Children.Add(consultationIncludes, 0, 2, 0, 1);
            subviewGrid.Children.Add(borderFrame, 0, 2, 1, 2);
            subviewGrid.Children.Add(total, 0, 2);
            subviewGrid.Children.Add(totalValue, 1, 2);
            subviewGrid.Children.Add(subdivider, 0, 2, 3, 4);
            subviewGrid.Children.Add(cancellation, 0, 2, 4, 5);
            subviewGrid.Children.Add(line, 0, 1, 5, 7);
            subviewGrid.Children.Add(cancellation1, 0, 2, 5, 6);
            subviewGrid.Children.Add(cancellation2, 0, 2, 6, 7);
            subviewGrid.Children.Add(buttonChat, 0, 1, 7, 8);
            subviewGrid.Children.Add(buttonCall, 1, 2, 7, 8);
            #endregion

            SetSubview(subviewGrid);

            #region Bindings
            image.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, "Image");
            name.SetBinding(Label.TextProperty, "Name");
            area.SetBinding(Label.TextProperty, "Area");
            status.SetBinding(Label.TextProperty, "Status", converter: new BookingStatusConverter());
            date.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.MonthDay });
            time.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.Hour });
            totalValue.SetBinding(Label.TextProperty, "Total", stringFormat: Labels.Labels.MoneyWithCentsFormat);
            cancellation2.SetBinding(Label.TextProperty, "Date", converter: new DateCancellationConverter());
            buttonChat.SetBinding(Button.CommandProperty, "Chat");
            buttonChat.SetBinding(Button.CommandParameterProperty, "Appointment");
            buttonCall.SetBinding(Button.CommandProperty, "VideoCall");
            buttonCall.SetBinding(Button.CommandParameterProperty, "Appointment");
            #endregion

            this.BindingContextChanged += (s, e) =>
            {
                var binding = BindingContext as Models.AppModels.BookingModel;
                if (binding == null) return;
                Device.BeginInvokeOnMainThread(() =>
                {
                    buttonChat.IsEnabled = false;
                    buttonChat.Opacity = 0.5;
                    buttonCall.IsEnabled = false;
                    buttonCall.Opacity = 0.5;
                });
                Task.Run(async () =>
                {
                    var remainingTime = (binding.Date - DateTime.Now).TotalMilliseconds - 300000;
                    if (remainingTime > 0)
                        await Task.Delay(Convert.ToInt32(remainingTime));
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        buttonChat.IsEnabled = true;
                        buttonChat.Opacity = 1;
                        buttonCall.IsEnabled = true;
                        buttonCall.Opacity = 1;
                    });
                });
            };
        }

        private class DateCancellationConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var date = value as DateTime?;
                if (date == null || !date.HasValue) return null;
                // Get date string
                try
                {
                    return string.Format(Labels.Labels.NonRefundableCancellation, date.Value.ToString("MMMM dd"));
                }
                catch
                {
                    return null;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}