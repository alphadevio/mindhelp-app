using AlexPacientes.Styles;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    class LoadingView : Frame
    {
        public bool IsRunning { get { return (bool)GetValue(IsRunningProperty); } set { SetValue(IsRunningProperty, value); } }
        public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(
            nameof(IsRunning),
            typeof(bool),
            typeof(LoadingView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = (s as LoadingView);
                if (sender == null) return;

                if ((bool)n)
                {
                    sender.IsVisible = true;
                    var animation = new Animation(p => sender.icon.Rotation = p * 360, 0, 1);
                    animation.Commit(sender, nameof(LoadingView), 16, 750, Easing.SinInOut, repeat: () => true);
                    sender._indicator.IsRunning = true;
                }
                else
                {
                    sender.IsVisible = false;
                    sender.AbortAnimation(nameof(LoadingView));
                    sender._indicator.IsRunning = false;
                }
            });

        CachedImage icon { get; set; }
        ActivityIndicator _indicator;
        public LoadingView()
        {
            int loaderSize = 100;
            float iconMultiplier = .75f;
            CornerRadius = 8;
            VerticalOptions = LayoutOptions.Center;
            Padding = 0;
            HorizontalOptions = LayoutOptions.Center;
            BackgroundColor = Colors.CommonCellBackground;
            WidthRequest = loaderSize;
            HeightRequest = loaderSize;
            MinimumWidthRequest = loaderSize;
            MinimumHeightRequest = loaderSize;

            HasShadow = Device.RuntimePlatform == Device.iOS ? false : true;
            BorderColor = Colors.Divider;

            icon = new CachedImage()
            {
                WidthRequest = loaderSize * iconMultiplier,
                HeightRequest = loaderSize * iconMultiplier,
                MinimumHeightRequest = loaderSize * iconMultiplier,
                MinimumWidthRequest = loaderSize * iconMultiplier,
                //Source = Icons.Gato
            };

            _indicator = new ActivityIndicator
            {
                WidthRequest = 50,
                HeightRequest = 50,
                IsRunning = false,
                Color=Color.Gray
            };

            Content = _indicator;
            IsVisible = false;
        }
    }
}
