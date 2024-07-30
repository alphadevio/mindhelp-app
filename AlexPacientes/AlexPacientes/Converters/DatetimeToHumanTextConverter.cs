using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace AlexPacientes.Converters
{
    public class DatetimeToHumanTextConverter : IValueConverter
    {
        public DatetimeToHumanTextStyle Parameter { get; set; } = DatetimeToHumanTextStyle.Short;
        public DatetimeToHumanTextInputType InputType { get; set; } = DatetimeToHumanTextInputType.DateTime;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
                Parameter = (DatetimeToHumanTextStyle)parameter;
            DateTime? date;
            if (InputType == DatetimeToHumanTextInputType.DateTime)
                date = value as DateTime?;
            else if (InputType == DatetimeToHumanTextInputType.UnixTimeSecond)
                date = DateTimeOffset.FromUnixTimeSeconds(((long)value)).DateTime;
            else
                date = DateTimeOffset.FromUnixTimeMilliseconds(((long)value)).DateTime;
            if (date == null)
                return Labels.Labels.Unknown;
            if (!date.HasValue)
            {
                DateTime temp;
                if (value is string && DateTime.TryParse((string)value, out temp))
                    date = temp;
                else
                    return Labels.Labels.Unknown;
            }
            switch (Parameter)
            {
                case DatetimeToHumanTextStyle.Short: return date.Value.ToShortDateString();
                case DatetimeToHumanTextStyle.Long: return date.Value.ToLongDateString().Replace(DateTimeFormatInfo.CurrentInfo.GetDayName(date.Value.DayOfWeek), "").TrimStart(", ".ToCharArray());
                case DatetimeToHumanTextStyle.Ago: return DateTimeAgo(date.Value);
                case DatetimeToHumanTextStyle.MonthDay: return date.Value.ToString("MMMMM dd");
                case DatetimeToHumanTextStyle.Hour: return date.Value.ToString("hh:mm tt");
                case DatetimeToHumanTextStyle.Age: return CalculateAge(date.Value);
                case DatetimeToHumanTextStyle.NameDayMonth: return date.Value.ToString("ddd, d MMMM");
                default: return date.Value.ToShortDateString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now;
        }

        private string DateTimeAgo(DateTime date)
        {
            TimeSpan span = DateTime.Now.Subtract(date);
            int differenceDays = (int)span.TotalDays;

            if (differenceDays <= 0)
                return Labels.Labels.Now;
            else if (differenceDays < 7)
                return string.Format(Labels.Labels.DaysAgoFormat, differenceDays);
            else if (differenceDays < 31)
                return string.Format(Labels.Labels.WeeksAgoFormat, Math.Ceiling((double)differenceDays / 7));
            else if (differenceDays < 365)
                return string.Format(Labels.Labels.MonthAgoFormat, Math.Ceiling((double)differenceDays / 31));
            else
                return string.Format(Labels.Labels.YearsAgoFormat, Math.Ceiling((double)differenceDays / 365));
        }

        private string CalculateAge(DateTime date)
        {
            int age = System.Convert.ToInt32((DateTime.Today - date).TotalDays / 365.2425);
            return string.Format(Labels.Labels.YearsOldFormat, age);
        }
    }

    public enum DatetimeToHumanTextStyle
    {
        Short, String, Long, Ago, MonthDay, Hour, Age, NameDayMonth
    }

    public enum DatetimeToHumanTextInputType
    {
        DateTime, UnixTimeSecond, UnixTimeMillisecond
    }

    public class LongToDatetimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (long)value;
            //var it = DateTimeOffset.FromUnixTimeMilliseconds(time);
            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime)value;
            return dt.Ticks;
        }
    }

}
