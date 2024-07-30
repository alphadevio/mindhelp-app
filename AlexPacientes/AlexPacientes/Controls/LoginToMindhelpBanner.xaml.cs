using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginToMindhelpBanner : ContentView
    {
        public Command ButtonCommand { get { return (Command)GetValue(ButtonCommandProperty); } set { SetValue(ButtonCommandProperty, value); } }
        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand),
            typeof(Command),
            typeof(LoginToMindhelpBanner),
            null,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as LoginToMindhelpBanner;
                if (sender == null) return;
                sender.GoToLoginButton.Command = n as Command;
            });
        public LoginToMindhelpBanner()
        {
            InitializeComponent();
        }
    }
}