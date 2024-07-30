using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using System.ComponentModel;
using AlexPacientes.Controls;
using AlexPacientes.Droid.CustomRenders;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerCR))]
namespace AlexPacientes.Droid.CustomRenders
{
    class CustomPickerCR : PickerRenderer
    {
        public Color BorderColor { get; set; }
        public Color BackgroundColor { get; set; }
        public float BorderRadius { get; set; }
        public int BorderStroke { get; set; }
        public float WidthElement { get; set; } = 0;
        public float HeightElement { get; set; } = 0;
        public CustomPickerCR() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var customPicker = e.NewElement as CustomPicker;
                var ancho = customPicker.Bounds;
                this.BorderColor = customPicker.BorderColor;
                this.BackgroundColor = customPicker.BackgroundColor;
                this.BorderRadius = customPicker.BorderRadius;
                this.BorderStroke = customPicker.BorderStroke;
                this.WidthElement = customPicker.WidthElement;
                this.HeightElement = customPicker.HeightElement;
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(this.BackgroundColor.ToAndroid());//Color del fondo
                gd.SetCornerRadius((this.BorderRadius / 100) * (WidthElement < HeightElement ? WidthElement * 2 : HeightElement * 2));//Border radius
                gd.SetStroke(this.BorderStroke, this.BorderColor.ToAndroid());//El grosor
                Control.SetHighlightColor(customPicker.TextColor.ToAndroid());
                Control.SetTextColor(customPicker.TextColor.ToAndroid());
                Control.SetHintTextColor(customPicker.TextColor.ToAndroid());
                Control.SetBackground(gd);
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                var customPicker = sender as CustomPicker;
                var ancho = customPicker.Bounds;
                this.BorderColor = customPicker.BorderColor;
                this.BackgroundColor = customPicker.BackgroundColor;
                this.BorderRadius = customPicker.BorderRadius;
                this.BorderStroke = customPicker.BorderStroke;
                this.WidthElement = customPicker.WidthElement;
                this.HeightElement = customPicker.HeightElement;
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(this.BackgroundColor.ToAndroid());//Color del fondo
                gd.SetCornerRadius((this.BorderRadius / 100) * (WidthElement < HeightElement ? WidthElement * 2 : HeightElement * 2));//Border radius
                gd.SetStroke(this.BorderStroke, this.BorderColor.ToAndroid());//El grosor
                Control.SetHighlightColor(customPicker.TextColor.ToAndroid());
                Control.SetTextColor(customPicker.TextColor.ToAndroid());
                Control.SetHintTextColor(customPicker.TextColor.ToAndroid());
                Control.SetBackground(gd);
            }
        }
    }
}