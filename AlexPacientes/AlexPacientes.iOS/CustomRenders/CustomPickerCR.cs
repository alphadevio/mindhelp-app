using System;
using System.ComponentModel;
using AlexPacientes.Controls;
using AlexPacientes.iOS.CustomRenders;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerCR))]
namespace AlexPacientes.iOS.CustomRenders
{
    public class CustomPickerCR : PickerRenderer
    {
        public CGColor BorderColor { get; set; }
        public new CGColor BackgroundColor { get; set; }
        public float BorderRadius { get; set; }
        public int BorderStroke { get; set; }
        public int WidthElement { get; set; }
        public int HeightElement { get; set; }

        public override void Draw(CGRect e)
        {
            CustomPicker basePicker = (CustomPicker)this.Element;
            this.BorderColor = basePicker.BorderColor.ToCGColor();
            this.BackgroundColor = basePicker.BackgroundColor.ToCGColor();
            this.WidthElement = basePicker.WidthElement;
            this.HeightElement = basePicker.HeightElement;
            float pre = WidthElement < HeightElement ? WidthElement / 2.5f : HeightElement / 2.5f;
            this.BorderRadius = (basePicker.BorderRadius / 100) * pre - 1;
            this.BorderStroke = basePicker.BorderStroke;

            using (var context = UIGraphics.GetCurrentContext())
            {
                this.SetBackgroundColor(Color.Transparent);
                var rc = this.Bounds.Inset(1, 1);

                BorderRadius = (float)Math.Max(0, Math.Min(BorderRadius, Math.Max(rc.Height / 2, rc.Width / 2)));
                var path = CGPath.FromRoundedRect(rc, BorderRadius, BorderRadius);
                context.SetFillColor(BackgroundColor);
                Control.BorderStyle = UITextBorderStyle.None;

                context.SetStrokeColor(BorderColor);
                context.SetLineWidth((float)BorderStroke);
                context.AddPath(path);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e != null)
            {
                this.SetNeedsDisplay();
            }
        }
    }
}