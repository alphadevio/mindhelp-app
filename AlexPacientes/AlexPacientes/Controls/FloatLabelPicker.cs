using AlexPacientes.Converters;
using AlexPacientes.Styles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    class FloatLabelPicker
    {
    }

    public class FloatLabelCustomPicker : Frame
    {
        private Label _label;
        public CustomPicker _picker;

        public FloatLabelCustomPicker()
        {
            Padding = 0;
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            BorderColor = Color.Transparent;

            _picker = new CustomPicker
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BindingContext = this,
                HeightRequest = 45,
                BorderStroke = 0,
                BorderColor = Color.Transparent,
                OKColor = Colors.EntryBorderColor,
                ErrorColor = Colors.Danger,
                BorderRadius = 15
            };
            _picker.BorderColor = Color.Transparent;
            _picker.BackgroundColor = Color.Transparent;
            _picker.SetBinding(CustomPicker.ValidatePickerProperty, "ValidatePicker");
            _picker.SetBinding(CustomPicker.TitleProperty, "Title");
            _picker.SetBinding(CustomPicker.TitleColorProperty, "TitleColor");
            _picker.SetBinding(CustomPicker.ItemsSourceProperty, "ItemsSource");
            _picker.SetBinding(CustomPicker.SelectedIndexProperty, "SelectedIndex");
            _picker.SetBinding(CustomPicker.SelectedItemProperty, "SelectedItem");
            _picker.SetBinding(CustomPicker.TextColorProperty, "TextColor");
            _picker.SetBinding(CustomPicker.FontAttributesProperty, "FontAttributes");
            _picker.SetBinding(CustomPicker.FontFamilyProperty, "FontFamily");
            _picker.SetBinding(CustomPicker.FontSizeProperty, "FontSize");
            _picker.SelectedIndexChanged += PickerSelectedIndexChanged;
            _label = new Label { FontSize = Fonts.SmallSize, TextColor = Colors.TextDarkHint, BindingContext = this };
            _label.SetBinding(Label.TextProperty, "Title");
            _label.SetBinding(Label.FontSizeProperty, "TitleFontSize");
            _label.SetBinding(Label.TextColorProperty, "TitleColor");

            _picker.BorderColor = Color.Transparent;
            var layout = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 0, Padding = 0 };
            layout.Children.Add(_label);
            layout.Children.Add(_picker);

            Content = layout;
            PickerSelectedIndexChanged(null, null);
        }

        #region Bindable Properties
        public bool ValidatePicker { get { return (bool)GetValue(ValidatePickerProperty); } set { SetValue(ValidatePickerProperty, value); } }
        public static readonly BindableProperty ValidatePickerProperty = BindableProperty.Create(
            nameof(ValidatePicker),
            typeof(bool),
            typeof(FloatLabelCustomPicker),
            false,
            BindingMode.TwoWay);

        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(FloatLabelCustomPicker),
            default(string),
            BindingMode.TwoWay
        );

        public Color TitleColor { get { return (Color)GetValue(TitleColorProperty); } set { SetValue(TitleColorProperty, value); } }
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            nameof(TitleColor),
            typeof(Color),
            typeof(FloatLabelCustomPicker),
            Color.Black,
            BindingMode.TwoWay
        );

        public double TitleFontSize { get { return (double)GetValue(TitleFontSizeProperty); } set { SetValue(TitleFontSizeProperty, value); } }
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
            nameof(TitleFontSize),
            typeof(double),
            typeof(FloatLabelCustomPicker),
            Fonts.SmallSize,
            BindingMode.TwoWay);

        public IList ItemsSource { get { return (IList)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(FloatLabelCustomPicker)
        );

        public int SelectedIndex { get { return (int)GetValue(SelectedIndexProperty); } set { SetValue(SelectedIndexProperty, value); } }
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(FloatLabelCustomPicker),
            -1,
            BindingMode.TwoWay
        );

        public object SelectedItem { get { return (object)GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); } }
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(FloatLabelCustomPicker),
            null,
            BindingMode.TwoWay
        );

        public Color TextColor { get { return (Color)GetValue(TextColorProperty); } set { SetValue(TextColorProperty, value); } }
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(FloatLabelCustomPicker),
            Color.Black,
            BindingMode.TwoWay
        );

        public FontAttributes FontAttributes { get { return (FontAttributes)GetValue(FontAttributesProperty); } set { SetValue(FontAttributesProperty, value); } }
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(FloatLabelCustomPicker),
            FontAttributes.None,
            BindingMode.TwoWay);

        public string FontFamily { get { return (string)GetValue(FontFamilyProperty); } set { SetValue(FontFamilyProperty, value); } }
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(FloatLabelCustomPicker),
            new Label().FontFamily,
            BindingMode.TwoWay);

        public double FontSize { get { return (double)GetValue(FontSizeProperty); } set { SetValue(FontSizeProperty, value); } }
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(FloatLabelCustomPicker),
            new Label().FontSize,
            BindingMode.TwoWay);
        #endregion

        private async void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var parent = _picker;
            var element = _label;

            if (parent == null || element == null) return;
            if (_picker.SelectedIndex >= 0)
            {
                await element.FadeTo(1, 200, Easing.SinOut);
                await element.TranslateTo(parent.X, parent.Y - 17, 200, Easing.SinOut);
            }
            else
            {
                await element.FadeTo(0, 200, Easing.SinIn);
                await element.TranslateTo(parent.X, parent.Y, 200, Easing.SinIn);
            }
        }
    }
}
