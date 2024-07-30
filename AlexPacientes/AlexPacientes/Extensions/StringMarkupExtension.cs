using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Extensions
{
    public class StringMarkupExtension : IMarkupExtension<string>
    {
        public string Value { get; set; }
        public bool UpperCase { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return (UpperCase) ? Value.ToUpper() : Value.ToLower();
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
        }
    }
}
