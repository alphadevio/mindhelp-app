using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Styles
{
    public static class Layouts
    {
        public static double IconSizeCommon => 32;
        public static double IconSizeSmall = 25;
        public static double CommonCallIconSize => 55;
        public static double Margin => 16;
        public static double DoubleMargin => Margin * 2;
        public static Thickness Padding => new Thickness(PaddingHorizontal, PaddingVertical);
        public static double PaddingHorizontal => 20;
        public static double PaddingVertical => 20;
        public static double DisplayXSizePX { get; set; }
        public static double DisplayYSizePX { get; set; }
        public static float DisplayScale { get; set; }

        public static Thickness Layout => new Thickness(DisplayXSizePX * .0, DisplayXSizePX * .2);
        public static double IndicatorHeight => Margin * 2;

        public static int BorderStroke
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        return 1;
                    case Device.Android:
                        return 3;
                    default: return 1;
                }
            }
        }

        public static int CornerRadius => 8;
        public static float SearchbarCornerRadius = 12;
        public static Thickness MarginThickness => new Thickness(Margin);
        public static double UserCardImage => (double)Math.Min(Layouts.DisplayXSizePX * .13, 45);

        #region Cards
        public static float FrameCornerRadius => 8;
        public static double FrameSmallWidth => Math.Min(DisplayXSizePX * 0.30, 200);
        public static double FrameSmallHeight => Math.Min(FrameSmallWidth / FrameAspectRatio, 200 / FrameAspectRatio);
        public static double FrameMediumWidth => Math.Min(DisplayXSizePX * 0.43, 280);
        public static double FrameMediumHeight => Math.Min(FrameMediumWidth / FrameAspectRatio, 280 / FrameAspectRatio);
        private static double FrameAspectRatio => 0.69;
        public static double FrameAlternativeWidth => Math.Min(DisplayXSizePX * 0.43, 280);
        public static double FrameAlternativeHeight => Math.Min(FrameAlternativeWidth / 0.85, 280 / 0.85);
        public static double FrameAreaWidth => Math.Min(DisplayXSizePX * 0.48, 300);
        public static double FrameAreaHeight => Math.Min(FrameAreaWidth / 0.85, 300 / 0.85);
        public static Thickness FramePaddingList => new Thickness(Margin, 1);
        #endregion

    }
}
