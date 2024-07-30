using AlexPacientes.Styles;
using FFImageLoading.Svg.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    /// <summary>
    /// Inherit from this class
    /// Just set the bindings of the control that you need
    /// </summary>
    public class BaseListCell:ViewCell
    {
        public Label Title;
        public Label SubTitle1;
        public Label SubTitle2;
        public SvgIconButton IconLeft;
        public SvgIconButton IconRight;
        public Grid MainGrid;
        public Frame MainFrame;


        //Font size defaults 

        //Default horizontal/Vertical options
        public LayoutOptions TitleHorizontalAlignment = LayoutOptions.StartAndExpand;
        public LayoutOptions TitleVerticalAlignment = LayoutOptions.CenterAndExpand;

        public LayoutOptions SubtitleHorizontalAlignment = LayoutOptions.StartAndExpand;
        public LayoutOptions SubtitleVerticalAlignment = LayoutOptions.CenterAndExpand;
        

        public BaseListCell()
        {

            #region InitViews
            Title = new LabelDarkPrimary()
            {
                HorizontalOptions = TitleHorizontalAlignment,
                VerticalOptions = TitleVerticalAlignment,
                IsVisible = false,
            };

            SubTitle1 = new LabelDarkSecondary()
            {
                HorizontalOptions = SubtitleHorizontalAlignment,
                VerticalOptions = SubtitleVerticalAlignment,
                IsVisible = false,
            };

            SubTitle2 = new LabelDarkHint()
            {
                HorizontalOptions = SubtitleHorizontalAlignment,
                VerticalOptions = SubtitleVerticalAlignment,
                IsVisible = false,
            };

            IconLeft = new SvgIconButton()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BitmapOptimizations=true,
                DownsampleToViewSize=true,
                CacheDuration=new TimeSpan(10,0,0,0),
                WidthRequest = 16,
                HeightRequest = 16
            };

            IconRight = new SvgIconButton()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BitmapOptimizations = true,
                DownsampleToViewSize = true,
                CacheDuration = new TimeSpan(10, 0, 0, 0),
                WidthRequest=16,
                HeightRequest=16
            };
            #endregion

            #region MainGrid
            MainGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                ColumnSpacing = 0
            };

            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });

            MainGrid.Children.Add(Title, 1, 2, 0, 1);
            MainGrid.Children.Add(SubTitle1, 1, 2, 1, 2);
            MainGrid.Children.Add(SubTitle2, 1, 2, 2, 3);
            MainGrid.Children.Add(IconLeft, 0, 1, 0, 3);
            MainGrid.Children.Add(IconRight, 2, 3, 0, 3);
            #endregion

            #region MainFrame
            MainFrame = new Frame()
            {
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.CommonCellBackground,
                Margin = new Thickness(Layouts.Margin, 4),
                CornerRadius = Layouts.FrameCornerRadius,
                HasShadow = false,
                Padding = Layouts.Padding,
                Content = MainGrid
            };
            #endregion

            #region Events
            Title.BindingContextChanged += TextContextChanged;
            SubTitle1.BindingContextChanged += TextContextChanged;
            SubTitle2.BindingContextChanged += TextContextChanged;
            IconLeft.BindingContextChanged += IconContextChanged;
            IconRight.BindingContextChanged += IconContextChanged;
            #endregion

            View = MainFrame;
        }

        public void OverrideLeftIcon(View view)
        {
            MainGrid.Children.Remove(IconLeft);
            MainGrid.Children.Add(view, 0, 1, 0, 3);

        }
        public void OverrideRightIcon(View view)
        {
            MainGrid.Children.Remove(IconRight);
            MainGrid.Children.Add(view, 2, 3, 0, 3);
        }

        public void OverrideTitle(View view)
        {
            MainGrid.Children.Remove(Title);
            MainGrid.Children.Add(view, 1, 2, 0, 1);
        }

        public void OverrideSubtitle1(View view)
        {
            MainGrid.Children.Remove(SubTitle1);
            MainGrid.Children.Add(view, 1, 2, 1, 2);
        }

        public void OverrideSubtitle2(View view)
        {
            MainGrid.Children.Remove(SubTitle2);
            MainGrid.Children.Add(view, 1, 2, 2, 3);
        }

        #region EventListeners
        private void TextContextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty((sender as Label).Text))
                (sender as Label).IsVisible = true;
        }

        private void IconContextChanged(object sender, EventArgs e)
        {
            var image = sender as SvgCachedImage;
            if (image.Source != null)
            {
                image.IsVisible = true;
                if (image == IconLeft)
                    image.Margin = new Thickness(0, 0, Layouts.Margin * 0.5f, 0);
                else
                    image.Margin = new Thickness(Layouts.Margin * 0.5f, 0, 0, 0);
            }
            else
            {
                image.WidthRequest = 0;
                image.Margin = 0;
            }
        }
        #endregion
    }
}
