using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dzApi.Core
{
    public static class DateTimeHelper
    {

        /// <summary>
        /// 获取时间范围内的工作日(包含节假日)数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetWeekdayCount(this DateTime startDate, DateTime endDate)
        {
            TimeSpan ts = endDate.Subtract(startDate);//TimeSpan得到时间间隔
            int countday = ts.Days + 1;//获取两个日期间的总天数
            int weekday = 0;//工作日

            //循环用来扣除总天数中的双休日
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = startDate.Date.AddDays(i);
                if (!tempdt.IsWeekend())
                {
                    weekday++;
                }
            }
            return weekday;
        }

        /// <summary>
        /// 获取时间范围内的工作日(包含节假日)
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<DateTime> GetWeekdayList(this DateTime startDate, DateTime endDate)
        {
            TimeSpan ts = endDate.Subtract(startDate);//TimeSpan得到时间间隔
            int countday = ts.Days + 1;//获取两个日期间的总天数
            List<DateTime> weekday = new List<DateTime>();//工作日列表

            //循环用来扣除总天数中的双休日
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = startDate.Date.AddDays(i);
                if (!tempdt.IsWeekend())
                {
                    weekday.Add(tempdt);
                }
            }
            return weekday;
        }

        /// <summary>
        /// 是否为周末
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            if (date.DayOfWeek == System.DayOfWeek.Saturday || date.DayOfWeek == System.DayOfWeek.Sunday)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否为节假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsHoliday(this DateTime date)
        {
            try
            {
                dzApi.Entity.holidaysEntities db = new Entity.holidaysEntities();
                dzApi.Entity.Holiday info = db.Holidays.FirstOrDefault(h => h.IsWork == false && h.DateId.Year == date.Year && h.DateId.Month == date.Month && h.DateId.Day == date.Day);
                if (info != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// 字符串转换成日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string date)
        {
            if (!string.IsNullOrEmpty(date) && date.Length == 8)
            {
                try
                {
                    return new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6)));
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
