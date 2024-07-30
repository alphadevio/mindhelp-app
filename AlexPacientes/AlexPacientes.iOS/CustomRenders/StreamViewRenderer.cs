using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AlexPacientes.Controls.StreamView), typeof(AlexPacientes.iOS.CustomRenders.StreamViewRenderer))]
namespace AlexPacientes.iOS.CustomRenders
{
    public class StreamViewRenderer : ViewRenderer<AlexPacientes.Controls.StreamView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AlexPacientes.Controls.StreamView> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                UIView view = new UIView();
                SetNativeControl(view);
            }

            if(e.NewElement != null)
            {
                e.NewElement.NativeView = Control;
            }
        }
    }
}