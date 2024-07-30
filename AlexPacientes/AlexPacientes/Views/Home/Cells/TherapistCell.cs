using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Home.Cells
{
    public class TherapistCell : ViewCell
    {
        public TherapistCell()
        {
            #region Views
            var imageSize = 50;
            var image = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize,
                HeightRequest = imageSize,
                Aspect = Aspect.AspectFill,
                Transformations = { new FFImageLoading.Transformations.CircleTransformation() }
            };
            var area = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.TextDarkSecondary,
                Margin = new Thickness(imageSize + Layouts.Margin, 0)
            };
            var name = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = Fonts.LargeSize,
                Margin = new Thickness(imageSize + Layouts.Margin, 0)
            };
            var rate = new RateLabel()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            var feedbacks = new LabelDarkSecondary();
            var yearsExp = new LabelDarkSecondary()
            {
                HorizontalOptions = LayoutOptions.Center
            };
            var yearsOld = new LabelDarkSecondary()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness() { Left = 50 + Layouts.Margin },
            };
            var availableDates = new LabelDarkSecondary()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var resume = new LabelDarkPrimary()
            {
                HorizontalOptions = LayoutOptions.Fill,
                MaxLines = 2,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            var tapProfileGesture = new TapGestureRecognizer {  };
            var tapResumeGesture = new TapGestureRecognizer { Command = new Command(async (param) => await App.NavigateFromTabbedPage(new Views.Home.ResumeView((string)param))) };
            var tapScheduleGesture = new TapGestureRecognizer { Command = new Command(async (param) => await App.NavigateFromTabbedPage(new Views.Home.ResumeView((string)param))) };


            var profileIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize/2,
                HeightRequest = imageSize/2,
                Aspect = Aspect.AspectFill,
                Source = Icons.Icon_Profile,
                GestureRecognizers = { tapProfileGesture }
            };
            var resumeIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize/2,
                HeightRequest = imageSize/2,
                Aspect = Aspect.AspectFill,
                Source = Icons.Icon_List,
                GestureRecognizers = { tapResumeGesture }
            };
            var appointmentIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = imageSize/2,
                HeightRequest = imageSize/2,
                Aspect = Aspect.AspectFill,
                Source = Icons.Icon_Calendar,
                //GestureRecognizers = { tapScheduleGesture }
                GestureRecognizers = { tapProfileGesture }
            };


            var button = new Button()
            {
                HeightRequest = 50,
                Text = Labels.Labels.BookAnAppointment,
                Command = new Command(async (doctor) => await App.NavigateFromTabbedPage(new Views.Appointments.BookAppointmentView((Models.NewApiModels.Responses.DoctorModel)doctor)))
            };
            #endregion

            #region Grid
            var grid = new Grid()
            {
                ColumnSpacing = 0,
                RowSpacing = Layouts.Margin / 2,
                Padding = Layouts.Margin,
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = GridLength.Star },
                    new ColumnDefinition() { Width = GridLength.Star },
                    new ColumnDefinition() { Width = GridLength.Star }
                },
                RowDefinitions =
                {
                    new RowDefinition() { Height = GridLength.Auto },
                    new RowDefinition() { Height = GridLength.Auto },
                    new RowDefinition() { Height = GridLength.Auto },
                    new RowDefinition() { Height = GridLength.Auto },
                    new RowDefinition() { Height = GridLength.Auto },
                    new RowDefinition() { Height = GridLength.Auto }
                }
            };
            grid.Children.Add(image, 0, 1, 0, 2);
            grid.Children.Add(area, 0, 3, 0, 1);
            grid.Children.Add(name, 0, 3, 1, 2);
            grid.Children.Add(rate, 2, 0);
            grid.Children.Add(feedbacks, 0, 2);
            grid.Children.Add(yearsExp, 1, 2);
            grid.Children.Add(new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Children ={ yearsOld, availableDates }
            },0 , 3, 2, 3);
            grid.Children.Add(resume, 0, 3, 3, 4);
            grid.Children.Add(new StackLayout()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Margin = Layouts.Margin/2,
                Children = {profileIcon, resumeIcon, appointmentIcon }
            }, 1, 3, 4, 5);
            grid.Children.Add(button, 0, 3, 5, 6);
            #endregion

            #region Bindings
            image.SetBinding(CachedImage.SourceProperty, "ProfileImage");
            area.SetBinding(Label.TextProperty, "Services[0].Name");
            name.SetBinding(Label.TextProperty, "Name");
            rate.SetBinding(RateLabel.RateProperty, "Doctor.TotalStar");
            feedbacks.SetBinding(Label.TextProperty, "Doctor.TotalReviews", stringFormat: Labels.Labels.FeedbacksFormat);
            yearsExp.SetBinding(Label.TextProperty, "Doctor.WorkingSince", stringFormat: Labels.Labels.YearsExpFormat);
            yearsOld.SetBinding(Label.TextProperty, "Birthday", 
                converter: new Converters.DatetimeToHumanTextConverter() { Parameter = Converters.DatetimeToHumanTextStyle.Age, InputType = Converters.DatetimeToHumanTextInputType.UnixTimeMillisecond });
            availableDates.SetBinding(Label.TextProperty, "AvailabilityMonth", stringFormat: $@"En {DateTime.Now.ToString("MMMM")} {{0}} citas disponibles");
            resume.SetBinding(Label.TextProperty, "UserDescription");
            resumeIcon.SetBinding(CachedImage.IsVisibleProperty, "ResumeMedia.Url", converter: new Converters.StringEmptyBooleanConverter());
            tapResumeGesture.SetBinding(TapGestureRecognizer.CommandParameterProperty, "ResumeMedia.Url");
            tapProfileGesture.SetBinding(TapGestureRecognizer.CommandProperty, "DoctorSelectedCommand");
            tapProfileGesture.SetBinding(TapGestureRecognizer.CommandParameterProperty, ".");
            button.SetBinding(Button.CommandParameterProperty, ".");
            #endregion

            View = grid;
        }
    }
}