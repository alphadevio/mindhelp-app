using AlexPacientes.Controls;
using AlexPacientes.Styles;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace AlexPacientes.Views.Chat.Cells
{
    public class PendingChatCell:ViewCell
    {
        public PendingChatCell()
        {
            double imageSize = Layouts.UserCardImage;
            double leftMargin = Layouts.Margin /2;
            var userImage = new CachedImage()
            {
                Margin = new Thickness(leftMargin, 0, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = imageSize,
                HeightRequest = imageSize,
                MinimumHeightRequest = imageSize,
                MinimumWidthRequest = imageSize,
                HorizontalOptions = LayoutOptions.Start,
                DownsampleToViewSize=true,
                Aspect=Aspect.AspectFill,
                Transformations =
                {
                    new FFImageLoading.Transformations.CircleTransformation()
                }
            };

            var name = new LabelDarkPrimary() { VerticalOptions =LayoutOptions.EndAndExpand, FontAttributes=FontAttributes.Bold, Margin=new Thickness(Layouts.Margin,0) };
            var content = new LabelDarkHint() { VerticalOptions = LayoutOptions.StartAndExpand, FontAttributes = FontAttributes.Bold, Margin = new Thickness(Layouts.Margin, 0), TextColor = Colors.PrimaryColor };
            var date = new LabelDarkHint() { 
                HorizontalOptions=LayoutOptions.EndAndExpand,
                VerticalOptions=LayoutOptions.StartAndExpand
            };

            var mainGrid = new Grid()
            {
                RowSpacing = Layouts.Margin/2,
                ColumnSpacing=0,
                Padding=Layouts.Margin,
                ColumnDefinitions =
                {
                    new ColumnDefinition(){Width=imageSize+leftMargin},
                    new ColumnDefinition(){Width=new GridLength(1,GridUnitType.Star)}
                },
                RowDefinitions =
                {
                    new RowDefinition(){Height=new GridLength(1,GridUnitType.Star)},
                    new RowDefinition(){Height=new GridLength(1,GridUnitType.Star)}
                }
            };

            mainGrid.Children.Add(userImage, 0, 1, 0, 2);
            mainGrid.Children.Add(name, 1, 2, 0, 1);
            mainGrid.Children.Add(content, 1, 2, 1, 2);
            mainGrid.Children.Add(date, 0, 2, 0, 2);
            View = mainGrid;

            userImage.SetBinding(CachedImage.SourceProperty, "UserImage");
            name.SetBinding(Label.TextProperty, "Name");
            content.SetBinding(Label.TextProperty, "Subtitle");
            date.SetBinding(Label.TextProperty, "StartDate");
        }
    }
}
