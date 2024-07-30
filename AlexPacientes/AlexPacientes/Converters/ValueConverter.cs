using System;
using System.Globalization;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    public class ValueConverter<T> : IValueConverter
    {
        private readonly Func<object, object, T> _convert;
        private readonly Func<T, object> _convertBack;

        public ValueConverter(Func<object, T> convert)
        {
            _convert = (value, parameter) => convert(value);
        }
        public ValueConverter(Func<object, object, T> convert)
        {
            _convert = convert;
        }
        public ValueConverter(Func<object, T> convert, Func<T, object> convertBack = null)
        {
            _convert = (value, parameter) => convert(value);
            _convertBack = convertBack;
        }
        public ValueConverter(Func<object, object, T> convert, Func<T, object> convertBack = null)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_convert == null) throw new NotImplementedException();

            var val = _convert(value, parameter);
            return val;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_convertBack == null) throw new NotImplementedException();

            var val = _convertBack((T)value);
            return val;
        }
    }
}
