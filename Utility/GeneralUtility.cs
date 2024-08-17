using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class GeneralUtility
    {

        public static string ToToman(this int? Price)
        {
            if (Price != null)
            {
                var p = (int)Price;
                return p.ToString("N0") + " تومان ";
            }
            else
            {
                return null;
            }

        }

        public static string ToShamsiDate(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTime).ToString();
            string month = persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0');
            return String.Format("{0}/{1}/{2}", year, month, day);

        }
        public static string toShamsiDate(DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTime).ToString();
            string month = persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0');
            return String.Format("{0}/{1}/{2}", year, month, day);

        }
        public static string toLongShamsiDate(DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTime).ToString();
            int month = persianCalendar.GetMonth(dateTime);
            string day = persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0');
            string Month = "";
            switch (month)
            {
                case 1: Month = "فروردین"; break;
                case 2: Month = "اردیبهشت"; break;
                case 3: Month = "خرداد"; break;
                case 4: Month = "تیر"; break;
                case 5: Month = "مرداد"; break;
                case 6: Month = "شهریور"; break;
                case 7: Month = "مهر"; break;
                case 8: Month = "آبان"; break;
                case 9: Month = "آذر"; break;
                case 10: Month = "دی"; break;
                case 11: Month = "بهمن"; break;
                case 12: Month = "اسفند"; break;
                default:
                    break;
            }
            return String.Format("{0} {1} {2}", day, Month, year);

        }

        public static DateTime ToDateTime(string expireDate)
        {
            DateTime dt = DateTime.Parse(expireDate, new CultureInfo("fa-IR"));
            return dt.ToUniversalTime();
        }
        public static string ClearUrl(string url)
        {
            return url.Replace(" ","-").Replace(".","").Replace("?", "").Replace("!", "").Replace("/", "").Replace("\\", "");
        }
    }
}
