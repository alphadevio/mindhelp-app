using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AlexPacientes.Controls;
using AlexPacientes.iOS;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CEntry))]
namespace AlexPacientes.iOS
{
    public class CEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement != null)
            {
                e.OldElement.Focused -= FocusedEvent;
                e.OldElement.Unfocused -= UnfocusedEvent;
            }

            if(e.NewElement != null)
            {
                e.NewElement.Focused += FocusedEvent;
                e.NewElement.Unfocused += UnfocusedEvent;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == CustomEntry.BorderTypeProperty.PropertyName)
                SetNeedsDisplay();
        }

        public override void Draw(CGRect e)
        {
            if ((Element as CustomEntry).BorderType == CustomEntryBorderType.Frame)
                DrawFrame(Element as CustomEntry);
            else if ((Element as CustomEntry).BorderType == CustomEntryBorderType.Line)
                DrawLine(Element as CustomEntry);
            else
                DrawNone();
        }

        void DrawFrame(CustomEntry entry) 
        {
            using (var context = UIGraphics.GetCurrentContext())
            {
                //Get the current UIView size, it allow us to don't get a nullptr/overflow when we try to make the corner radious :P
                var currentSizeValue = this.SizeThatFits(new CGSize() { Height = 0, Width = 0 });

                //need to be transparent, if not, the original control color will override the "canvas" layout color
                this.SetBackgroundColor(Color.Transparent);
                //TintColor = baseEntry.TextColor.ToUIColor();

                var rc = this.Bounds.Inset(1, 1);
                var minSide = Math.Min((float)currentSizeValue.Width, (float)currentSizeValue.Height) / 2;

                var BorderRadius = minSide * entry.BorderRadius / 100;

                //must be less than the half of the smaller side and >=0
                BorderRadius = Math.Max(Math.Min(BorderRadius, minSide - 1), 0);
                var path = CGPath.FromRoundedRect(rc, BorderRadius, BorderRadius);
                Control.BorderStyle = UITextBorderStyle.None;
                //this.TintColor = baseEntry.TextColor.ToUIColor();//if we can set the "hint" as a different color e.e
                context.SetFillColor(entry.BackgroundColor.ToCGColor());
                context.SetStrokeColor(entry.BorderColor.ToCGColor());
                context.SetLineWidth((float)entry.BorderStroke);
                context.AddPath(path);
                context.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }

        void DrawLine(CustomEntry entry)
        {
            // Remove native borders
            Control.BorderStyle = UIKit.UITextBorderStyle.None;

            // Hide bottom line 
            //Control.Layer.Sublayers[2].Hidden = true;

            // QUITA EL ESPACIO EXTRA DE ERROR
            //Control.UnderlineErrorSpaceEnabled = false;

            // Creates normal layer
            var normalLayer = new CANormalLayer();
            var rect = new CGRect(NativeView.Bounds.X, Control.Frame.Height - 3, NativeView.Bounds.Width, 1f);
            normalLayer.Frame = rect;
            normalLayer.BackgroundColor = entry.BorderColor.ToCGColor();

            // Creates focus layer
            var focusLayer = new CAFocusLayer();
            rect = new CGRect(NativeView.Bounds.X, Control.Frame.Height - 4, NativeView.Bounds.Width, 2f);
            focusLayer.Frame = rect;
            focusLayer.BackgroundColor = entry.FocusedBorderColor.ToCGColor();

            // REMPLAZA LA ANTERIOR CAPA NORMAL SI EXISTE
            if (NativeView.Layer.Sublayers[0] is CANormalLayer)
                NativeView.Layer.ReplaceSublayer(NativeView.Layer.Sublayers[0], normalLayer);
            // AGREGA LA CAPA NORMAL
            else
                NativeView.Layer.InsertSublayer(normalLayer, 0);

            // REMPLAZA EL ANTERIOR GRADIENTE SI EXISTE
            if (NativeView.Layer.Sublayers[1] is CAFocusLayer)
                NativeView.Layer.ReplaceSublayer(NativeView.Layer.Sublayers[1], focusLayer);
            // AGREGA LA CAPA DEL GRADIENTE
            else
                NativeView.Layer.InsertSublayer(focusLayer, 1);

            // OCULTA LA CAPA DEL GRADIENTE
            NativeView.Layer.Sublayers[1].Hidden = true;
        }

        void DrawNone()
        {
            // Remove native borders
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }

        void FocusedEvent(object sender, FocusEventArgs e)
        {
            if ((Element as CustomEntry).BorderType != CustomEntryBorderType.Line) return;
            // Shows focus line
            NativeView.Layer.Sublayers[1].Hidden = false;
        }

        void UnfocusedEvent(object sender, FocusEventArgs e)
        {
            if ((Element as CustomEntry).BorderType != CustomEntryBorderType.Line) return;
            // Hide focus line
            NativeView.Layer.Sublayers[1].Hidden = true;
        }

        /// <summary>
        /// Custom class for normal layer
        /// </summary>
        private class CANormalLayer : CALayer { }

        /// <summary>
        /// Custome class for focus layer
        /// </summary>
        private class CAFocusLayer : CALayer { }
    }
}