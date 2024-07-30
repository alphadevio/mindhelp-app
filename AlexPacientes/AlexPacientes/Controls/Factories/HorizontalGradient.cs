using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Controls.Factories
{
    public class HorizontalGradient : Frame
    {
        private readonly Image _image;



        public HorizontalGradient()
        {
            HasShadow = false;
            Padding = 0;
            OutlineColor = Color.Transparent;
            BackgroundColor = Color.Transparent;

            _image = new Image() { HeightRequest = 6, Aspect = Aspect.AspectFill, Source = "gradient_down.png" };

            Content = _image;
        }

        #region Properties
        public HorizontalGradientOrientation Orientation { get; set; }
        #endregion

        /// <summary>
        /// Invoked whenever the Parent of an element is set. Implement this method in order to add behavior when the element is added to a parent.
        /// </summary>
        /// <remarks>
        /// It is required to call the base implementation.
        /// </remarks>
        protected override void OnParentSet()
        {
            _image.Source = Orientation == HorizontalGradientOrientation.Down ? "gradient_down.png" : "gradient_up.png";

            base.OnParentSet();
        }
    }

    public enum HorizontalGradientOrientation
    {
        Down,
        Up
    }
}
