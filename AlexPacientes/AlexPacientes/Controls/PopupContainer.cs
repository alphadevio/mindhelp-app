using AlexPacientes.Styles;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class PopupContainer:PopupPage
    {
        public View PopupContent { get { return (View)GetValue(PopupContentProperty); } set { SetValue(PopupContentProperty, value); } }
        public static readonly BindableProperty PopupContentProperty = BindableProperty.Create(
            nameof(PopupContent),
            typeof(View),
            typeof(PopupContainer),
            null,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
             {
                 var context = s as PopupContainer;
                 if (context != null&&context.container!=null)
                 {
                     context.container.Content = n as View;
                 }
             });
        
        Frame container;
        public PopupContainer(View content)
        {
            container = new Frame()
            {
                Margin = Layouts.Margin * 2,
                Padding = Layouts.Margin,
                BorderColor = Colors.EntryBorderColor,
                BackgroundColor = Colors.PopupBackground,
                HasShadow = Device.RuntimePlatform == Device.iOS ? false : true,
            };
            PopupContent = content;
            this.Content = container;
            this.BackgroundColor = Colors.PopupBackground.MultiplyAlpha(0.7);
        }

        public async Task HidePopup()
        {
            await Navigation.PopPopupAsync();
        }

        public async Task ShowPopup()
        {
            await Navigation.PushPopupAsync(this);
        }
    }
}
