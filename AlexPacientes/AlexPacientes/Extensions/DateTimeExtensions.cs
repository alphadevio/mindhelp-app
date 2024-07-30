using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the first week day following a date.
        /// </summary> 
        /// <param name="date">The date.</param> 
        /// <param name="dayOfWeek">The day of week to return.</param> 
        /// <returns>The first dayOfWeek day following date, or date if it is on dayOfWeek.</returns> 
        public static DateTime GetNextWeekDay(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }

        ///<summary>Gets the first week day following a date.</summary>
        ///<param name="date">The date.</param>
        ///<param name="dayOfWeek">The day of week to return.</param>
        ///<returns>The first dayOfWeek day following date, or date if it is on dayOfWeek.</returns>
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the hour of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfHour(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the hour of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfHour(this DateTime date)
        {
            return date.BeginningOfHour().AddHours(1).AddSeconds(-1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the day of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the day of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.BeginningOfDay().AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the week of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfWeek(this DateTime date)
        {
            date = date.AddDays(-((int)date.DayOfWeek));
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the week of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime date)
        {
            return date.BeginningOfWeek().AddDays(7).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the month of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the month of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.BeginningOfMonth().AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        /// Get a DateTime that represents the beginning of the year of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime BeginningOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0, 0, date.Kind);
        }

        /// <summary>
        /// Get a DateTime that represents the end of the year of the provided date.
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime date)
        {
            return date.BeginningOfYear().AddYears(1).AddTicks(-1);
        }

        /// <summary>
        /// Determines if the provided datetime is a weekend.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Check is date is between a range of time
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="start">Start of the range</param>
        /// <param name="end">End of the range</param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime date, TimeSpan start, TimeSpan end)
        {
            var time = date.TimeOfDay;
            if (start <= end)
                return time >= start && time <= end;
            else
                return time >= start || time <= end;
        }

        /// <summary>
        /// Change the time part of the given datetime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hours">New hour component</param>
        /// <param name="minutes">New minute component</param>
        /// <param name="seconds">New second component</param>
        /// <param name="milliseconds">New milisecond component</param>
        /// <returns></returns>
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                hours == 24 ? dateTime.AddDays(1).Day : dateTime.Day,
                hours==24?0:hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        public static long MillisecondsTimestamp(this DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime() - baseDate).TotalMilliseconds;
        }
    }
}
