using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class RateLabel : StackLayout
    {
        public double Rate { get { return (double)GetValue(RateProperty); } set { SetValue(RateProperty, value); } }
        public static readonly BindableProperty RateProperty = BindableProperty.Create(
            nameof(Rate), typeof(double), typeof(RateLabel), -1.0, propertyChanged: (s, o, n) =>
            {
                var sender = s as RateLabel;
                if (sender == null || sender._label == null || (double)n < 0) return;
                sender._label.Text = string.Format("{0:F1}", n);
            });

        private LabelDarkPrimary _label;
        private SvgCachedImage _icon;

        public RateLabel()
        {
            _icon = new SvgCachedImage()
            {
                WidthRequest = 16,
                HeightRequest = 16,
                Source = Styles.Icons.Star
            };
            _label = new LabelDarkPrimary()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Styles.Colors.TextDarkSecondary
            };

            Orientation = StackOrientation.Horizontal;
            Spacing = 3;
            Children.Add(_icon);
            Children.Add(_label);
        }
    }
}
