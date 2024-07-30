using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Chat.Cells
{
    public class PacientChatCell:ViewCell
    {
        public PacientChatCell()
        {
            double imageSize = Layouts.DisplayXSizePX * .16;
            double bubbleMargin = Layouts.DisplayXSizePX * .1;
            //CachedImage userImage = new CachedImage()
            //{
            //    HorizontalOptions = LayoutOptions.Start,
            //    Margin = 0,
            //    VerticalOptions = LayoutOptions.Center,
            //    WidthRequest = imageSize,
            //    HeightRequest = imageSize,
            //    MinimumHeightRequest = imageSize,
            //    MinimumWidthRequest = imageSize,
            //    DownsampleToViewSize = true,
            //    Aspect = Aspect.AspectFill,
            //    Transformations = new List<FFImageLoading.Work.ITransformation>()
            //    {
            //        new CircleTransformation(1,"#000000")
            //    }
            //};

            var message = new LabelDarkSecondary() { HorizontalOptions = LayoutOptions.EndAndExpand };
            Frame bubble = new Frame()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Margin = new Thickness(bubbleMargin, 0, 0, 0),
                CornerRadius = Layouts.CornerRadius*3,
                Padding = Layouts.Margin,
                BackgroundColor = Colors.PacientBubble,
                Content = message,
                HasShadow = Device.RuntimePlatform == Device.iOS ? false : true
            };
            Grid mainGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                ColumnSpacing = Layouts.Margin,
                Margin = new Thickness(Layouts.Margin / 2, Layouts.Margin),
                ColumnDefinitions =
                {
                    new ColumnDefinition(){Width=new GridLength(1,GridUnitType.Star)},
                    //new ColumnDefinition(){Width=imageSize}
                }
            };
            mainGrid.Children.Add(bubble, 0, 1, 0, 1);
            //mainGrid.Children.Add(userImage, 1, 2, 0, 1);

            View = mainGrid;

            message.SetBinding(Label.TextProperty, "Message");
            //userImage.SetBinding(CachedImage.SourceProperty, "UserImage");
            //userImage.SetBinding(CachedImage.IsVisibleProperty, "NeedDisplayPicture");

        }
    }
}
