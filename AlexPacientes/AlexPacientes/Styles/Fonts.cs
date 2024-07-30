using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Styles
{
    public static class Fonts
    {
        public static double ExtraSmallSize => Device.GetNamedSize(NamedSize.Micro, typeof(Label)) + 1 + FontSizeAdded;
        public static double SmallSize => Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 1 + FontSizeAdded;
        public static double MediumSize => Device.GetNamedSize(NamedSize.Medium, typeof(Label)) - 2 + FontSizeAdded;
        public static double LargeSize => Device.GetNamedSize(NamedSize.Medium, typeof(Label)) + 1 + FontSizeAdded;
        public static double ExtraLargeSize => Device.GetNamedSize(NamedSize.Large, typeof(Label)) + FontSizeAdded;
        public static double ExtraExtraLargeSize => Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 4 + FontSizeAdded;
        public static double ExtraExtraExtraLargeSize => Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 12 + FontSizeAdded;

        public static double ListItemFontSize => MediumSize + FontSizeAdded;
        public static double ListHeaderFontSize => LargeSize + FontSizeAdded;
        public static double TitleViewFontSize => 20 + FontSizeAdded;

        public static FontAttributes DetailLabelAttributes => FontAttributes.None;
        public static FontAttributes DetailValueAttributes => FontAttributes.None;

        public static double FontSizeAdded = Device.RuntimePlatform==Device.iOS?3:-2;

        public static string KanitExtraBoldFont => Device.RuntimePlatform == Device.iOS ? "Fonts/Kanit-ExtraBold" : "Fonts/Kanit-ExtraBold.ttf#Kanit ExtraBold";
        public static string LatoLightFont => Device.RuntimePlatform == Device.iOS ? "Fonts/Lato-Light" : "Fonts/Lato-Light.ttf#Lato-Light";
        public static string LatoLightItalicFont => Device.RuntimePlatform == Device.iOS ? "Fonts/Lato-LightItalic" : "Fonts/Lato-LightItalic.ttf#Lato-LightItalic";
        public static string NexaBoldFont => Device.RuntimePlatform == Device.iOS ? "Fonts/Nexa-Bold" : "Fonts/Nexa-Bold.otf#Nexa Bold";
    }
}
