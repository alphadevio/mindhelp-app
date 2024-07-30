using System;
using System.Globalization;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    public class InvertedBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !((bool)value);
            }

            throw new InvalidCastException("Unable to cast " + value + " to a boolean.");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !((bool)value);
            }

            throw new InvalidCastException("Unable to cast " + value + " to a boolean.");
        }
    }
}
