using AlexPacientes.Styles;
using System;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public abstract class LabelBase : Label
    {
        protected virtual Color CustomColor => Colors.DefaultDarkTextColor;
        protected virtual double CustomFontSize => Fonts.MediumSize;
        public LabelBase()
        {
            TextColor = CustomColor;
            FontSize = CustomFontSize;
        }
        public LabelBase(string text) : this()
        {
            Text = text;
        }
    }

    // Dark on light background
    public class LabelDarkPrimary : LabelBase
    {
        protected override Color CustomColor => Colors.TextDarkPrimary;
        protected override double CustomFontSize => Fonts.MediumSize;
        public LabelDarkPrimary() : base() { }
        public LabelDarkPrimary(string text) : base(text) { }
    }
    public class LabelDarkSecondary : LabelBase
    {
        protected override Color CustomColor => Colors.TextDarkSecondary;
        protected override double CustomFontSize => Fonts.SmallSize;
        public LabelDarkSecondary(string text) : base(text) { }
        public LabelDarkSecondary() : base() { }
    }
    public class LabelDarkHint : LabelBase
    {
        protected override Color CustomColor => Colors.TextDarkHint;
        protected override double CustomFontSize => Fonts.ExtraSmallSize;
        public LabelDarkHint(string text) : base(text) { }
        public LabelDarkHint() : base() { }
    }

    // White on dark background
    class LabelLightPrimary : LabelBase
    {
        public bool RefreshText
        {
            get
            {
                return (bool)GetValue(RefreshTextProperty);
            }
            set
            {
                SetValue(RefreshTextProperty, value);
            }
        }

        public static readonly BindableProperty RefreshTextProperty = BindableProperty.Create(
            nameof(RefreshText), typeof(bool), typeof(LabelLightPrimary), false, BindingMode.TwoWay, propertyChanged:
            (sender, oldvalue, newvalue) =>
            {
                ((LabelLightPrimary)sender).Refresh();
            }
        );

        private void Refresh()
        {
            
        }

        protected override Color CustomColor => Colors.TextLightPrimary;
        protected override double CustomFontSize => Fonts.MediumSize;
        public LabelLightPrimary() : base() { }
        public LabelLightPrimary(string text) : base(text) { }
    }
    class LabelLightSecondary : LabelBase
    {
        protected override Color CustomColor => Colors.TextLightSecondary;
        protected override double CustomFontSize => Fonts.SmallSize;
        public LabelLightSecondary(string text) : base(text) { }
        public LabelLightSecondary() : base() { }
    }
    class LabelLightHint : LabelBase
    {
        protected override Color CustomColor => Colors.TextLightHint;
        protected override double CustomFontSize => Fonts.ExtraSmallSize;
        public LabelLightHint(string text) : base(text) { }
        public LabelLightHint() : base() { }
    }
}
