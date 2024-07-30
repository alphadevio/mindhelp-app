using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Extensions
{
    public class ToolsTimespanMarckupExtension : IMarkupExtension<TimeSpan>
    {
        public int Days { get; set; } = 0;
        public int Hours { get; set; } = 0;
        public int Minutes { get; set; } = 0;
        public int Seconds { get; set; } = 0;
        public int Miliseconds { get; set; } = 0;
        public TimeSpan ProvideValue(IServiceProvider serviceProvider)
        {
            return new TimeSpan(Days, Hours, Minutes, Seconds, Miliseconds);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<TimeSpan>).ProvideValue(serviceProvider);
        }
    }
}
