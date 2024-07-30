using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Home.Controls
{
    public class TopicFrame : ContentView
    {
        public ImageSource Source { get { return (ImageSource)GetValue(SourceProperty); } set { SetValue(SourceProperty, value); } }
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source), typeof(ImageSource), typeof(TopicFrame), null, propertyChanged: (s, o, n) =>
            {
                var sender = s as TopicFrame;
                if (sender == null || sender._image == null) return;
                sender._image.Source = (ImageSource)n;
            });

        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text), typeof(string), typeof(TopicFrame), string.Empty, propertyChanged: (s, o, n) =>
            {
                var sender = s as TopicFrame;
                if (sender == null || sender._label == null) return;
                sender._label.Text = (string)n;
            });

        private CachedImage _image;
        private LabelDarkPrimary _label;
        private Grid _grid;

        public TopicFrame()
        {
            double width = (Layouts.DisplayXSizePX - (Layouts.Margin * 1.5)) / 2;
            //double width = Layouts.DisplayXSizePX / 2;
            //double height = width * 3 / 4;
            double height = Layouts.DisplayYSizePX * (Device.RuntimePlatform==Device.iOS? 0.165f:0.18f);
            VerticalOptions = LayoutOptions.CenterAndExpand;

            _image = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = width,
                HeightRequest = height,
                Aspect = Aspect.AspectFill,
                LoadingPlaceholder = Icons.MindPlaceholder,
                InputTransparent = true,
                Transformations = { new FFImageLoading.Transformations.RoundedTransformation(25, width, height) }
            };

            _label = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.End,
                FontSize = Fonts.LargeSize,
                TextColor = Color.White,
                Margin = Layouts.Margin,
                InputTransparent = true
            };

            _grid = new Grid()
            {
                InputTransparent = true,
                Margin = Layouts.Margin / 2,
                Children = { _image, _label }
            };

            Padding = 0;
            Content = _grid;
        }
    }
}
