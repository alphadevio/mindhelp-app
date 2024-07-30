using System;
using System.Globalization;
using Xamarin.Forms;


namespace AlexPacientes.Converters
{
    public class BooleanOpacityConverter : IValueConverter
    {
        public bool Negate { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Negate)
            {
                return ((bool)value) ? 0.0 : 1.0;
            }
            else
            {
                return ((bool)value) ? 1.0 : 0.0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value > 0.0) ? true : false;
        }
    }
}
