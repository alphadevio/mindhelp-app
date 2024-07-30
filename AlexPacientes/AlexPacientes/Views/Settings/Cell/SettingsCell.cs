using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Settings.Cell
{
    public class SettingsCell:ViewCell
    {
        public SettingsCell()
        {
            var title = new LabelDarkPrimary()
            {
                Margin = Layouts.Margin,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            View = title;

            title.SetBinding(Label.TextProperty, "Title");
        }
    }
}
