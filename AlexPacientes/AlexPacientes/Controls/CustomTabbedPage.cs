using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
        public Color SelectedIconColor { get { return (Color)GetValue(SelectedIconColorProperty); } set { SetValue(SelectedIconColorProperty, value); } }
        public static readonly BindableProperty SelectedIconColorProperty = BindableProperty.Create(
            nameof(SelectedIconColor),
            typeof(Color),
            typeof(CustomTabbedPage),
            Color.Accent);

        public Color UnselectedIconColor { get { return (Color)GetValue(UnselectedIconColorProperty); } set { SetValue(UnselectedIconColorProperty, value); } }
        public static readonly BindableProperty UnselectedIconColorProperty = BindableProperty.Create(
           nameof(UnselectedIconColor),
           typeof(Color),
           typeof(CustomTabbedPage),
           Color.Gray);

        public Color SelectedTextColor { get { return (Color)GetValue(SelectedTextColorProperty); } set { SetValue(SelectedTextColorProperty, value); } }
        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
           nameof(SelectedTextColor),
           typeof(Color),
           typeof(CustomTabbedPage),
           Color.Accent);

        public Color UnselectedTextColor { get { return (Color)GetValue(UnselectedTextColorProperty); } set { SetValue(UnselectedTextColorProperty, value); } }
        public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(
           nameof(UnselectedTextColor),
           typeof(Color),
           typeof(CustomTabbedPage),
           Color.Gray);
    }
}
