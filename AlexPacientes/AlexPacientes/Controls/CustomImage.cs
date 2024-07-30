using System;
using System.Collections.Generic;
using System.Text;
using AlexPacientes.Utilities;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class CustomImage:Image
    {
        //Remember code the shadow stuff

        public Color BorderColor { get { return (Color)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomImage),
            Color.Default,
            propertyChanged: OnItemsSourcePropertyChanged);        

        /// <summary>
        /// 0% - 100%
        /// </summary>
        public int BorderRadius { get { return (int)GetValue(BorderRadiusProperty); } set { SetValue(BorderRadiusProperty, value); } }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
            nameof(BorderRadius),
            typeof(int),
            typeof(CustomImage),
            1,
            propertyChanged: OnItemsSourcePropertyChanged);


        public int BorderStroke { get { return (int)GetValue(BorderStrockeProperty); } set { SetValue(BorderStrockeProperty, value); } }
        
        public static BindableProperty BorderStrockeProperty = BindableProperty.Create(
           nameof(BorderStroke),
           typeof(int),
           typeof(CustomImage),
           1,
           propertyChanged: OnItemsSourcePropertyChanged);
                
        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomImage;
            var changingFrom = oldValue;
            var changingTo = newValue;
        }

        
    }
}

