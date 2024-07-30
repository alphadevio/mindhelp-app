using System;
using System.Collections.Generic;
using System.Text;
using AlexPacientes.Utilities;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class CustomSearchBar : SearchBar
    {
        public Color TextFieldBackgroundColor { get { return (Color)GetValue(TextFieldBackgroundColorProperty); } set { SetValue(TextFieldBackgroundColorProperty, value); } }
        public static readonly BindableProperty TextFieldBackgroundColorProperty = BindableProperty.Create(
            nameof(TextFieldBackgroundColor),
            typeof(Color),
            typeof(CustomSearchBar),
            Color.Default,
            BindingMode.TwoWay,
            propertyChanged:OnItemsSourcePropertyChanged);

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomSearchBar;
            var changingFrom = oldValue;
            var changingTo = newValue;
        }
    }
}

