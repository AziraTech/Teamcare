using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teamcare.web.app.Helpers
{
    public static class DateFormater
    {
        public static string GetDateOffsetFormat(this DateTimeOffset dt, bool isWithTime)
        {
            if (isWithTime)
            {
                return dt.ToString("dd MMM yyyy hh:mm tt");
            }
            else
            {
                return dt.ToString("dd MMM yyyy");
            }
        }

        public static string GetDateFormat(this DateTime dt, bool isWithTime)
        {
            if (isWithTime)
            {
                return dt.ToString("dd MMM yyyy hh:mm tt");
            }
            else
            {
                return dt.ToString("dd MMM yyyy");
            }
        }
    }
}
