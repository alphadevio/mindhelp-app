using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AlexPacientes.Controls;
using AlexPacientes.iOS.CustomRenders;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace AlexPacientes.iOS.CustomRenders
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            var SearchBar = e.NewElement as CustomSearchBar;

            if (e.NewElement != null && Control != null && SearchBar != null)
            {
                Control.BackgroundColor = Color.Transparent.ToUIColor();
                SetControls(Control.Subviews, SearchBar);

                //-------------------------------------do it in that way when we all update to VS 2019--------------------------------------------
                //if (UIDevice.CurrentDevice.CheckSystemVersion(12,0))
                //    SetControls(Control.Subviews, SearchBar);
                //else
                //{
                //    Control.SearchTextField.BackgroundColor = SearchBar.BackgroundColor.ToUIColor();
                //    Control.SearchTextField.TextColor = SearchBar.TextColor.ToUIColor();
                //    Control.SearchTextField.TintColor = SearchBar.TextColor.ToUIColor();
                //}

                //Control.SearchTextField.BackgroundColor = UIColor.Red;
                this.Control.TextChanged += (s, ea) =>
                {
                    this.Control.ShowsCancelButton = true;
                };

                this.Control.TextChanged += (s, ea) =>
                {
                    this.Control.ShowsCancelButton = true;
                };

                this.Control.OnEditingStarted += (s, ea) => // when control receives focus
                {
                    this.Control.ShowsCancelButton = true;
                };

                this.Control.OnEditingStopped += (s, ea) => // when control loses focus 
                {
                    this.Control.ShowsCancelButton = false;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e != null)
            {
                this.SetNeedsDisplay();
            }
        }

        void SetControls(UIView[] collection, CustomSearchBar e)
        {
            foreach (var sview in collection)
            {
                if (sview.GetType() == typeof(UITextField))
                {
                    var tf = sview as UITextField;
                    var searchB = (e as CustomSearchBar);
                    tf.BackgroundColor = e.BackgroundColor.ToUIColor();
                    tf.TextColor = e.TextColor.ToUIColor();
                    tf.TintColor = e.TextColor.ToUIColor();
                    var searchIcon = tf.LeftView as UIImageView;
                    if (searchIcon != null && searchIcon.Image != null)
                    {
                        searchIcon.Image = searchIcon.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                        //searchIcon.TintColor = e.SearchBarIconColor.ToUIColor();
                    }
                }
                else if (sview.GetType() == typeof(UIImageView))
                {
                    var img = sview as UIImageView;
                    //img.TintColor = e.SearchBarIconColor.ToUIColor();
                }
                else if (sview.GetType() == typeof(UIView))
                    SetControls(sview.Subviews, e);
            }
        }
    }
}