using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Controls
{
    public class CircleLine : SKCanvasView
    {
        public Color Color { get { return (Color)GetValue(ColorProperty); } set { SetValue(ColorProperty, value); } }
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(CircleLine), Color.Accent,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as CircleLine;
                if (sender == null) return;
                sender.InvalidateSurface();
            });

        public CircleLine()
        {
            Color = (Color)ColorProperty.DefaultValue;
            HorizontalOptions = LayoutOptions.Start;
            VerticalOptions = LayoutOptions.Start;
            PropertyChanged += CircleLinePropertyChanged;
        }

        private void CircleLinePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == WidthProperty.PropertyName || e.PropertyName == HeightProperty.PropertyName)
            {
                if ((sender as CircleLine).Width > 0 && (sender as CircleLine).Height > 0)
                    InvalidateSurface();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            if (Height < 0 || Width < 0) return;

            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            var width = info.Width;
            var height = info.Height;
            var radius = width / 2;

            canvas.Clear();

            using (var paint = new SKPaint())
            {
                paint.Color = Color.ToSKColor();
                paint.IsAntialias = true;
                paint.Style = SKPaintStyle.Fill;
                paint.StrokeWidth = radius;

                float x1 = radius;
                float y1 = radius;
                float x2 = radius;
                float y2 = height - radius;

                canvas.DrawCircle(x1, y1, radius, paint);
                canvas.DrawCircle(x2, y2, radius, paint);
                canvas.DrawLine(x1, y1, x2, y2, paint);
            }
        }
    }
}