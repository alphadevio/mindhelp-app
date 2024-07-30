using AlexPacientes.Controls;
using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Notes.Cells
{
    public class NoteCell:ViewCell
    {
        public NoteCell()
        {
            var title = new LabelDarkPrimary()
            {
                FontAttributes = FontAttributes.Bold
            };
            var content = new LabelDarkSecondary()
            {
                MaxLines = 2,
                LineBreakMode = LineBreakMode.TailTruncation,
                Margin=new Thickness(Layouts.Margin,0,0,Layouts.Margin/2)
            };

            View = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Padding = Layouts.Margin,
                Spacing = Layouts.Margin/2,
                Children = { title, content }
            };

            title.SetBinding(Label.TextProperty, "Title");
            content.SetBinding(Label.TextProperty, "Content");

        }
    }
}
