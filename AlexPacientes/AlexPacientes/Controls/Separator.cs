using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public enum StrokeType
    {
        Solid,
        Dotted,
        Dashed
    }
    public enum SeparatorOrientation
    {
        Vertical,
        Horizontal
    }

    public class Separator : View
    {
        /**
         * Orientation property
         */
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create("Orientation", typeof(SeparatorOrientation), typeof(Separator), SeparatorOrientation.Horizontal, BindingMode.OneWay, null, null, null, null);

        /**
         * Orientation of the separator. Only
         */
        public SeparatorOrientation Orientation
        {
            get
            {
                return (SeparatorOrientation)GetValue(OrientationProperty);
            }

            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        /**
         * Color property
         */
        public static readonly BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(Separator), Color.Default, BindingMode.OneWay, null, null, null, null);

        /**
         * Color of the separator. Black is a default color
         */
        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }


        /**
         * SpacingBefore property
         */

        public static readonly BindableProperty SpacingBeforeProperty = BindableProperty.Create("SpacingBefore", typeof(double), typeof(Separator), (double)0, BindingMode.OneWay, null, null, null, null);

        /**
         * Padding before the separator. PrimaryText is 1.
         */
        public double SpacingBefore
        {
            get
            {
                return (double)GetValue(SpacingBeforeProperty);
            }
            set
            {
                SetValue(SpacingBeforeProperty, value);
            }
        }

        /**
         * Spacing After property
         */
        public static readonly BindableProperty SpacingAfterProperty = BindableProperty.Create("SpacingAfter", typeof(double), typeof(Separator), (double)0, BindingMode.OneWay, null, null, null, null);

        /**
         * Padding after the separator. PrimaryText is 1.
         */
        public double SpacingAfter
        {
            get
            {
                return (double)GetValue(SpacingAfterProperty);
            }
            set
            {
                SetValue(SpacingAfterProperty, value);
            }
        }

        /**
         * Spacing Left Side property
         */
        public static readonly BindableProperty SpacingLeftSideProperty = BindableProperty.Create("SpacingLeftSide", typeof(double), typeof(Separator), (double)0, BindingMode.OneWay, null, null, null, null);

        /**
         * Padding to the left of the separator.
         */
        public double SpacingLeftSide
        {
            get
            {
                return (double)GetValue(SpacingLeftSideProperty);
            }
            set
            {
                SetValue(SpacingLeftSideProperty, value);
            }
        }

        /**
         * Spacing Right Side property
         */
        public static readonly BindableProperty SpacingRightSideProperty = BindableProperty.Create("SpacingRightSide", typeof(double), typeof(Separator), (double)0, BindingMode.OneWay, null, null, null, null);

        /**
         * Padding to the Right of the separator.
         */
        public double SpacingRightSide
        {
            get
            {
                return (double)GetValue(SpacingRightSideProperty);
            }
            set
            {
                SetValue(SpacingRightSideProperty, value);
            }
        }

        /**
         * Thickness property
         */
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create("Thickness", typeof(double), typeof(Separator), (double)1, BindingMode.OneWay, null, null, null, null);

        /**
         * How thick should the separator be. PrimaryText is 1
         */
        public double Thickness
        {
            get
            {
                return (double)GetValue(ThicknessProperty);
            }
            set
            {
                SetValue(ThicknessProperty, value);
            }
        }


        /**
         * Stroke type property
         */
        public static readonly BindableProperty StrokeTypeProperty = BindableProperty.Create("StrokeType", typeof(StrokeType), typeof(Separator), StrokeType.Solid, BindingMode.OneWay, null, null, null, null);

        /**
         * Stroke style of the separator. PrimaryText is Solid.
         */
        public StrokeType StrokeType
        {
            get
            {
                return (StrokeType)GetValue(StrokeTypeProperty);
            }
            set
            {
                SetValue(StrokeTypeProperty, value);
            }
        }

        public Separator()
        {
            UpdateRequestedSize();
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ThicknessProperty.PropertyName ||
               propertyName == ColorProperty.PropertyName ||
               propertyName == SpacingBeforeProperty.PropertyName ||
               propertyName == SpacingAfterProperty.PropertyName ||
               propertyName == StrokeTypeProperty.PropertyName ||
               propertyName == OrientationProperty.PropertyName)
            {
                UpdateRequestedSize();
            }
        }


        private void UpdateRequestedSize()
        {
            var minSize = Thickness;
            var optimalSize = SpacingBefore + Thickness + SpacingAfter;

            if (Orientation == SeparatorOrientation.Horizontal)
            {
                MinimumHeightRequest = minSize;
                HeightRequest = optimalSize;
                //HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                MinimumWidthRequest = minSize;
                WidthRequest = optimalSize;
                //VerticalOptions = LayoutOptions.Start;
            }
        }
    }
}
