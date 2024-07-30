using AlexPacientes.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views
{
    public class BaseContentPage : ContentPage, INavBarConfig
    {
        #region Bindable Properties

        public View CustomView { get { return (View)GetValue(CustomViewProperty); } set { SetValue(CustomViewProperty, value); } }
        public static readonly BindableProperty CustomViewProperty = BindableProperty.Create(
            nameof(CustomView),
            typeof(View),
            typeof(BaseContentPage),
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || n as View == null) return;
                sender._navBar.CustomView = n as View;
            });

        public new View Content { get { return (View)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }
        public static readonly new BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(View), typeof(BaseContentPage),
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || n == null) return;
                sender._grid.Children.Add((View)n, 0, 1);
                sender._grid.RaiseChild(sender._loadingView);
            });

        public Command RequestAuthenticationCommand { get { return (Command)GetValue(RequestAuthenticationCommandProperty); } set { SetValue(RequestAuthenticationCommandProperty, value); } }
        public static readonly BindableProperty RequestAuthenticationCommandProperty = BindableProperty.Create(
            nameof(RequestAuthenticationCommand), typeof(Command), typeof(BaseContentPage), null,
            propertyChanged:(s,o,n)=> {
                var sender = s as BaseContentPage;
                if (sender == null || sender._navBar == null || sender._loginBanner == null || n == null) return;
                sender._loginBanner.ButtonCommand = n as Command;
            });

        public bool NeedsUserSession { get { return (bool)GetValue(NeedsUserSessionProperty); } set { SetValue(NeedsUserSessionProperty, value); } }
        public static readonly BindableProperty NeedsUserSessionProperty = BindableProperty.Create(
            nameof(NeedsUserSession), typeof(bool), typeof(BaseContentPage), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || sender._navBar == null || n == null) return;
                if ((bool)n)
                {
                    if (AlexPacientes.Settings.GlobalConfig.Identity != null) return;
                    if (sender._loginBanner == null)
                        sender._loginBanner = new LoginToMindhelpBanner();
                    sender._loginBanner.ButtonCommand = sender.RequestAuthenticationCommand;
                    sender._grid.Children.Add(sender._loginBanner, 0, 1, 0, 2);
                    sender._grid.RaiseChild(sender._loginBanner);
                    sender._grid.ChildAdded += (s1, e1) => _grid_ChildAdded(sender, e1);
                }
                else
                {
                    if (sender._loginBanner != null && sender._grid.Children.Contains(sender._loginBanner))
                        sender._grid.Children.Remove(sender._loginBanner);
                    sender._loginBanner = null;
                    sender._grid.ChildAdded -= (s1, e1) => _grid_ChildAdded(sender, e1);
                }
            });

        public static readonly BindableProperty NavBarTitleProperty = BindableProperty.Create(
            nameof(NavBarTitle), typeof(string), typeof(BaseContentPage),
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || sender._navBar == null || n == null) return;
                sender._navBar.Title = (string)n;
            });

        public static readonly BindableProperty NavBarLeftIconCommandProperty = BindableProperty.Create(
            nameof(NavBarLeftIconCommand), typeof(Command), typeof(BaseContentPage),
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || sender._navBar == null || n == null) return;
                sender._navBar.LeftIconCommand = (Command)n;
            });

        public static readonly BindableProperty NavBarRightIconCommandProperty = BindableProperty.Create(
            nameof(NavBarRightIconCommand), typeof(Command), typeof(BaseContentPage),
            propertyChanged: (s, o, n) =>
            {
                var sender = s as BaseContentPage;
                if (sender == null || sender._navBar == null || n == null) return;
                sender._navBar.RightIconCommand = (Command)n;
            });


        #endregion

        #region NavBar Config
        public NavBarSize NavBarSize { get => _navBar.Size; set => _navBar.Size = value; }
        public Color NavBarColor { get => _navBar.NavBarColor; set => _navBar.NavBarColor = value; }
        public string NavBarTitle { get => (string)GetValue(NavBarTitleProperty); set => SetValue(NavBarTitleProperty, value); }
        public NavBarTextAlignment NavBarTitleAlignment { get => _navBar.TitleAlignment; set => _navBar.TitleAlignment = value; }
        public ImageSource NavBarLeftIcon { get => _navBar.LeftIcon; set => _navBar.LeftIcon = value; }
        public ImageSource NavBarRightIcon { get => _navBar.RightIcon; set => _navBar.RightIcon = value; }
        public bool NavBarHideLeftIcon { get => _navBar.HideLeftIcon; set => _navBar.HideLeftIcon = value; }
        public bool NavBarHideRightIcon { get => _navBar.HideRightIcon; set => _navBar.HideRightIcon = value; }
        public bool NavBarIsVisible { get => _navBar.IsVisible; set => _navBar.IsVisible = value; }
        public Command NavBarLeftIconCommand { get => (Command)GetValue(NavBarLeftIconCommandProperty); set => SetValue(NavBarLeftIconCommandProperty, value); }
        public Command NavBarRightIconCommand { get => (Command)GetValue(NavBarRightIconCommandProperty); set => SetValue(NavBarRightIconCommandProperty, value); }
        #endregion

        private Grid _grid;
        private LoadingView _loadingView;
        private NavBar _navBar;
        private LoginToMindhelpBanner _loginBanner;

        public BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            _loadingView = new LoadingView();
            _navBar = new NavBar()
            {
                Size = NavBarSize.Small,
                NavBarColor = Color.White
            };
            _grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                ColumnSpacing = 0,
            RowDefinitions = { new RowDefinition { Height = GridLength.Auto }, new RowDefinition { Height = GridLength.Star } }
            };
            if (Device.RuntimePlatform == Device.iOS)
            {
                //_grid.Padding = new Thickness(0, 20, 0, 0);
                _navBar.Padding = new Thickness(0, 20, 0, 0);
            }
            _grid.Children.Add(_navBar, 0, 0);
            _grid.Children.Add(_loadingView, 0, 1);

            base.Content = _grid;
            BackgroundColor = Styles.Colors.PrimaryBackgroundColor;

            _loadingView.SetBinding(LoadingView.IsRunningProperty, "IsBusy");
        }

        private static void _grid_ChildAdded(object s, ElementEventArgs e)
        {
            var sender = s as BaseContentPage;
            if (sender.NeedsUserSession && sender._loginBanner != null && sender._grid.Children.Contains(sender._loginBanner))
                Device.BeginInvokeOnMainThread(() => sender._grid.RaiseChild(sender._loginBanner));
        }

    }

    public interface INavBarConfig
    {
        NavBarSize NavBarSize { get; set; }
        Color NavBarColor { get; set; }
        string NavBarTitle { get; set; }
        NavBarTextAlignment NavBarTitleAlignment { get; set; }
        ImageSource NavBarLeftIcon { get; set; }
        ImageSource NavBarRightIcon { get; set; }
        bool NavBarHideLeftIcon { get; set; }
        bool NavBarHideRightIcon { get; set; }
        bool NavBarIsVisible { get; set; }
        Command NavBarLeftIconCommand { get; set; }
        Command NavBarRightIconCommand { get; set; }
    }
}
