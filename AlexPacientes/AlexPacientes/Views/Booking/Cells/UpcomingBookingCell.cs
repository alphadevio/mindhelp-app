using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Cells
{
    public class UpcomingBookingCell : BookingCell
    {
        private LabelDarkPrimary _viewMore;
        private LabelDarkPrimary _viewLess;
        private Grid _subviewGrid;

        public UpcomingBookingCell() : base()
        {
            #region Views
            _viewMore = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.End,
                Text = Labels.Labels.ViewMore,
                TextColor = Colors.PrimaryColor,
                Margin = new Thickness(0, Layouts.Margin),
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(() => ShowMore(true)) } }
            };
            _viewLess = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.End,
                Text = Labels.Labels.ViewLess,
                TextColor = Colors.PrimaryColor,
                Margin = new Thickness(0, Layouts.Margin / 2),
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(() => ShowMore(false)) } }
            };
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
                Content = grid
            };
            var total = new LabelDarkSecondary()
            {
                VerticalOptions = LayoutOptions.Start,
                Text = Labels.Labels.TotalPrice
            };
            var totalValue = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Colors.PrimaryColor,
            };
            var Reschedule = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End,
                VerticalTextAlignment=TextAlignment.End,
                TextColor = Colors.PrimaryColor,
                HeightRequest=Layouts.Margin*3,
                Margin = new Thickness(0, Layouts.Margin),
                Text=Labels.Labels.Reschedule,                
            };
            var tgrReschedule = new TapGestureRecognizer();
            Reschedule.GestureRecognizers.Add(tgrReschedule);
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
            #endregion

            #region Subview Grid
            _subviewGrid = new Grid()
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
            _subviewGrid.Children.Add(consultationIncludes, 0, 2, 0, 1);
            _subviewGrid.Children.Add(borderFrame, 0, 2, 1, 2);
            _subviewGrid.Children.Add(total, 0, 2);
            _subviewGrid.Children.Add(totalValue, 1, 2);
            _subviewGrid.Children.Add(Reschedule, 1, 2);
            _subviewGrid.Children.Add(subdivider, 0, 2, 3, 4);
            _subviewGrid.Children.Add(cancellation, 0, 2, 4, 5);
            _subviewGrid.Children.Add(line, 0, 1, 5, 7);
            _subviewGrid.Children.Add(cancellation1, 0, 2, 5, 6);
            _subviewGrid.Children.Add(cancellation2, 0, 2, 6, 7);
            _subviewGrid.Children.Add(_viewLess, 1, 2, 7, 8);
            #endregion

            var containerSubviewGrid = new Grid()
            {
                Children = { _viewMore, _subviewGrid }
            };
            SetSubview(containerSubviewGrid);
            ShowMore(false);

            #region Bindings
            image.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, "Image");
            name.SetBinding(Label.TextProperty, "Name");
            area.SetBinding(Label.TextProperty, "Area");
            status.SetBinding(Label.TextProperty, "Status", converter: new BookingStatusConverter());
            date.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.MonthDay });
            time.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.Hour });
            tgrReschedule.SetBinding(TapGestureRecognizer.CommandProperty, "Reschedule");
            
            totalValue.SetBinding(Label.TextProperty, "Total", stringFormat: Labels.Labels.MoneyWithCentsFormat);
            cancellation2.SetBinding(Label.TextProperty, "Date", converter: new DateCancellationConverter());
            #endregion
        }

        private void ShowMore(bool show)
        {
            _subviewGrid.IsVisible = show;
            _viewMore.IsVisible = !show;
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
