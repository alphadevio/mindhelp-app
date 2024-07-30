using AlexPacientes.Styles;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class Divider : BoxView
    {
        public Divider()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Color = Colors.Divider;
            Margin = new Thickness(0, 4);
            HeightRequest = 1;
        }
    }

    class DividerHorizontal : BoxView
    {
        public DividerHorizontal()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            Color = Colors.Divider;
            Margin = new Thickness(4, 0);
            WidthRequest = 1;
        }
    }
}
