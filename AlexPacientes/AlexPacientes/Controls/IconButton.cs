using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class IconButton : CachedImage
    {
        public Command ClickCommand { get { return (Command)GetValue(ClickCommandProperty); } set { SetValue(ClickCommandProperty, value); } }
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
            nameof(ClickCommand), typeof(Command), typeof(SvgIconButton), defaultBindingMode: BindingMode.TwoWay);

        public IconButton()
        {
            // TapGesture for click
            var gesture = new TapGestureRecognizer();
            gesture.Command = new Command(() => Click());
            GestureRecognizers.Add(gesture);

            // Visual States
            var vsg = new VisualStateGroup() { Name = "TapStates" };
            var vsNormal = new VisualState() { Name = "Normal" };
            var vsTapped = new VisualState() { Name = "Tapped" };
            vsTapped.Setters.Add(new Setter
            {
                Property = BackgroundColorProperty,
                Value = Color.FromHex("#EEEEEE")
            });
            vsg.States.Add(vsNormal);
            vsg.States.Add(vsTapped);
            VisualStateManager.GetVisualStateGroups(this).Add(vsg);
        }

        private async void Click()
        {
            VisualStateManager.GoToState(this, "Tapped");
            await System.Threading.Tasks.Task.Delay(200);
            VisualStateManager.GoToState(this, "Normal");
            if (ClickCommand != null)
                ClickCommand.Execute(null);
        }
    }
}
