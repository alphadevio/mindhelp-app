using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class CustomPicker : Picker
    {
        public int WidthElement = 0;
        public int HeightElement = 0;

        /// <summary>
        /// Set or get the border color
        /// </summary>
        public Color BorderColor { get { return (Color)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomPicker),
            Color.Default,
            propertyChanged: OnItemsSourcePropertyChanged);

        public Color OKColor { get { return (Color)GetValue(OKColorProperty); } set { SetValue(OKColorProperty, value); } }
        public static readonly BindableProperty OKColorProperty = BindableProperty.Create(
            nameof(OKColor),
            typeof(Color),
            typeof(CustomEntry),
            Colors.EntryBorderColor,
            BindingMode.TwoWay);

        public Color ErrorColor { get { return (Color)GetValue(ErrorColorProperty); } set { SetValue(ErrorColorProperty, value); } }
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
            nameof(ErrorColor),
            typeof(Color),
            typeof(CustomEntry),
            Colors.Critical,
            BindingMode.TwoWay);

        public new Color BackgroundColor { get { return (Color)GetValue(BackgroundColorProperty); } set { SetValue(BackgroundColorProperty, value); } }

        public new static BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(CustomPicker),
            Color.White,
            propertyChanged: OnItemsSourcePropertyChanged);

        public bool IsRequired { get { return (bool)GetValue(IsRequiredProperty); } set { SetValue(IsRequiredProperty, value); } }
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
            nameof(IsRequired),
            typeof(bool),
            typeof(CustomPicker),
            false,
            BindingMode.TwoWay);

        public bool ValidatePicker { get { return (bool)GetValue(ValidatePickerProperty); } set { SetValue(ValidatePickerProperty, value); } }
        public static readonly BindableProperty ValidatePickerProperty = BindableProperty.Create(
            nameof(ValidatePicker),
            typeof(bool),
            typeof(CustomEntry),
            false,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as CustomPicker;
                if (!sender.IsRequired || sender == null || !(bool)n) return;
                sender.CustomPicker_SelectedIndexChanged(sender, null);
                sender.ValidatePicker = false;
            });


        /// <summary>
        /// From 0 to 100 of radius
        /// </summary>
        public int BorderRadius { get { return (int)GetValue(BorderRadiusProperty); } set { SetValue(BorderRadiusProperty, value); } }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
            nameof(BorderRadius),
            typeof(int),
            typeof(CustomPicker),
            0,
            propertyChanged: OnItemsSourcePropertyChanged);
        

        public int BorderStroke { get { return (int)GetValue(BorderStrockeProperty); } set { SetValue(BorderStrockeProperty, value); } }

        public static BindableProperty BorderStrockeProperty = BindableProperty.Create(
           nameof(BorderStroke),
           typeof(int),
           typeof(CustomPicker),
           1,
           propertyChanged: OnItemsSourcePropertyChanged);


        public CustomPicker()
        {
            this.SelectedIndexChanged += CustomPicker_SelectedIndexChanged;
            this.BackgroundColor = Colors.EntryBackgroundColor;
            this.TextColor = Colors.TextDarkSecondary;
        }

        private void CustomPicker_SelectedIndexChanged(object s, EventArgs e)
        {
            var sender = s as CustomPicker;
            if (sender == null) return;
            if (sender.SelectedIndex == -1)
            {
                sender.BorderColor = ErrorColor;
            }
            else
                sender.BorderColor = OKColor;
            sender.BorderRadius = 12;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (BorderRadius > 100)
                BorderRadius = 100;
            else if (BorderRadius < 0)
                BorderRadius = 0;

            this.WidthElement = (int)width;
            this.HeightElement = (int)height;
            BorderRadius = BorderRadius + 1;
            BorderRadius = BorderRadius - 1;
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomPicker;
            var changingFrom = oldValue;
            var changingTo = newValue;
        }
    }
}
