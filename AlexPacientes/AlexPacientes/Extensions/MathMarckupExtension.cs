using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Extensions
{
    public class MathMinMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Min(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class MathMaxMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Max(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }
}
