using System;
using System.Collections.Generic;
using System.Text;
using AlexPacientes.Styles;
using AlexPacientes.Utilities;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace AlexPacientes.Controls
{
    public class SvgIconButton : SvgCachedImage
    {
        public object ClickCommandParameter { get { return (object)GetValue(ClickCommandParameterProperty); } set { SetValue(ClickCommandParameterProperty, value); } }
        public static readonly BindableProperty ClickCommandParameterProperty = BindableProperty.Create(
            nameof(ClickCommandParameter),
            typeof(object),
            typeof(SvgIconButton),
            null,
            BindingMode.TwoWay);

        public Command ClickCommand { get { return (Command)GetValue(ClickCommandProperty); } set { SetValue(ClickCommandProperty, value); } }
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
            nameof(ClickCommand), typeof(Command), typeof(SvgIconButton), defaultBindingMode: BindingMode.TwoWay);

        public string ColorToOverride { get { return (string)GetValue(ColorToOverrideProperty); }  set { SetValue(ColorToOverrideProperty, value); } }
        public static readonly BindableProperty ColorToOverrideProperty = BindableProperty.Create(
            nameof(ColorToOverride),
            typeof(string),
            typeof(SvgIconButton),
            string.Empty,
            BindingMode.TwoWay);

        public Color OverrideColor { get { return (Color)GetValue(OverrideColorProperty); } set { SetValue(OverrideColorProperty, value); } }
        public static readonly BindableProperty OverrideColorProperty = BindableProperty.Create(
            nameof(OverrideColor),
            typeof(Color),
            typeof(SvgIconButton),
            null,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) =>
             {
                 try
                 {
                     var sender = s as SvgIconButton;
                     if (sender == null || string.IsNullOrEmpty(sender.ColorToOverride) || n == null) return;
                     sender.ReplaceStringMap = null;
                     string colorToOverride, overrideColor;
                     colorToOverride = sender.ColorToOverride;
                     overrideColor = GlobalUtilities.ColorToString((Color)n);
                     Dictionary<string, string> sources = new Dictionary<string, string>();
                     sources.Add(colorToOverride, overrideColor);
                     sender.ReplaceStringMap = sources;
                     //sender.ReloadImage();
                 }
                 catch { return; }

             });

        public SvgIconButton() : base()
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
                Value = Colors.IconTappedBackColor
            });
            vsg.States.Add(vsNormal);
            vsg.States.Add(vsTapped);
            VisualStateManager.GetVisualStateGroups(this).Add(vsg);

            PropertyChanged += SvgIconButton_PropertyChanged;
        }

        private void SvgIconButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Renderer")
            {
                if (OverrideColor != null)
                {
                    var color = OverrideColor;
                    OverrideColor = Color.Default;
                    OverrideColor = color;
                }
            }
        }

        private async void Click()
        {
            VisualStateManager.GoToState(this, "Tapped");
            await System.Threading.Tasks.Task.Delay(200);
            VisualStateManager.GoToState(this, "Normal");
            if (ClickCommand != null)
                ClickCommand.Execute(ClickCommandParameter);
        }
    }
}
