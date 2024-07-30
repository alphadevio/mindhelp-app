using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    public class CurrencyAmountConverter : IValueConverter
    {
        public string AmountProperty { get; set; }
        public string CurrencyProperty { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string && !string.IsNullOrEmpty(parameter as string))
            {
                var p = (parameter as string).Split(new string[] { "|" }, StringSplitOptions.None);
                if (p.Length != 2)
                    throw new ArgumentException("Parameter must follow the format Amount|CurrencyCode");
                AmountProperty = p[0];
                CurrencyProperty = p[1];
            }

            if (AmountProperty == null)
                throw new ArgumentNullException("You must set AmountProperty");
            if (CurrencyProperty == null)
                throw new ArgumentNullException("You must set CurrencyProperty");

            try
            {
                var amount = value.GetType().GetProperty(AmountProperty).GetValue(value);
                var currency = value.GetType().GetProperty(CurrencyProperty).GetValue(value);
                return Utilities.GlobalUtilities.FormatCurrency((decimal)amount, (string)currency);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
