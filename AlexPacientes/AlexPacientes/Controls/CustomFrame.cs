
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class CustomFrame : ContentView
    {
        public float TopLeftCornerRadius { get { return (float)GetValue(TopLeftCornerRadiusProperty); } set { SetValue(TopLeftCornerRadiusProperty, value); } }
        public static BindableProperty TopLeftCornerRadiusProperty = BindableProperty.Create(
            nameof(TopLeftCornerRadius),
            typeof(float),
            typeof(CustomFrame),
            0f);

        public float TopRightCornerRadius { get { return (float)GetValue(TopRightCornerRadiusProperty); } set { SetValue(TopRightCornerRadiusProperty, value); } }
        public static BindableProperty TopRightCornerRadiusProperty = BindableProperty.Create(
            nameof(TopRightCornerRadius),
            typeof(float),
            typeof(CustomFrame),
            0f);

        public float BottomLeftCornerRadius { get { return (float)GetValue(BottomLeftCornerRadiusProperty); } set { SetValue(BottomLeftCornerRadiusProperty, value); } }
        public static BindableProperty BottomLeftCornerRadiusProperty = BindableProperty.Create(
            nameof(BottomLeftCornerRadius),
            typeof(float),
            typeof(CustomFrame),
            0f);

        public float BottomRightCornerRadius { get { return (float)GetValue(BottomRightCornerRadiusProperty); } set { SetValue(BottomRightCornerRadiusProperty, value); } }
        public static BindableProperty BottomRightCornerRadiusProperty = BindableProperty.Create(
           nameof(BottomRightCornerRadius),
           typeof(float),
           typeof(CustomFrame),
           0f);
    }
}
