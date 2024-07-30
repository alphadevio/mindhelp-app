using AlexPacientes.Controls;
using AlexPacientes.Styles;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Views.Chat.Controls
{
    public class RatingSessionView : PopupPage
    {
        public Command SubmitReviewCommand { get { return (Command)GetValue(SubmitReviewCommandProperty); } set { SetValue(SubmitReviewCommandProperty, value); } }
        public static readonly BindableProperty SubmitReviewCommandProperty = BindableProperty.Create(
            nameof(SubmitReviewCommand), typeof(Command), typeof(RatingSessionView));



        private CustomEntry _commentary;
        private RateComponent ratingview;

        public RatingSessionView()
        {
            #region Views
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
                        new Span { Text = Labels.Labels.RatingSessionLabel_02 },
                        new Span { Text = newLine, FontSize = Layouts.Margin },
                        new Span { Text = Labels.Labels.RatingSessionLabel_03 }
                    }
                }
            };
            var label2 = new LabelDarkPrimary()
            {
                FontSize = Fonts.SmallSize,
                FormattedText = new FormattedString()
                {
                    Spans =
                    {
                        new Span { Text = Labels.Labels.RatingSessionLabel_04 },
                    }
                }
            };

            _commentary = new CustomEntry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor=Color.Transparent,
                Margin = new Thickness(Layouts.Margin * 2, Layouts.Margin),
                BorderColor = Color.Transparent,
                Placeholder= Labels.Labels.RatingSessionLabel_05,
                PlaceholderColor= Colors.TextDarkHint,
                
            };

            var submit = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions=LayoutOptions.Center,
                Text = Labels.Labels.SubmitReview,
                TextColor = Colors.PrimaryColor,
                GestureRecognizers = { new TapGestureRecognizer { Command = new Command(() => RaiseSubmit())} }
            };
            ratingview = new RateComponent()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = Math.Min(Layouts.DisplayXSizePX * 0.5, 230),
                Count = 5,
                BorderOffColor = Colors.PrimaryColor,
                BorderOnColor = Colors.PrimaryColor,
                FillColor = Colors.PrimaryColor
            };
            var divider = new Divider()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Color = Color.FromHex("#707070")
            };
            #endregion

            #region Grid Review
            var reviewGrid = new Grid()
            {
                RowSpacing = 0,
                Padding=0,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };
            label1.Margin = new Thickness(Layouts.Margin*2, Layouts.Margin * 2, Layouts.Margin * 2,0 );
            ratingview.Margin = new Thickness(Layouts.Margin*2, Layouts.Margin * 1.4, Layouts.Margin * 2,0);
            label2.Margin = new Thickness(Layouts.Margin*2, Layouts.Margin * 1.4, Layouts.Margin * 2,0);
            submit.Margin = new Thickness(Layouts.Margin*2, Layouts.Margin * 1.4, Layouts.Margin * 2, Layouts.Margin * 1.4);
            reviewGrid.Children.Add(label1, 0, 0);
            reviewGrid.Children.Add(ratingview, 0, 1);
            reviewGrid.Children.Add(label2, 0, 2);
            reviewGrid.Children.Add(_commentary, 0, 3);
            reviewGrid.Children.Add(divider, 0, 4);
            reviewGrid.Children.Add(submit, 0, 5);

            var reviewFrame = new Frame()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.PopupBackground,
                CornerRadius = Layouts.FrameCornerRadius,
                HasShadow = false,
                Padding = 0,
                Margin = new Thickness(Layouts.Margin * 1.5, 0),
                Content = reviewGrid
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
                Children = { reviewFrame, cancelFrame }
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

        private void RaiseSubmit()
        {
            SubmitReviewCommand?.Execute(new List<Tuple<string, object>>() {
                new Tuple<string, object>("Rating", ratingview.Value),
                new Tuple<string, object>("Commentary", _commentary.Text)
            });
        }
    }
}
