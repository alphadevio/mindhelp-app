using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Extensions
{
    public class ScreenMarckupExtension : IMarkupExtension<double>
    {
        public double FullValue { get; set; }
        public double RequiredPercentage { get; set; }
        public double AddValueToResult { get; set; } = 0;
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            //var percentage = RequiredPercentage > 100 ? 100 : RequiredPercentage < 0 ? 0 : RequiredPercentage;
            return (RequiredPercentage * FullValue * .01)+AddValueToResult;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class ScreenMarckupFloatExtension : IMarkupExtension<float>
    {
        public double FullValue { get; set; }
        public double RequiredPercentage { get; set; }
        public float ProvideValue(IServiceProvider serviceProvider)
        {
            var percentage = RequiredPercentage > 100 ? 100 : RequiredPercentage < 0 ? 0 : RequiredPercentage;
            return (float)(percentage * FullValue * .01);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<float>).ProvideValue(serviceProvider);
        }
    }

    public class ThicknessExtension : IMarkupExtension<Thickness>
    {
        public double Left { get; set; } = 0;
        public double Right { get; set; } = 0;
        public double Up { get; set; } = 0;
        public double Bottom { get; set; } = 0;

        public Thickness ProvideValue(IServiceProvider serviceProvider)
        {
            return new Thickness(Left, Up, Right, Bottom);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Thickness>).ProvideValue(serviceProvider);
        }
    }
}
