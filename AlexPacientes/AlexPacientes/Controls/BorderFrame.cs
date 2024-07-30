using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class BorderFrame : Frame
    {
        public new View Content { get { return (View)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }
        public static readonly new BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(View), typeof(BorderFrame), propertyChanged: (s, o, n) =>
            {
                var sender = s as BorderFrame;
                if (sender == null || sender._contentView == null || n == null) return;
                sender._contentView.Content = (View)n;
            });

        ContentView _contentView;

        public BorderFrame()
        {
            if(Device.RuntimePlatform == Device.iOS)
            {
                HasShadow = false;
            }
            _contentView = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Colors.PrimaryBackgroundColor,
                Padding = new Thickness(Layouts.Margin / 2, Layouts.Margin / 2)
            };
            var border = new BoxView()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 3,
                Color = Colors.PrimaryColor
            };
            var stack = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Children = { border, _contentView }
            };

            Padding = 0;
            CornerRadius = Layouts.FrameCornerRadius;
            base.Content = stack;
        }
    }
}
