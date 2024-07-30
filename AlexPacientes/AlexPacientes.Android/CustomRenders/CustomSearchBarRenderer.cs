using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AlexPacientes.Controls;
using AlexPacientes.Droid.CustomRenders;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace AlexPacientes.Droid.CustomRenders
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var sbar = e.NewElement as CustomSearchBar;
                UpdateBackgroundColor(sbar.TextFieldBackgroundColor);
                //UpdateIconColor(sbar.SearchBarIconColor);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomSearchBar.TextFieldBackgroundColorProperty.PropertyName)
                UpdateBackgroundColor((sender as CustomSearchBar).TextFieldBackgroundColor);
            //else if (e.PropertyName == CustomSearchBar.SearchBarIconColorProperty.PropertyName)
            //    UpdateIconColor((sender as CustomSearchBar).SearchBarIconColor);
        }

        void UpdateBackgroundColor(Color color)
        {
            Control.SetBackgroundColor(color.ToAndroid());
        }

        void UpdateIconColor(Color color)
        {
            int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            var icon = (Control.FindViewById(searchIconId)) as ImageView;
            icon?.SetColorFilter(color.ToAndroid());
        }
    }
}