using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace TradingAssistant
{
    class Utils
    {
        public static DateTime DateTimeFromString(string sDate)
        {
            if (sDate == null || sDate == string.Empty)
            {
                return new DateTime();
            }

            int pos = -1;
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;

            string[] arrTemp = sDate.Split('/');
            if (arrTemp.Length > 0)
            {
                day = int.Parse(arrTemp[0]);
            }
            if (arrTemp.Length > 1)
            {
                month = int.Parse(arrTemp[1]);
            }
            if (arrTemp.Length > 2)
            {
                pos = arrTemp[2].IndexOf(' ');
                if(pos == -1)
                    pos = arrTemp[2].IndexOf('-');
                if (pos == -1)
                    year = int.Parse(arrTemp[2]);
                else
                {
                    string sTime = arrTemp[2].Substring(pos + 1).Trim();
                    year = int.Parse(arrTemp[2].Substring(0, pos));
                    arrTemp = sTime.Split(':');
                    if (arrTemp.Length > 0)
                        hour = int.Parse(arrTemp[0]);
                    if (arrTemp.Length > 1)
                        minute = int.Parse(arrTemp[1]);
                    if (arrTemp.Length > 2)
                        second = int.Parse(arrTemp[2]);
                }
            }

            return new DateTime(year, month, day, hour, minute, second);
        }

        public static string DateTimeToString(DateTime dt)
        {
            string res = string.Empty;
            if (dt == null)
            {
                return res;
            }
            res = string.Format("{0}/{1}/{2}",dt.Day, dt.Month, dt.Year);
            return res;
        }

        public static void DebugPrint(string msg, [CallerFilePath] string file = null, [CallerLineNumber] int line = 0, [CallerMemberName] string member = null)
        {
            string filename = file.Substring(file.LastIndexOf('\\'));
            Debug.Print(string.Format("{0}[{1}]:{2}:{3}", filename, line, member, msg));
        }
    }
}
