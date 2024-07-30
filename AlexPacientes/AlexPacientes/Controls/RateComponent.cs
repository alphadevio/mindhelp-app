using SkiaRate;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    //Need to be inside a view because a bug with layout options of the current control
    public class RateComponent:ContentView
    {
        public int Count { get { return (int)GetValue(CountProperty); } set { SetValue(CountProperty, value); } }
        public static readonly BindableProperty CountProperty = BindableProperty.Create(
            nameof(Count),
            typeof(int),
            typeof(RateComponent),
            5,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
             {
                 var sender = s as RateComponent;
                 if (sender == null) return;
                 sender._rateComponent.Count = (int)n;
             });

        public double Value { get { return (double)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(double),
            typeof(RateComponent),
            0.0,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as RateComponent;
                if (sender == null || o == n || sender._rateComponent.Value == (double)n) return;
                sender._rateComponent.Value = (double)n;
            });

        public Color FillColor { get { return (Color)GetValue(FillColorProperty); } set { SetValue(FillColorProperty, value); } }
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor),
            typeof(Color),
            typeof(RateComponent),
            Color.Yellow,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as RateComponent;
                if (sender == null) return;
                sender._rateComponent.ColorOn = (Color)n;
            });

        public Color BorderOnColor { get { return (Color)GetValue(BorderOnColorProperty); } set { SetValue(BorderOnColorProperty, value); } }
        public static readonly BindableProperty BorderOnColorProperty = BindableProperty.Create(
            nameof(BorderOnColor),
            typeof(Color),
            typeof(RateComponent),
            Color.Transparent,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as RateComponent;
                if (sender == null) return;
                sender._rateComponent.OutlineOnColor = (Color)n;
            });

        public Color BorderOffColor { get { return (Color)GetValue(BorderOffColorProperty); } set { SetValue(BorderOffColorProperty, value); } }
        public static readonly BindableProperty BorderOffColorProperty = BindableProperty.Create(
            nameof(BorderOffColor),
            typeof(Color),
            typeof(RateComponent),
            Color.Black,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as RateComponent;
                if (sender == null) return;
                sender._rateComponent.OutlineOffColor = (Color)n;
            });

        private RatingView _rateComponent;
        
        public RateComponent()
        {
            _rateComponent = new RatingView()
            {
                Count = Count,
                ColorOn = FillColor,
                OutlineOffColor = BorderOffColor,
                OutlineOnColor = BorderOnColor,
                Margin = 0,
                RatingType = RatingType.Half,
                StrokeWidth = .5f,
                Value = Value
            };
            _rateComponent.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == RatingView.ValueProperty.PropertyName)
                {
                    Value = (s as RatingView).Value;
                }
            };

            Content = _rateComponent;
        }
    }
}
