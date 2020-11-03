using System;
using System.Collections.Generic;
using System.Text;

namespace RoadFlow.Utility
{
    public static class DateExtensions
    {
        /// <summary>
        /// 得到当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static DateTime MaxValue
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public static DateTime MinValue
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 格式化为长日期格式(yyyy年M月d日)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToLongDate(this DateTime date)
        {
            return date.ToString("yyyy年M月d日");
        }

        /// <summary>
        /// 格式化为长日期格式(yyyy年MM月dd日)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToLongDate1(this DateTime date)
        {
            return date.ToString("yyyy年MM月dd日");
        }

        /// <summary>
        /// 格式化为日期格式(yyyy-MM-dd)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 格式化为日期时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="date"></param>
        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 格式化为日期时间格式(yyyy-MM-dd HH:mm)
        /// </summary>
        /// <param name="date"></param>
        public static string ToShortDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 格式化为日期时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="date"></param>
        public static string ToDateTimeString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateTimeString() : string.Empty;
        }

       

        /// <summary>
        /// 将日期时间转换为INT
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns></returns>
        public static int ToInt(this DateTime dateTime)
        {
            DateTime dt1 = new DateTime(1970, 1, 1, 8, 0, 0);
            DateTime dt2 = Convert.ToDateTime(dateTime);
            return Convert.ToInt32((dt2 - dt1).TotalSeconds);
        }

        /// <summary>
        /// 将INT转换为日期时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int ticks)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            startTime = startTime.AddSeconds(ticks).ToLocalTime();
            return startTime;
        }
    }
}
