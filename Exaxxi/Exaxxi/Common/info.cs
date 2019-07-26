using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Common
{
    public static class info
    {
        public static string keyBF = "superior5";
        public static string RegEx = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
        public static int PAGE_SIZE = 8;
        public static int PAGE_SIZE_AD = 5; //phân trang sử dụng trong admin
        public static double payment_fee = 0.03;
        public static string Day(DateTime time)
        {
            DateTime now = DateTime.Now;
            TimeSpan milisecondsDiff = now.Subtract(time) / (1000 * 60);
            double qd = milisecondsDiff.TotalMilliseconds;
            if (qd < 10)
            {
                return "Just Now";
            }
            else if (qd < 60 && qd > 10)
            {
                return qd.ToString() + " minutes ago";
            }
            else
            {
                var day = time;
                return day.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            }            
        }
    }
}
