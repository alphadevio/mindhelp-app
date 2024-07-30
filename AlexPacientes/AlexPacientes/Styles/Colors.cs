using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Styles
{
    public static class Colors
    {
        public static Color Divider = Color.FromHex("#F0F0F0"); 
        public static Color PrimaryBackgroundColor = Color.White;
        public static Color SecondaryBackgroundColor = Color.FromHex("#F2F2F2");
        public static Color PopupBackground = Color.FromRgba(254, 254, 254,0.7);
        public static Color EntryBorderColor = Color.Gray;
        public static Color EntryBackgroundColor = Color.White;
        public static Color CommonCellBackground = Color.White;
        public static Color CommonBlack = Color.FromHex("#666666");
        public static Color CommonGrayLight = Color.FromHex("#C1C2C3");
        public static Color CommonGray = Color.FromHex("#212B36");
        public static Color Critical = Color.Red;
        public static Color OKColor = Color.Green;
        public static Color Danger = Color.FromHex("#55FF0000");
        public static Color Disabled = Color.LightGray;
        public static Color DefaultDarkTextColor = CommonBlack;
        public static Color TextDarkPrimary = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 1);
        public static Color TextDarkSecondary = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 0.8);
        public static Color TextDarkHint = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 0.6);
        public static Color TextLightPrimary = Color.White;
        public static Color TextLightSecondary = Color.FromHex("#DEDEDE");
        public static Color TextLightHint = Color.FromHex("#B1B1B1");
        public static Color IconTappedBackColor = Color.FromHex("#33FFFFFF");
        public static Color PrimaryColor = Color.FromHex("#44797B");
        public static Color PrimaryColorLight = Color.FromHex("#4CD964");
        public static Color PacientBubble = Color.FromHex("#E5E5EA");
        public static Color CustomEntryBackground = Color.FromHex("#FAFAFA");
        public static Color CallBackgroundColor = Color.FromHex("#707070");
    }
}