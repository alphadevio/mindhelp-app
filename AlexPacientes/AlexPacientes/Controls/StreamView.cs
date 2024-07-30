
using System;

namespace AlexPacientes.Controls
{
    public class StreamView : Xamarin.Forms.View
    {
        public int ID { get; set; }
        private object _nativeView;
        public object NativeView 
        {
            get => _nativeView;
            set
            {
                _nativeView = value;
                RaiseNativeViewSetEvent();
            }
        }

        public event EventHandler NativeViewSet;

        protected virtual void RaiseNativeViewSetEvent()
        {
            NativeViewSet?.Invoke(this, EventArgs.Empty);
        }
    }
}
