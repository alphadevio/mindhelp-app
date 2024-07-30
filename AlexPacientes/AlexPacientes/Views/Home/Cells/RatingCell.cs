using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Home.Cells
{
    public class RatingCell : ViewCell
    {
        public RatingCell()
        {
            #region Views
            var image = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = 32,
                HeightRequest = 32,
                Aspect = Aspect.AspectFill,
                LoadingPlaceholder = Icons.MindPlaceholder,
                ErrorPlaceholder = Icons.MindPlaceholder,
                Margin = new Thickness(Layouts.Margin, 0, 0, 0),
                Transformations = { new FFImageLoading.Transformations.CircleTransformation() },
                IsVisible = false
            };
            var name = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            var review = new LabelDarkSecondary();
            var rate = new RateLabel()
            {
                VerticalOptions = LayoutOptions.Center
            };
            var date = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.TextDarkSecondary,
                Margin = new Thickness(0, 0, Layouts.Margin, 0),
            };
            var divider = new Divider()
            {
                Margin = new Thickness(0, Layouts.Margin * 2, 0, 0)
            };
            #endregion

            #region Grid
            var grid = new Grid()
            {
                Padding = new Thickness(0, Layouts.Margin, 0, 0),
                RowSpacing = 0,
                ColumnDefinitions =
                {
                    new ColumnDefinition(){ Width = GridLength.Auto },
                    new ColumnDefinition(){ Width = GridLength.Star },
                    new ColumnDefinition(){ Width = GridLength.Auto },
                    new ColumnDefinition(){ Width = GridLength.Auto }
                },
                RowDefinitions =
                {
                    new RowDefinition(){ Height = GridLength.Auto },
                    new RowDefinition(){ Height = GridLength.Auto },
                    new RowDefinition(){ Height = GridLength.Auto }
                }
            };
            grid.Children.Add(image, 0, 1, 0, 2);
            grid.Children.Add(name, 1, 0);
            grid.Children.Add(rate, 2, 0);
            grid.Children.Add(date, 3, 0);
            grid.Children.Add(review, 1, 3, 1, 2);
            grid.Children.Add(divider, 0, 4, 2, 3);
            #endregion

            #region Bindings
            image.SetBinding(CachedImage.SourceProperty, new Binding("ProfileImage") { TargetNullValue = Icons.MindPlaceholder } );
            name.SetBinding(Label.TextProperty, "Name");
            review.SetBinding(Label.TextProperty, "Message");
            rate.SetBinding(RateLabel.RateProperty, "Rate");
            date.SetBinding(Label.TextProperty, "Date", converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.Ago }); 
            #endregion

            View = grid;
        }
    }
}
