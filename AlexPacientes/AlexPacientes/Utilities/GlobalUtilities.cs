using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace AlexPacientes.Utilities
{
    public static partial class GlobalUtilities
    {
        public static string ColorToString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }

        public static string LongToDatetimeString(long datetime)
        {
            var time = datetime;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(time).ToLocalTime();
            return date.ToShortDateString();
        }

        public static string FileExtension(this Plugin.Media.Abstractions.MediaFile file)
        {
            string path = file.Path;
            string extension = path.Substring(path.LastIndexOf("."));
            return extension;
        }

        public static async System.Threading.Tasks.Task<string> ConvertToBase64(System.IO.Stream stream)
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }

        private static readonly Dictionary<string, CultureInfo> ISOCurrencies =
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new { c, new RegionInfo(c.LCID).ISOCurrencySymbol })
            .GroupBy(x => x.ISOCurrencySymbol)
            .ToDictionary(g => g.Key, g => g.First().c, StringComparer.OrdinalIgnoreCase);

        public static string FormatCurrency(decimal amount, string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
                return amount.ToString("{0:C}");
            CultureInfo culture;
            if (ISOCurrencies.TryGetValue(currencyCode, out culture))
                return string.Format(culture, "{0:C}", amount);
            return amount.ToString("{0:C}");
        }
    }
}
