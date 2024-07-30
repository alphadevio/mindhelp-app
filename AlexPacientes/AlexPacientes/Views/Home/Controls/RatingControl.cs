using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Home.Controls
{
    public class RatingControl : Grid
    {
        public RatingControl(string image, string name, string review, double rate, DateTime date)
        {
            #region Views
            var imageProfile = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = 32,
                HeightRequest = 32,
                Aspect = Aspect.AspectFill,
                Margin = new Thickness(Layouts.Margin, 0, 0, 0),
                Source = image,
                Transformations = { new FFImageLoading.Transformations.CircleTransformation() }
            };
            var labelName = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                Text = name
            };
            var labelReview = new LabelDarkSecondary() 
            {
                Text = review
            };
            var labelRate = new RateLabel()
            {
                VerticalOptions = LayoutOptions.Center,
                Rate = rate
            };
            var labelDate = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.TextDarkSecondary,
                Margin = new Thickness(0, 0, Layouts.Margin, 0),
                Text = DateTimeAgo(date)
            };
            var divider = new Divider()
            {
                Margin = new Thickness(0, Layouts.Margin * 2, 0, 0)
            };
            #endregion

            #region Grid
            Padding = new Thickness(0, Layouts.Margin, 0, 0);
            RowSpacing = 0;
            ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Children.Add(imageProfile, 0, 0);
            Children.Add(labelName, 1, 0);
            Children.Add(labelRate, 2, 0);
            Children.Add(labelDate, 3, 0);
            Children.Add(labelReview, 1, 3, 1, 2);
            Children.Add(divider, 0, 4, 2, 3);
            #endregion
        }

        private string DateTimeAgo(DateTime date)
        {
            TimeSpan span = DateTime.Now.Subtract(date);
            int differenceDays = (int)span.TotalDays;

            if (differenceDays <= 0)
                return Labels.Labels.Now;
            else if (differenceDays < 7)
                return string.Format(Labels.Labels.DaysAgoFormat, differenceDays);
            else if (differenceDays < 31)
                return string.Format(Labels.Labels.WeeksAgoFormat, Math.Ceiling((double)differenceDays / 7));
            else if (differenceDays < 365)
                return string.Format(Labels.Labels.MonthAgoFormat, Math.Ceiling((double)differenceDays / 31));
            else
                return string.Format(Labels.Labels.YearsAgoFormat, Math.Ceiling((double)differenceDays / 365));
        }
    }
}
