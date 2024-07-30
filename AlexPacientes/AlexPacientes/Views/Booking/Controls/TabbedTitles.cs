using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Booking.Controls
{
    public class TabbedTitles : Grid
    {
        public IEnumerable<string> Titles { get { return (IEnumerable<string>)GetValue(TitlesProperty); } set { SetValue(TitlesProperty, value); } }
        public static readonly BindableProperty TitlesProperty = BindableProperty.Create(
            nameof(Titles), typeof(IEnumerable<string>), typeof(TabbedTitles), null,
            propertyChanged: (s, o, n) =>
             {
                 var sender = s as TabbedTitles;
                 if (sender == null || n == null) return;
                 sender.AddTitles((IEnumerable<string>)n);
             });

        public int SelectedIndex { get { return (int)GetValue(SelectedIndexProperty); } set { SetValue(SelectedIndexProperty, value); } }
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex), typeof(int), typeof(TabbedTitles), -1,
            propertyChanged: (s, o, n) =>
            {
                var sender = s as TabbedTitles;
                if (sender == null || (int)n < 0) return;
                sender.ChangeSelectedTab();
            });

        public TabbedTitles()
        {
            ColumnSpacing = Layouts.Margin * 1.5;
            RowSpacing = 0;
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        private void AddTitles(IEnumerable<string> titles)
        {
            ColumnDefinitions.Clear();
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            int column = 1;
            foreach(string title in titles)
            {
                var label = new LabelDarkPrimary()
                {
                    Text = title,
                    FontSize = Fonts.LargeSize
                };
                var divider = new Divider()
                {
                    Color = Colors.PrimaryColor,
                    HeightRequest = 2
                };

                var tap = new TapGestureRecognizer() { Command = new Command(() => SetTab(label.Text)) };
                label.GestureRecognizers.Add(tap);
                divider.GestureRecognizers.Add(tap);

                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                Children.Add(label, column, 0);
                Children.Add(divider, column, 1);

                column++;
            }

            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            ChangeSelectedTab();
        }

        private void ChangeSelectedTab()
        {
            foreach(var view in Children)
            {
                if(Grid.GetColumn(view) == SelectedIndex + 1)
                {
                    if (view is LabelDarkPrimary)
                        (view as LabelDarkPrimary).TextColor = Colors.PrimaryColor;
                    else if (view is Divider)
                        view.IsVisible = true;
                }
                else
                {
                    if (view is LabelDarkPrimary)
                        (view as LabelDarkPrimary).TextColor = Colors.TextDarkPrimary;
                    else if (view is Divider)
                        view.IsVisible = false;
                }
            }
        }

        private void SetTab(string title)
        {
            int index = 0;
            foreach(var t in Titles)
            {
                if(t == title)
                {
                    SelectedIndex = index;
                    break;
                }
                index++;
            }
        }
    }
}
