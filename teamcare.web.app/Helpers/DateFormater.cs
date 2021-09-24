using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace teamcare.web.app.Helpers
{
	public static class DateFormater
	{
        public static string GetDateCurrentFormat(this DateTimeOffset dt, bool isWithTime)
        {
			if (isWithTime)
			{
                return dt.ToString("dd MMM yyyy mm:ss tt");
			}
			else
			{
				return dt.ToString("dd MMM yyyy");
			}
        }
    }
}
