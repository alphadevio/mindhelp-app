using System;
using System.Globalization;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    class StringEmptyBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is string && !string.IsNullOrEmpty((string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
