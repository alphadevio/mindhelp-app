using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    public class StringBooleanConverter : IValueConverter
    {
        public string True { get; set; }
        public string False { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) throw new ArgumentException();
            return (bool)value ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
