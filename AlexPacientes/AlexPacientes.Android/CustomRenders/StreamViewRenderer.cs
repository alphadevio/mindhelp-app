using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AlexPacientes.Controls.StreamView), typeof(AlexPacientes.Droid.CustomRenders.StreamViewRenderer))]
namespace AlexPacientes.Droid.CustomRenders
{
    public class StreamViewRenderer : ViewRenderer<AlexPacientes.Controls.StreamView, FrameLayout>
    {
        public StreamViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<AlexPacientes.Controls.StreamView> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                FrameLayout frameLayout = new FrameLayout(Context);
                SetNativeControl(frameLayout);
            }

            if(e.NewElement != null)
            {
                e.NewElement.NativeView = Control;
            }
        }
    }
}