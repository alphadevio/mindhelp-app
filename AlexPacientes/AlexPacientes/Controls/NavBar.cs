using AlexPacientes.Styles;
using FFImageLoading.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public enum NavBarSize
    {
        Small = 60, Medium = 110, Large = 155
    }

    public enum NavBarTextAlignment
    {
        Start, Center, End
    }

    public class NavBar : ContentView
    {
        public View CustomView { get { return (View)GetValue(CustomViewProperty); } set { SetValue(CustomViewProperty, value); } }
        public static readonly BindableProperty CustomViewProperty = BindableProperty.Create(
            nameof(CustomView),
            typeof(View),
            typeof(NavBar),
            propertyChanged: (s, o, n) =>
             {
                 var sender = s as NavBar;
                 if (sender == null || n as View == null) return;
                 sender._grid.Children.Add(n as View, 1, 2, 1, 2);
             });

        public NavBarSize Size { get { return (NavBarSize)GetValue(SizeProperty); } set { SetValue(SizeProperty, value); } }
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size), typeof(NavBarSize), typeof(NavBar), NavBarSize.Small,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._navBarBackground == null || n == null) return;
                sender.HeightRequest = (int)((NavBarSize)n);
                sender._navBarBackground.SetSize((NavBarSize)n);
                sender.ChangeGridBySize((NavBarSize)n);
                if (((NavBarSize)n) != NavBarSize.Small)
                    sender._grid.Children.Add(sender._mindIcon, 0, 2, 0, 1);
            });

        public Color NavBarColor { get { return (Color)GetValue(NavBarColorProperty); } set { SetValue(NavBarColorProperty, value); } }
        public static readonly BindableProperty NavBarColorProperty = BindableProperty.Create(
            nameof(NavBarColor), typeof(Color), typeof(NavBar), Color.Default,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._navBarBackground == null || n == null) return;
                sender._navBarBackground.SetColor(((Color)n).ToSKColor());
            });

        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(NavBar), string.Empty,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._titleLabel == null || n == null) return;
                sender._titleLabel.Text = (string)n;
            });

        public NavBarTextAlignment TitleAlignment { get { return (NavBarTextAlignment)GetValue(TitleAlignmentProperty); } set { SetValue(TitleAlignmentProperty, value); } }
        public static readonly BindableProperty TitleAlignmentProperty = BindableProperty.Create(
            nameof(TitleAlignment), typeof(NavBarTextAlignment), typeof(NavBar), NavBarTextAlignment.Start,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._titleLabel == null || n == null) return;
                sender._titleLabel.HorizontalOptions = (NavBarTextAlignment)n == NavBarTextAlignment.Start ? LayoutOptions.Start 
                    : ((NavBarTextAlignment)n == NavBarTextAlignment.Center ? LayoutOptions.Center : LayoutOptions.End);
            });

        public ImageSource LeftIcon { get { return (ImageSource)GetValue(LeftIconProperty); } set { SetValue(LeftIconProperty, value); } }
        public static readonly BindableProperty LeftIconProperty = BindableProperty.Create(
            nameof(LeftIcon), typeof(ImageSource), typeof(NavBar), null,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._leftIconButton == null || n == null) return;
                sender._leftIconButton.Source = (ImageSource)n;
            });

        public ImageSource RightIcon { get { return (ImageSource)GetValue(RightIconProperty); } set { SetValue(RightIconProperty, value); } }
        public static readonly BindableProperty RightIconProperty = BindableProperty.Create(
            nameof(RightIcon), typeof(ImageSource), typeof(NavBar), null,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._rightIconButton == null || n == null) return;
                sender._rightIconButton.Source = (ImageSource)n;
            });

        public bool HideLeftIcon { get { return (bool)GetValue(HideLeftIconProperty); } set { SetValue(HideLeftIconProperty, value); } }
        public static readonly BindableProperty HideLeftIconProperty = BindableProperty.Create(
            nameof(HideLeftIcon), typeof(bool), typeof(NavBar), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._leftIconButton == null || n == null) return;
                sender._leftIconButton.IsVisible = !(bool)n;
                if ((bool)n && sender._titleLabel != null)
                    sender._titleLabel.Margin = new Thickness(10, 0, 0, 0);
                else if (!(bool)n && sender._titleLabel != null)
                    sender._titleLabel.Margin = 0;
            });

        public bool HideRightIcon { get { return (bool)GetValue(HideRightIconProperty); } set { SetValue(HideRightIconProperty, value); } }
        public static readonly BindableProperty HideRightIconProperty = BindableProperty.Create(
            nameof(HideRightIcon), typeof(bool), typeof(NavBar), false,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._rightIconButton == null || n == null) return;
                sender._rightIconButton.IsVisible = !(bool)n;
                if ((bool)n && sender._titleLabel != null)
                    sender._titleLabel.Margin = new Thickness(0, 0, 10, 0);
                else if (!(bool)n && sender._titleLabel != null)
                    sender._titleLabel.Margin = 0;
            });

        public Command LeftIconCommand { get { return (Command)GetValue(LeftIconCommandProperty); } set { SetValue(LeftIconCommandProperty, value); } }
        public static readonly BindableProperty LeftIconCommandProperty = BindableProperty.Create(
            nameof(LeftIconCommand), typeof(Command), typeof(NavBar), null,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._leftIconButton == null || n == null) return;
                sender._leftIconButton.ClickCommand = (Command)n;
            });

        public Command RightIconCommand { get { return (Command)GetValue(RightIconCommandProperty); } set { SetValue(RightIconCommandProperty, value); } }
        public static readonly BindableProperty RightIconCommandProperty = BindableProperty.Create(
            nameof(RightIconCommand), typeof(Command), typeof(NavBar), null,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as NavBar;
                if (sender == null || sender._rightIconButton == null || n == null) return;
                sender._rightIconButton.ClickCommand = (Command)n;
            });

        private Grid _grid;
        private NavBarBackground _navBarBackground;
        private LabelDarkPrimary _titleLabel;
        private SvgIconButton _leftIconButton;
        private SvgIconButton _rightIconButton;
        private CachedImage _mindIcon;

        public NavBar()
        {
            BackgroundColor = Colors.PrimaryColor;
            Size = (NavBarSize)SizeProperty.DefaultValue;
            NavBarColor = (Color)NavBarColorProperty.DefaultValue;

            HorizontalOptions = LayoutOptions.FillAndExpand;
            HeightRequest = (int)Size;
            var iconSize = 32;
            _mindIcon = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = iconSize*2,
                MinimumHeightRequest = iconSize*2,
                WidthRequest = iconSize*3,
                MinimumWidthRequest = iconSize*3,
                Margin = new Thickness(Layouts.Margin,0),
                Source = Icons.Icon_MindhelpLarge,
                InputTransparent = true
            };
            _navBarBackground = new NavBarBackground(Size, NavBarColor.ToSKColor())
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = Convert.ToDouble((int)Size)
            };
            _titleLabel = new LabelDarkPrimary()
            {
                Margin=0,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment=TextAlignment.Center,
                TextColor = Color.White,
                FontSize = Device.RuntimePlatform == Device.iOS? Fonts.MediumSize : Fonts.ExtraLargeSize,
                //FontSize = Fonts.MediumSize,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            _leftIconButton = new SvgIconButton()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = iconSize,
                HeightRequest = iconSize,
                Source = Icons.BackArrow,
                Margin = new Thickness(10, 0, 0, 0)
            };
            _rightIconButton = new SvgIconButton()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = iconSize,
                HeightRequest = iconSize,
                Source = Icons.Notification,
                Margin = new Thickness(0, 0, 10, 0)
            };

            _grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Auto }, new ColumnDefinition { Width = GridLength.Star }, new ColumnDefinition { Width = GridLength.Auto } },
                Children = { _navBarBackground, _titleLabel, _leftIconButton, _rightIconButton }
            };
            Grid.SetColumn(_titleLabel, 1);
            Grid.SetColumn(_leftIconButton, 0);
            Grid.SetColumn(_rightIconButton, 2);
            Grid.SetColumn(_navBarBackground, 0);
            Grid.SetColumnSpan(_navBarBackground, 3);
            ChangeGridBySize(Size);

            Content = _grid;
            Padding = Device.RuntimePlatform == Device.iOS ? new Thickness(0, 10, 0, 0) : 0;
        }

        private void ChangeGridBySize(NavBarSize size)
        {
            _grid.RowDefinitions.Clear();

            switch (size)
            {
                case NavBarSize.Small:
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    _grid.Children.Add(_titleLabel, 1, 2, 0, 2);
                    _titleLabel.MaxLines = 1;
                    Grid.SetRow(_titleLabel, 0);

                    break;
                case NavBarSize.Medium:
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((int)NavBarSize.Medium / 2) });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((int)NavBarSize.Medium / 2) });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    _titleLabel.MaxLines = 1;
                    Grid.SetRow(_titleLabel, 1);
                    break;
                case NavBarSize.Large:
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((int)NavBarSize.Small / 2) });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength((int)NavBarSize.Small / 2) });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    _titleLabel.MaxLines = 2;
                    Grid.SetRow(_titleLabel, 2);
                    Grid.SetRowSpan(_titleLabel, 2);
                    break;
            }

            Grid.SetRow(_leftIconButton, 0);
            Grid.SetRowSpan(_leftIconButton, 2);
            Grid.SetRow(_rightIconButton, 0);
            Grid.SetRowSpan(_rightIconButton, size == NavBarSize.Small? 2: 1);
            Grid.SetRow(_navBarBackground, 0);
            Grid.SetRowSpan(_navBarBackground, _grid.RowDefinitions.Count);
        }

        private class NavBarBackground : SKCanvasView
        {
            private NavBarSize _size;
            private SKPaint _paint;

            public NavBarBackground(NavBarSize size, SKColor fillColor)
            {
                _size = size;
                _paint = new SKPaint()
                {
                    Color = fillColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill
                };
            }

            public void SetSize(NavBarSize size)
            {
                _size = size;
                InvalidateSurface();
            }

            public void SetColor(SKColor color)
            {
                _paint.Color = color;
                InvalidateSurface();
            }

            protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
            {
                base.OnPaintSurface(e);

                var info = e.Info;
                var surface = e.Surface;
                var canvas = surface.Canvas;

                var width = info.Width;
                var height = (int)_size * Layouts.DisplayScale;
                float yCurve = (int)NavBarSize.Small * 1.2f;
                float xCurve = width * 0.15f;
                canvas.Clear();

                using (var path = new SKPath())
                {
                    path.MoveTo(width- xCurve, height);
                    path.CubicTo(width - xCurve, height, width - xCurve*.24f, height - 5, width, height - yCurve);
                    path.LineTo(width, height);
                    path.LineTo(width- xCurve, height);
                    path.Close();
                    canvas.DrawPath(path, _paint);
                }
            }
        }
    }
}
