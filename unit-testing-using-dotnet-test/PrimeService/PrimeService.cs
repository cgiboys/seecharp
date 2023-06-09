using System;
using System.Globalization;

namespace Prime.Services
{
    public class PrimeService
    {
        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
            {
                if (candidate % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int GetWeekNumber(int month, int day)
        {
            DateTime date = new DateTime(DateTime.Now.Year, month, day);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
