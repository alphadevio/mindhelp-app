using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AlexPacientes.Views.Navigation
{
    public class NavigationTabbedPage : Xamarin.Forms.TabbedPage
    {
        public NavigationTabbedPage()
        {
            ChangeAndroidTabbedPage();

            BarBackgroundColor = Colors.PrimaryBackgroundColor;
            SelectedTabColor = Colors.PrimaryColor;
            UnselectedTabColor = Colors.TextDarkSecondary;

            Children.Add(new Xamarin.Forms.NavigationPage(new Home.HomeView()) { Title = Labels.Labels.Home, IconImageSource = "ic_home.png" });
            Children.Add(new Xamarin.Forms.NavigationPage(new Booking.BookingView()) { Title = Labels.Labels.Bookings, IconImageSource = "ic_book.png" });
            //Children.Add(new Xamarin.Forms.NavigationPage(new Chat.PendingChatsView()) { Title = Labels.Labels.Chat, IconImageSource = "ic_chat" });
            Children.Add(new Xamarin.Forms.NavigationPage(new Settings.SettingsView()) { Title = Labels.Labels.Settings, IconImageSource = "ic_settings" });
            On<iOS>().SetUseSafeArea(true);
        }

        private void ChangeAndroidTabbedPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.Android>()
               .SetToolbarPlacement(ToolbarPlacement.Bottom)
               .SetOffscreenPageLimit(4)
               .SetIsLegacyColorModeEnabled(true)
               .SetElevation(200f);
        }
    }
}
