using AlexPacientes.Converters;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class FloatLabelEntry : Frame
    {
        private Label _label;
        public Entry Entry;

        public FloatLabelEntry()
        {
            Padding = 0;
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            BorderColor = Color.Transparent;

            Entry = new Entry
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                BindingContext = this,
                PlaceholderColor = Colors.TextDarkHint,
                TextColor = true ? Color.Black : Colors.TextDarkPrimary
            };
            Entry.SetBinding(Entry.PlaceholderProperty, "Placeholder");
            Entry.SetBinding(Entry.TextProperty, "Text");
            Entry.SetBinding(Entry.KeyboardProperty, "Keyboard");
            Entry.SetBinding(IsEnabledProperty, "IsEnabled");
            Entry.SetBinding(Entry.IsPasswordProperty, "IsPassword");
            Entry.TextChanged += OnFloatLabelTextChanged;

            _label = new Label { FontSize = Fonts.ExtraSmallSize, TextColor = Colors.TextDarkHint, BindingContext = this };
            _label.SetBinding(Label.TextProperty, "Placeholder", converter: new ValueConverter<string>(value =>
            {
                return (value ?? string.Empty).ToString().ToUpper();
            }));

            var layout = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 0, Padding = 0 };
            layout.Children.Add(_label);
            layout.Children.Add(Entry);

            Content = layout;
        }
        #region Bindable Properties

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            "Placeholder",
            typeof(string),
            typeof(FloatLabelEntry),
            default(string),
            BindingMode.TwoWay
        );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text",
            typeof(string),
            typeof(FloatLabelEntry),
            default(string),
            BindingMode.TwoWay
            );

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            "Keyboard",
            typeof(Keyboard),
            typeof(FloatLabelEntry),
            default(Keyboard),
            BindingMode.TwoWay
            );

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            "IsPassword",
            typeof(bool),
            typeof(FloatLabelEntry),
            default(bool),
            BindingMode.TwoWay
            );

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }
        public static readonly BindableProperty ConverterProperty = BindableProperty.Create(
           "Converter",
            typeof(IValueConverter),
           typeof(FloatLabelEntry),
           default(IValueConverter),
           BindingMode.TwoWay
           );


        #endregion

        #region Overrides

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            OnFloatLabelTextChanged(null, new TextChangedEventArgs("", this.Text));
        }

        #endregion

        #region Event Handlers

        protected virtual async void OnFloatLabelTextChanged(object sender, TextChangedEventArgs e)
        {
            var parent = Entry;
            var element = _label;

            if (parent == null || element == null) return;
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                await element.FadeTo(1, 200, Easing.SinOut);
                await element.TranslateTo(parent.X, parent.Y - 12, 200, Easing.SinOut);
            }
            else
            {
                await element.FadeTo(0, 200, Easing.SinIn);
                await element.TranslateTo(parent.X, parent.Y, 200, Easing.SinIn);
            }
        }
        #endregion


    }

    public class FloatLabelCustomEntry : Frame
    {
        private Label _label;
        private CustomEntry _entry;

        public FloatLabelCustomEntry()
        {
            Padding = 0;
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            BorderColor = Color.Transparent;

            _entry = new CustomEntry
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BindingContext = this,
                HeightRequest = 45,                
                PlaceholderColor = Colors.TextDarkHint,
                BorderStroke = Device.iOS==Device.RuntimePlatform?1:3,
                BorderColor = Colors.EntryBorderColor,
                OKColor=Colors.EntryBorderColor,
                ErrorColor=Colors.Danger,
                BorderRadius = 15,
                
            };
            _entry.SetBinding(CustomEntry.PlaceholderProperty, "Placeholder");
            _entry.SetBinding(CustomEntry.TextProperty, "Text");
            _entry.SetBinding(CustomEntry.TextColorProperty, "TextColor");
            _entry.SetBinding(CustomEntry.KeyboardProperty, "Keyboard");
            _entry.SetBinding(IsEnabledProperty, "IsEnabled");
            _entry.SetBinding(CustomEntry.IsPasswordProperty, "IsPassword");
            _entry.SetBinding(CustomEntry.FontFamilyProperty, "FontFamily");
            _entry.SetBinding(CustomEntry.FontSizeProperty, "FontSize");
            _entry.SetBinding(CustomEntry.BorderTypeProperty, "BorderType");
            _entry.TextChanged += OnFloatLabelTextChanged;
            _label = new Label { FontSize = Fonts.SmallSize, TextColor = Colors.TextDarkPrimary, BindingContext = this };
            _label.SetBinding(Label.TextProperty, "Placeholder");
            _label.SetBinding(Label.TextColorProperty, "PlaceholderColor");
            _label.SetBinding(Label.FontSizeProperty, "PlaceholderFontSize");

            var layout = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 0, Padding = 0 };
            layout.Children.Add(_label);
            layout.Children.Add(_entry);

            Content = layout;
        }

        #region Bindable Properties

        public bool IsReadOnly { get; set; }
        public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(
            nameof(IsReadOnly),
            typeof(bool),
            typeof(FloatLabelCustomEntry),
            false,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as FloatLabelCustomEntry;
                if (sender == null) return;
                if ((bool)n)
                {
                    sender._entry.IsReadOnly = true;
                    sender._entry.InputTransparent = true;
                    sender._entry.BackgroundColor = Colors.Disabled;
                }
                else
                {
                    sender._entry.IsReadOnly = false;
                    sender._entry.InputTransparent = false;
                    sender._entry.BackgroundColor = Color.White;
                }
            });

        public string ValidateRegex { get { return (string)GetValue(ValidateRegexProperty); } set { SetValue(ValidateRegexProperty, value); } }
        public static readonly BindableProperty ValidateRegexProperty = BindableProperty.Create(
            nameof(ValidateRegex),
            typeof(string),
            typeof(CustomEntry),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged:(s,o,n)=>
            {
                var sender = s as FloatLabelCustomEntry;
                if (sender == null) return;
                sender._entry.ValidateRegex = n as string;
            });

        public bool ValidateEntry { get { return (bool)GetValue(ValidateEntryProperty); } set { SetValue(ValidateEntryProperty, value); } }
        public static readonly BindableProperty ValidateEntryProperty = BindableProperty.Create(
            nameof(ValidateEntry),
            typeof(bool),
            typeof(CustomEntry),
            false,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as FloatLabelCustomEntry;
                if (sender == null) return;
                sender._entry.ValidateEntry = (bool)n;
            });

        public string Placeholder { get { return (string)GetValue(PlaceholderProperty); } set { SetValue(PlaceholderProperty, value); } }
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            "Placeholder",
            typeof(string),
            typeof(FloatLabelCustomEntry),
            default(string),
            BindingMode.TwoWay
        );

        public Color PlaceholderColor { get { return (Color)GetValue(PlaceholderColorProperty); } set { SetValue(PlaceholderColorProperty, value); } }
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(FloatLabelCustomEntry),
            Color.Gray,
            BindingMode.TwoWay
            );

        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text",
            typeof(string),
            typeof(FloatLabelCustomEntry),
            default(string),
            BindingMode.TwoWay
            );

        public Color TextColor { get { return (Color)GetValue(TextColorProperty); } set { SetValue(TextColorProperty, value); } }
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(FloatLabelCustomEntry),
            Color.Black,
            BindingMode.TwoWay
            );

        public Keyboard Keyboard { get { return (Keyboard)GetValue(KeyboardProperty); } set { SetValue(KeyboardProperty, value); } }
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            "Keyboard",
            typeof(Keyboard),
            typeof(FloatLabelCustomEntry),
            default(Keyboard),
            BindingMode.TwoWay
            );

        public bool IsPassword { get { return (bool)GetValue(IsPasswordProperty); } set { SetValue(IsPasswordProperty, value); } }
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            "IsPassword",
            typeof(bool),
            typeof(FloatLabelCustomEntry),
            default(bool),
            BindingMode.TwoWay
            );

        public IValueConverter Converter { get { return (IValueConverter)GetValue(ConverterProperty); } set { SetValue(ConverterProperty, value); } }
        public static readonly BindableProperty ConverterProperty = BindableProperty.Create(
           "Converter",
            typeof(IValueConverter),
           typeof(FloatLabelCustomEntry),
           default(IValueConverter),
           BindingMode.TwoWay
           );

        public string FontFamily { get { return (string)GetValue(FontFamilyProperty); } set { SetValue(FontFamilyProperty, value); } }
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(FloatLabelCustomEntry),
            new Label().FontFamily,
            BindingMode.TwoWay);

        public double FontSize { get { return (double)GetValue(FontSizeProperty); } set { SetValue(FontSizeProperty, value); } }
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(FloatLabelCustomEntry),
            new Label().FontSize,
            BindingMode.TwoWay);

        public double PlaceholderFontSize { get { return (double)GetValue(PlaceholderFontSizeProperty); } set { SetValue(PlaceholderFontSizeProperty, value); } }
        public static readonly BindableProperty PlaceholderFontSizeProperty = BindableProperty.Create(
            nameof(PlaceholderFontSize),
            typeof(double),
            typeof(FloatLabelCustomEntry),
            Fonts.SmallSize,
            BindingMode.TwoWay);

        public CustomEntryBorderType BorderType { get { return (CustomEntryBorderType)GetValue(BorderTypeProperty); } set { SetValue(BorderTypeProperty, value); } }
        public static readonly BindableProperty BorderTypeProperty = BindableProperty.Create(
            nameof(BorderType),
            typeof(CustomEntryBorderType),
            typeof(FloatLabelCustomEntry),
            CustomEntryBorderType.Frame,
            BindingMode.TwoWay);
        #endregion

        #region Overrides

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            OnFloatLabelTextChanged(null, new TextChangedEventArgs("", this.Text));
        }

        #endregion

        #region Event Handlers

        protected virtual async void OnFloatLabelTextChanged(object sender, TextChangedEventArgs e)
        {
            var parent = _entry;
            var element = _label;

            if (parent == null || element == null) return;
            if (!string.IsNullOrEmpty(e.NewTextValue))
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
        #endregion


    }
}
