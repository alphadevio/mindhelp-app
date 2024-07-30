using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Utilities
{
    public static class Utilities
    {
        public static int GetExtraSummerHoursOfDate(DateTime targetDate, bool FromLocalToUTC = true)
        {
            int extraHours = 0;
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            var isSummerTime = tzi.IsDaylightSavingTime(targetDate);
            if (isSummerTime)
                extraHours = FromLocalToUTC ? -1 : 1;//Summer time
            return 0;
            return extraHours;
        }
    }
}
