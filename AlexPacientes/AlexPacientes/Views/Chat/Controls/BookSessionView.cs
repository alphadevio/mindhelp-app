using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Svg.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Views.Chat.Controls
{
    public class BookSessionView : PopupPage
    {
        public Command BookCommand { get { return (Command)GetValue(BookCommandProperty); } set { SetValue(BookCommandProperty, value); } }
        public static readonly BindableProperty BookCommandProperty = BindableProperty.Create(
            nameof(BookCommand), typeof(Command), typeof(BookSessionView));

        public BookSessionView()
        {
            #region Views
            var logo = new SvgCachedImage()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 80,
                HeightRequest = 80,
                Source = Icons.LogoGreen
            };
            var newLine = Environment.NewLine + Environment.NewLine;
            var label1 = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FormattedText = new FormattedString()
                {
                    Spans =
                    {
                        new Span { Text = Labels.Labels.RatingSessionLabel_01, FontSize = Fonts.LargeSize },
                        new Span { Text = newLine, FontSize = Layouts.Margin },
                        new Span { Text = Labels.Labels.BookSession_01 }
                    }
                }
            };
            var book = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = Labels.Labels.BookAgain,
                TextColor = Colors.PrimaryColor,
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(() => RaiseBook()) } }
            };
            var divider = new Divider()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Color = Color.FromHex("#707070")
            };
            #endregion

            #region Grid Book
            var bookGrid = new Grid()
            {
                RowSpacing = Layouts.Margin,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };
            bookGrid.Children.Add(logo, 0, 0);
            bookGrid.Children.Add(label1, 0, 1);
            bookGrid.Children.Add(divider, 0, 2);
            bookGrid.Children.Add(book, 0, 3);
            
            var bookFrame = new Frame()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.PopupBackground,
                CornerRadius = Layouts.FrameCornerRadius,
                HasShadow = false,
                Padding = Layouts.Padding,
                Margin = new Thickness(Layouts.Margin * 1.5, 0),
                Content = bookGrid
            };
            #endregion

            #region Cancel
            var cancel = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = Labels.Labels.Cancel,
                TextColor = Colors.PrimaryColor
            };
            var cancelFrame = new Frame()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.PopupBackground,
                CornerRadius = Layouts.FrameCornerRadius,
                HasShadow = false,
                Padding = Layouts.Padding,
                Margin = new Thickness(Layouts.Margin * 1.5, 0),
                Content = cancel,
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(async () => await Hide()) } }
            };
            #endregion

            #region Main StackLayout
            var stack = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = Layouts.Margin,
                Children = { bookFrame, cancelFrame }
            };
            #endregion

            Content = stack;
        }

        public async Task Show()
        {
            await Navigation.PushPopupAsync(this);
        }

        public async Task Hide()
        {
            await Navigation.PopPopupAsync();
        }

        private void RaiseBook()
        {
            BookCommand?.Execute(null);
        }
    }
}
