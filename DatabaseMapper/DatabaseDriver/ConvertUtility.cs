using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public static class ConvertUtility
    {
        public static object ConvertToInt(object o, bool doNotAcceptZero=false)
        {
            if (o == null || string.IsNullOrEmpty(o.ToString().Trim()))
                return "null";

            else
            {
                int result = int.Parse(o.ToString().Trim());
                if (doNotAcceptZero && result.ToString() == "0")
                    return "null";

                return result;
            }
        }

        public static object ConvertToString(object o)
        {

            if (o == null)
                return "null";
            else
            {
                string result = o.ToString();
                result = result.Replace("'", "").Replace("\"","");
                result = result.Replace("(بتاجا)", "").Replace("(سلحشوران)", "").Replace("(جوانان)", "").Replace("(نوپديد)", "").Replace("(عمران هادی)", "");
                return "\'" + result + "\'";
            }
        }

        public static object 
            ConvertToGregorianDate(object dateObject)
        {
            try
            {
            if (dateObject == null || string.IsNullOrEmpty(dateObject.ToString()))
                return "null";

            string dateString = dateObject.ToString().Trim();

            if (dateString.Length < 4)
                return "null";


            PersianCalendar pc = new PersianCalendar();
            int year = 0, month = 1, day = 1;

            if (dateString.Length >= 2)
            {
                if (dateString.Length == 8)
                    year = int.Parse(dateString.Substring(0, 4));
                else
                    year = int.Parse("13" + dateString.Substring(0, 2));
            }

            if (dateString.Length >= 4)
            {
                if (dateString.Length == 8)
                    month = int.Parse(dateString.Substring(4, 2));
                else
                    month = int.Parse(dateString.Substring(2, 2));
            }
            if (dateString.Length == 6)
            {
                if (dateString.Length == 8)
                    day = int.Parse(dateString.Substring(6, 2));
                else
                    day = int.Parse(dateString.Substring(4, 2));
            }

            if (month == 12)
            {
                if (day == 31 || day == 30)
                    day = 29;
            }
            else if (month > 6 && month < 12)
            {
                if (day == 31 )
                    day = 30;
            }
                return "\'" + pc.ToDateTime(year, 
                    (month == 0 || month > 12) ? 1 : month,
                    (day == 0 || day > 31) ? 1 : day,
                    0, 0, 0, 0).ToShortDateString() + "\'";
            }
            catch
            {
                return "null";
            }
        }

        public static string Prefix(string data, char prefixData, int desiredLength)
        {
            string result = data;
            int currentLength = data.Length;
            for (int i = desiredLength; i > currentLength; i--)
            {
                result = prefixData.ToString() + result;
            }

            return result;
        }

        public static string ConvertToUnderSupportRelationId(string relation)
        {
            if (relation == "پدر")
                return "1";

            if (relation == "مادر")
                return "2";

            if (relation == "همسر" || relation=="همسردوم")
                return "3";

            if (relation == "برادر")
                return "4";

            if (relation == "خواهر")
                return "5";

            if (relation == "دختر")
                return "6";

            if (relation == "پسر")
                return "7";

            if (relation == "داماد")
                return "8";

            if (relation == "ساير")
                return "9";

            if (relation == "عروس")
                return "10";

            if (relation == "فرزند" || relation == "فرزندچهارم")
                return "11";

            if (relation == "فرزندخوانده")
                return "12";

            if (relation == "فرزندهمسر")
                return "13";

            if (relation == "ناپدري")
                return "14";

            if (relation == "نامادري")
                return "15";

            if (relation == "نوه")
                return "16";

            if (relation == "سرپرست")
                return "17";

            return null;
        }
    }
}
