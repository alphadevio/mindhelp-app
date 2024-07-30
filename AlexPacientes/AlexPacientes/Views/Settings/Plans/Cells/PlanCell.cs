using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Settings.Plans.Cells
{
    public class PlanCell : ViewCell
    {
        public PlanCell()
        {
            var name = new LabelDarkPrimary()
            {
                FontSize = Fonts.ExtraLargeSize,
                FontAttributes = FontAttributes.Bold,
            };
            var popular = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                FontSize = Fonts.MediumSize,
                FontAttributes = FontAttributes.Bold,
                Text = Labels.Labels.MostPopular,
                TextColor = Color.White,
                BackgroundColor = Colors.PrimaryColor, 
                Padding = 3
            };

            var active = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                FontSize = Fonts.MediumSize,
                FontAttributes = FontAttributes.Bold,
                Text = Labels.Labels.Active,
                TextColor = Color.White,
                BackgroundColor = Colors.PrimaryColor,
                Padding = 3
            };

            var description = new LabelDarkPrimary()
            {
                FontSize = Fonts.MediumSize,
            };
            var amount = new LabelDarkPrimary()
            {
                FontSize = Fonts.ExtraLargeSize,
            };

            var grid = new Grid
            {
                RowSpacing = Layouts.Margin / 2,
                ColumnSpacing = 4,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            grid.Children.Add(name, 0, 0);
            grid.Children.Add(popular, 1, 0);
            grid.Children.Add(description, 0, 2, 1, 2);
            grid.Children.Add(amount, 0, 2, 2, 3);
            grid.Children.Add(active, 1, 2, 2, 3);

            var frame = new Frame()
            {
                HasShadow = false,
                CornerRadius = Layouts.CornerRadius,
                BorderColor = Colors.PrimaryColor,
                IsClippedToBounds = true,
                Margin = Layouts.Margin,
                Padding = new Thickness(Layouts.Margin, Layouts.Margin / 2),
                Content = grid
            };

            name.SetBinding(Label.TextProperty, "Name");
            description.SetBinding(Label.TextProperty, "Description");
            amount.SetBinding(Label.TextProperty, ".", converter: new Converters.CurrencyAmountConverter() { AmountProperty = "Amount", CurrencyProperty = "Currency" });
            popular.SetBinding(Label.IsVisibleProperty, "IsPreferred");
            active.SetBinding(Label.IsVisibleProperty, "IsActive");

            View = frame;
            
        }
    }
}
