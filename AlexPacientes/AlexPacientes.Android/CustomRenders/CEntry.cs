using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AlexPacientes.Controls;
using AlexPacientes.Droid;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CEntry))]
namespace AlexPacientes.Droid
{
    class CEntry : EntryRenderer
    {
        public CEntry(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                Draw(e.NewElement as CustomEntry);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Draw(sender as CustomEntry);
        }

        void Draw(CustomEntry entry)
        {
            if (entry.BorderType == CustomEntryBorderType.Frame)
                DrawFrame(entry);
            else if (entry.BorderType == CustomEntryBorderType.Line)
                DrawLine(entry);
            else
                DrawNone();
        }

        void DrawFrame(CustomEntry entry)
        {
            var bas = Control as TextView;

            //Get the measure to get the ViewSize
            bas.Measure(0, 0);

            //is common always get the heigh value instead of width, we can "pre define" this value as a reference
            var h = (entry.HeightRequest > 0 ? entry.HeightRequest : bas.MeasuredHeight / 2);

            var side = Math.Max(h, 0);
            var radius = side * entry.BorderRadius / 100;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(entry.BackgroundColor.ToAndroid());//background
            gd.SetCornerRadius((float)radius);//Border radius
            gd.SetStroke(entry.BorderStroke, entry.BorderColor.ToAndroid());//stroke
            Control.SetBackground(gd);
        }

        void DrawLine(CustomEntry entry)
        {
            // Background drawable
            GradientDrawable backgroundDrawable = new GradientDrawable();
            backgroundDrawable.SetShape(ShapeType.Rectangle);
            backgroundDrawable.SetColor(Color.Transparent.ToAndroid());

            // Bottom line normal
            GradientDrawable lineNormalDrawable = new GradientDrawable();
            lineNormalDrawable.SetShape(ShapeType.Rectangle);
            lineNormalDrawable.SetColor(entry.BorderColor.ToAndroid());
            lineNormalDrawable.SetSize((int)Element.Width, (int)DpToPixels(Context, 1));

            // Creates layer to contain background drawable and bottom line drawable
            int verticalPadding = (int)DpToPixels(Context, 10);
            int horizontalPadding = (int)DpToPixels(Context, 5);
            Drawable[] drawables = new Drawable[] { backgroundDrawable, lineNormalDrawable };
            LayerDrawable normalLayer = new LayerDrawable(drawables);
            normalLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            normalLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)DpToPixels(Context, 8));
            normalLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates focused bottom line drawable
            GradientDrawable lineFocusedDrawable = new GradientDrawable();
            lineFocusedDrawable.SetShape(ShapeType.Rectangle);
            lineFocusedDrawable.SetColor(entry.FocusedBorderColor.ToAndroid());
            lineFocusedDrawable.SetSize((int)Element.Width, (int)DpToPixels(Context, 2));

            // Creates layer to contain background and focus drawable layers
            drawables = new Drawable[] { backgroundDrawable, lineFocusedDrawable };
            LayerDrawable focusLayer = new LayerDrawable(drawables);
            focusLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            focusLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)DpToPixels(Context, 8));
            focusLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates drawable state list
            StateListDrawable state = new StateListDrawable();
            state.AddState(new int[] { Android.Resource.Attribute.StateFocused }, focusLayer);
            state.AddState(new int[] { }, normalLayer);

            Control.SetBackground(state);
        }

        void DrawNone()
        {
            Control.SetBackground(null);
        }

        public static float DpToPixels(Context context, float dp)
        {
            var metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, metrics);
        }
    }
}